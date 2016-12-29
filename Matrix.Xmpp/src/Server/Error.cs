using Matrix.Core.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Server
{
    [XmppTag(Name = "error", Namespace = Namespaces.Server)]
    public class Error : Base.Error
    {
        public Error() : base(Namespaces.Server)
        {
        }

        public Error(ErrorCondition condition)
            : this()
        {
            Condition = condition;
        }

        public Error(ErrorCondition condition, ErrorType type)
            : this(condition)
        {
            Type = type;
        }
    }
}