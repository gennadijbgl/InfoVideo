using System.ComponentModel;

namespace InfoVideo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
       using System.Security.Principal;


    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$",ErrorMessage = "Увядзіце сапраўдую пошту")]
        public string Email { get; set; }

        [Required]
        [StringLength(40)]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$",
                            ErrorMessage = "Пароль не адпавядае правілам")]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z]{1,20}$",
                            ErrorMessage = "Імя не адпавядае правілам")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z]{1,20}$",
                            ErrorMessage = "Імя не адпавядае правілам")]
        public string LastName { get; set; }

        [StringLength(50)]
       
        public string Address { get; set; }


        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$",
                            ErrorMessage = "Імя не адпавядае правілам")]
        public string Login { get; set; }

        public virtual ICollection<History> History { get; set; } = new HashSet<History>();


        public virtual ICollection<UserRoles> UserRoles { get; set; } = new HashSet<UserRoles>();
    }

    public class LoginModel
    {
        [Required]
        [StringLength(20)]
       
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
