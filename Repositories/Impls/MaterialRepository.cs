using AutoMapper;
using BusinessObjects;
using DataAccessObjects.Impls;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
    }
}
