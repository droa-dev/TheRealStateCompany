using Properties.Domain;
using System.Collections.Generic;

namespace Properties.Application.BussinesCases.ListProperty
{
    public sealed class ListPropertyPresenter : IOutputPort
    {
        public IList<Property> Properties { get; private set; }
        public bool? IsNotFound { get; private set; }
        public bool? InvalidOutput { get; private set; }
        public void Invalid() => InvalidOutput = true;
        public void NotFound() => IsNotFound = true;
        public void Ok(IList<Property> properties) => Properties = properties;
    }
}
