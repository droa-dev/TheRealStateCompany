using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.AddPropertyImage
{
    /// <summary>
    ///     Add Property Image
    /// </summary>
    public interface IAddPropertyImageUseCase
    {
        /// <summary>
        ///     Executes the Use Case
        /// </summary>
        Task Execute(string fileName, byte[] file, Guid propertyGuid);
        /// <summary>
        ///     Sets the Output Port.
        /// </summary>
        /// <param name="outputPort">Output Port</param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
