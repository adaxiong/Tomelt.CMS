using System;
using System.Web;
using Tomelt.Autoroute.Services;
using Tomelt.Commands;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.ContentPicker.Models;
using Tomelt.Core.Common.Models;
using Tomelt.Core.Navigation.Models;
using Tomelt.Core.Navigation.Services;
using Tomelt.Security;
using Tomelt.Settings;
using Tomelt.Core.Title.Models;
using Tomelt.UI.Navigation;
using Tomelt.Utility;

namespace Tomelt.Pages.Commands {
    public class PageCommands : DefaultTomeltCommandHandler {
        private readonly IContentManager _contentManager;
        private readonly IMembershipService _membershipService;
        private readonly ISiteService _siteService;
        private readonly IMenuService _menuService;
        private readonly INavigationManager _navigationManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHomeAliasService _homeAliasService;

        public PageCommands(
            IContentManager contentManager, 
            IMembershipService membershipService, 
            IAuthenticationService authenticationService,
            ISiteService siteService,
            IMenuService menuService,
            INavigationManager navigationManager, 
            IHomeAliasService homeAliasService) {

            _contentManager = contentManager;
            _membershipService = membershipService;
            _siteService = siteService;
            _menuService = menuService;
            _navigationManager = navigationManager;
            _homeAliasService = homeAliasService;
            _authenticationService = authenticationService;
        }

        [TomeltSwitch]
        public string Slug { get; set; }

        [TomeltSwitch]
        public string Title { get; set; }

        [TomeltSwitch]
        public string Path { get; set; }

        [TomeltSwitch]
        public string Text { get; set; }

        [TomeltSwitch]
        public string Owner { get; set; }

        [TomeltSwitch]
        public bool Homepage { get; set; }

        [TomeltSwitch]
        public bool Publish { get; set; }

        [TomeltSwitch]
        public bool UseWelcomeText { get; set; }

        [TomeltSwitch]
        public string MenuText { get; set; }

        [TomeltSwitch]
        public string MenuName { get; set; }

