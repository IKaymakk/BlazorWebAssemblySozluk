﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infastructure;

public class PasswordEncryptor
{
    public static string Encrypt(string password)
    {
        using var md5 = MD5.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(password);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}
