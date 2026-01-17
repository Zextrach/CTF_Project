using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace CTFGame
{
    // ===================================================================================
    // KRİPTOLOJİ ARAÇLARI (Servis Katmanı - "Siber Şef")
    // ===================================================================================
    public static class CryptoHelper
    {
        public static string Base64Encode(string text) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

        public static string Base64Decode(string base64)
        {
            try { return Encoding.UTF8.GetString(Convert.FromBase64String(base64)); }
            catch { return "Hata: Geçersiz Base64"; }
        }

        public static string Rot13(string input) => CaesarCipher(input, 13);

        public static string CaesarCipher(string input, int shift)
        {
            char[] buffer = input.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char d = char.IsUpper(letter) ? 'A' : 'a';
                    int offset = (letter + shift) - d;
                    buffer[i] = (char)((((offset % 26) + 26) % 26) + d);
                }
            }
            return new string(buffer);
        }

        public static string HexToString(string hex)
        {
            try
            {
                hex = hex.Replace(" ", "").Replace("-", "");
                byte[] raw = new byte[hex.Length / 2];
                for (int i = 0; i < raw.Length; i++)
                    raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                return Encoding.UTF8.GetString(raw);
            }
            catch { return "Hata: Geçersiz Hex"; }
        }

        public static string BinaryToString(string binary)
        {
            try
            {
                string cleanBinary = binary.Replace(" ", "");
                var byteList = new List<byte>();
                for (int i = 0; i < cleanBinary.Length; i += 8)
                {
                    if (i + 8 > cleanBinary.Length) break;
                    string byteStr = cleanBinary.Substring(i, 8);
                    byteList.Add(Convert.ToByte(byteStr, 2));
                }
                return Encoding.UTF8.GetString(byteList.ToArray());
            }
            catch { return "Hata: Geçersiz Binary Dizisi"; }
        }

        public static string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string AsciiShift(string input, int shift)
        {
            char[] buffer = input.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)(buffer[i] + shift);
            }
            return new string(buffer);
        }

        public static string Atbash(string input)
        {
            char[] buffer = input.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char c = buffer[i];
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    buffer[i] = (char)('Z' - (char.ToUpper(c) - 'A') + (char.IsLower(c) ? 32 : 0));
                }
            }
            return new string(buffer);
        }

        public static string CalculateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("x2"));
                return sb.ToString();
            }
        }

        public static string CrackMD5(string targetHash)
        {
            string[] commonPasswords = {
                "12345", "123456", "password", "admin", "root",
                "qwerty", "1234", "111111", "welcome", "login", "master", "cyber"
            };

            targetHash = targetHash.Trim().ToLower();

            foreach (string word in commonPasswords)
            {
                string hash = CalculateMD5(word);
                if (hash == targetHash)
                {
                    return $"KIRILDI: {word}";
                }
            }
            return "BAŞARISIZ: Hash sözlükte bulunamadı.";
        }

        public static string MorseDecode(string input)
        {
            Dictionary<string, char> morseMap = new Dictionary<string, char>()
            {
                {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'}, {".", 'E'},
                {"..-.", 'F'}, {"--.", 'G'}, {"....", 'H'}, {"..", 'I'}, {".---", 'J'},
                {"-.-", 'K'}, {".-..", 'L'}, {"--", 'M'}, {"-.", 'N'}, {"---", 'O'},
                {".--.", 'P'}, {"--.-", 'Q'}, {".-.", 'R'}, {"...", 'S'}, {"-", 'T'},
                {"..-", 'U'}, {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'}, {"-.--", 'Y'},
                {"--..", 'Z'}, {".----", '1'}, {"..---", '2'}, {"...--", '3'}, {"....-", '4'},
                {".....", '5'}, {"-....", '6'}, {"--...", '7'}, {"---..", '8'}, {"----.", '9'},
                {"-----", '0'}, {"/", ' '}
            };

            StringBuilder sb = new StringBuilder();
            string[] words = input.Split(new string[] { "   ", " / ", "  " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                string[] letters = word.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var letter in letters)
                {
                    if (morseMap.ContainsKey(letter))
                        sb.Append(morseMap[letter]);
                    else
                        sb.Append("?");
                }
                sb.Append(" ");
            }
            return sb.ToString().Trim();
        }

        public static string A1Z26Decode(string input)
        {
            StringBuilder sb = new StringBuilder();
            string[] parts = input.Split(new char[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int val) && val >= 1 && val <= 26)
                {
                    sb.Append((char)('A' + val - 1));
                }
                else
                {
                    sb.Append("?");
                }
            }
            return sb.ToString();
        }

        public static string OctalToString(string input)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    int val = Convert.ToInt32(part, 8);
                    sb.Append((char)val);
                }
                return sb.ToString();
            }
            catch { return "Hata: Geçersiz Octal"; }
        }

        public static string RailFenceDecode(string input)
        {
            int len = input.Length;
            if (len < 2) return input;

            int mid = (int)Math.Ceiling(len / 2.0);
            string r1 = input.Substring(0, mid);
            string r2 = input.Substring(mid);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < r2.Length; i++)
            {
                sb.Append(r1[i]);
                sb.Append(r2[i]);
            }
            if (r1.Length > r2.Length)
            {
                sb.Append(r1[r1.Length - 1]);
            }
            return sb.ToString();
        }
    }
}