/*
 * Copyright (c) 2003-2017 by Alexander Gnauck, AG-Software
 * All Rights Reserved.
 * Contact information for AG-Software is available at http://www.ag-software.de
 * 
 * xpnet is a deriviative of James Clark's XP parser.
 * See copying.txt for more info.
 */
using System;

namespace XpNet
{
	/// <summary>
	/// Represents information returned by <code>Encoding.tokenizeContent</code>.
	/// @see Encoding#tokenizeContent
	/// </summary>
	public class ContentToken : Token
	{
		private const int INIT_ATT_COUNT = 8;
		private int     _attCount           = 0;
		private int[]   _attNameStart       = new int[INIT_ATT_COUNT];
		private int[]   _attNameEnd         = new int[INIT_ATT_COUNT];
		private int[]   _attValueStart      = new int[INIT_ATT_COUNT];
		private int[]   _attValueEnd        = new int[INIT_ATT_COUNT];
		private bool[]  _attNormalized      = new bool[INIT_ATT_COUNT];       
		
		/// <summary>
		/// Returns the number of attributes specified in the start-tag or empty element tag.
		/// </summary>
		/// <returns></returns>
		public int GetAttributeSpecifiedCount() 
		{
			return _attCount;
		}

		/// <summary>
		/// Returns the index of the first character of the name of the
		/// attribute index <code>i</code>.
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public int GetAttributeNameStart(int i) 
		{
			if (i >= _attCount)
				throw new IndexOutOfRangeException();
			return _attNameStart[i];
        }

		/// <summary>
        /// Returns the index following the last character of the name of the
        /// attribute index<code>i</code>.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetAttributeNameEnd(int i) 
		{
			if (i >= _attCount)
				throw new IndexOutOfRangeException();
			return _attNameEnd[i];
        }
		
		/// <summary>
        /// Returns the index of the character following the opening quote of
        /// attribute index<code> i</code>.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetAttributeValueStart(int i) 
		{
			if (i >= _attCount)
				throw new IndexOutOfRangeException();
			return _attValueStart[i];
		}


        /// <summary>
        /// Returns the index of the closing quote attribute index
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetAttributeValueEnd(int i) 
		{
			if (i >= _attCount)
				throw new IndexOutOfRangeException();
			return _attValueEnd[i];
        }

	
         /// <summary>
         /// Returns true if attribute index<code>i</code> does not need to
         /// be normalized.This is an optimization that allows further processing
         /// of the attribute to be avoided when it is known that normalization
         /// cannot change the value of the attribute.
         /// </summary>
         /// <param name="i"></param>
         /// <returns></returns>
        public bool IsAttributeNormalized(int i) 
		{
			if (i >= _attCount)
				throw new IndexOutOfRangeException();
			return _attNormalized[i];
		}

		/// <summary>
		/// Clear out all of the current attributes
		/// </summary>
		public void ClearAttributes() 
		{
			_attCount = 0;
		}
  
		/// <summary>
		/// Add a new attribute
		/// </summary>
		/// <param name="nameStart"></param>
		/// <param name="nameEnd"></param>
		/// <param name="valueStart"></param>
		/// <param name="valueEnd"></param>
		/// <param name="normalized"></param>
		public void AppendAttribute(int nameStart, int nameEnd,
			int valueStart, int valueEnd,
			bool normalized) 
		{
			if (_attCount == _attNameStart.Length) 
			{
				_attNameStart = Grow(_attNameStart);
				_attNameEnd = Grow(_attNameEnd);
				_attValueStart = Grow(_attValueStart);
				_attValueEnd = Grow(_attValueEnd);
				_attNormalized = Grow(_attNormalized);
			}
			_attNameStart[_attCount] = nameStart;
			_attNameEnd[_attCount] = nameEnd;
			_attValueStart[_attCount] = valueStart;
			_attValueEnd[_attCount] = valueEnd;
			_attNormalized[_attCount] = normalized;
			++_attCount;
		}

		/// <summary>
		/// Is the current attribute unique?
		/// </summary>
		/// <param name="buf"></param>
		public void CheckAttributeUniqueness(byte[] buf)
		{
			for (int i = 1; i < _attCount; i++) 
			{
				int len = _attNameEnd[i] - _attNameStart[i];
				for (int j = 0; j < i; j++) 
				{
					if (_attNameEnd[j] - _attNameStart[j] == len) 
					{
						int n = len;
						int s1 = _attNameStart[i];
						int s2 = _attNameStart[j];
						do 
						{
							if (--n < 0)
								throw new InvalidTokenException(_attNameStart[i],
									InvalidTokenException.DuplicateAttribute);
						} while (buf[s1++] == buf[s2++]);
					}
				}
			}
		}

		private static int[] Grow(int[] v) 
		{
			int[] tem = v;
			v = new int[tem.Length << 1];
            Array.Copy(tem, 0, v, 0, tem.Length);
			return v;
		}

		private static bool[] Grow(bool[] v) 
		{
			bool[] tem = v;
			v = new bool[tem.Length << 1];
            Array.Copy(tem, 0, v, 0, tem.Length);			
			return v;
		}
	}
}