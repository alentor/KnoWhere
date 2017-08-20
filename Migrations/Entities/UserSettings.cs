using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class UserSettings : SettingsBase
    {
        [Key]
        public int Id { get; set; }
    }
}
