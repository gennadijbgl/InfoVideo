namespace InfoVideo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History")]
    public partial class History
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdEdition { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual Edition Edition { get; set; }

        public virtual User User { get; set; }
    }
}
