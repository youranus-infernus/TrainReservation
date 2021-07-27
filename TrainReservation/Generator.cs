using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation
{
    class Generator
    {
        private static readonly Random random = new Random();
        private static readonly string alphabet = "0123456789ABCDEF";
        public string Generate()
        {
            var buffer = new char[32];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = alphabet[random.Next(alphabet.Length)];
            }
            return new string(buffer);
        }
    }
}
