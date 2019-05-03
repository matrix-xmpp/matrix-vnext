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

using System.Threading.Tasks;

namespace Matrix
{
    /// <summary>
    /// Implements exponential backoff strategy for reconnect logic.
    /// </summary>
    public class ExponentialBackoff
    {
        private readonly int startDelay;
        private readonly int maxDelay;
        private int delay;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialBackoff"/> class.
        /// </summary>
        /// <param name="startDelay">delay to start with in seconds</param>
        /// <param name="maxDelay">maximum delay in seconds</param>
        public ExponentialBackoff(int startDelay = 1, int maxDelay = 128)
        {
            this.delay = startDelay;
            this.startDelay = startDelay;
            this.maxDelay = maxDelay;
        }

        public Task Delay()
        {
            // store current delay
            var curDelay = this.delay;

            // increase for next interation
            if (delay < 128)
            {
                this.delay = delay << 1;
            }

            return Task.Delay(curDelay * 1000);
        }
    }
}
