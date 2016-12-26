using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Sasl
{
    public enum SaslMechanism
    {
        [Name(null)]
        None,
#if WIN
        [Name("GSSAPI")]
        Gssapi,
        
        [Name("NTLM")]
        Ntlm,
#endif
#if WIN || MONO || COREFX     
        [Name("EXTERNAL")]
        External,
#endif
        [Name("SCRAM-SHA-1")]
        ScramSha1,
        
        [Name("ANONYMOUS")]
        Anonymous,

        [Name("PLAIN")]
        Plain,
        
        [Name("DIGEST-MD5")]
        DigestMd5,
        
        [Name("X-GOOGLE-TOKEN")]
        XGoogleToken,
        
        [Name("X-FACEBOOK-PLATFORM")]
        XFacebookPlatform,
        
        [Name("X-MESSENGER-OAUTH2")]
        XMessengerOauth2,
        
        [Name("X-OAUTH2")]
        XOauth2,
        
        [Name("CISCO-VTG-TOKEN")]
        CiscoVtgToken,
        
        [Name("WEBEX-TOKEN")]
        WebexToken
    }
}