using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public bool IstVermittler { get; }
        public bool IsAdmin { get; }
        public bool IsBearbeiter { get; }

        public string Email { get; set; }
        public string KeycloakUserId { get; }
        public int? ApiUserId { get; set; }
        public List<string> Roles { get; set; }
        
        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            KeycloakUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            
            var resourceAccess = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "resource_access")?.Value;
            
            if (resourceAccess != null && resourceAccess.Contains("roles"))
            {
                Roles = JsonConvert.DeserializeObject<Root>(resourceAccess).InsuranceApi.roles;
                
                if (Roles.Contains("bearbeiter"))
                    IsBearbeiter = true;

                if (Roles.Contains("admin"))
                    IsAdmin = true;

                if (Roles.Contains("vertriebler"))
                    IstVermittler = true;
            }
        }

        public class InsuranceApi    {
            public List<string> roles { get; set; } 
        }

        public class Root    {
            [JsonProperty("insurance-api")]
            public InsuranceApi InsuranceApi { get; set; } 
        }
    }
}