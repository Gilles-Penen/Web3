using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Spells.Api.Contracts.DTO {
    public class DifficultyClassRequestDTO {
        public DcTypeRequestDTO DcType { get; set; }
        public string DcSuccess { get; set; }
    }
}
