namespace Hostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rooms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rooms()
        {
            Decree = new HashSet<Decree>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomsId { get; set; }

        public int? NumberSeats { get; set; }

        public int? NumberSeatsFree { get; set; }

        public int? ChairNumbers { get; set; }

        public int? TableNambers { get; set; }

        public int? BedNumbers { get; set; }

        public int? CupboardNumbers { get; set; }

        public int? RefrigeratorNumbers { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HousingId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Decree> Decree { get; set; }

        public virtual Housing Housing { get; set; }
    }
}
