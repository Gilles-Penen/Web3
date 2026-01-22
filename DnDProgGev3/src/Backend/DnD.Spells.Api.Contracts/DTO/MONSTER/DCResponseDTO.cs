
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DnD.Spells.Api.Contracts.DTO {
    public class DC {
        public DCType DcType { get; set; }
        public int DcValue { get; set; }
        public string SuccessType { get; set; }
    }
}
