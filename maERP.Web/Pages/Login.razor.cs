using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace maERP.Web
{
    public class LoginModel : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] AuthenticationStateProvider asp { get; set; } = null;

        #region Properties für Datenbinding
        public string Username { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        #endregion

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            // Reaktion auf diese URL
            if (this.NavigationManager.Uri.ToLower().Contains("/logout"))
            {
                await ((AuthenticationManager)asp).Logout();
            }
        }

        /// <summary>
        /// Reaktion auf Benutzeraktion
        /// </summary>
        protected async Task Login()
        {
            Message = "Logging in...";
            bool ok = await (asp as AuthenticationManager).Login(Username, Password);
            if (ok) this.NavigationManager.NavigateTo("/main");
            else Message = "Login Error!";
        }
    } // end class Login
}