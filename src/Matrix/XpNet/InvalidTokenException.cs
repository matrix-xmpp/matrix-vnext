/*
 * Copyright (c) 2003-2017 by Alexander Gnauck, AG-Software
 * All Rights Reserved.
 * Contact information for AG-Software is available at http://www.ag-software.de
 * 
 * xpnet is a deriviative of James Clark's XP parser.
 * See copying.txt for more info.
 */
namespace Matrix.XpNet
{
    /// <summary>
    /// Several kinds of token problems.
    /// </summary>
    internal class InvalidTokenException : TokenException
    {
        private readonly byte _type;

        /// <summary>
        /// An illegal character
        /// </summary>
        public const byte IllegalChar = 0;

        /// <summary>
        /// Doc prefix wasn't XML
        /// </summary>
        public const byte XmlTarget = 1;

        /// <summary>
        /// More than one attribute with the same name on the same element
        /// </summary>
        public const byte DuplicateAttribute = 2;

        /// <summary>
        /// Some other type of bad token detected
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="type"></param>
        public InvalidTokenException(int offset, byte type)
        {
            Offset   = offset;
            _type   = type;
        }

        /// <summary>
        /// Illegal character detected
        /// </summary>
        /// <param name="off"></param>
        public InvalidTokenException(int off)
        {
            Offset  = off;
            _type   = IllegalChar;
        }

        /// <summary>
        /// Offset into the buffer where the problem ocurred.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Type of exception
        /// </summary>
        public int Type => _type;
    }
}
