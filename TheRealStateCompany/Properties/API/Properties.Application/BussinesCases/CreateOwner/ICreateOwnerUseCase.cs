using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.CreateOwner
{
    public interface ICreateOwnerUseCase
    {
        /// <summary>
        ///     Executes the Use Case
        /// </summary>
        Task Execute(decimal identificationNumber, string name, string address, byte[]? photo, DateTime? birthday);
        /// <summary>
        ///     Sets the Output Port.
        /// </summary>
        /// <param name="outputPort">Output Port</param>
        void SetOutputPort(IOutputPort outputPort);
    }
}
