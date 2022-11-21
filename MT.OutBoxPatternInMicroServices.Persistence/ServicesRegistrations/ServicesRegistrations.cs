using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MT.OutBoxPatternInMicroServices.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Persistence.ServicesRegistrations
{
    public static class ServicesRegistrations
    {
        public static void AddPersistenceServices(this ServiceCollection services)
        {
            services.AddDbContext<OutBoxDbContext>(opt => opt.UseSqlServer("server=.\\MERVESERVER; database=PersonSODb; user id=sa; password=951413Mt.X"));
        }
    }
}
