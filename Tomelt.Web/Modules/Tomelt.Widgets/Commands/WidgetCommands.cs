using Tomelt.Commands;
using Tomelt.Widgets.Services;

namespace Tomelt.Widgets.Commands {
    public class WidgetCommands : DefaultTomeltCommandHandler {
        private readonly IWidgetCommandsService _widgetCommandsService;

        public WidgetCommands(
            IWidgetCommandsService widgetCommandsService) {
            _widgetCommandsService = widgetCommandsService;            

            RenderTitle = true;
        }

        [TomeltSwitch]
        public string Title { get; set; }

        [TomeltSwitch]
        public string Name { get; set; }

        [TomeltSwitch]
        public bool RenderTitle { get; set; }

        [TomeltSwitch]
        public string Zone { get; set; }

        [TomeltSwitch]
        public string Position { get; set; }

        [TomeltSwitch]
        public string Layer { get; set; }

        [TomeltSwitch]
        public string Identity { get; set; }

        [TomeltSwitch]
        public string Owner { get; set; }

        [TomeltSwitch]
        public string Text { get; set; }

        [TomeltSwitch]
        public bool UseLoremIpsumText { get; set; }

        [TomeltSwitch]
        public bool Publish { get; set; }

        [TomeltSwitch]
        public string MenuName { get; set; }

        [CommandName("widget create")]
        [CommandHelp("widget create <type> /Title:<title> /Name:<name> /Zone:<zone> /Position:<position> /Layer:<layer> [/Identity:<identity>] [/RenderTitle:true|false] [/Owner:<owner>] [/Text:<text>] [/UseLoremIpsumText:true|false] [/MenuName:<name>]\r\n\t" + "Creates a new widget")]
        [TomeltSwitches("Title,Name,Zone,Position,Layer,Identity,Owner,Text,UseLoremIpsumText,MenuName,RenderTitle")]
        public void Create(string type) {
            var widget = _widgetCommandsService.CreateBaseWidget(
                Context, type, Title, Name, Zone, Position, Layer, Identity, RenderTitle, Owner, Text, UseLoremIpsumText, MenuName);

            if (widget == null) {
                return;
            }

            _widgetCommandsService.Publish(widget);
            Context.Output.WriteLine(T("Widget created successfully.").Text);
        }
    }
}