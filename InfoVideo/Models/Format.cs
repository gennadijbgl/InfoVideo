namespace InfoVideo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Format")]
    public partial class Format
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Format()
        {
            Edition = new HashSet<Edition>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Container { get; set; }

        [Required]
        [StringLength(20)]
        public string Languages { get; set; }

        public bool? Support3D { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Edition> Edition { get; set; }
    }
}
