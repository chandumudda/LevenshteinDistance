using System;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using LD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Server;

namespace LD.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IOptions<AuthenticationDetails> _authDetails;

        public AuthorizationController(IOptions<AuthenticationDetails> authDetails)
        {
            _authDetails = authDetails;
        }

        [HttpPost("connect/token"), Produces("application/json")]
        public IActionResult Exchange(OpenIdConnectRequest openIdConnectRequestrequest)
        {
            if (openIdConnectRequestrequest.IsPasswordGrantType())
            {
                if (openIdConnectRequestrequest.Username != _authDetails.Value.UserName ||
                    openIdConnectRequestrequest.Password != _authDetails.Value.Password)
                {
                    return Forbid(OpenIddictServerDefaults.AuthenticationScheme);
                }

                var identity = new ClaimsIdentity(
                    OpenIddictServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role);

                identity.AddClaim(OpenIdConnectConstants.Claims.Subject,
                    _authDetails.Value.ClaimSubject,
                    OpenIdConnectConstants.Destinations.AccessToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.Name, _authDetails.Value.ClaimName,
                    OpenIdConnectConstants.Destinations.AccessToken);

                var principal = new ClaimsPrincipal(identity);

                return SignIn(principal, OpenIddictServerDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }
    }
}