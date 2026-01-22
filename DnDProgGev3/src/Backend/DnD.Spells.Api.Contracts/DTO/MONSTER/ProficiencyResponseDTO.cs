using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DnD.Spells.Api.Contracts.DTO {
    public class Proficiency {
        public int Value { get; set; }
        public ProficiencyDetail Proficiencydetail { get; set; }
    }
}