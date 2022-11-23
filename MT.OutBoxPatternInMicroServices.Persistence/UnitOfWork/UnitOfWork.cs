
using MT.OutBoxPatternInMicroServices.Application.Interfaces.UnitOfWork;
using MT.OutBoxPatternInMicroServices.Persistence.Context;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OutBoxDbContext _outBoxDbContext;
        public UnitOfWork(OutBoxDbContext appDbContext)
        {
            _outBoxDbContext = appDbContext;
        }
        public void Commit()
        {
            _outBoxDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _outBoxDbContext.SaveChangesAsync();
        }
    }
}
