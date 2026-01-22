

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DnD.Spells.Api.Contracts.DTO {
    public class SpecialAbility {
        public string Name { get; set; }
        public string Desc { get; set; }
        public DC DC { get; set; }
    }
}