using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DnD.Spells.Api.Contracts.DTO {
    public class Action {
        public string Name { get; set; }
        public string Desc { get; set; }
        public ActionDetails[] Actions { get; set; }
        public int AttackBonus { get; set; }
        public DC DC { get; set; }
        public Damage[] Damage { get; set; }
    }
}
