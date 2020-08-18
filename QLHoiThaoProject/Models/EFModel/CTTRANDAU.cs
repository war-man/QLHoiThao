namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTTRANDAU")]
    public partial class CTTRANDAU
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDDOI { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTD { get; set; }

        [StringLength(255)]
        public string KETQUA { get; set; }

        [StringLength(255)]
        public string TISO { get; set; }

        public int? DIEM { get; set; }

        public virtual DOI DOI { get; set; }

        public virtual TRANDAU TRANDAU { get; set; }
    }
}
