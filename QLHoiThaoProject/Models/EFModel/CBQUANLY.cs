namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CBQUANLY")]
    public partial class CBQUANLY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CBQUANLY()
        {
            QUANLYHTs = new HashSet<QUANLYHT>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string TENCB { get; set; }

        [StringLength(255)]
        public string EMAILCB { get; set; }

        [StringLength(255)]
        public string SDTCB { get; set; }

        [StringLength(255)]
        public string USERNAME { get; set; }

        [StringLength(255)]
        public string PASWORD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUANLYHT> QUANLYHTs { get; set; }
    }
}
