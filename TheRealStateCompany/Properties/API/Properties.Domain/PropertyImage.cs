using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class PropertyImage
    {
        public PropertyImageId PropertyImageId { get; }
        public Name FileName { get; }
        public File File { get; }
        public Enabled Enabled { get; }
        public PropertyId PropertyId { get; }
        public Property? Property { get; set; }
    }
}
