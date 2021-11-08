using Properties.Application.Services;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.CreateProperty
{
    public sealed class CreatePropertyValidationUseCase : ICreatePropertyUseCase
    {
        private readonly ICreatePropertyUseCase _useCase;
        private readonly Notification _notification;
        private IOutputPort _outputPort;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreatePropertyValidationUseCase" /> class.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="notification"></param>
        public CreatePropertyValidationUseCase(ICreatePropertyUseCase useCase, Notification notification)
        {
            this._useCase = useCase;
            this._notification = notification;
            this._outputPort = new CreatePropertyPresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort)
        {
            this._outputPort = outputPort;
            this._useCase.SetOutputPort(outputPort);
        }

        /// <inheritdoc />
        public async Task Execute(string name, string address, decimal price, string codeInternal, string year, decimal ownerIdentification, string countryStateAbb)
        {
            if (string.IsNullOrEmpty(name))
            {
                this._notification
                    .Add(nameof(name), "Name is required.");
            }

            if (string.IsNullOrEmpty(address))
            {
                this._notification
                    .Add(nameof(address), "Address is required.");
            }

            if (price <= 0)
            {
                this._notification
                    .Add(nameof(price), "Price needs to be greater than zero.");
            }

            if (string.IsNullOrEmpty(codeInternal))
            {
                this._notification
                    .Add(nameof(codeInternal), "Internal Code is required.");
            }

            if (string.IsNullOrEmpty(year))
            {
                this._notification
                    .Add(nameof(year), "Year is required.");
            }

            if (ownerIdentification <= 0)
            {
                this._notification
                    .Add(nameof(ownerIdentification), "Owner identification needs to be greater than zero.");
            }

            if (string.IsNullOrEmpty(countryStateAbb))
            {
                this._notification
                    .Add(nameof(countryStateAbb), "State Abbreviation is required.");
            }

            if (this._notification
                .IsInvalid)
            {
                this._outputPort
                    .Invalid();
                return;
            }

            await this._useCase
                .Execute(name, address, price, codeInternal, year, ownerIdentification, countryStateAbb)
                .ConfigureAwait(false);
        }
    }
}
