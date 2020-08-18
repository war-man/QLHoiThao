namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QUYDINH")]
    public partial class QUYDINH
    {
        [Key]
        public int IDQUYDINH { get; set; }

        public int IDMTD { get; set; }

        public string MOTA { get; set; }

        public virtual MONTHIDAU MONTHIDAU { get; set; }
    }
}
