using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Spells.Storage.Contracts {
    public class SpellModel {

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
        public DifficultyClass Dc { get; set; }
        public MagicSchool School { get; set; }
        public List<Class> Classes { get; set; }
        public List<Subclass> Subclasses { get; set; }
        public string Url { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
