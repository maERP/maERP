#nullable disable

using System.Threading.Tasks;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace maERP.Web
{
    /// <summary>
    /// Authentifizierung zum Debugging
    /// </summary>
    public class AuthenticationManager : AuthenticationStateProvider
    {
        LoginProxy proxy { get; set; }

        public AuthenticationManager(LoginProxy proxy)
        {
            this.proxy = proxy;
        }

        public const string ClientID = "11111111-1111-1111-1111-111111111111";
        public LoginInfo CurrentLoginInfo = null;

        public string Token { get { return CurrentLoginInfo?.Token; } }

        /// <summary>
        /// Login to be called by Razor Component Login.razor
        /// </summary>
        public async Task<bool> Login(string username, string password)
        {
            bool result = false;
            CurrentLoginInfo = null;
            var l = new LoginInfo() { Username = username, Password = password, ClientID = AuthenticationManager.ClientID };
            try
            {
                CurrentLoginInfo = await proxy.LoginAsync(l);
                if (String.IsNullOrEmpty(CurrentLoginInfo.Token))
                {
                    Console.WriteLine("Anmeldung NICHT erfolgreich: " + this.CurrentLoginInfo.Username);
                }
                else
                {
                    result = true;
                    Console.WriteLine("Anmeldung erfolgreich: " + this.CurrentLoginInfo.Username);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Anmeldefehler: " + ex.Message);
            }
            Notify();
            return result;
        }

        /// <summary>
        /// Logout to be called by Razor Component Login.razor
        /// </summary>
        public async Task Logout()
        {
            Console.WriteLine("Logout", this.CurrentLoginInfo);
            if (this.CurrentLoginInfo == null) return;
            var e = await proxy.LogoffAsync(this.CurrentLoginInfo.Token);
            if (e)
            {
                // Remove LoginInfo in RAM for clearing authenticaton state
                CurrentLoginInfo = null;
                Notify();
            }
            else
            {
                Console.WriteLine("Logout Error!");
            }
        }

        /// <summary>
        /// NEU in Teil 3: Notify Blazor infrastructure about new Authentication State
        /// </summary>
        private void Notify()
        {
            Console.WriteLine("Notify: " + CurrentLoginInfo?.Username);
            this.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // NEU in Teil 3
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (this.CurrentLoginInfo != null && !String.IsNullOrEmpty(this.CurrentLoginInfo.Token) && !String.IsNullOrEmpty(proxy.BaseUrl))
            {
                const string authType = "MiracleList WebAPI Authentication";
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim("Backend", proxy.BaseUrl),
                    new Claim(ClaimTypes.Sid, this.CurrentLoginInfo.Token), // use SID claim for token
                    new Claim(ClaimTypes.Name, this.CurrentLoginInfo.Username),
                }, authType);

                var cp = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(cp);
                Console.WriteLine("GetAuthenticationStateAsync: " + this.CurrentLoginInfo.Username);
                return state;
            }
            else
            {
                Console.WriteLine("GetAuthenticationStateAsync: no user");
                var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                return state;
            }
        }

        public class LoginInfo
        {
            public String Username { get; set; }
            public String Password { get; set; }
            public String ClientID { get; set; }
            public String Token { get; set; }
        }

        public class LoginProxy
        {
            public string BaseUrl = "https://erp.martin-andrich.de/";

            public async LoginInfo LoginAsync(LoginInfo li)
            {
                await dummy();
                return li;
            }

            public Task LogoffAsync(string token)
            {
                return true;
            }
        }

        public async void dummy()
        {

        }
    }
}