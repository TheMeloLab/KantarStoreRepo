using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Infrastructure.Seeders
{
    public interface IUserSeeder
    {
        Task Seed();
    }
}
