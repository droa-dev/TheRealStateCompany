using Properties.Application.Services;
using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.ChangePropertyPrice
{
    public sealed class ChangePropertyPriceUseCase : IChangePropertyPriceUseCase
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly IPropertyTraceFactory _propertyTraceFactory;
        private readonly IPropertyRepository _propertyRepository;        
        private readonly IPropertyFactory _propertyFactory;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        public ChangePropertyPriceUseCase(
            IPropertyTraceRepository propertyTraceRepository,
            IPropertyTraceFactory propertyTraceFactory,
            IPropertyRepository propertyRepository,            
            IPropertyFactory propertyFactory,
            IUnitOfWork unitOfWork)
        {
            _propertyTraceRepository = propertyTraceRepository;
            _propertyTraceFactory = propertyTraceFactory;
            _propertyRepository = propertyRepository;            
            _propertyFactory = propertyFactory;
            _unitOfWork = unitOfWork;
            _outputPort = new ChangePropertyPricePresenter();
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        public Task Execute(
            Guid propertyGuid, decimal price, decimal tax)
            => this.ChangePropertyPrice(new PropertyGuid(propertyGuid), new Money(price), new Money(tax));

        private async Task ChangePropertyPrice(PropertyGuid propertyGuid, Money price, Money tax)
        {
            IProperty property = await this._propertyRepository
                .GetProperty(propertyGuid)
                .ConfigureAwait(false);

            if (property is Property registeredProperty)
            {
                Property propertyUpdated = this._propertyFactory
                    .NewProperty(
                    registeredProperty.Name, registeredProperty.Address, price, registeredProperty.CodeInternal, 
                    registeredProperty.Year, registeredProperty.OwnerGuid, registeredProperty.CountryStatesId);

                PropertyTrace propertyTrace = this._propertyTraceFactory
                        .NewPropertyTrace(registeredProperty.Name, price, tax, registeredProperty.PropertyGuid);

                await this.ChangePrice(propertyUpdated, propertyTrace)
                    .ConfigureAwait(false);

                this._outputPort?.Ok(propertyUpdated);
            }

            this._outputPort.NotFound();
        }

        private async Task ChangePrice(Property property, PropertyTrace propertyTrace)
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
    }
}
