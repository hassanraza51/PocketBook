using PocketBook.Core.IRepositories;

namespace PocketBook.Core.IConfiguration
{
    public interface IUnitofWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}
