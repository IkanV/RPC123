using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RPS123
{
    class HMACKey
    {
        public string HMAC { get; private set; }

        public byte[] Key { get; private set; }

        private RandomNumberGenerator rng;

        const int KEY_LENGTH = 128;

        public HMACKey()
        {
            Key = new byte[KEY_LENGTH / 8];
            rng = RandomNumberGenerator.Create();
        }

        public byte[] GenerateKey()
        {
            rng.GetBytes(Key);
            return Key;
        }

        public byte[] GenerateHMAC(string move)
        {
            byte[] moveInBytes = Encoding.ASCII.GetBytes(move);
            HMACSHA256 hash = new HMACSHA256(Key);
            return hash.ComputeHash(moveInBytes);
        }
    }
}