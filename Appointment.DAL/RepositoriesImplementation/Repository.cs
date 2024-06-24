using Appointment.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Appointment.DAL.RepositoriesImplementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _unitOfWork;

        public Repository(ApplicationDbContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate, bool getDefault = true)
        {
            return await (getDefault ? _unitOfWork.Set<T>().FirstOrDefaultAsync(predicate) : _unitOfWork.Set<T>().FirstAsync(predicate));
        }

        public async Task<int> AddAsync(T entity)
        {
            await using var dbContextTransaction = await _unitOfWork.Database.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Set<T>().AddAsync(entity);
                var saveChangesAsync = await _unitOfWork.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                return saveChangesAsync;
            }
            catch
            {
                await dbContextTransaction.RollbackAsync();
                throw;
            }

        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

    }

}
