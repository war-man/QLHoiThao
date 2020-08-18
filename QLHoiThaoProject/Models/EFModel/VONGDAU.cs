namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VONGDAU")]
    public partial class VONGDAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VONGDAU()
        {
            TRANDAUs = new HashSet<TRANDAU>();
        }

        [Key]
        public int IDVONGDAU { get; set; }

        public int IDMTD { get; set; }

        public int IDHT { get; set; }

        [StringLength(255)]
        public string TENVONGDAU { get; set; }

        public virtual HOITHAO HOITHAO { get; set; }

        public virtual MONTHIDAU MONTHIDAU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANDAU> TRANDAUs { get; set; }
    }
}
