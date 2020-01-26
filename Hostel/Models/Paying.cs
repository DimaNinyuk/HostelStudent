namespace Hostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Paying")]
    public partial class Paying
    {
        public int PayingId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ServicePayment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DatePayment { get; set; }

        public int? StudentsId { get; set; }

        public virtual Students Students { get; set; }
    }
}
