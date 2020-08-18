namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MONTHIDAU")]
    public partial class MONTHIDAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONTHIDAU()
        {
            COMTDs = new HashSet<COMTD>();
            DOIs = new HashSet<DOI>();
            GIAITHUONGs = new HashSet<GIAITHUONG>();
            KINHPHIHOTROes = new HashSet<KINHPHIHOTRO>();
            QUYDINHs = new HashSet<QUYDINH>();
            VONGDAUs = new HashSet<VONGDAU>();
        }

        [Key]
        public int IDMTD { get; set; }

        public int IDLOAIMTD { get; set; }

        [StringLength(255)]
        public string TENMTD { get; set; }

        public string MOTA { get; set; }

        public string HINHANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMTD> COMTDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOI> DOIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIAITHUONG> GIAITHUONGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KINHPHIHOTRO> KINHPHIHOTROes { get; set; }

        public virtual LOAIMTD LOAIMTD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUYDINH> QUYDINHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VONGDAU> VONGDAUs { get; set; }
    }
}
