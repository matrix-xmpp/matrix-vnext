using System;
using System.Collections.Generic;

namespace Matrix.Core
{
    public class FullJidComparer : IComparer<Jid>   
    {
        #region IComparer<Jid> Members
        public int Compare(Jid x, Jid y)
        {
            if (x != null && y != null)
            {
                if (x.ToString() == y.ToString())
                    return 0;

                return String.Compare(x.ToString(), y.ToString());
            }

            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return -1;
            }
               
            return 1;
        }
        #endregion
    }
}