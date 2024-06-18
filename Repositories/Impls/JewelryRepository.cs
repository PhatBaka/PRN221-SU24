using BusinessObjects;
using DataAccessObjects.Impls;
using DataAccessObjects.Interfaces;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class JewelryRepository : BaseRepository<Jewelry>, IJewelryRepository
    {
    }
}
