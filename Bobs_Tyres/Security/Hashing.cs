using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace Bobs_Tyres.Security
{
    public class Hashing
    {
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20;
        public const int P2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int P2Index = 2;

        public static string ComputeHash(string password)
        {
            var hashProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            hashProvider.GetBytes(salt);

            var hash = GetP2Bytes(password, salt, P2Iterations, HashByteSize);
            return P2Iterations + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[P2Index]);

            var testHash = GetP2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetP2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var P2 = new Rfc2898DeriveBytes(password, salt);
            P2.IterationCount = iterations;
            return P2.GetBytes(outputBytes);
        }

    }
}