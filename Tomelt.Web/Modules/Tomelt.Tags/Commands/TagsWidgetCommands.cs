using System;
using Tomelt.Commands;
using Tomelt.ContentManagement;
using Tomelt.Tags.Models;
using Tomelt.Widgets.Services;

namespace Tomelt.Tags.Commands {
    public class TagWidgetCommands : DefaultTomeltCommandHandler {
        private readonly IWidgetCommandsService _widgetCommandsService;

        public TagWidgetCommands(
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
        public string Slug { get; set; }

        [TomeltSwitch]
        public string Buckets { get; set; }

        [CommandName("tags widget create tagcloud")]
        [CommandHelp("tags widget create tagcloud /Title:<title> /Name:<name> /Zone:<zone> /Position:<position> /Layer:<layer> [/Identity:<identity>] [/RenderTitle:true|false] [/Owner:<owner>] [/Slug:<slug>] [/Buckets:<number>]\r\n\t" + "Creates a new widget")]
        [TomeltSwitches("Title,Name,Zone,Position,Layer,Buckets,Identity,Owner,RenderTitle,Slug")]
        public void CreateTagsCloudWidget() {
            var type = "TagCloud";

            // Check any custom parameters that could cause creating the widget to fail.
            // Nothing to check in this widget, see BlogWidgetCommands.cs for an example.

            // Create the widget using the standard parameters.
            var widget = _widgetCommandsService.CreateBaseWidget(
                Context, type, Title, Name, Zone, Position, Layer, Identity, RenderTitle, Owner, null, false, null);

            if (widget == null) {
                return;
            }

            // Set the custom parameters.
            widget.As<TagCloudPart>().Slug = Slug;

            // It's an optional parameter and defaults to 5.
            if (!string.IsNullOrWhiteSpace(Buckets)) {
                int BucketsAsNumber = 0;
                if (Int32.TryParse(Buckets, out BucketsAsNumber)) {
                    widget.As<TagCloudPart>().Buckets = BucketsAsNumber;
                }
            }

            // Publish the successfully created widget.
            _widgetCommandsService.Publish(widget);
            Context.Output.WriteLine(T("{0} widget created successfully.", type).Text);
        }
    }
}