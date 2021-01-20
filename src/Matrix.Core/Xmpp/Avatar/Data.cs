namespace Matrix.Xmpp.Avatar
{
    using Matrix.Attributes;
    using Matrix.Xmpp.Base;

    /// <summary>
    /// Represents the avatar data element
    /// </summary>
    [XmppTag(Name = "data", Namespace = Namespaces.AvatarData)]
    public class Data : XmppXElementWithBased64Value
    {
        public Data()
            : base(Namespaces.AvatarData, "data")
        {
        }
    }
}
