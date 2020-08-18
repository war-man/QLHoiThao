namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THANHVIENDOI")]
    public partial class THANHVIENDOI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THANHVIENDOI()
        {
            THUOCDOIs = new HashSet<THUOCDOI>();
        }

        [Key]
        public int IDVDVDOI { get; set; }

        public int? IDSV { get; set; }

        [StringLength(255)]
        public string TENVDV { get; set; }

        [StringLength(255)]
        public string EMAILVDV { get; set; }

        [StringLength(255)]
        public string SDTVDV { get; set; }

        public bool? BOTHIDAU { get; set; }

        public string GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THUOCDOI> THUOCDOIs { get; set; }
    }
}
