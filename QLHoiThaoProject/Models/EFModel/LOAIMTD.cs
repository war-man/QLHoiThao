namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOAIMTD")]
    public partial class LOAIMTD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAIMTD()
        {
            MONTHIDAUs = new HashSet<MONTHIDAU>();
        }

        [Key]
        public int IDLOAIMTD { get; set; }

        [StringLength(255)]
        public string TENLOAIMTD { get; set; }

        public string MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MONTHIDAU> MONTHIDAUs { get; set; }
    }
}
