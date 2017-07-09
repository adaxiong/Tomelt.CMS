using System;
using Tomelt.Commands;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.Security;
using Tomelt.Settings;
using Tomelt.Widgets.Models;

namespace Tomelt.Widgets.Commands {
    public class LayerCommands : DefaultTomeltCommandHandler {
        private readonly IContentManager _contentManager;
        private readonly ISiteService _siteService;
        private readonly IMembershipService _membershipService;

        public LayerCommands(IContentManager contentManager, ISiteService siteService, IMembershipService membershipService) {
            _contentManager = contentManager;
            _siteService = siteService;
            _membershipService = membershipService;
        }

        [TomeltSwitch]
        public string LayerRule { get; set; }

        [TomeltSwitch]
        public string Description { get; set; }

        [TomeltSwitch]
        public string Owner { get; set; }

        [CommandName("layer create")]
        [CommandHelp("layer create <name> /LayerRule:<rule> [/Description:<description>] [/Owner:<owner>]\r\n\t" + "Creates a new layer")]
        [TomeltSwitches("LayerRule,Description,Owner")]
        public void Create(string name) {
            Context.Output.WriteLine(T("Creating Layer {0}", name));

            IContent layer = _contentManager.Create<LayerPart>("Layer", t => {
                                                                            t.Name = name; 
                                                                            t.LayerRule = LayerRule;
                                                                            t.Description = Description ?? String.Empty;
                                                                        });

            _contentManager.Publish(layer.ContentItem);
            if (String.IsNullOrEmpty(Owner)) {
                Owner = _siteService.GetSiteSettings().SuperUser;
            }
            var owner = _membershipService.GetUser(Owner);
            layer.As<ICommonPart>().Owner = owner;

            Context.Output.WriteLine(T("Layer created successfully.").Text);
        }
    }
}