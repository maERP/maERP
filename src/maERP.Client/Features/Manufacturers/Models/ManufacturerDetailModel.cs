using maERP.Client.Features.Manufacturers.Services;
using maERP.Domain.Dtos.Manufacturer;

namespace maERP.Client.Features.Manufacturers.Models;

/// <summary>
/// Model for manufacturer detail page using MVUX pattern.
/// Receives manufacturer ID from navigation data.
/// </summary>
public partial record ManufacturerDetailModel
{
    private readonly IManufacturerService _manufacturerService;
    private readonly INavigator _navigator;
    private readonly Guid _manufacturerId;

    public ManufacturerDetailModel(
        IManufacturerService manufacturerService,
        INavigator navigator,
        ManufacturerDetailData data)
    {
        _manufacturerService = manufacturerService;
        _navigator = navigator;
        _manufacturerId = data.manufacturerId;
    }

    /// <summary>
    /// State for error messages from API operations.
    /// </summary>
    public IState<string> ErrorMessage => State<string>.Value(this, () => string.Empty);

    /// <summary>
    /// Feed that loads the manufacturer details.
    /// </summary>
    public IFeed<ManufacturerDetailDto> Manufacturer => Feed.Async(async ct =>
    {
        var manufacturer = await _manufacturerService.GetManufacturerAsync(_manufacturerId, ct);
        return manufacturer ?? throw new InvalidOperationException($"Manufacturer {_manufacturerId} not found");
    });

    /// <summary>
    /// Navigate to edit manufacturer page.
    /// </summary>
    public async Task EditManufacturer()
    {
        await _navigator.NavigateDataAsync(this, new ManufacturerEditData(_manufacturerId));
    }

    /// <summary>
    /// Navigate back to manufacturer list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
