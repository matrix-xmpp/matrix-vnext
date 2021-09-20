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
