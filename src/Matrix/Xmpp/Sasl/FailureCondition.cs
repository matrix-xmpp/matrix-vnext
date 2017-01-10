using Matrix.Attributes;

namespace Matrix.Xmpp.Sasl
{
    public enum FailureCondition
    {
        /// <summary>
        /// The receiving entity acknowledges an abort element sent by the initiating entity; sent in reply to the abort element.
        /// </summary>
        [Name("aborted")]
        Aborted,

        /// <summary>
        /// The account of the initiating entity has been temporarily disabled; sent in reply to an auth element (with or without initial response data) or a response element.
        /// </summary>
        [Name("account-disabled")]
        AccountDisabled,

        /// <summary>
        /// The authentication failed because the initiating entity provided credentials that have expired; sent in reply to a response element or an auth element with initial response data.
        /// </summary>
        [Name("credentials-expired")]
        CredentialsExpired,

        /// <summary>
        /// The mechanism requested by the initiating entity cannot be used unless the confidentiality and integrity of the underlying stream are protected (typically via TLS); sent in reply to an <auth/> element (with or without initial response data).
        /// </summary>
        [Name("encryption-required")]
        EncryptionRequired,

        /// <summary>
        /// The data provided by the initiating entity could not be processed because the [BASE64] (Josefsson, S., “The Base16, Base32, and Base64 Data Encodings,” July 2003.) encoding is incorrect (e.g., because the encoding does not adhere to the definition in Section 3 of [BASE64] (Josefsson, S., “The Base16, Base32, and Base64 Data Encodings,” July 2003.)); sent in reply to a <response/> element or an <auth/> element with initial response data.
        /// </summary>
        [Name("incorrect-encoding")]
        IncorrectEncoding,

        /// <summary>
        /// The authzid provided by the initiating entity is invalid, either because it is incorrectly formatted or because the initiating entity does not have permissions to authorize that ID; sent in reply to a <response/> element or an <auth/> element with initial response data.
        /// </summary>
        [Name("invalid-authzid")]
        InvalidAuthzId,

        /// <summary>
        /// The initiating entity did not provide a mechanism or requested a mechanism that is not supported by the receiving entity; sent in reply to an <auth/> element.
        /// </summary>
        [Name("invalid-mechanism")]
        InvalidMechanism,

        /// <summary>
        /// The request is malformed (e.g., the <auth/> element includes initial response data but the mechanism does not allow that, or the data sent violates the syntax for the specified SASL mechanism); sent in reply to an abort, auth, challenge, or response element.
        /// </summary>
        [Name("malformed-request")]
        MalformedRequest,

        /// <summary>
        /// The mechanism requested by the initiating entity is weaker than server policy permits for that initiating entity; sent in reply to a <response/> element or an <auth/> element with initial response data.
        /// </summary>
        [Name("mechanism-too-weak")]
        MechanismTooWeak,

        /// <summary>
        /// The authentication failed because the initiating entity did not provide valid credentials (this includes but is not limited to the case of an unknown username); sent in reply to a <response/> element or an <auth/> element with initial response data.
        /// </summary>
        [Name("not-authorized")]
        NotAuthorized,

        /// <summary>
        /// The authentication failed because of a temporary error condition within the receiving entity; sent in reply to an <auth/> element or <response/> element.
        /// </summary>
        [Name("temporary-auth-failure")]
        TemporaryAuthFailure,

        /// <summary>
        /// Unknown Condition
        /// </summary>
        UnknownCondition
    }
}