﻿using System.Security.Cryptography;
using System.Text;

namespace WSVentas.Tools
{
    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            StringBuilder sb = new();
            byte[] stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i=0; i < stream.Length; i++) 
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }
    }
}
