using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public abstract class SettingsBase
    {

        [ForeignKey("RadiusId")]
        public virtual Radius RangeNav { get; set; }

        public int RangeId { get; set; }
        
    }
}
