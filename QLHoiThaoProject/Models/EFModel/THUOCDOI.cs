namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THUOCDOI")]
    public partial class THUOCDOI
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDDOI { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDVDVDOI { get; set; }

        [StringLength(255)]
        public string GHICHU { get; set; }

        public virtual DOI DOI { get; set; }

        public virtual THANHVIENDOI THANHVIENDOI { get; set; }
    }
}
