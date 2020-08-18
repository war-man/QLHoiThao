namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HINHANH")]
    public partial class HINHANH
    {
        [Key]
        public int IDHA { get; set; }

        public int IDTD { get; set; }

        [StringLength(255)]
        public string TENHA { get; set; }

        [StringLength(255)]
        public string LINK { get; set; }

        public virtual TRANDAU TRANDAU { get; set; }
    }
}
