namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THANHVIEN")]
    public partial class THANHVIEN
    {
        [Key]
        public int IDTV { get; set; }

        public int IDLOP { get; set; }

        [StringLength(255)]
        public string TENTV { get; set; }

        [StringLength(255)]
        public string EMAILTV { get; set; }

        [StringLength(255)]
        public string SDTTV { get; set; }

        [StringLength(255)]
        public string USERNAME { get; set; }

        [StringLength(255)]
        public string PASSWORD { get; set; }

        public bool? TRANGTHAI { get; set; }

        public virtual LOP LOP { get; set; }
    }
}
