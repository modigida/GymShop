using Blazored.LocalStorage;
using GymShopBlazor.Event;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace GymShopBlazor.AuthService;
public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateNotifier _authenticationStateNotifier;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public AuthStateProvider(ILocalStorageService localStorage, AuthenticationStateNotifier authenticationStateNotifier)
    {
        _localStorage = localStorage;
        _authenticationStateNotifier = authenticationStateNotifier;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenObject = await _localStorage.GetItemAsync<string>("authToken");

        if (!string.IsNullOrEmpty(tokenObject))
        {
            try
            {
                var jsonDoc = JsonDocument.Parse(tokenObject);
                if (jsonDoc.RootElement.TryGetProperty("token", out var tokenProperty))
                {

                    var scopedClaims = ParseClaimsFromJwt(tokenProperty.ToString());
                    var scopedUser = new ClaimsPrincipal(new ClaimsIdentity(scopedClaims, "jwt"));
                    return new AuthenticationState(scopedUser);
                }
            }
            catch (JsonException)
            {
            }

            var claims = ParseClaimsFromJwt(tokenObject);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            return new AuthenticationState(user);
        }

        return new AuthenticationState(_anonymous);
    }


    public async Task NotifyUserAuthentication(string token)
    {
        var claims = ParseClaimsFromJwt(token);
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));

        _authenticationStateNotifier.NotifyStateChanged();
    }
    public bool IsUserAdmin(ClaimsPrincipal user)
    {
        return user.IsInRole("Admin");
    }

    public async Task LogoutUser()
    {
        await _localStorage.RemoveItemAsync("authToken");

        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));

        _authenticationStateNotifier.NotifyStateChanged();
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));

        _authenticationStateNotifier.NotifyStateChanged();
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var jwtHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        try
        {
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims.ToList();

            var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Default"));
            }

            return claims;
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<Claim>();
        }
    }
}
