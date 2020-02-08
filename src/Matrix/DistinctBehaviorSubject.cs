/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

namespace Matrix
{
    public abstract class DistinctBehaviorSubject<T>
    {
        public DistinctBehaviorSubject(T init)
        {
            Subject = new BehaviorSubject<T>(init);
        }

        public T Value
        {
            get { return Subject.Value; }
            set { Subject.OnNext(value); }
        }

        public BehaviorSubject<T> Subject { get; private set; }

        /// <summary>
        /// When the Value changed
        /// </summary>
        public IObservable<T> ValueChanged => Subject.DistinctUntilChanged();
    }
}
