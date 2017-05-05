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

using Matrix.Attributes;

namespace Matrix.Crypt
{
    public enum HashAlgorithms
    {
        [Name("unknown")]
        Unknown,

        [Name("sha-1")]
        Sha1,

        [Name("sha-256")]
        Sha256,

        [Name("sha-384")]
        Sha384,

        [Name("sha-512")]
        Sha512,

        [Name("md5")]
        Md5,
    }
}
