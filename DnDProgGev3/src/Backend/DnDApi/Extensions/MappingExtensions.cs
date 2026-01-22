using DnD.Spells.Api.Contracts.DTO;
using DnD.Spells.Storage.Contracts;
using DnDApi.Controllers;
using System.Runtime.CompilerServices;

namespace DnDApi.Extensions {
    public static class MappingExtensions {



        public static SpellModel MapToModel(this SpellRequestDTO request) {
            return new SpellModel {
                Index = request.Index,
                Name = request.Name,
                Desc = request.Desc ?? new List<string>(), // Zorg ervoor dat null wordt vervangen door een lege lijst
                HigherLevel = request.HigherLevel ?? new List<string>(),
                Range = request.Range,
                Components = request.Components ?? new List<string>(),
                Material = request.Material,
                Ritual = request.Ritual,
                Duration = request.Duration,
                Concentration = request.Concentration,
                CastingTime = request.CastingTime,
                Level = request.Level,

            };
        }
        
        public static SpellResponseDTO MapToResponse(this SpellModel spell) {
            return new SpellResponseDTO {
                Index = spell.Index,
                Name = spell.Name,
                Desc = spell.Desc,
                HigherLevel = spell.HigherLevel,
                Range = spell.Range,
                Components = spell.Components,
                Material = spell.Material,
                Ritual = spell.Ritual,
                Duration = spell.Duration,
                Concentration = spell.Concentration,
                CastingTime = spell.CastingTime,
                Level = spell.Level,
                Dc = spell.Dc != null
             ? new DifficultyClassResponseDTO {
                 DcType = spell.Dc.DcType != null
                     ? new DcTypeResponseDTO {
                         Index = spell.Dc.DcType.Index,
                         Name = spell.Dc.DcType.Name,
                         Url = spell.Dc.DcType.Url
                     }
                     : null,
                 DcSuccess = spell.Dc.DcSuccess
             }
             : null,
                School = spell.School != null
             ? new MagicSchoolResponseDTO {
                 Index = spell.School.Index,
                 Name = spell.School.Name,
                 Url = spell.School.Url
             }
             : null,
                Classes = spell.Classes?.Select(c => new ClassResponseDTO {
                    Index = c.Index,
                    Name = c.Name,
                    Url = c.Url
                }).ToList(),
                Subclasses = spell.Subclasses?.Select(sc => new SubclassResponseDTO {
                    Index = sc.Index,
                    Name = sc.Name,
                    Url = sc.Url
                }).ToList(),
                Url = spell.Url,
                UpdatedAt = spell.UpdatedAt
            };

        }


    }
}
