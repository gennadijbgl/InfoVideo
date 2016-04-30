using System.Drawing;

namespace InfoVideo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Edition")]
    public partial class Edition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Edition()
        {
            History = new HashSet<History>();
        }

        public int Id { get; set; }

        public int? IdVideo { get; set; }

        public int? IdFormat { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString= "{0:C0}")]
        public decimal? Price { get; set; }

        [StringLength(30)]
        public string Box { get; set; }

        public virtual Format Format { get; set; }

        public virtual Video Video { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History> History { get; set; }
    }
}
