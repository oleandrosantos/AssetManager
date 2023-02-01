using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Application.Helpers
{
    public static class PasswordHelper
    {
        private const KeyDerivationPrf HASH_TYPE = KeyDerivationPrf.HMACSHA1;
        private const int ITER_COUNT = 1000;
        private const int SUBKEY_LENGTH = 256 / 8;
        private const int SALT_SIZE = 128 / 8;

        public static string CriandoHashDaSenha(string senha)
        {
            byte[] salt = new byte[SALT_SIZE];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            byte[] subkey = KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: HASH_TYPE,
                iterationCount: ITER_COUNT,
                numBytesRequested: SUBKEY_LENGTH);

            var outputBytes = new byte[1 + SALT_SIZE + SUBKEY_LENGTH];
            outputBytes[0] = 0x00;
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SALT_SIZE);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SALT_SIZE, SUBKEY_LENGTH);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerificandoSenha(byte[] hashPassword, string password)
        {
            if (hashPassword.Length != 1 + SALT_SIZE + SUBKEY_LENGTH)
                return false;

            byte[] salt = new byte[SALT_SIZE];
            Buffer.BlockCopy(hashPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[SUBKEY_LENGTH];
            Buffer.BlockCopy(hashPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, HASH_TYPE, ITER_COUNT, SUBKEY_LENGTH);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
    }
}
