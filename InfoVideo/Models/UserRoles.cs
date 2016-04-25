namespace InfoVideo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRoles
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdRole { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
