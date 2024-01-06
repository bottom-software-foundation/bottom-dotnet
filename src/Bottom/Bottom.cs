using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bottom
{
    public static class Bottomify
    {
        #region Private Attributes

        private const string LEGACY_BYTE_TERMINATOR = "\u200B";
        private const string BYTE_TERMINATOR = "👉👈";
        private const string NULL_VALUE = "❤️";

        private static readonly Dictionary<byte, string> _characterValues = new Dictionary<byte, string>()
        {
            {200, "🫂"},
            {50, "💖"},
            {10, "✨"},
            {5, "🥺"},
            {1, ","},
            {0, NULL_VALUE}
        };

        private static readonly Dictionary<string, byte> _characterValuesReversed =
            _characterValues.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        private static readonly string[] _byteToStrippedCharacterValueGroup =
            Enumerable.Range(byte.MinValue, byte.MaxValue)
                      .Select(i => ByteToStrippedCharacterValueGroup((byte)i))
                      .ToArray();

        #endregion

        #region Public methods

        /// <summary>
        /// Determines if an input string is a valid Bottom character value group.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <returns>True if the input string is a Bottom chracter value group, otherwise false.</returns>
        public static bool IsCharacterValueGroup(string input) =>
            TryDecodeCharacterValueGroup(input, out _);


        /// <summary>
        /// Encode a Byte in a Bottom character value group.
        /// </summary>
        /// <param name="value">The byte to encode.</param>
        /// <returns>The encoded Bottom character value group.</returns>
        public static string EncodeByte(byte value) =>
            _byteToStrippedCharacterValueGroup[value] + BYTE_TERMINATOR;


        /// <summary>
        /// Decode a Bottom character value group into a byte.
        /// </summary>
        /// <param name="input">The Bottom character value group to decode.</param>
        /// <exception cref="KeyNotFoundException">A non-valid bottom character value group was passed.</exception>
        /// <returns>The decoded byte.</returns>
        public static byte DecodeCharacterValueGroup(string input)
        {
            try
            {
                return DecodeStrippedCharacterValueGroup(GetStrippedCharacterValueGroups(input).Single());
            }
            catch (Exception e) when (e is InvalidOperationException || e is KeyNotFoundException)
            {
                throw new KeyNotFoundException($"Cannot decode value character \"{input}\".");
            }
        }


        /// <summary>
        /// Try to decode a Bottom character value group into a byte.
        /// </summary>
        /// <param name="input">The Bottom character value group to decode.</param>
        /// <param name="value">The decoded byte or 0 if the input string could not be decoded.</param>
        /// <returns>True if the input string was a valid Bottom character value group, otherwise false.</returns>
        public static bool TryDecodeCharacterValueGroup(string input, out byte value)
        {
            try
            {
                value = DecodeCharacterValueGroup(input);
                return true;
            }
            catch (KeyNotFoundException)
            {
                value = 0;
                return false;
            }
        }


        /// <summary>
        /// Determines whether a string is Bottom encoded.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <returns>True if the input string is Bottom encoded, otherwise false.</returns>
        public static bool IsEncoded(string input) =>
            TryDecodeString(input, out _);


        /// <summary>
        /// Encode a string in Bottom.
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <returns>The Bottom encoded string.</returns>
        public static string EncodeString(string input) =>
            string.Join("", Encoding.UTF8.GetBytes(input).Select(EncodeByte));


        /// <summary>
        /// Decode a Bottom encoded string.
        /// </summary>
        /// <param name="input">The Bottom encoded string to decode.</param>
        /// <exception cref="KeyNotFoundException">The input string contained an invalid Bottom character value group.</exception>
        /// <returns>The decoded string.</returns>
        public static string DecodeString(string input) =>
            Encoding.UTF8.GetString(GetStrippedCharacterValueGroups(input)
                                        .Select(DecodeStrippedCharacterValueGroup)
                                        .ToArray());


        /// <summary>
        /// Try to decode a Bottom encoded string.
        /// </summary>
        /// <param name="input">The Bottom encoded string to decode.</param>
        /// <param name="output">The decoded string or null if the input string could not be decoded.</param>
        /// <returns>Whether the input string was a valid Bottom encoded string.</returns>
        public static bool TryDecodeString(string input, out string output)
        {
            try
            {
                output = DecodeString(input);
                return true;
            }
            catch (KeyNotFoundException)
            {
                output = null;
                return false;
            }
        }

        #endregion

        #region Private Methods

        private static byte DecodeStrippedCharacterValueGroup(string input)
        {
            if (string.Equals(input, NULL_VALUE))
            {
                return 0;
            }

            return StrippedCharacterValueGroupToByte(input);
        }


        private static string ByteToStrippedCharacterValueGroup(byte value)
        {
            if (value == 0)
            {
                return NULL_VALUE;
            }

            var buffer = new StringBuilder();

            do
            {
                foreach (var kvp in _characterValues)
                {
                    if (value >= kvp.Key)
                    {
                        buffer.Append(kvp.Value);
                        value -= kvp.Key;
                        break;
                    }
                }
            } while (value > 0);

            return buffer.ToString();
        }

        private static IEnumerable<string> GetStrippedCharacterValueGroups(string input)
        {
            input = input.Replace(LEGACY_BYTE_TERMINATOR, BYTE_TERMINATOR);

            while (!string.IsNullOrEmpty(input))
            {
                var byteEndIndex = input.IndexOf(BYTE_TERMINATOR);

                if (byteEndIndex < 1)
                {
                    throw new KeyNotFoundException($"Cannot decode input \"{input}\".");
                }

                yield return input.Substring(0, byteEndIndex);
                input = input.Substring(byteEndIndex + BYTE_TERMINATOR.Length);
            }
        }


        private static byte StrippedCharacterValueGroupToByte(string input) => 
            (byte)GetCodepoints(input).Select(s => (int)_characterValuesReversed[s]).Sum();


        private static IEnumerable<string> GetCodepoints(string input)
        {
            while (!string.IsNullOrEmpty(input))
            {
                yield return char.ConvertFromUtf32(char.ConvertToUtf32(input, 0));
                input = input.Substring(char.IsHighSurrogate(input, 0) ? 2 : 1);
            }
        }

        #endregion

    }
}
