using Properties.Domain;

namespace Properties.Application.BussinesCases.AddPropertyImage
{
    /// <summary>
    ///     Add Property Image Presenter.
    /// </summary>
    public class AddPropertyImagePresenter : IOutputPort
    {
        public PropertyImage? PropertyImage { get; private set; }
        public bool? IsNotFound { get; private set; }
        public bool? InvalidOutput { get; private set; }
        public void Invalid() => InvalidOutput = true;
        public void NotFound() => IsNotFound = true;
        public void Ok(PropertyImage propertyImage) => PropertyImage = propertyImage;
    }
}
