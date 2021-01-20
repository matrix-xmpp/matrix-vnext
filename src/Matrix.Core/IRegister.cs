using Matrix.Xmpp.Register;
using System.Threading.Tasks;

namespace Matrix
{
    public interface IRegister
    {
        /// <summary>
        /// MatriX will try to register an account on the server when set to true.
        /// </summary>
        bool RegisterNewAccount { get; }

        /// <summary>
        /// Gets invoked during the new account registration process. The client must handle the register object by adding username or password to it.
        /// Or fill out the data from when available.
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        Task<Register> RegisterAsync(Register register);
    }
}
