using Properties.Application.Services;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.ChangePropertyPrice
{
    public sealed class ChangePropertyPriceValidationUseCase : IChangePropertyPriceUseCase
    {
        private readonly IChangePropertyPriceUseCase _useCase;
        private readonly Notification _notification;
        private IOutputPort _outputPort;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangePropertyPriceValidationUseCase" /> class.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="notification"></param>
        public ChangePropertyPriceValidationUseCase(IChangePropertyPriceUseCase useCase, Notification notification)
        {
            this._useCase = useCase;
            this._notification = notification;
            this._outputPort = new ChangePropertyPricePresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort)
        {
            this._outputPort = outputPort;
            this._useCase.SetOutputPort(outputPort);
        }

        /// <inheritdoc />
        public async Task Execute(Guid propertyGuid, decimal price, decimal tax)
        {
            if (price <= 0)
            {
                this._notification
                    .Add(nameof(price), "Price needs to be greater than zero.");
            }

            if (this._notification
                .IsInvalid)
            {
                this._outputPort
                    .Invalid();
                return;
            }

            await this._useCase
                .Execute(propertyGuid, price, tax)
                .ConfigureAwait(false);
        }
    }
}
