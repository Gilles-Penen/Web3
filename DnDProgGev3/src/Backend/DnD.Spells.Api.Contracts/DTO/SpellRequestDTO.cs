using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Spells.Api.Contracts.DTO {
    public class SpellRequestDTO {
        public string Index { get; set; }
        public string Name { get; set; }
        public List<string> Desc { get; set; }
        public List<string> HigherLevel { get; set; }
        public string Range { get; set; }
        public List<string> Components { get; set; }
        public string Material { get; set; }
        public bool Ritual { get; set; }
        public string Duration { get; set; }
        public bool Concentration { get; set; }
        public string CastingTime { get; set; }
        public int Level { get; set; }
        public DifficultyClassRequestDTO Dc { get; set; }
        public MagicSchoolRequestDTO School { get; set; }
        public List<ClassRequestDTO> Classes { get; set; }
        public List<SubclassRequestDTO> Subclasses { get; set; }
        public string Url { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
