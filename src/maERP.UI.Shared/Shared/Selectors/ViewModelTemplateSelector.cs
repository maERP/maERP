using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace maERP.UI.Shared.Shared.Selectors;

public class ViewModelTemplateSelector : DataTemplateSelector
{
    public DataTemplate? LoginTemplate { get; set; }
    public DataTemplate? RegistrationTemplate { get; set; }
    public DataTemplate? ForgotPasswordTemplate { get; set; }
    public DataTemplate? ResetPasswordTemplate { get; set; }
    public DataTemplate? TenantSetupTemplate { get; set; }

    protected override DataTemplate? SelectTemplateCore(object item, DependencyObject container)
    {
        if (item == null)
            return base.SelectTemplateCore(item, container);

        var typeName = item.GetType().Name;

        return typeName switch
        {
            "LoginViewModel" => LoginTemplate,
            "RegistrationViewModel" => RegistrationTemplate,
            "ForgotPasswordViewModel" => ForgotPasswordTemplate,
            "ResetPasswordViewModel" => ResetPasswordTemplate,
            "TenantSetupViewModel" => TenantSetupTemplate,
            _ => base.SelectTemplateCore(item, container)
        };
    }
}
