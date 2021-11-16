using Properties.Application.Services;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer
{
    /// <summary>
    /// </summary>
    public sealed class UnitOfWorkFake : IUnitOfWork
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<int> Save() => await Task.FromResult(0)
            .ConfigureAwait(false);
    }
}
