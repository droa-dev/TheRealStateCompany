using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class PropertyImage
    {
        public PropertyImage(PropertyImageGuid propertyImageGuid, Name fileName, File file, Enabled enabled, PropertyId propertyId) 
        {
            this.PropertyImageGuid = propertyImageGuid;
            this.FileName = fileName;
            this.File = file;
            this.Enabled = enabled;
            this.PropertyId = propertyId;
        }
        public PropertyImageGuid PropertyImageGuid { get; }
        public PropertyImageId PropertyImageId { get; }
        public Name FileName { get; }
        public File File { get; }
        public Enabled Enabled { get; }
        public PropertyId PropertyId { get; }
        public Property? Property { get; set; }
    }
}
