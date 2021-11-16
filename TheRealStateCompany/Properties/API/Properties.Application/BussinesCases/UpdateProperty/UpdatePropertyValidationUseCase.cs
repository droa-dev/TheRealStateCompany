using Properties.Application.Services;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.UpdateProperty
{
    public sealed class UpdatePropertyValidationUseCase : IUpdatePropertyUseCase
    {
        private readonly IUpdatePropertyUseCase _useCase;
        private readonly Notification _notification;
        private IOutputPort _outputPort;


        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdatePropertyValidationUseCase" /> class.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="notification"></param>
        public UpdatePropertyValidationUseCase(IUpdatePropertyUseCase useCase, Notification notification)
        {
            this._useCase = useCase;
            this._notification = notification;
            this._outputPort = new UpdatePropertyPresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort)
        {
            this._outputPort = outputPort;
            this._useCase.SetOutputPort(outputPort);
        }

        /// <inheritdoc />
        public async Task Execute(
            Guid propertyGuid, string? name, string? address, decimal? price, decimal? tax, string? codeInternal,
            string? year, decimal? ownerIdentification, string? countryStateAbb)
        {
            if (propertyGuid == Guid.Empty)
            {
                this._notification
                    .Add(nameof(propertyGuid), "propertyGuid is required.");
            }

            if (this._notification
                .IsInvalid)
            {
                this._outputPort
                    .Invalid();
                return;
            }

            await this._useCase
                .Execute(propertyGuid, name, address, price, tax, codeInternal, year, ownerIdentification, countryStateAbb)
                .ConfigureAwait(false);
        }
    }
}
