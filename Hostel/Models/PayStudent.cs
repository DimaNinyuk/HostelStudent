namespace Hostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PayStudent")]
    public partial class PayStudent
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PayingId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ServicePayment { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DatePayment { get; set; }

       
        public int StudentsId { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Faculty { get; set; }

        public int? HousingId { get; set; }
    }
}
