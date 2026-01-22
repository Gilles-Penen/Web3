using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Spells.Api.Contracts.DTO {
    public class LegendaryAction {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int AttackBonus { get; set; }
        public Damage[] Damage { get; set; }
    }
}