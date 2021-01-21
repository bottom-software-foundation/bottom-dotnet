using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bottom_NET
{
    public static class Bottom
    {
        private const string LINE_ENDING = "👉👈";

        private static readonly Dictionary<byte, string> _character_values = new Dictionary<byte, string>()
        {
            {200, "🫂"},
            {50, "💖"},
            {10, "✨"},
            {5, "🥺"},
            {1, ","},
            {0, "❤️"}
        };

        private static readonly Dictionary<byte, string> _byte_to_emoji = MapByteToEmoji();
        private static readonly Dictionary<string, byte> _emoji_to_byte = MapEmojiToByte();

        # region Public methods

        public static string encode_byte(byte value)
        {
            return _byte_to_emoji[value] + LINE_ENDING;
        }


        public static byte decode_byte(string input) 
        {
            if (_emoji_to_byte.ContainsKey(input))
            {
                return _emoji_to_byte[input];
            }
            throw new KeyNotFoundException($"Cannot decode character {input}");
        }

        public static string encode_string(string input)
        {
            return string.Join("", Encoding.UTF8.GetBytes(input).Select(encode_byte).ToArray());
        }

        public static string decode_string(string input)
        {
            string[] chars = input.Split(new string[] { "\u200B", LINE_ENDING }, StringSplitOptions.RemoveEmptyEntries);
            return Encoding.UTF8.GetString(chars.Select(decode_byte).ToArray());
        }

        #endregion

        #region Private Methods

        private static string ByteToEmoji(byte value)
        {
            StringBuilder buffer = new StringBuilder();

            if (value == 0)
            {
                return _character_values[0];
            }

            while (value > 0)
            {
                string to_push = string.Empty;
                byte subtract_by = 0;

                foreach (KeyValuePair<byte, string> mapping in _character_values)
                {
                    if (value >= mapping.Key)
                    {
                        to_push = mapping.Value;
                        subtract_by = mapping.Key;
                        goto push;
                    }
                }

                push:;
                buffer.Append(to_push);
                value -= subtract_by;
            }
            return buffer.ToString();
        }

        private static Dictionary<byte, string> MapByteToEmoji()
        {
            Dictionary<byte, string> mapping = new Dictionary<byte, string>();

            byte i = 0;
            do
            {
                mapping[i] = ByteToEmoji(i);
                i++;
            } while (i != 0);

            return mapping;
        }

        private static Dictionary<string, byte> MapEmojiToByte()
        {
            Dictionary<string, byte> mapping = new Dictionary<string, byte>();

            foreach (KeyValuePair<byte, string> map in _byte_to_emoji)
            {
                mapping[map.Value] = map.Key;
            }

            return mapping;
        }

        #endregion

    }
}
