using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using UEditor.Models;

namespace UEditor.Drivers
{
    public class AlbumDrivers: ContentPartDriver<AlbumPart>
    {
        //protected override DriverResult Display(
        //    AlbumPart part, string displayType, dynamic shapeHelper)
        //{

        //    return ContentShape("Parts_Album", () => shapeHelper.Parts_Album(
        //        OriginalPath: part.OriginalPath,
        //        ThumbPath: part.ThumbPath,
        //        Describe: part.Describe
        //        ));
        //}

        //GET
        protected override DriverResult Editor(
            AlbumPart part, dynamic shapeHelper)
        {




            return ContentShape("Parts_Album_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/Album",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(
            AlbumPart part, IUpdateModel updater, dynamic shapeHelper)
        {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}