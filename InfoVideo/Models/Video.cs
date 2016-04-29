namespace InfoVideo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Video")]
    public partial class Video
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Video()
        {
            Edition = new HashSet<Edition>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(2500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Logo { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [StringLength(40)]
        public string Genre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Edition> Edition { get; set; }
    }
}
