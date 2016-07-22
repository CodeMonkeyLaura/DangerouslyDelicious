// Simple OAuth .Net
// (c) 2012 Daniel McKenzie
// Simple OAuth .Net may be freely distributed under the MIT license.

using System;
using System.Collections.Generic;
using SimpleOAuthXamarin.Generators;

namespace SimpleOAuthXamarin.Internal
{
    internal static class SignatureMethod
    {

        private static Dictionary<EncryptionMethod, ISignatureGenerator> _instancedGenerators = new Dictionary<EncryptionMethod, ISignatureGenerator>();

        private static ISignatureGenerator GetGeneratorForMethod(EncryptionMethod signMethod)
        {
            if (_instancedGenerators.ContainsKey(signMethod))
            {
                return _instancedGenerators[signMethod];
            }
            else
            {
                Type signType = signMethod.GetSignatureType();
                ISignatureGenerator instancedType = Activator.CreateInstance(signType) as ISignatureGenerator;
                _instancedGenerators.Add(signMethod, instancedType);
                return instancedType;
            }
        }

        public static string CreateSignature(EncryptionMethod signMethod, string baseString, string key)
        {
            return GetGeneratorForMethod(signMethod).Generate(baseString.Trim(), key.Trim());
        }

    }
}
