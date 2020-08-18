namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIAITHUONG")]
    public partial class GIAITHUONG
    {
        [Key]
        public int IDGT { get; set; }

        public int IDMTD { get; set; }

        [StringLength(255)]
        public string TENGT { get; set; }

        [StringLength(255)]
        public string GIATRIGT { get; set; }

        public virtual MONTHIDAU MONTHIDAU { get; set; }
    }
}
