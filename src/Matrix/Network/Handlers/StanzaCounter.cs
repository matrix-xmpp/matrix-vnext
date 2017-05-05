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

using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Matrix.Network.Handlers
{   
    public class StanzaCounter
    {
        private readonly BehaviorSubject<int> valueSubject;

        public StanzaCounter()
        {
            valueSubject = new BehaviorSubject<int>(0);
        }

        public int Value
        {
            get { return valueSubject.Value; }
            set { valueSubject.OnNext(value); }
        }

        /// <summary>
        /// When the SessionState changed
        /// </summary>
        public IObservable<int> ValueChanged => valueSubject.DistinctUntilChanged();
    }
}
