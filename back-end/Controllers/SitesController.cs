using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace back_end.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private static readonly GoogleCredential credential = GoogleCredential.FromFile("credentials.json")
            .CreateScoped(new[] { Google.Apis.Webmasters.v3.WebmastersService.Scope.WebmastersReadonly
            });

        [HttpGet]
        public string Get()
        {
            using (var searchConsole = new Google.Apis.Webmasters.v3.WebmastersService(
                new Google.Apis.Services.BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Search Console API test"
                }))
            {
                var sitesList = searchConsole.Sites.List().Execute();
                var sitesResult = new List<string>();
                foreach (var site in sitesList.SiteEntry)
                {
                    if (site.PermissionLevel.Equals("siteOwner") || site.PermissionLevel.Equals("siteFullUser"))
                    {
                        sitesResult.Add(site.SiteUrl);
                    }
                }

                return sitesResult.Count > 0 ? JsonConvert.SerializeObject(sitesResult) : "No sites found";
            }

        }


    }
}