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

        public Task Execute(Guid propertyGuid, string name, string address, decimal price,
            decimal tax, string codeInternal, string year, decimal ownerIdentification, string countryStateAbb) =>
            this.UpdateProperty(
                new PropertyGuid(propertyGuid), new Name(name), new Address(address), new Money(price),
                new Money(tax), codeInternal, year, new Identification(ownerIdentification), new Abbreviation(countryStateAbb));

        private async Task UpdateProperty(
            PropertyGuid propertyGuid, Name name, Address address, Money price, Money tax,
            string codeInternal, string year, Identification ownerIdentification, Abbreviation countryStateAbb)
        {
            Property property = await this._propertyRepository
                .GetProperty(propertyGuid)
                .ConfigureAwait(false);

            if (property is Property registeredProperty)
            {
                Owner owner = await this._ownerRepository
                .GetOwner(ownerIdentification)
                .ConfigureAwait(false); ;

                if (owner is Owner registeredOwner)
                {
                    CountryStates countryStates = await this._countryStatesRepository
                    .GetCountryState(countryStateAbb);

                    if (countryStates is CountryStates registeredState)
                    {
                        Property updatedProperty = this._propertyFactory
                        .UpdateProperty(registeredProperty.PropertyGuid, name, address, price, codeInternal, year, registeredOwner.OwnerId, registeredState.CountryStatesId);

                        if (registeredProperty.Price != price)
                        {
                            PropertyTrace propertyTrace = this._propertyTraceFactory
                            .NewPropertyTrace(name, price, tax, property.PropertyGuid);

                            await this.Update(updatedProperty, propertyTrace)
                            .ConfigureAwait(false);
                        }
                        else
                        {
                            await this.Update(updatedProperty)
                            .ConfigureAwait(false);
                        }

                        this._outputPort?.Ok(property);
                    }
                }
            }

            this._outputPort.NotFound();
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
                .Create(property)
                .ConfigureAwait(false);

            await this._unitOfWork
                .Save()
                .ConfigureAwait(false);
        }
    }
}
