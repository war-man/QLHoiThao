namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANTHIDAU")]
    public partial class SANTHIDAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANTHIDAU()
        {
            TRANDAUs = new HashSet<TRANDAU>();
        }

        [Key]
        public int IDSTD { get; set; }

        [StringLength(255)]
        public string TENSTD { get; set; }

        public string MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANDAU> TRANDAUs { get; set; }
    }
}
