using Properties.Domain;
using Properties.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories.Fakes
{
    public sealed class PropertyImageRepositoryFake : IPropertyImageRepository
    {
        private readonly RealStateDbContextFake _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public PropertyImageRepositoryFake(RealStateDbContextFake context) => this._context = context;

        public async Task Add(PropertyImage propertyImage)
        {
            this._context
                .PropertyImages
                .Add(propertyImage);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task Update(PropertyImage propertyImage)
        {
            PropertyImage? propertyImageOld = this._context
                .PropertyImages
                .SingleOrDefault(e => e.PropertyImageGuid.Equals(propertyImage.PropertyImageGuid));

            if (propertyImageOld != null)
            {
                this._context.PropertyImages.Remove(propertyImageOld);
            }

            this._context
                .PropertyImages
                .Add(propertyImage);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }
    }
}
