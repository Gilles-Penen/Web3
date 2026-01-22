using DnDApi.Controllers;

namespace DnDApi.Services {
    public interface ISPellsService {


        
        Task<SpellResponseDTO?> GetSpellAsync(string id);


    }
}
