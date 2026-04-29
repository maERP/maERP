# CLAUDE.md — maERP.Client

Cross-platform Uno Platform app. Single-project, multi-target: `net10.0-android`, `net10.0-browserwasm`, `net10.0-desktop`, `net10.0` (iOS temporarily disabled — see comment in `.csproj`).

Refer to the root `/CLAUDE.md` for cross-cutting rules. This file covers Client-specific conventions.

## Stack

| Concern | Choice |
|---|---|
| UI framework | Uno Platform 6.5.x (WinUI-flavored XAML) |
| Presentation pattern | **MVUX** (Model-View-Update-eXtended) — records with `IFeed`, `IListFeed`, `IState` |
| Markup | XAML |
| Theme | **Material** (via `Uno.Toolkit.UI.Material`) — Theme Service + DSP |
| Renderer | Skia (all platforms) |
| Navigation | `Uno.Extensions.Navigation` (`NavigateViewModelAsync<T>`, RouteMap, ViewMap) |
| HTTP | Kiota + named `HttpClient` "MaErpApi" |
| Auth | Web (Uno authentication) |
| Localization | `IStringLocalizer` with `.resw` resource files |
| Logging | Serilog (`LoggingSerilog` Uno feature) |
| DI / Config | Uno Hosting + Configuration |

The active Uno features are declared in `maERP.Client.csproj` under `<UnoFeatures>` — bumping/changing them is the canonical way to enable new capabilities.

> The Client has **no DB access**. All data goes through the Server's REST API.

## Feature-Based Architecture

Every business capability is a **module** under `Features/`. Module pattern is required for new features — copy an existing one (`Customers/`, `Orders/`).

```
Features/{Feature}/
├── {Feature}Module.cs      # static: RegisterServices / RegisterViews / GetRoutes
├── Models/                 # MVUX records (one per page: List/Detail/Edit)
│   ├── {Feature}ListModel.cs
│   ├── {Feature}DetailModel.cs
│   └── {Feature}EditModel.cs
├── Views/                  # XAML pages + code-behind
│   ├── {Feature}ListPage.xaml(.cs)
│   ├── {Feature}DetailPage.xaml(.cs)
│   └── {Feature}EditPage.xaml(.cs)
└── Services/               # I{Feature}Service + {Feature}Service (HTTP calls)
```

`App.xaml.cs` calls each module's static `RegisterServices(services)`, `RegisterViews(views)`, `GetRoutes(views)` — **don't register views or routes globally**, do it in the module.

### Models (MVUX)

Models are `partial record` types injected with services via primary constructor. They expose state through MVUX feeds:

```csharp
public partial record CustomerListModel(ICustomerService Service)
{
    public IState<string> SearchQuery => State<string>.Value(this, () => string.Empty);
    public IListFeed<CustomerListDto> Customers =>
        ListFeed.Async(async ct => await Service.GetAllAsync(ct));
}
```

- `IFeed<T>` — read-only async value
- `IListFeed<T>` — read-only async list
- `IState<T>` — two-way bindable state (search input, selected item, etc.)

XAML binds with the Uno `Bindings` extension. `ViewMap<{Page}, {Model}>` connects them in the module.

## API Calls & Error Handling

Use the established pipeline — do not add new HTTP plumbing.

**In services** — let exceptions propagate by extending the response check:

```csharp
var response = await _http.GetAsync("api/v1/customers", ct);
await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
return await response.Content.ReadFromJsonAsync<...>(ct);
```

**In models** — catch `ApiException` (in `Core/Exceptions/`) and surface server-side validation:

```csharp
try { /* call service */ }
catch (ApiException ex) { ErrorMessage = ex.CombinedMessage; }
```

`ApiException.CombinedMessage` already aggregates RFC 7807 messages from the Server.

## Localization

- German resources: `Strings/de/Resources.resw`
- English resources: `Strings/en/Resources.resw`
- **Critical**: `ResourceLoader.GetString()` uses **dots** as separators, not slashes:
  - ✅ `resourceLoader.GetString("Page.Section.Key")`
  - ❌ `resourceLoader.GetString("Page/Section/Key")` returns `null`

Inject `IStringLocalizer<T>` into models; for code-behind use a `ResourceLoader` instance.

## Styling

Styles live in `Styles/`:
- `ColorPaletteOverride.xaml` — Material color tokens (paired with `.json` for tooling)
- `InputControls.xaml` — compact input styling
- `TenantSwitcher.xaml` — tenant-selector specific

### Cards (mandatory pattern)

Cards must use `ThemeShadow` + `Translation` for the 3D effect. **Do not use `BorderBrush`/`BorderThickness`** to outline cards.

```xml
<Border Background="{ThemeResource SurfaceBrush}"
        CornerRadius="12"
        Translation="0,0,8">
  <Border.Shadow>
    <ThemeShadow />
  </Border.Shadow>
  <!-- card content -->
</Border>
```

- `CornerRadius="12"` for rounded corners
- `Translation="0,0,8"` for elevation

## Dialogs

`ContentDialog` quirks under Uno + Material:

- A programmatically created `ContentDialog` does **not** receive an implicit style. Set it explicitly:
  `dialog.Style = (Style)Application.Current.Resources["ContentDialogStyle"];`
- `ContentDialogStyle` is the Material key (per `material-controls-styles.md`). `DefaultContentDialogStyle` (Fluent) and `MaterialContentDialogStyle` do **not** work.
- Set `XamlRoot = this.XamlRoot` before `ShowAsync()`.
- Do **not** put a `ContentDialog` into the visual tree — it would auto-show on page load.

## Navigation

- Routes are aggregated from each module's `GetRoutes()` and registered in `App.xaml.cs`.
- Navigate via `_navigator.NavigateViewModelAsync<TModel>()`.
- When adding a new XAML page, also add the matching `ViewMap<TPage, TModel>` in the module — and verify if any `DataTemplate` needs to land in `MainView.axaml`.

## Cross-Platform Considerations

When adding UI, think about all four runtimes (Desktop / WASM / Android / iOS):
- Skia renderer evens out a lot, but platform quirks remain (file pickers, secure storage, deep links).
- Performance matters on WASM — avoid large eager loads; prefer `Feed.Async` + virtualization.
- Test layouts at multiple sizes — phone, tablet, desktop.

## Common Tasks

| Task | Where to look first |
|---|---|
| Add a feature | Copy an existing module (`Features/Customers/`) |
| Add a new endpoint call | Add method to `I{Feature}Service` + implementation; reuse `MaErpApi` `HttpClient` |
| Translate a string | Add the same key to **both** `de` and `en` `Resources.resw` |
| Add a converter | `Core/Converters` (or local to a module) and reference in App.xaml `<Application.Resources>` |
| Page-level error display | Catch `ApiException` in the model, bind `ErrorMessage` to a `TextBlock` |
