using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HslCommunication.Core
{
    public class SoftBasic
    {
        public static bool[] ByteToBoolArray(byte[] InBytes, int length)
        {
            if (InBytes == null) return null;

            if (length > InBytes.Length * 8) length = InBytes.Length * 8;
            bool[] buffer = new bool[length];

            for (int i = 0; i < length; i++)
            {
                int index = i / 8;
                int offect = i % 8;

                byte temp = 0;
                switch (offect)
                {
                    case 0: temp = 0x01; break;
                    case 1: temp = 0x02; break;
                    case 2: temp = 0x04; break;
                    case 3: temp = 0x08; break;
                    case 4: temp = 0x10; break;
                    case 5: temp = 0x20; break;
                    case 6: temp = 0x40; break;
                    case 7: temp = 0x80; break;
                    default: break;
                }

                if ((InBytes[index] & temp) == temp)
                {
                    buffer[i] = true;
                }
            }

            return buffer;
        }

        public static T[] ArrayExpandToLength<T>(T[] data, int length)
        {
            if (data == null) return new T[length];

            if (data.Length == length) return data;

            T[] buffer = new T[length];

            Array.Copy(data, buffer, Math.Min(data.Length, buffer.Length));

            return buffer;
        }

        public static T[] ArrayExpandToLengthEven<T>(T[] data)
        {
            if (data == null) return new T[0];

            if (data.Length % 2 == 1)
            {
                return ArrayExpandToLength(data, data.Length + 1);
            }
            else
            {
                return data;
            }
        }

        public static byte[] BoolArrayToByte(bool[] array)
        {
            if (array == null) return null;

            int length = array.Length % 8 == 0 ? array.Length / 8 : array.Length / 8 + 1;
            byte[] buffer = new byte[length];

            for (int i = 0; i < array.Length; i++)
            {
                int index = i / 8;
                int offect = i % 8;

                byte temp = 0;
                switch (offect)
                {
                    case 0: temp = 0x01; break;
                    case 1: temp = 0x02; break;
                    case 2: temp = 0x04; break;
                    case 3: temp = 0x08; break;
                    case 4: temp = 0x10; break;
                    case 5: temp = 0x20; break;
                    case 6: temp = 0x40; break;
                    case 7: temp = 0x80; break;
                    default: break;
                }

                if (array[i]) buffer[index] += temp;
            }

            return buffer;
        }

        public static bool IsByteTokenEquel(byte[] head, Guid token)
        {
            return IsTwoBytesEquel(head, 12, token.ToByteArray(), 0, 16);
        }

        public static bool IsTwoBytesEquel(byte[] b1, int start1, byte[] b2, int start2, int length)
        {
            if (b1 == null || b2 == null) return false;
            for (int i = 0; i < length; i++)
            {
                if (b1[i + start1] != b2[i + start2])
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] SpliceTwoByteArray(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1 == null && bytes2 == null) return null;
            if (bytes1 == null) return bytes2;
            if (bytes2 == null) return bytes1;

            byte[] buffer = new byte[bytes1.Length + bytes2.Length];
            bytes1.CopyTo(buffer, 0);
            bytes2.CopyTo(buffer, bytes1.Length);
            return buffer;
        }

        public static List<T[]> ArraySplitByLength<T>(T[] array, int length)
        {
            if (array == null) return new List<T[]>();

            List<T[]> result = new List<T[]>();
            int index = 0;
            while (index < array.Length)
            {
                if (index + length < array.Length)
                {
                    T[] tmp = new T[length];
                    Array.Copy(array, index, tmp, 0, length);
                    index += length;
                    result.Add(tmp);
                }
                else
                {
                    T[] tmp = new T[array.Length - index];
                    Array.Copy(array, index, tmp, 0, tmp.Length);
                    index += length;
                    result.Add(tmp);
                }
            }
            return result;
        }

        public static string GetUniqueStringByGuidAndRandom()
        {
            Random random = new Random();
            return Guid.NewGuid().ToString("N") + random.Next(1000, 10000);
        }

        public static string ByteToHexString(byte[] InBytes)
        {
            return ByteToHexString(InBytes, (char)0);
        }

        public static string ByteToHexString(byte[] InBytes, char segment)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte InByte in InBytes)
            {
                if (segment == 0) sb.Append(string.Format("{0:X2}", InByte));
                else sb.Append(string.Format("{0:X2}{1}", InByte, segment));
            }

            if (segment != 0 && sb.Length > 1 && sb[sb.Length - 1] == segment)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}
