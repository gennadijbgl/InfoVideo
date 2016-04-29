using System.ComponentModel;

namespace InfoVideo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            History = new HashSet<History>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"(?=^.{6,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$",
                            ErrorMessage = "Пароль не адпавядае правілам")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$", ErrorMessage = "Увядзіце сапраўдую пошту")]

        public string Email { get; set; }

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

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public short? Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History> History { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoles> UserRoles { get; set; }
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
