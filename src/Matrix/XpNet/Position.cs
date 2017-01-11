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
    /// Represents a position in an entity.
    /// A position can be modified by <code>Encoding.movePosition</code>.
    /// Creates a position for the start of an entity: the line number is
    /// 1 and the column number is 0.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Returns the line number.
        /// The first line number is 1.
        /// </summary>
        public int LineNumber { get; set; } = 1;

        /// <summary>
        /// Returns the column number.
        /// The first column number is 0.
        /// A tab character is not treated specially.
        /// </summary>
        public int ColumnNumber { get; set; } = 0;
    }
}