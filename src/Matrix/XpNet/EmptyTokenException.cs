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
    /// An empty token was detected. This only happens with a buffer of length 0 is passed in
    /// to the parser.
    /// </summary>
    internal class EmptyTokenException : TokenException
    {
    }
}
