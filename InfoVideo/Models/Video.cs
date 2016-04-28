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
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Logo { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [StringLength(20)]
        public string Genre { get; set; }

        public virtual ICollection<Edition> Edition { get; set; } = new HashSet<Edition>();
    }
}
