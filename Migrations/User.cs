using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Password { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public int UserSettingsId { get; set; }

        [ForeignKey("UserSettingsId")]
        public virtual UserSettings Settings { get; set; }


    }
}
