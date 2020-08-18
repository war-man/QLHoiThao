using QLHoiThaoProject.Models.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHoiThaoProject.Models
{
    public class DSGiaiTheoLop
    {
        //public string TENNGANH { get; set; }
        //public IEnumerable<LOP> LOPS { get; set; }
        //public IEnumerable<DOI> DOIS { get; set; }
        //public int SOGIAI { get; set; }
        public string TENLOP { get; set; }
        public IEnumerable<DOI> DOIS { get; set; }
        public int SOGIAI { get; set; }
    }
}