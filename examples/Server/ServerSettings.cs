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

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Server
{
    public static class ServerSettings
    {
        public static bool IsSsl
        {
            get
            {
                string ssl = ExampleHelper.Configuration["ssl"];
                return !string.IsNullOrEmpty(ssl) && bool.Parse(ssl);
            }
        }

        public static List<IConfigurationSection> Hosts => ExampleHelper.Configuration.GetSection("hosts").GetChildren().ToList();

        public static int Port => int.Parse(ExampleHelper.Configuration["port"]);

        public static bool HostExists(string host)
        {
            foreach (var h in Hosts)
            {
                if (h.Key == host)
                    return true;
            }
            return false;
        }

        public static IConfigurationSection Certificate(string host)
        {
            foreach (var h in Hosts)
            {
                if (h.Key == host)
                    return h.GetSection("certificate");
            }
            return null;
        }
    }
}
