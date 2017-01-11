using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Client
{
    [XmppTag(Name = Tag.Error, Namespace = Namespaces.Client)]
    public class Error : Base.Error
    {
        public Error() : base(Namespaces.Client)
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