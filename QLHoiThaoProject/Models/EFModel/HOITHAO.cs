namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOITHAO")]
    public partial class HOITHAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOITHAO()
        {
            COMTDs = new HashSet<COMTD>();
            DOIs = new HashSet<DOI>();
            KINHPHIHOTROes = new HashSet<KINHPHIHOTRO>();
            QUANLYHTs = new HashSet<QUANLYHT>();
            VONGDAUs = new HashSet<VONGDAU>();
        }

        [Key]
        public int IDHT { get; set; }

        [StringLength(255)]
        public string TENHT { get; set; }

        public DateTime? TUNGAY { get; set; }

        public DateTime? DENNGAY { get; set; }

        public DateTime? NGAYBDDK { get; set; }

        public DateTime? NGAYKTDK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMTD> COMTDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOI> DOIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KINHPHIHOTRO> KINHPHIHOTROes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUANLYHT> QUANLYHTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VONGDAU> VONGDAUs { get; set; }
    }
}
