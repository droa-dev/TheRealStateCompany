using Properties.Application.Services;
using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.UpdateProperty
{
    public sealed class UpdatePropertyUseCase : IUpdatePropertyUseCase
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly IPropertyTraceFactory _propertyTraceFactory;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICountryStatesRepository _countryStatesRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPropertyFactory _propertyFactory;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        public UpdatePropertyUseCase(
            IPropertyTraceRepository propertyTraceRepository,
            IPropertyTraceFactory propertyTraceFactory,
            IPropertyRepository propertyRepository,
            ICountryStatesRepository countryStatesRepository,
            IOwnerRepository ownerRepository,
            IPropertyFactory propertyFactory,
            IUnitOfWork unitOfWork)
        {
            _propertyTraceRepository = propertyTraceRepository;
            _propertyTraceFactory = propertyTraceFactory;
            _propertyRepository = propertyRepository;
            _countryStatesRepository = countryStatesRepository;
            _ownerRepository = ownerRepository;
            _propertyFactory = propertyFactory;
            _unitOfWork = unitOfWork;
            _outputPort = new UpdatePropertyPresenter();
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        public Task Execute(Guid propertyGuid, string? name, string? address, decimal? price,
            decimal? tax, string? codeInternal, string? year, decimal? ownerIdentification, string? countryStateAbb) =>
            this.UpdateProperty(
                new PropertyGuid(propertyGuid), new Name(name ?? string.Empty), new Address(address ?? string.Empty), new Money(price.GetValueOrDefault()),
                new Money(tax.GetValueOrDefault()), codeInternal, year, new Identification(ownerIdentification.GetValueOrDefault()),
                new Abbreviation(countryStateAbb ?? string.Empty));

        private async Task UpdateProperty(
            PropertyGuid propertyGuid, Name? name, Address? address, Money? price, Money? tax,
            string? codeInternal, string? year, Identification? ownerIdentification, Abbreviation? countryStateAbb)
        {
            OwnerGuid ownerGuid = new();
            CountryStatesId countryStatesId = new();

            IProperty property = await this._propertyRepository
                .GetPropertyForUpdate(propertyGuid)
                .ConfigureAwait(false);

            if (property is Property registeredProperty)
            {
                if (ownerIdentification.HasValue && !ownerIdentification.Value.IsZero())
                {
                    IOwner owner = await this._ownerRepository
                        .GetOwner(ownerIdentification.Value)
                        .ConfigureAwait(false); ;

                    if (owner != null) ownerGuid = owner.OwnerGuid;
                }

                if (countryStateAbb.HasValue && !string.IsNullOrEmpty(countryStateAbb.Value.TextAbbreviation))
                {
                    CountryStates state = await this._countryStatesRepository
                   .GetCountryState(countryStateAbb.Value)
                   .ConfigureAwait(false);

                    if (state != null) countryStatesId = state.CountryStatesId;
                }

                Property updatedProperty = this._propertyFactory
                        .UpdateProperty(
                            registeredProperty.PropertyGuid,
                            (name.HasValue && !string.IsNullOrEmpty(name.Value.TextName)) ? name : registeredProperty.Name,
                            (address.HasValue && !string.IsNullOrEmpty(address.Value.TextAddress)) ? address : registeredProperty.Address,
                            price ?? registeredProperty.Price, codeInternal ?? registeredProperty.CodeInternal,
                            year ?? registeredProperty.Year, ownerGuid.Id == Guid.Empty ? registeredProperty.OwnerGuid : ownerGuid,
                            countryStatesId.IsZero() ? registeredProperty.CountryStatesId : countryStatesId);

                if (price.HasValue && !price.Value.IsZero() && registeredProperty.Price != price)
                {
                    PropertyTrace propertyTrace = this._propertyTraceFactory
                    .NewPropertyTrace(updatedProperty.Name, updatedProperty.Price, tax!.Value, registeredProperty.PropertyGuid);

                    await this.Update(updatedProperty, propertyTrace)
                    .ConfigureAwait(false);
                }
                else
                {
                    await this.Update(updatedProperty)
                    .ConfigureAwait(false);
                }

                this._outputPort?.Ok(updatedProperty);
                return;
            }

            this._outputPort?.NotFound();
        }

        private async Task Update(Property property, PropertyTrace propertyTrace)
        {
            await this._propertyRepository
                .Update(property)
                .ConfigureAwait(false);

            await this._propertyTraceRepository
                .Create(propertyTrace);

            await this._unitOfWork
                .Save()
                .ConfigureAwait(false);
        }

        private async Task Update(Property property)
        {
            await this._propertyRepository
                .Update(property)
                .ConfigureAwait(false);

            await this._unitOfWork
                .Save()
                .ConfigureAwait(false);
        }
    }
}
