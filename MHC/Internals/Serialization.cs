﻿using System;
using System.Linq;
using System.Net;
using Org.BouncyCastle.Utilities.Encoders;

namespace MHC.Internals
{
    /// <summary>
    /// MetaHash encoding implementation
    /// Credits to https://github.com/maincoon/MHCCrypto
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// Check endian and encode value
        /// </summary>
        public static byte[] LE(short value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                value = IPAddress.NetworkToHostOrder(value);
            }
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Check endian and encode value
        /// </summary>
        public static byte[] LE(ushort value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                value = (ushort)IPAddress.NetworkToHostOrder(value);
            }
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Check endiann and encode value
        /// </summary>
        public static byte[] LE(int value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                value = IPAddress.NetworkToHostOrder(value);
            }
            return BitConverter.GetBytes(value);
        }


        /// <summary>
        /// Check endian and encode value
        /// </summary>
        public static byte[] LE(uint value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                value = (uint)IPAddress.NetworkToHostOrder(value);
            }
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Check endian and encode value
        /// </summary>
        public static byte[] LE(long value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                value = IPAddress.NetworkToHostOrder(value);
            }
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Encode up to int64 values no negative numbers
        /// </summary>
        public static byte[] Encode(decimal value)
        {
            if (value <= 249)
            {
                return new byte[] {
                    (byte) value
                };
            }
            if (value <= ushort.MaxValue)
            {
                return new byte[] {
                    0xfa
                }.Concat(LE((ushort)value)).ToArray();
            }
            if (value <= uint.MaxValue)
            {
                return new byte[] {
                    0xfb
                }.Concat(LE((uint)value)).ToArray();
            }
            return new byte[] {
                0xfc
            }.Concat(LE((long)value)).ToArray();
        }

        /// <summary>
        /// Encode transaction values
        /// </summary>
        public static byte[] Encode(string to, decimal value, decimal fee, int nonce, string data)
        {
            // following field values are given for signature: to, value, fee, nonce,  data
            byte[] encoded = Hex.Decode(to.Substring(2));
            encoded = encoded.
                Concat(Encode(value)).
                Concat(Encode(fee)).
                Concat(Encode(nonce)).ToArray();
            if (string.IsNullOrEmpty(data))
            {
                encoded = encoded.Concat(new byte[] { 0x00 }).ToArray();
            }
            else
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                encoded = encoded.Concat(Encode(bytes.Length)).Concat(bytes).ToArray();
            }
            return encoded;
        }
    }
}
