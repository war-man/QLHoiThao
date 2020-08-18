using QLHoiThaoProject.Models.EFModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace QLHoiThaoProject.Common
{
    [Serializable]
    public class TVLogin
    {
        [Key]
        public int IDTV { get; set; }

        public int IDLOP { get; set; }

        [StringLength(255)]
        public string TENTV { get; set; }

        [StringLength(255)]
        public string EMAILTV { get; set; }

        [StringLength(255)]
        public string SDTTV { get; set; }

        [Required]
        [StringLength(255)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(255)]
        public string PASSWORD { get; set; }

        public bool? TRANGTHAI { get; set; }

        public virtual LOP LOP { get; set; }
    }
}