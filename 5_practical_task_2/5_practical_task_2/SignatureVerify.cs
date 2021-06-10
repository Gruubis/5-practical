using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _5_practical_task_2
{
    class SignatureVerify
    {
        
        public static bool VerifySignedHash(string DataToVerify, byte[] SignedData, string Key)
        {
            try
            {
                ASCIIEncoding ByteConverter = new ASCIIEncoding();
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.FromXmlString(Key);

                byte[] data = Encoding.ASCII.GetBytes(DataToVerify);

                RSAParameters test = RSAalg.ExportParameters(true);
                return RSAalg.VerifyData(data, SHA256.Create(), SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
