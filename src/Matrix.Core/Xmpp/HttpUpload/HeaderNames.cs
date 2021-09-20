using Matrix.Attributes;

namespace Matrix.Xmpp.HttpUpload
{
    public enum HeaderNames
    {
        [Name("Authorization")]
        Authorization,

        [Name("Cookie")]
        Cookie,

        [Name("Expires")]
        Expires
    }
}
