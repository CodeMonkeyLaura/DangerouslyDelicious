// Simple OAuth .Net
// (c) 2012 Daniel McKenzie
// Simple OAuth .Net may be freely distributed under the MIT license.

using System.ComponentModel;
using SimpleOAuthXamarin.Generators;
using SimpleOAuthXamarin.Internal;

namespace SimpleOAuth
{
    /// <summary>
    /// The types of Encryption that can be performed to generate an oauth_signature.
    /// </summary>
    public enum EncryptionMethod
    {
        /// <summary>
        /// PLAINTEXT
        /// </summary>
        [Description("PLAINTEXT")]
        Plain,

        /// <summary>
        /// HMAC-SHA1
        /// </summary>
        [SignatureType(typeof(HmacSha1Generator))]
        [Description("HMAC-SHA1")]
        HMACSHA1
    }
}
