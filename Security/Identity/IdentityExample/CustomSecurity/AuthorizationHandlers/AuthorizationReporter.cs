using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace IdentityExample.CustomSecurity.AuthorizationHandlers;

public class AuthorizationReporter
{
    private IAuthorizationService _authorizationService;
    private RequestDelegate _next;
    private readonly IAuthorizationPolicyProvider _policyProvider;

    public AuthorizationReporter(RequestDelegate next, IAuthorizationPolicyProvider provider, IAuthorizationService service)
    {
        _next = next;
        _policyProvider = provider;
        _authorizationService = service;
    }

    public async Task Invoke(HttpContext context)
    {
        Endpoint? ep = context.GetEndpoint();
        if (ep != null)
        {
            bool allowAnonymous = ep.Metadata.GetMetadata<IAllowAnonymous>() != null;
            IEnumerable<IAuthorizeData> authorizeData = ep.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? [];
            AuthorizationPolicy? policy = await AuthorizationPolicy.CombineAsync(_policyProvider, authorizeData);


            Dictionary<(string, string), bool> authorizationResults = new Dictionary<(string, string), bool>();
            foreach (ClaimsPrincipal claimsPrincipal in GetUsers())
            {
                string userName = claimsPrincipal.Identity?.Name ?? "(No user)";
                string authenticationType = claimsPrincipal.Identity?.AuthenticationType ?? "Unknown Authentication";

                authorizationResults[(userName, authenticationType)] = allowAnonymous || policy == null || await AuthorizeUser(claimsPrincipal, policy);
            }

            context.Items[Constants.AuthorizationReport] = authorizationResults;
            await ep.RequestDelegate?.Invoke(context)!;

        }
        else
        {
            await _next(context);
        }
    }

    private async Task<bool> AuthorizeUser(ClaimsPrincipal claimsPrincipal, AuthorizationPolicy policy)
    {
        IEnumerable<string?> authenticationSchemes = claimsPrincipal.Identities.Select(x => x.AuthenticationType);
        IEnumerable<string?> policySchemes = policy.AuthenticationSchemes;

        bool anyMatch = authenticationSchemes.Intersect(policySchemes).Any();

        return anyMatch && (await _authorizationService.AuthorizeAsync(claimsPrincipal, policy)).Succeeded;


    }

    private IEnumerable<ClaimsPrincipal> GetUsers() => BuiltInUsersAndClaims.GetUsers().Concat(new[] { new ClaimsPrincipal(new ClaimsIdentity()) });
}