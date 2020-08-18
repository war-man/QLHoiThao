namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOI")]
    public partial class DOI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOI()
        {
            CTTRANDAUs = new HashSet<CTTRANDAU>();
            THUOCDOIs = new HashSet<THUOCDOI>();
        }

        [Key]
        public int IDDOI { get; set; }

        public int IDHT { get; set; }

        public int IDMTD { get; set; }

        public int? IDBANG { get; set; }

        public int IDLOP { get; set; }

        public bool BILOAI { get; set; }

        [StringLength(255)]
        public string TENDOI { get; set; }

        public virtual BANGDAU BANGDAU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTTRANDAU> CTTRANDAUs { get; set; }

        public virtual HOITHAO HOITHAO { get; set; }

        public virtual LOP LOP { get; set; }

        public virtual MONTHIDAU MONTHIDAU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THUOCDOI> THUOCDOIs { get; set; }
    }
}
