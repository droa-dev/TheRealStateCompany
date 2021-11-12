using Properties.Domain;

namespace Properties.Application.BussinesCases.ChangePropertyPrice
{
    /// <summary>
    ///     Change Property Price Presenter.
    /// </summary>
    public sealed class ChangePropertyPricePresenter : IOutputPort
    {
        public Property? Property { get; private set; }
        public bool? IsNotFound { get; private set; }
        public bool? InvalidOutput { get; private set; }
        public void Invalid() => InvalidOutput = true;
        public void NotFound() => IsNotFound = true;
        public void Ok(Property property) => Property = property;
    }
}
