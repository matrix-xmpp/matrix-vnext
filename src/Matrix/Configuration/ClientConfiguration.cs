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

namespace Matrix.Configuration
{
    public class ClientConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether automatic reconnect is enabled or not
        /// </summary>
        public bool AutoReconnect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use stream management or not
        /// </summary>
        public bool StreamManagement { get; set; }

        /// <summary>
        /// Enable auto reconnect
        /// </summary>
        /// <returns></returns>
        public ClientConfiguration UseAutoReconnect()
        {
            AutoReconnect = true;
            return this;
        }

        /// <summary>
        /// Enable the usage of stream management
        /// </summary>
        /// <returns></returns>
        public ClientConfiguration UseStreamManagement()
        {
            StreamManagement = true;
            return this;
        }
    }
}
