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

using Shouldly;
using Xunit;

namespace Matrix.Tests.Configuration
{
    public class ClientConfigurationTests
    {
        [Fact]
        public void StreamManagementShouldBeDisabledByDefault()
        {
            new Matrix.Configuration.ClientConfiguration().StreamManagement.ShouldBe(false);
        }

        [Fact]
        public void TestEnableStreamManagement()
        {
            new Matrix.Configuration.ClientConfiguration().UseStreamManagement().StreamManagement.ShouldBeTrue();
            var config = new Matrix.Configuration.ClientConfiguration(){ StreamManagement = true }; 
            config.StreamManagement.ShouldBeTrue();
        }

        [Fact]
        public void TestEnableAutoReconnect()
        {
            new Matrix.Configuration.ClientConfiguration().UseAutoReconnect().AutoReconnect.ShouldBeTrue();
            var config = new Matrix.Configuration.ClientConfiguration() { AutoReconnect = true };
            config.AutoReconnect.ShouldBeTrue();
        }

        [Fact]
        public void FluentConfigurationShouldReturnSelf()
        {
            var config = new Matrix.Configuration.ClientConfiguration().UseStreamManagement();
            config.ShouldBeOfType<Matrix.Configuration.ClientConfiguration>();
        }
    }
}
