using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Common
{
    public class EncryptDecrypt
    {

        private static byte[] Key = Encoding.UTF8.GetBytes("tu89geji340t89u2");
        private static byte[] IV = Encoding.UTF8.GetBytes("tu89geji340t89u2");

        public static byte[] EncryptToBytesUsingCBC(string toEncrypt)
        {
            byte[] src = Encoding.UTF8.GetBytes(toEncrypt);
            byte[] dest = new byte[src.Length];
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.BlockSize = 128;
                aes.KeySize = 128;
                aes.IV = IV;
                aes.Key = Key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                // encryption
                using (ICryptoTransform encrypt = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return encrypt.TransformFinalBlock(src, 0, src.Length);
                }
            }
        }

        public static string EncryptUsingCBC(string toEncrypt)
        {
            return Convert.ToBase64String(EncryptToBytesUsingCBC(toEncrypt));
        }

        public static string DecryptToBytesUsingCBC(byte[] toDecrypt)
        {
            byte[] src = toDecrypt;
            byte[] dest = new byte[src.Length];
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.BlockSize = 128;
                aes.KeySize = 128;
                aes.IV = IV;
                aes.Key = Key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                // decryption
                using (ICryptoTransform decrypt = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] decryptedText = decrypt.TransformFinalBlock(src, 0, src.Length);

                    return Encoding.UTF8.GetString(decryptedText);
                }
            }
        }

        public static string DecryptUsingCBC(string toDecrypt)
        {
            string s = DecryptToBytesUsingCBC(Convert.FromBase64String(toDecrypt));
            return s;
        }
    }
}
