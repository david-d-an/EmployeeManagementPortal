using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

/* 
    RSACryptoServiceProvider couldn't be used to make X.509 cert works on OSX.
    For deails,
    https://stackoverflow.com/questions/54180171/cspkeycontainerinfo-requires-windows-cryptographic-api-capi-which-is-not-av
*/

namespace EMP.Sts.Security
{
    public class RsaKeyService
    {
        /// <summary>
        /// This points to a JSON file in the format: 
        /// {
        ///  "Modulus": "",
        ///  "Exponent": "",
        ///  "P": "",
        ///  "Q": "",
        ///  "DP": "",
        ///  "DQ": "",
        ///  "InverseQ": "",
        ///  "D": ""
        /// }
        /// </summary>
        private string _file {
            get {
                return Path.Combine(_environment.ContentRootPath, Config.GetRsaKeyLocation(_cfg));
            }
        }
        private readonly IWebHostEnvironment _environment;
        private readonly TimeSpan _timeSpan;
        private readonly IConfiguration _cfg;

        public RsaKeyService(IWebHostEnvironment environment, TimeSpan timeSpan, IConfiguration cfg) {
            _environment = environment;
            _timeSpan = timeSpan;
            _cfg = cfg;
        }

        public bool NeedsUpdate() {
            if (File.Exists(_file)) {
                var creationDate = File.GetCreationTime(_file);
                return DateTime.Now.Subtract(creationDate) > _timeSpan;
            }
            return true;
        }

        public RSAParameters GetRandomKey()
        {
            // RSACryptoServiceProvider couldn't be used to make X.509 cert works on OSX 
            // using (var rsa = new RSACryptoServiceProvider(2048))
            using (var rsa = RSA.Create(2048))
            {
                try
                {
                    return rsa.ExportParameters(true);
                }
                finally
                {
                    // RSACryptoServiceProvider couldn't be used to make X.509 cert works on OSX 
                    // rsa.PersistKeyInCsp = false;
                }
            }
        }

        public RsaKeyService GenerateKeyAndSave(bool forceUpdate = false)
        {
            if (forceUpdate || NeedsUpdate()) {
                var p = GetRandomKey();
                RSAParametersWithPrivate t = new RSAParametersWithPrivate();
                t.SetParameters(p);
                File.WriteAllText(_file, JsonConvert.SerializeObject(t, Formatting.Indented));
            }
            return this;
        }

        /// <summary>
        /// 
        /// Generate 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public RSAParameters GetKeyParameters()
        {
            if (!File.Exists(_file)) throw new FileNotFoundException("Check configuration - cannot find auth key file: " + _file);
            var keyParams = JsonConvert.DeserializeObject<RSAParametersWithPrivate>(File.ReadAllText(_file));
            return keyParams.ToRSAParameters();
        }

        public RsaSecurityKey GetKey() {
            if (NeedsUpdate()) GenerateKeyAndSave();

            // RSACryptoServiceProvider couldn't be used to make X.509 cert works on OSX 
            // var provider = new System.Security.Cryptography.RSACryptoServiceProvider();
            var provider = RSA.Create(2048);

            provider.ImportParameters(GetKeyParameters());
            return new RsaSecurityKey(provider);
        }


        /// <summary>
        /// Util class to allow restoring RSA parameters from JSON as the normal
        /// RSA parameters class won't restore private key info.
        /// </summary>
        private class RSAParametersWithPrivate
        {
            public byte[] D { get; set; }
            public byte[] DP { get; set; }
            public byte[] DQ { get; set; }
            public byte[] Exponent { get; set; }
            public byte[] InverseQ { get; set; }
            public byte[] Modulus { get; set; }
            public byte[] P { get; set; }
            public byte[] Q { get; set; }

            public void SetParameters(RSAParameters p)
            {
                D = p.D;
                DP = p.DP;
                DQ = p.DQ;
                Exponent = p.Exponent;
                InverseQ = p.InverseQ;
                Modulus = p.Modulus;
                P = p.P;
                Q = p.Q;
            }
            public RSAParameters ToRSAParameters()
            {
                return new RSAParameters()
                {
                    D = this.D,
                    DP = this.DP,
                    DQ = this.DQ,
                    Exponent = this.Exponent,
                    InverseQ = this.InverseQ,
                    Modulus = this.Modulus,
                    P = this.P,
                    Q = this.Q

                };
            }
        }
    }
}