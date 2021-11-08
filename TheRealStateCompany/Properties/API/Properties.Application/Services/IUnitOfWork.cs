using System.Threading.Tasks;

namespace Properties.Application.Services
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Applies all database changes.
        /// </summary>
        /// <returns>Number of affected rows.</returns>
        Task<int> Save();
    }
}
