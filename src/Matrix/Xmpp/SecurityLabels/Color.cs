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

namespace Matrix.Xmpp.SecurityLabels
{
    /// <summary>
    /// CSS colors (W3C colors + "Orange")
    /// </summary>
    public enum Color
    {
        UnknownColor = -1,

        [Color(Hex = "#00FFFF")]
        Aqua,
        
        [Color(Hex = "#000000")]
        Black,

        [Color(Hex = "#0000FF")]
        Blue,

        [Color(Hex = "#FF00FF")]
        Fuchsia,

        [Color(Hex = "#808080")]
        Gray,
        
        [Color(Hex = "#008000")]
        Green,

        [Color(Hex = "#00FF00")]
        Lime,

        [Color(Hex = "#800000")]
        Maroon,

        [Color(Hex = "#000080")]
        Navy,

        [Color(Hex = "#808000")]
        Olive,

        [Color(Hex = "#800080")]
        Purple,

        [Color(Hex = "#FF0000")]
        Red,

        [Color(Hex = "#C0C0C0")]
        Silver,

        [Color(Hex = "#008080")]
        Teal,

        [Color(Hex = "#FFFFFF")]
        White,

        [Color(Hex = "#FFFF00")]
        Yellow,

        [Color(Hex = "#FFA500")]
        Orange,
    }
} 
