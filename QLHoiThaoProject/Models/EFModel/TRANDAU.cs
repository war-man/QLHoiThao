namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRANDAU")]
    public partial class TRANDAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRANDAU()
        {
            CTTRANDAUs = new HashSet<CTTRANDAU>();
            HINHANHs = new HashSet<HINHANH>();
        }

        [Key]
        public int IDTD { get; set; }

        public int IDVONGDAU { get; set; }

        public int IDTRONGTAI { get; set; }

        public int IDSTD { get; set; }

        [StringLength(255)]
        public string THOIGIANBD { get; set; }

        [StringLength(255)]
        public string THOIGIANKT { get; set; }

        [StringLength(20)]
        public string NGAYTHIDAU { get; set; }

        public bool? TRANGTHAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTTRANDAU> CTTRANDAUs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANH> HINHANHs { get; set; }

        public virtual SANTHIDAU SANTHIDAU { get; set; }

        public virtual TRONGTAI TRONGTAI { get; set; }

        public virtual VONGDAU VONGDAU { get; set; }
    }
}
