using DnD.Spells.Storage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Spells.Storage {
    public interface ISpellRepository {

        Task<SpellModel?> GetSpellAsync(string id);

    }
}
