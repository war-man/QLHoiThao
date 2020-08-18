namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMTD")]
    public partial class COMTD
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDMTD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDHT { get; set; }

        [StringLength(255)]
        public string GHICHU { get; set; }

        public virtual MONTHIDAU MONTHIDAU { get; set; }

        public virtual HOITHAO HOITHAO { get; set; }
    }
}
