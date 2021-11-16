using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class PropertyImage
    {
        public PropertyImage(PropertyImageGuid propertyImageGuid, Name fileName, File file, Enabled enabled, PropertyGuid propertyGuid)
        {
            this.PropertyImageGuid = propertyImageGuid;
            this.FileName = fileName;
            this.File = file;
            this.Enabled = enabled;
            this.PropertyGuid = propertyGuid;
        }
        public PropertyImageGuid PropertyImageGuid { get; }
        //public PropertyImageId PropertyImageId { get; }
        public Name FileName { get; }
        public File File { get; }
        public Enabled Enabled { get; }
        public PropertyGuid PropertyGuid { get; }
        public Property? Property { get; set; }
    }
}
