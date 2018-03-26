/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

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
