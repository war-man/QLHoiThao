namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LIENHE")]
    public partial class LIENHE
    {
        [Key]
        public int IDLIENHE { get; set; }

        [StringLength(255)]
        public string DONVI { get; set; }

        [StringLength(255)]
        public string TIEUDE { get; set; }

        public string NOIDUNG { get; set; }
    }
}
