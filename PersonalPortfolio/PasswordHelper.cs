using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PersonalPortfolio.Helpers
{
    public static class PasswordHelper
    {
        // Seed data için sabit salt değeri
        private static readonly byte[] SeedSalt = new byte[] 
        { 
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 
        };

        public static string HashPassword(string password, bool forSeed = false)
        {
            byte[] salt;
            if (forSeed)
            {
                // Seed data için sabit salt kullan
                salt = SeedSalt;
            }
            else
            {
                // Normal kullanım için rastgele salt
                salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public static bool VerifyPassword(string storedHash, string password)
        {
            try
            {
                var parts = storedHash.Split('.');
                if (parts.Length != 2) return false;

                var salt = Convert.FromBase64String(parts[0]);
                var hash = parts[1];

                string newHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                return hash == newHash;
            }
            catch
            {
                return false;
            }
        }
    }
}