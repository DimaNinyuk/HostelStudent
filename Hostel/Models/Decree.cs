namespace Hostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Decree")]
    public partial class Decree
    {
        public int DecreeId { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DateSigning { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DateArrival { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DateEviction { get; set; }

        public int? StudentsId { get; set; }

        public int? RoomsId { get; set; }

        public int? HousingId { get; set; }

        public virtual Students Students { get; set; }

        public virtual Rooms Rooms { get; set; }

        public virtual Housing Housing { get; set; }
    }
}
