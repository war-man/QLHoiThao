namespace QLHoiThaoProject.Models.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KINHPHIHOTRO")]
    public partial class KINHPHIHOTRO
    {
        [Key]
        public int IDKINHPHI { get; set; }

        public int IDHT { get; set; }

        public int IDMTD { get; set; }

        public int GIATRI { get; set; }

        public virtual HOITHAO HOITHAO { get; set; }

        public virtual MONTHIDAU MONTHIDAU { get; set; }
    }
}
