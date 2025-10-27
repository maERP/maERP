using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Views;

namespace maERP.Client.Droid;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(
    new[] { Android.Content.Intent.ActionView },
    Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
    DataScheme = "myprotocol")]
public class WebAuthenticationBrokerActivity : Uno.AuthenticationBroker.WebAuthenticationBrokerActivityBase
{
}
