namespace Hostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [StringLength(50)]
        public string UsersId { get; set; }

        [StringLength(50)]
        public string Possword { get; set; }

        public int? HousingId { get; set; }

        [StringLength(20)]
        public string Login { get; set; }

        public virtual Housing Housing { get; set; }
    }
}
