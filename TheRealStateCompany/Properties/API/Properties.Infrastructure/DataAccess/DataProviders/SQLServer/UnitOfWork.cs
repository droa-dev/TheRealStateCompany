using Properties.Application.Services;
using System;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RealStateDbContext _context;
        private bool _disposed;

        public UnitOfWork(RealStateDbContext context) => this._context = context;
        public void Dispose() => this.Dispose(true);

        /// <inheritdoc />
        public async Task<int> Save()
        {
            int affectedRows = await this._context
                .SaveChangesAsync()
                .ConfigureAwait(false);
            return affectedRows;
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
            {
                this._context.Dispose();
            }

            this._disposed = true;
        }
    }
}
