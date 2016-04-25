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

        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(40)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Address { get; set; }


        [Required]
        [StringLength(20)]
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
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
