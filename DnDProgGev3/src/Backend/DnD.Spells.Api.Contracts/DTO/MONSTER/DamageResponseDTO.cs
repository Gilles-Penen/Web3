

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DnD.Spells.Api.Contracts.DTO {
    public class Damage {
        public DamageType DamageType { get; set; }
        public string DamageDice { get; set; }
    }
}
