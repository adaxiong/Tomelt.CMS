using System.Collections.Generic;
using System.Xml.Serialization;
using Tomelt.ContentManagement.Records;
using Tomelt.Data.Conventions;

namespace Tomelt.MediaProcessing.Models {
    public class ImageProfilePartRecord : ContentPartRecord {
        public ImageProfilePartRecord() {
            Filters = new List<FilterRecord>();
            FileNames = new List<FileNameRecord>();
        }

        public virtual string Name { get; set; }

        [CascadeAllDeleteOrphan, Aggregate]
        [XmlArray("FilterRecords")]
        public virtual IList<FilterRecord> Filters { get; set; }

        [CascadeAllDeleteOrphan, Aggregate]
        [XmlArray("FileNameRecords")]
        public virtual IList<FileNameRecord> FileNames { get; set; }
    }
}