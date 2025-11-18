using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Sudoku.Application.Interfaces;

namespace Sudoku.WebApi.Infrastructure.Services;

public class CryptographyServices(IConfiguration configuration) : ICryptographyServices
{
    public string Hash(string stringToHash)
    {
        if (stringToHash == null) throw new ArgumentNullException(nameof(stringToHash));

        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(stringToHash);
            byte[] hash = sha256.ComputeHash(bytes);

            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

    public string AesEncrypt(string text)
    {
        using (Aes aes = Aes.Create())
        {
            var key = configuration["AesKey"];
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

    }

    public string AesDecrypt(string text)
    {
        using (Aes aes = Aes.Create())
        {
            var key = configuration["AesKey"];
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text.Replace(" ", "+"))))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

    }

}
