using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;

namespace Matrix.Xmpp.Compression
{
    public abstract class Compression : XmppXElement
    {
        internal Compression(string ns)
            : base(ns, "compression")
        {
        }

        //protected Compression() : this(Namespaces.FeatureCompress)
        //{            
        //}

        /// <summary>
        /// Add a compression method/algorithm
        /// </summary>
        /// <param name="method"></param>
        public void AddMethod(Methods method)
        {
            if (!Supports(method))
            {
                if (GetType() == typeof(Stream.Features.Compression))
                    Add(new Features.Method(method));
                else
                    Add(new Method(method));
            }
        }

        /// <summary>
        /// Is the given compression method/algrithm supported?
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public bool Supports(Methods method)
        {
            return GetMethods().Any(m => m.CompressionMethod == method);
        }

        internal IEnumerable<Method> GetMethods()
        {
            return Elements<Method>();
        }
    }
}
