using DnD.Spells.Storage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Spells.Storage {
    public interface IAlignmentsRepository {

        Task UploadAlignmentAsync(Alingments alignment);
        Task<Alingments> GetAlignmentAsync(string index);

    }
}
