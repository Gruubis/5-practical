using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _5_practical_task
{

    class RSACSPSample
    {
        public ASCIIEncoding ByteConverter = new ASCIIEncoding();
        public RSAParameters Key;
        public string test;

        public byte[] HashAndSignBytes(string DataToSign)
        {
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                Key = RSAalg.ExportParameters(true);
                byte[] originalData = ByteConverter.GetBytes(DataToSign);

                RSAalg.ImportParameters(Key);
                 test = RSAalg.ToXmlString(true);

                return RSAalg.SignData(originalData, SHA256.Create());

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, string Key)
        {
            try
            {
                
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.FromXmlString(Key);

                return RSAalg.VerifyData(DataToVerify, SHA256.Create(), SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
