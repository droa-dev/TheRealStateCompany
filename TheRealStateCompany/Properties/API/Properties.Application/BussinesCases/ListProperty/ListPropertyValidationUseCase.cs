using Properties.Application.Services;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.ListProperty
{
    public sealed class ListPropertyValidationUseCase : IListPropertyUseCase
    {
        private readonly IListPropertyUseCase _useCase;
        private readonly Notification _notification;
        private IOutputPort _outputPort;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ListPropertyValidationUseCase" /> class.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="notification"></param>
        public ListPropertyValidationUseCase(IListPropertyUseCase useCase, Notification notification)
        {
            this._useCase = useCase;
            this._notification = notification;
            this._outputPort = new ListPropertyPresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort)
        {
            this._outputPort = outputPort;
            this._useCase.SetOutputPort(outputPort);
        }

        /// <inheritdoc />
        public async Task Execute(
            decimal? ownerIdentification, string countryStateAbb, decimal? initialPrice,
            decimal? maxPrice, string year, string codeInternal)
        {
            if (initialPrice.HasValue && initialPrice.Value < 0)
            {
                this._notification
                    .Add(nameof(initialPrice), "Initial price needs to be a positive number.");
            }

            if (maxPrice.HasValue && maxPrice.Value <= 0)
            {
                this._notification
                    .Add(nameof(initialPrice), "Initial price needs to be greater than zero.");
            }

            if (ownerIdentification.HasValue && ownerIdentification.Value <= 0)
            {
                this._notification
                    .Add(nameof(ownerIdentification), "Owner identification needs to be greater than zero.");
            }

            if (this._notification
                .IsInvalid)
            {
                this._outputPort
                    .Invalid();
                return;
            }

            await this._useCase
                .Execute(ownerIdentification, countryStateAbb, initialPrice, maxPrice, year, codeInternal)
                .ConfigureAwait(false);
        }
    }
}
