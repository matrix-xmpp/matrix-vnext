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
using System.Security.Cryptography;
using System.Text;

namespace Matrix.Crypt
{
    public class Randoms
    {
        public static byte[] GenerateRandom(int lenght)
        {
            var random = new Byte[lenght];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return random;
        }

        public static string GenerateRandomString(int lenght)
        {
            const string content = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var sb = new StringBuilder();

            var rnd = new Random();
            for (int i = 0; i < lenght; i++)
                sb.Append(content[rnd.Next(content.Length)]);

            return sb.ToString();
        }
    }
}
