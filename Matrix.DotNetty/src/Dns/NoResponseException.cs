//
// Bdev.Net.Dns by Rob Philpott, Big Developments Ltd. Please send all bugs/enhancements to
// rob@bigdevelopments.co.uk  This file and the code contained within is freeware and may be
// distributed and edited without restriction.
// 

using System;

namespace Matrix.DotNetty.Dns
{
	/// <summary>
	/// Thrown when the server does not respond
	/// </summary>	
    internal class NoResponseException : Exception
	{
		public NoResponseException()
		{
	    }

		public NoResponseException(Exception innerException) :  base(null, innerException) 
		{
		}

		public NoResponseException(string message, Exception innerException) : base (message, innerException)
		{
		}
	}
}