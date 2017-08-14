using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public abstract class SettingsBase
    {

        public virtual Range RangeNav { get; set; }

        [ForeignKey("RangeNav")]
        public int RangeId { get; set; }


    }
}
