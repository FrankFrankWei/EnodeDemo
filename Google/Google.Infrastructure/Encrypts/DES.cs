using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Google.Infrastructure.Encrypts
{
    public class DES
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plain">明文</param>
        /// <param name="key">加密密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>返回密文</returns>
        public static string Encrypt(string plain, string key, string iv)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            byte[] ivBytes = Encoding.ASCII.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, des.CreateEncryptor(keyBytes, ivBytes), CryptoStreamMode.Write);
            StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.WriteLine(plain);
            swEncrypt.Close();
            csEncrypt.Close();
            byte[] bytesCipher = msEncrypt.ToArray();
            msEncrypt.Close();
            return Convert.ToBase64String(bytesCipher);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipher">密文</param>
        /// <param name="key">加密密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static string Decrypt(string cipher, string key, string iv)
        {
            byte[] cipherByte = Convert.FromBase64String(cipher);
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] ivBytes = Encoding.ASCII.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream msDecrypt = new MemoryStream(cipherByte);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, des.CreateDecryptor(keyBytes, ivBytes), CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            string strPlainText = srDecrypt.ReadLine();
            srDecrypt.Close();
            csDecrypt.Close();
            msDecrypt.Close();
            return strPlainText;
        }
    }
}
