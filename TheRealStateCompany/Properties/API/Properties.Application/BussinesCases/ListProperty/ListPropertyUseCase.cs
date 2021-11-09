using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.ListProperty
{
    public sealed class ListPropertyUseCase : IListPropertyUseCase
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyFactory _propertyFactory;
        private IOutputPort _outputPort;

        public ListPropertyUseCase(
            IPropertyRepository propertyRepository,
            IPropertyFactory propertyFactory
            )
        {
            _propertyRepository = propertyRepository;
            _propertyFactory = propertyFactory;
            _outputPort = new ListPropertyPresenter();
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        public Task Execute(decimal? ownerIdentification, string countryStateAbb, decimal? initialPrice,
            decimal? maxPrice, string year, string codeInternal) =>
            this.ListProperty(new Identification(ownerIdentification.GetValueOrDefault()), new Abbreviation(countryStateAbb), new Money(initialPrice.GetValueOrDefault()),
                new Money(maxPrice.GetValueOrDefault()), year, codeInternal);

        private async Task ListProperty(
            Identification? ownerIdentification, Abbreviation? countryStateAbb, Money? initialPrice, Money? maxPrice, string year, string codeInternal)
        {
            PropertyFilters propertyFilters = this._propertyFactory
                .ListProperty(ownerIdentification, countryStateAbb, initialPrice, maxPrice, year, codeInternal);

            IList<Property> properties = await this._propertyRepository
                .GetPropertiesFilter(propertyFilters)
                .ConfigureAwait(false);

            if (properties.Any())
            {
                this._outputPort?.Ok(properties);
            }

            this._outputPort?.NotFound();
        }
    }
}
