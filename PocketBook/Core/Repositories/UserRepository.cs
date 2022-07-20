using Microsoft.EntityFrameworkCore;
using PocketBook.Core.IRepositories;
using PocketBook.Data;
using PocketBook.Model;

namespace PocketBook.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {

        }
        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return new List<User>();
            }
        }
        public override async Task<bool> Upsert(User entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingUser == null)
                    return await Add(entity);

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert All method error", typeof(UserRepository));
                return false;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"{Repo} Delete Method Error",typeof(UserRepository));
                return false;
            }
        }
    }
}