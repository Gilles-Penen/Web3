

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DnD.Spells.Api.Contracts.DTO {
    public class ActionDetails {
        public string ActionName { get; set; }
        public int Count { get; set; }
        public string Type { get; set; }
    }
}