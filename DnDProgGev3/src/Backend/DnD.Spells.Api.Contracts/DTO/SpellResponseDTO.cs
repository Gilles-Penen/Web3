using DnD.Spells.Api.Contracts.DTO;
using Newtonsoft.Json;

namespace DnDApi.Controllers {
    public class SpellResponseDTO {

        [JsonProperty("id")]
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
        public DifficultyClassResponseDTO Dc { get; set; }
        public MagicSchoolResponseDTO School { get; set; }
        public List<ClassResponseDTO> Classes { get; set; }
        public List<SubclassResponseDTO> Subclasses { get; set; }
        public string Url { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}