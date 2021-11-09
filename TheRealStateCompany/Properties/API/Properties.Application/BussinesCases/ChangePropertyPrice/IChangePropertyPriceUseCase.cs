using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.ChangePropertyPrice
{
    public interface IChangePropertyPriceUseCase
    {
        /// <summary>
        ///     Executes the Use Case
        /// </summary>
        Task Execute(Guid propertyGuid, decimal price, decimal tax);
        /// <summary>
        ///     Sets the Output Port.
        /// </summary>
        /// <param name="outputPort">Output Port</param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
