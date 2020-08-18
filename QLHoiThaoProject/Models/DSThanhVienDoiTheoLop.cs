using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHoiThaoProject.Models
{
    public class DSThanhVienDoiTheoLop
    {
        //public int IDLOP { get; set; }
        public string TENLOP { get; set; }
        public IEnumerable<QLHoiThaoProject.Models.EFModel.THANHVIENDOI> THANHVIENS { get; set; }
    }
}