
using DnD.Spells.Api.Contracts.DTO;

namespace DnDApi.Services {
    public interface IMonsterService {

        Task<MonsterResponseDTO> GetMonsterAsync(string id);
         
    }
}
