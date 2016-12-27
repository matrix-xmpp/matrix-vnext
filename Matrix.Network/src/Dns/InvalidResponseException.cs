//
// Bdev.Net.Dns by Rob Philpott, Big Developments Ltd. Please send all bugs/enhancements to
// rob@bigdevelopments.co.uk  This file and the code contained within is freeware and may be
// distributed and edited without restriction.
// 

using System;

namespace Matrix.Network.Dns
{
	/// <summary>
	/// Thrown when the server delivers a response we are not expecting to hear
	/// </summary>	
    internal class InvalidResponseException : Exception
	{
		public InvalidResponseException()
		{
		}

		public InvalidResponseException(Exception innerException) :  base(null, innerException) 
		{
		}

		public InvalidResponseException(string message, Exception innerException) : base (message, innerException)
		{
		}
	}
}
