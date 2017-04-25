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

namespace Matrix.Xml.Parser
{
    public class ByteBuffer
    {
        private byte[] buf = new byte[0];

        public void Write(byte[] bytes)
        {
            buf = Combine(buf, bytes);
        }

        /// <summary>
        /// Get the current aggregate contents of the buffer.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBuffer()
        {
            return buf;
        }

        /// <summary>
        /// Removes the given numer of bytes from the beginning of the buffer.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public void RemoveFirst(int offset)
        {
            buf = RemoveFirst(buf, offset);
        }

        /// <summary>
        /// Clears the buffer
        /// </summary>
        public void Clear()
        {
            buf = new byte[0];
        }

        /// <summary>
        /// To show the buffer as a nice string in the debugger
        /// </summary>
        public override string ToString()
        {
            byte[] b = GetBuffer();
            return System.Text.Encoding.UTF8.GetString(b, 0, b.Length);
        }

        /// <summary>
        /// Removes the given number of bytes at the beginning of a byte array.
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public static byte[] RemoveFirst(byte[] buf, int x)
        {
            if (x >= buf.Length)
                return new byte[0];

            byte[] ret = new byte[buf.Length - x];
            Buffer.BlockCopy(buf, x, ret, 0, ret.Length);
            return ret;
        }

        /// <summary>
        /// Combines two byte arrays
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }
}
