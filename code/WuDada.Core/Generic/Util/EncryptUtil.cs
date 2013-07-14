using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace WuDada.Core.Generic.Util
{
    public class EncryptUtil
    {

        /// <summary>   
        /// 取得 MD5 編碼後的 Hex 字串   
        /// 加密後為 32 Bytes Hex String (16 Byte)   
        /// </summary>   
        /// <param name="original">原始字串</param>   
        /// <returns></returns>   
        public static string GetMD5(string original)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = md5.ComputeHash(Encoding.UTF8.GetBytes(original));
            return BitConverter.ToString(b).Replace("-", string.Empty);
        }

        /// <summary>   
        /// DES 加密字串   
        /// </summary>   
        /// <param name="original">原始字串</param>   
        /// <param name="key">Key，長度必須為 8 個 ASCII 字元</param>   
        /// <param name="iv">IV，長度必須為 8 個 ASCII 字元</param>   
        /// <returns></returns>   
        public static string EncryptDES(string original, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = Encoding.ASCII.GetBytes(iv);
                byte[] s = Encoding.ASCII.GetBytes(original);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                return BitConverter.ToString(desencrypt.TransformFinalBlock(s, 0, s.Length)).Replace("-", string.Empty);
            }
            catch { return original; }
        }

        /// <summary>   
        /// DES 解密字串   
        /// </summary>   
        /// <param name="hexString">加密後 Hex String</param>   
        /// <param name="key">Key，長度必須為 8 個 ASCII 字元</param>   
        /// <param name="iv">IV，長度必須為 8 個 ASCII 字元</param>   
        /// <returns></returns>   
        public static string DecryptDES(string hexString, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = Encoding.ASCII.GetBytes(iv);

                byte[] s = new byte[hexString.Length / 2];
                int j = 0;
                for (int i = 0; i < hexString.Length / 2; i++)
                {
                    s[i] = Byte.Parse(hexString[j].ToString() + hexString[j + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 2;
                }
                ICryptoTransform desencrypt = des.CreateDecryptor();
                return Encoding.ASCII.GetString(desencrypt.TransformFinalBlock(s, 0, s.Length));
            }
            //catch { return hexString; }
            catch { return string.Empty; }
        }


        /// <summary>   
        /// RSA 加密字串   
        /// </summary>   
        /// <param name="original">原始字串</param>   
        /// <param name="xmlString">公鑰 xml 字串</param>   
        /// <returns></returns>   
        public static string EncryptRSA(string original, string xmlString)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlString);
                byte[] s = Encoding.ASCII.GetBytes(original);
                return BitConverter.ToString(rsa.Encrypt(s, false)).Replace("-", string.Empty);
            }
            catch { return original; }
        }

        /// <summary>   
        /// RSA 加密字串   
        /// 加密後為 256 Bytes Hex String (128 Byte)   
        /// </summary>   
        /// <param name="original">原始字串</param>   
        /// <param name="parameters">公鑰 RSAParameters 類別</param>   
        /// <returns></returns>   
        public static string EncryptRSA(string original, RSAParameters parameters)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(parameters);
                byte[] s = Encoding.ASCII.GetBytes(original);
                return BitConverter.ToString(rsa.Encrypt(s, false)).Replace("-", string.Empty);
            }
            catch { return original; }
        }

        /// <summary>   
        /// RSA 解密字串   
        /// </summary>   
        /// <param name="hexString">加密後 Hex String</param>   
        /// <param name="xmlString">私鑰 xml 字串</param>   
        /// <returns></returns>   
        public static string DecryptRSA(string hexString, string xmlString)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlString);
                byte[] s = new byte[hexString.Length / 2];
                int j = 0;
                for (int i = 0; i < hexString.Length / 2; i++)
                {
                    s[i] = Byte.Parse(hexString[j].ToString() + hexString[j + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 2;
                }
                return Encoding.ASCII.GetString(rsa.Decrypt(s, false));
            }
            catch { return hexString; }
        }

        /// <summary>   
        /// RSA 解密字串   
        /// </summary>   
        /// <param name="hexString">加密後 Hex String</param>   
        /// <param name="parameters">私鑰 RSAParameters 類別</param>   
        /// <returns></returns>   
        public static string DecryptRSA(string hexString, RSAParameters parameters)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(parameters);
                byte[] s = new byte[hexString.Length / 2];
                int j = 0;
                for (int i = 0; i < hexString.Length / 2; i++)
                {
                    s[i] = Byte.Parse(hexString[j].ToString() + hexString[j + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 2;
                }
                return Encoding.ASCII.GetString(rsa.Decrypt(s, false));
            }
            catch { return hexString; }
        }

    }
}
