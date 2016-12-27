using System;
using System.Collections.Generic;

namespace Matrix.Core
{
    /// <summary>
    /// BareJidComparer
    /// </summary>
    public class BareJidComparer : IComparer<Jid>
    {
        public int Compare(Jid x, Jid y)
        {
            if (x != null && y != null)
            {
                if (x.Bare == y.Bare)
                    return 0;

                return String.CompareOrdinal(x.Bare, y.Bare);
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
    }
}