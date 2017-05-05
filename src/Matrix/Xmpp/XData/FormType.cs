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

namespace Matrix.Xmpp.XData
{
    /// <summary>
	/// Form Types
	/// </summary>
    public enum FormType
	{
		/// <summary>
		/// The forms-processing entity is asking the forms-submitting entity to complete a form.
		/// </summary>
		[Name("form")]
		Form,

		/// <summary>
		/// The forms-submitting entity is submitting data to the forms-processing entity.
		/// </summary>
        [Name("submit")]
        Submit,

		/// <summary>
		/// The forms-submitting entity has cancelled submission of data to the forms-processing entity.
		/// </summary>
        [Name("cancel")]
        Cancel,

		/// <summary>
		/// The forms-processing entity is returning data (e.g., search results) to the forms-submitting entity, or the data is a generic data set.
		/// </summary>
        [Name("result")]
        Result    
	}
}
