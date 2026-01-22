using DnD.Spells.Storage;
using DnD.Spells.Storage.Contracts;

namespace DnDApi.Services {
    public class BlobStorageService {


        private readonly IAlignmentsRepository _alignmentRepository;

        public BlobStorageService(IAlignmentsRepository alignmentRepository) {
            _alignmentRepository = alignmentRepository;
        }

        public async Task CreateAlignmentAsync(Alingments alignment) {
            await _alignmentRepository.UploadAlignmentAsync(alignment);
        }

        public async Task<Alingments> GetAlignmentAsync(string index) {
            return await _alignmentRepository.GetAlignmentAsync(index);
        }
    }
}
