using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGemService
    {
        public Task<IList<GetGemDTO>> GetGems();
        public Task<bool> CreateGem(GemDTO gemDTO);
    }
}
