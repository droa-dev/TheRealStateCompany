using Properties.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.ViewModels
{
    /// <summary>
    ///     PropertyImage.
    /// </summary>
    public class PropertyImageModel
    {
        /// <summary>
        ///     PropertyImage constructor.
        /// </summary>
        public PropertyImageModel(PropertyImage propertyImage)
        {
            this.Name = propertyImage.FileName.TextName;
            this.FileByteArray = propertyImage.File.FileBinary;
            this.PropertyId = propertyImage.PropertyGuid.Id;
        }

        /// <summary>
        ///     Gets the Name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     Gets the FileByteArray.
        /// </summary>
        [Required]
        public byte[] FileByteArray { get; set; }

        /// <summary>
        ///     Gets the PropertyId.
        /// </summary>
        [Required]
        public Guid PropertyId { get; set; }
    }
}