        [CommandName("page create")]
        [CommandHelp("page create [/Slug:<slug>] /Title:<title> /Path:<path> [/Text:<text>] [/Owner:<username>] [/MenuName:<name>] [/MenuText:<menu text>] [/Homepage:true|false] [/Publish:true|false] [/UseWelcomeText:true|false]\r\n\t" + "Creates a new page")]
        [TomeltSwitches("Slug,Title,Path,Text,Owner,MenuText,Homepage,MenuName,Publish,UseWelcomeText")]
        public void Create() {
            if (String.IsNullOrEmpty(Owner)) {
                Owner = _siteService.GetSiteSettings().SuperUser;
            }

            var owner = _membershipService.GetUser(Owner);
            _authenticationService.SetAuthenticatedUserForRequest(owner);

            var page = _contentManager.Create("Page", VersionOptions.Draft);
            page.As<TitlePart>().Title = Title;
            page.As<ICommonPart>().Owner = owner;

            if (!String.IsNullOrWhiteSpace(MenuText)) {
                var menu = _menuService.GetMenu(MenuName);

                if (menu != null) {
                    var menuItem = _contentManager.Create<ContentMenuItemPart>("ContentMenuItem");
                    menuItem.Content = page;
                    menuItem.As<MenuPart>().MenuPosition = Position.GetNext(_navigationManager.BuildMenu(menu));
                    menuItem.As<MenuPart>().MenuText = MenuText;
                    menuItem.As<MenuPart>().Menu = menu;
                }
            }

            if (Homepage) {
                _homeAliasService.PublishHomeAlias(page);
            }

            var layout = default(string);
            if (UseWelcomeText) {
                var text = T(
                    @"<p>You've successfully setup your Tomelt Site and this is the homepage of your new site.
Here are a few things you can look at to get familiar with the application.
Once you feel confident you don't need this anymore, you can
<a href=""Admin/Contents/Edit/{0}"">remove it by going into editing mode</a>
and replacing it with whatever you want.</p>
<p>First things first - You'll probably want to <a href=""Admin/Settings"">manage your settings</a>
and configure Tomelt to your liking. After that, you can head over to
<a href=""Admin/Themes"">manage themes to change or install new themes</a>
and really make it your own. Once you're happy with a look and feel, it's time for some content.
You can start creating new custom content types or start from the built-in ones by
<a href=""Admin/Contents/Create/Page"">adding a page</a>, or <a href=""Admin/Navigation"">managing your menus.</a></p>
<p>Finally, Tomelt has been designed to be extended. It comes with a few built-in
modules such as pages and blogs or themes. If you're looking to add additional functionality,
you can do so by creating your own module or by installing one that somebody else built.
Modules are created by other users of Tomelt just like you so if you feel up to it,
<a href=""http://www.tomelt.com"">please consider participating</a>.</p>
<p>Thanks for using Tomelt – The Tomelt Team </p>", page.Id).Text;

                var asideFirstText = T(
@"<h2>First Leader Aside</h2>
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a nibh ut tortor dapibus vestibulum.
Aliquam vel sem nibh. Suspendisse vel condimentum tellus.</p>").Text;

                var asideSecondText = T(
@"<h2>Second Leader Aside</h2>
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a nibh ut tortor dapibus vestibulum.
Aliquam vel sem nibh. Suspendisse vel condimentum tellus.</p>").Text;

                var asideThirdText = T(
@"<h2>Third Leader Aside</h2>
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a nibh ut tortor dapibus vestibulum.
Aliquam vel sem nibh. Suspendisse vel condimentum tellus.</p>").Text;

                layout =
                    "{\"elements\": [{" +
                        "\"typeName\": \"Tomelt.Layouts.Elements.Canvas\"," +
                        "\"elements\": [{" +
                            "\"typeName\": \"Tomelt.Layouts.Elements.Grid\"," +
                            "\"elements\": [{" +
                                "\"typeName\": \"Tomelt.Layouts.Elements.Row\"," +
                                "\"elements\": [{" +
                                    "\"typeName\": \"Tomelt.Layouts.Elements.Column\"," +
                                    "\"data\": \"Width=12\"," +
                                    "\"elements\": [{" +
                                        "\"typeName\": \"Tomelt.Layouts.Elements.Html\"," +
                                        "\"data\": \"Content=" + Encode(text) + "\"" +
                                    "}]" +
                                "}]" +
                            "},{" +
                                "\"typeName\": \"Tomelt.Layouts.Elements.Row\"," +
                                "\"elements\": [{" +
                                    "\"typeName\": \"Tomelt.Layouts.Elements.Column\"," +
                                    "\"data\": \"Width=4\"," +
                                    "\"elements\": [{" +
                                        "\"typeName\": \"Tomelt.Layouts.Elements.Html\"," +
                                        "\"data\": \"Content=" + Encode(asideFirstText) + "\"" +
                                    "}]" +
                                "},{" +
                                    "\"typeName\": \"Tomelt.Layouts.Elements.Column\"," +
                                    "\"data\": \"Width=4\"," +
                                    "\"elements\": [{" +
                                        "\"typeName\": \"Tomelt.Layouts.Elements.Html\"," +
                                        "\"data\": \"Content=" + Encode(asideSecondText) + "\"" +
                                    "}]" +
                                "},{" +
                                    "\"typeName\": \"Tomelt.Layouts.Elements.Column\"," +
                                    "\"data\": \"Width=4\"," +
                                    "\"elements\": [{" +
                                        "\"typeName\": \"Tomelt.Layouts.Elements.Html\"," +
                                        "\"data\": \"Content=" + Encode(asideThirdText) + "\"" +
                                    "}]" +
                                "}]" +
                            "}]" +
                        "}]}]}";
            }
            else {
                if (!String.IsNullOrEmpty(Text)) {
                    layout = @"{
    'elements': [{
        'typeName': 'Tomelt.Layouts.Elements.Canvas',
        'elements': [{
            'typeName': 'Tomelt.Layouts.Elements.Html',
            'data': 'Content=" + Encode(Text) + @"'
        }]
    }]
}";
                }
            }

            // (Layout) Hackish way to access the LayoutPart on the page without having to declare a dependency on Tomelt.Layouts.
            var layoutPart = ((dynamic) page).LayoutPart;
            var bodyPart = page.As<BodyPart>();

            if (bodyPart != null)
                bodyPart.Text = Text;

            // Check if we have a LayoutPart attached of type "LayoutPart". If Layouts is disabled, then the type would be "ContentPart".
            // This happens when the user executed the Core recipe, which does not enable the Layouts feature.
            if (layoutPart != null && layoutPart.GetType().Name == "LayoutPart")
                layoutPart.LayoutData = layout;

            if (Publish) {
                _contentManager.Publish(page);
            }

            Context.Output.WriteLine(T("Page created successfully.").Text);
        }

        private static string Encode(string text) {
            return HttpUtility.UrlEncode(text);
        }
    }
}
