﻿using System.Collections.Generic;
using System.Linq;
using Tomelt.Commands;
using Tomelt.Localization.Services;

namespace Tomelt.Localization.Commands {
    public class CultureCommands : DefaultTomeltCommandHandler {
        private readonly ICultureManager _cultureManager;
        private readonly ITomeltServices _tomeltServices;

        public CultureCommands(ICultureManager cultureManager, ITomeltServices tomeltServices) {
            _cultureManager = cultureManager;
            _tomeltServices = tomeltServices;
        }

        [CommandHelp("cultures list \r\n\t" + "List site cultures")]
        [CommandName("cultures list")]
        public void ListCultures() {
            Context.Output.WriteLine(T("Listing Cultures:"));

            var cultures = string.Join(" ", _cultureManager.ListCultures());

            Context.Output.WriteLine(cultures);
        }


        [CommandHelp("cultures get site culture \r\n\t" + "Get culture for the site")]
        [CommandName("cultures get site culture")]
        public void GetSiteCulture() {
            Context.Output.WriteLine(T("Site Culture is {0}", _tomeltServices.WorkContext.CurrentSite.SiteCulture));
        }

        [CommandHelp("cultures set site culture <culture-name> \r\n\t" + "Set culture for the site")]
        [CommandName("cultures set site culture")]
        public void SetSiteCulture(string cultureName) {
            Context.Output.WriteLine(T("Setting site culture to {0}", cultureName));

            if (!_cultureManager.IsValidCulture(cultureName)) {
                Context.Output.WriteLine(T("Supplied culture name {0} is not valid.", cultureName));
                return;
            }

            _cultureManager.AddCulture(cultureName); 
            _tomeltServices.WorkContext.CurrentSite.SiteCulture = cultureName;

            Context.Output.WriteLine(T("Site culture set to {0} successfully", cultureName));
        }

        [CommandHelp("cultures add <culture-name-1> ... <culture-name-n>\r\n\t" + "Add one or more cultures to the site")]
        [CommandName("cultures add")]
        public void AddCultures(params string[] cultureNames) {
            IEnumerable<string> siteCultures = _cultureManager.ListCultures();

            Context.Output.WriteLine(T("Adding site cultures {0}", string.Join(",", cultureNames)));

            foreach (var cultureName in cultureNames.Distinct().Where(s => !siteCultures.Contains(s))) {
                if (_cultureManager.IsValidCulture(cultureName)) {
                    _cultureManager.AddCulture(cultureName);
                }
                else {
                    Context.Output.WriteLine(T("Supplied culture name {0} is not valid.", cultureName));
                }
            }
        }
    }
}

