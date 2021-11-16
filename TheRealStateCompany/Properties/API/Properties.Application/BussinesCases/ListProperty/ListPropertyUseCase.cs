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
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICountryStatesRepository _countryStatesRepository;
        private readonly IPropertyFactory _propertyFactory;
        private IOutputPort _outputPort;

        public ListPropertyUseCase(
            IOwnerRepository ownerRepository,
            IPropertyRepository propertyRepository,
            ICountryStatesRepository countryStatesRepository,
            IPropertyFactory propertyFactory
            )
        {
            _ownerRepository = ownerRepository;
            _propertyRepository = propertyRepository;
            _countryStatesRepository = countryStatesRepository;
            _propertyFactory = propertyFactory;
            _outputPort = new ListPropertyPresenter();
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        public Task Execute(decimal? ownerIdentification, string? countryStateAbb, decimal? initialPrice,
            decimal? maxPrice, string? year, string? codeInternal) =>
            this.ListProperty(
                new Identification(ownerIdentification.GetValueOrDefault()), new Abbreviation(countryStateAbb ?? string.Empty), new Money(initialPrice.GetValueOrDefault()),
                new Money(maxPrice.GetValueOrDefault()), year ?? string.Empty, codeInternal ?? string.Empty);

        private async Task ListProperty(
            Identification? ownerIdentification, Abbreviation? countryStateAbb, Money? initialPrice, Money? maxPrice, string year, string codeInternal)
        {
            OwnerGuid ownerGuid = new();
            CountryStatesId countryStateId = new();

            if (ownerIdentification.HasValue && ownerIdentification.Value.IdNumber > 0)
            {
                IOwner owner = await this._ownerRepository
                    .GetOwner(ownerIdentification.Value)
                    .ConfigureAwait(false);

                if (owner != null) ownerGuid = owner.OwnerGuid;
            }

            if (countryStateAbb.HasValue && !string.IsNullOrEmpty(countryStateAbb.Value.TextAbbreviation))
            {
                CountryStates state = await this._countryStatesRepository
                    .GetCountryState(countryStateAbb.Value)
                    .ConfigureAwait(false);

                if (state != null) countryStateId = state.CountryStatesId;
            }

            PropertyFilters propertyFilters = this._propertyFactory
                .ListProperty(ownerGuid, countryStateId, initialPrice, maxPrice, year, codeInternal);

            IList<Property> properties = await this._propertyRepository
                .GetPropertiesFilter(propertyFilters)
                .ConfigureAwait(false);

            if (properties.Any())
            {
                foreach (Property property in properties)
                {
                    CountryStates countryState = await this._countryStatesRepository
                        .GetCountryState(property.CountryStatesId)
                        .ConfigureAwait(false);

                    property.CountryStateAbb = countryState.Abbrev;
                }

                this._outputPort?.Ok(properties);
                return;
            }

            this._outputPort?.NotFound();
        }
    }
}
