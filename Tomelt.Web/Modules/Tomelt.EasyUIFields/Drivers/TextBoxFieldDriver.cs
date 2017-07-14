using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.ContentManagement.Handlers;
using Tomelt.EasyUIFields.Fields;
using Tomelt.EasyUIFields.Settings;
using Tomelt.Localization;

namespace Tomelt.EasyUIFields.Drivers
{
    public class TextBoxFieldDriver: ContentFieldDriver<TextBoxField>
    {
        public ITomeltServices Services { get; set; }
        public TextBoxFieldDriver(ITomeltServices services)
        {
            Services = services;
            T = NullLocalizer.Instance;
        }
        public Localizer T { get; set; }

        /// <summary>
        /// 获取前缀
        /// </summary>
        /// <param name="field"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        private static string GetPrefix(ContentField field, ContentPart part)
        {
            return part.PartDefinition.Name + "." + field.Name;
        }
        private static string GetDifferentiator(TextBoxField field, ContentPart part)
        {
            return field.Name;
        }
        //显示
        protected override DriverResult Display(ContentPart part, TextBoxField field, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Fields_TextBox", GetDifferentiator(field, part), () => {
                var settings = field.PartFieldDefinition.Settings.GetModel<TextBoxFieldSettings>();
                return shapeHelper.Fields_Input().Settings(settings);
            });
        }
        protected override DriverResult Editor(ContentPart part, TextBoxField field, dynamic shapeHelper)
        {
            return ContentShape("Fields_TextBox_Edit", GetDifferentiator(field, part),
                () => {
                    if (part.IsNew() && String.IsNullOrEmpty(field.Value))
                    {
                        var settings = field.PartFieldDefinition.Settings.GetModel<TextBoxFieldSettings>();
                        field.Value = settings.DefaultValue;
                    }
                    return shapeHelper.EditorTemplate(TemplateName: "Fields/TextBox.Edit", Model: field, Prefix: GetPrefix(field, part));
                });
        }
        protected override DriverResult Editor(ContentPart part, TextBoxField field, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(field, GetPrefix(field, part), null, null))
            {
                var settings = field.PartFieldDefinition.Settings.GetModel<TextBoxFieldSettings>();

                if (settings.Required && String.IsNullOrWhiteSpace(field.Value))
                {
                    updater.AddModelError(GetPrefix(field, part), T("{0} 此字段必填.", T(field.DisplayName)));
                }
            }

            return Editor(part, field, shapeHelper);
        }

        protected override void Importing(ContentPart part, TextBoxField field, ImportContentContext context)
        {
            context.ImportAttribute(field.FieldDefinition.Name + "." + field.Name, "Value", v => field.Value = v);
        }

        protected override void Exporting(ContentPart part, TextBoxField field, ExportContentContext context)
        {
            context.Element(field.FieldDefinition.Name + "." + field.Name).SetAttributeValue("Value", field.Value);
        }

        protected override void Describe(DescribeMembersContext context)
        {
            context
                .Member(null, typeof(string), T("值"), T("此字段值."))
                .Enumerate<TextBoxField>(() => field => new[] { field.Value });
        }
    }
}