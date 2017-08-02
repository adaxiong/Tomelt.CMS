using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.ContentManagement.Handlers;

using Tomelt.Localization;
using UEditor.Fields;
using UEditor.Settings;

namespace UEditor.Drivers
{
    public class FilePickFieldDriver : ContentFieldDriver<FilePickField>
    {
        public ITomeltServices Services { get; set; }
        public FilePickFieldDriver(ITomeltServices services)
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
        private static string GetDifferentiator(FilePickField field, ContentPart part)
        {
            return field.Name;
        }
        //显示
        protected override DriverResult Display(ContentPart part, FilePickField field, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Fields_FilePick", GetDifferentiator(field, part), () => {
                var settings = field.PartFieldDefinition.Settings.GetModel<FilePickSettings>();
                return shapeHelper.Fields_FilePick().Settings(settings);
            });
        }
        protected override DriverResult Editor(ContentPart part, FilePickField field, dynamic shapeHelper)
        {
            return ContentShape("Fields_FilePick_Edit", GetDifferentiator(field, part),
                () => shapeHelper.EditorTemplate(TemplateName: "Fields/FilePick.Edit", Model: field, Prefix: GetPrefix(field, part)));
        }
        protected override DriverResult Editor(ContentPart part, FilePickField field, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(field, GetPrefix(field, part), null, null))
            {
                var settings = field.PartFieldDefinition.Settings.GetModel<FilePickSettings>();

                if (settings.Required && String.IsNullOrWhiteSpace(field.Path))
                {
                    updater.AddModelError(GetPrefix(field, part), T("{0} 此字段必填.", T(field.DisplayName)));
                }
            }

            return Editor(part, field, shapeHelper);
        }

        protected override void Importing(ContentPart part, FilePickField field, ImportContentContext context)
        {
            context.ImportAttribute(field.FieldDefinition.Name + "." + field.Name, "Path", v => field.Path = v);
        }

        protected override void Exporting(ContentPart part, FilePickField field, ExportContentContext context)
        {
            context.Element(field.FieldDefinition.Name + "." + field.Name).SetAttributeValue("Path", field.Path);
        }

        protected override void Describe(DescribeMembersContext context)
        {
            context
                .Member(null, typeof(string), T("文件路径"), T("文件虚拟路径."))
                .Enumerate<FilePickField>(() => field => new[] { field.Path });
        }
    }
}