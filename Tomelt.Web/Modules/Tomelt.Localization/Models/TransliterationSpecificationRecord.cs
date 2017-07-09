using Tomelt.Data.Conventions;
using Tomelt.Environment.Extensions;

namespace Tomelt.Localization.Models {
    [TomeltFeature("Tomelt.Localization.Transliteration")]
    public class TransliterationSpecificationRecord {
        public virtual int Id { get; set; }
        public virtual string CultureFrom { get; set; }
        public virtual string CultureTo { get; set; }

        [StringLengthMax]
        public virtual string Rules { get; set; }
    }
}