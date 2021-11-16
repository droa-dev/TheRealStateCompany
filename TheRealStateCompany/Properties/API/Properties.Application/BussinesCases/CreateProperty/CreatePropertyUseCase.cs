using Properties.Application.Services;
using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.CreateProperty
{
    public sealed class CreatePropertyUseCase : ICreatePropertyUseCase
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly IPropertyTraceFactory _propertyTraceFactory;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICountryStatesRepository _countryStatesRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPropertyFactory _propertyFactory;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        public CreatePropertyUseCase(
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
            _outputPort = new CreatePropertyPresenter();
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        public Task Execute(string name, string address, decimal price, decimal tax,
            string codeInternal, string year, decimal ownerIdentification, string countryStateAbb) =>
            this.CreateProperty(
                new Name(name), new Address(address), new Money(price), new Money(tax),
                codeInternal, year, new Identification(ownerIdentification), new Abbreviation(countryStateAbb));

        private async Task CreateProperty(
            Name name, Address address, Money price, Money tax, string codeInternal,
            string year, Identification ownerIdentification, Abbreviation countryStateAbb)
        {
            IOwner owner = await this._ownerRepository
                .GetOwner(ownerIdentification)
                .ConfigureAwait(false); ;

            if (owner is Owner registeredOwner)
            {
                CountryStates countryStates = await this._countryStatesRepository
                .GetCountryState(countryStateAbb);

                if (countryStates is CountryStates registeredState)
                {
                    Property property = this._propertyFactory
                    .NewProperty(name, address, price, codeInternal, year, registeredOwner.OwnerGuid, registeredState.CountryStatesId);

                    PropertyTrace propertyTrace = this._propertyTraceFactory
                        .NewPropertyTrace(name, price, tax, property.PropertyGuid);

                    await this.Create(property, propertyTrace)
                        .ConfigureAwait(false);

                    this._outputPort?.Created(property);
                    return;
                }
            }

            this._outputPort?.NotFound();
        }

        private async Task Create(Property property, PropertyTrace propertyTrace)
        {
            await this._propertyRepository
                .Create(property)
                .ConfigureAwait(false);

            await this._propertyTraceRepository
                .Create(propertyTrace);

            await this._unitOfWork
                .Save()
                .ConfigureAwait(false);
        }
    }
}
