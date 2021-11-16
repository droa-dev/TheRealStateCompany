using Properties.Domain;

namespace Properties.Application.BussinesCases.CreateOwner
{
    /// <summary>
    ///     Create Owner Presenter.
    /// </summary>
    public sealed class CreateOwnerPresenter : IOutputPort
    {
        public Owner? Owner { get; private set; }
        public bool? InvalidOutput { get; private set; }
        public void Invalid() => InvalidOutput = true;
        public void Ok(Owner owner) => Owner = owner;
    }
}
