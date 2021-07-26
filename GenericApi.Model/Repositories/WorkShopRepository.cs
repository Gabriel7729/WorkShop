using GenericApi.Core.BaseModel;
using GenericApi.Model.Contexts;
using GenericApi.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericApi.Model.Repositories
{
    public interface IWorkShopRepository : IBaseRepository<WorkShop> {}
    public class WorkShopRepository : BaseRepository<WorkShop>, IWorkShopRepository
    {
        public WorkShopRepository(WorkShopContext context) : base(context)
        {
        }
    }
}
