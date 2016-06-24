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

        public int? IdUser { get; set; }

        public int? IdEdition { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Edition Edition{ get; set; }

        public virtual Users Users { get; set; }

        public void CalculatePriceBuy()
        {
            if (Edition.Price.HasValue && Users.Discount.HasValue)
            {
                Price = Edition.Price.Value*(1-Users.Discount.Value);
            }
        }
    }
}
