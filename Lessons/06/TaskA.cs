﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace Lessons._06
{

    /// <summary>
    /// Implement MorseCodeConverter to pass all tests.
    /// Implement MorseCode class to pass all tests. Use MorseCodeConverter.
    /// </summary>
    public class TaskA
    {
        public class MorseCodeConverterTests
        {
            // Task: Implement MorseCodeConverter.ToMorseCode
            // Don't care about invalid characters.

            [Theory]
            [InlineData("", "")]
            [InlineData("a", ".-")]
            [InlineData("A", ".-")]
            [InlineData("42", "....-|..---")]
            [InlineData("Keep@it@secret!", "-.-|.|.|.--.|.--.-.|..|-|.--.-.|...|.|-.-.|.-.|.|-|-.-.--")]
            public void ToMorseCode_ForGivenText_ShouldReturnExpected(string text, string expectedMorseCode)
            {
                var morseCode = MorseCodeConverter.ToMorseCode(text);
                Assert.Equal(expectedMorseCode, morseCode);
            }

            // Task: Implement MorseCodeConverter.FromMorseCode
            // Don't care about invalid characters.

            [Theory]
            [InlineData("", "")]
            [InlineData("....|.|.-..|.--.|--|.", "helpme")]
            [InlineData("...|.|-.-.|.-.|.|-|...--|..---|----.|----.|---..|--...", "secret329987")]
            public void FromMorseCode_ForGivenText_ShouldReturnExpected(string morseCode, string expectedText)
            {
                var text = MorseCodeConverter.FromMorseCode(morseCode);
                Assert.Equal(expectedText, text);
            }
        }

        public class MorseCodeTests
        {
            [Theory]
            [InlineData("", "")]
            [InlineData("a", ".-")]
            [InlineData("A", ".-")]
            [InlineData("42", "....-|..---")]
            [InlineData("Keep@it@secret!", "-.-|.|.|.--.|.--.-.|..|-|.--.-.|...|.|-.-.|.-.|.|-|-.-.--")]
            public void Ctor_ForGivenTextAndDefaultDelimiter_ShouldReturnExpectedFromImplicitConversion(string text, string expectedMorseCodeText)
            {

                var morseCode = new MorseCode(text);
                string morseCodeText = morseCode;

                Assert.Equal(expectedMorseCodeText, morseCodeText);
            }

            [Fact]
            public void Ctor_ForGivenTextAndExplicitDelimiter_ShouldReturnExpectedFromImplicitConversion()
            {
                var morseCode = new MorseCode("helpme", "*");
                string morseCodeText = morseCode;
                var expectedMorseCodeText = "....*.*.-..*.--.*--*.";

                Assert.Equal(expectedMorseCodeText, morseCodeText);
            }

            [Fact]
            public void ImplicitConversionFromString_ForGivenString_ShouldReturnExpectedFromImplicitConversion()
            {
                MorseCode morseCode = "helpme";
                string morseCodeText = morseCode;
                var expectedMorseCodeText = "....|.|.-..|.--.|--|.";

                Assert.Equal(expectedMorseCodeText, morseCodeText);
            }
        }

        public class MorseCode
        {
            private readonly string _plainText;
            private static string _morseDelimiter;

            public MorseCode(string plainText, string morseDelimiter = MorseCodeConstants.MorseCodesSeparator)
            {
                _plainText = plainText;
                _morseDelimiter = morseDelimiter;
            }

            // Implement implicit operator for converting from string to MorseCode.
            public static implicit operator MorseCode(string text)
            {
                return new MorseCode(text);
            }

            // Implement implicit operator for converting from MorseCode to string.
            public static implicit operator string(MorseCode morseCode)
            {
                return MorseCodeConverter.ToMorseCode(morseCode._plainText, _morseDelimiter);
            }
        }

        public static class MorseCodeConverter
        {
            private static readonly IDictionary<char, string> _alphaToMorseCodeDictionary;
            private static readonly IDictionary<string, char> _morseCodeToAlphaDictionary;
            private const string _separator = MorseCodeConstants.MorseCodesSeparator;

            static MorseCodeConverter()
            {
                _alphaToMorseCodeDictionary = JsonConvert.DeserializeObject<Dictionary<char, string>>(MorseCodeConstants.MorseAlphabetAsJson);
                _morseCodeToAlphaDictionary = _alphaToMorseCodeDictionary.ToDictionary(x => x.Value, x => x.Key);
            }

            public static string ToMorseCode(string text, string separator = _separator)
            {
                return string.Join(separator, text.Select(@char => _alphaToMorseCodeDictionary[char.ToLower(@char)]));
            }

            public static string FromMorseCode(string morseCode, string separator = _separator)
            {
                var message = morseCode.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);

                return string.Join(string.Empty, message.Select(morseChar => _morseCodeToAlphaDictionary[morseChar]));
            }
        }

        public class MorseCodeConstants
        {
            public const string MorseCodesSeparator = "|";

            public const string MorseAlphabetAsJson =
                @"
{
  ""0"": ""-----"",
  ""1"": "".----"",
  ""2"": ""..---"",
  ""3"": ""...--"",
  ""4"": ""....-"",
  ""5"": ""....."",
  ""6"": ""-...."",
  ""7"": ""--..."",
  ""8"": ""---.."",
  ""9"": ""----."",
  ""a"": "".-"",
  ""b"": ""-..."",
  ""c"": ""-.-."",
  ""d"": ""-.."",
  ""e"": ""."",
  ""f"": ""..-."",
  ""g"": ""--."",
  ""h"": ""...."",
  ""i"": "".."",
  ""j"": "".---"",
  ""k"": ""-.-"",
  ""l"": "".-.."",
  ""m"": ""--"",
  ""n"": ""-."",
  ""o"": ""---"",
  ""p"": "".--."",
  ""q"": ""--.-"",
  ""r"": "".-."",
  ""s"": ""..."",
  ""t"": ""-"",
  ""u"": ""..-"",
  ""v"": ""...-"",
  ""w"": "".--"",
  ""x"": ""-..-"",
  ""y"": ""-.--"",
  ""z"": ""--.."",
  ""."": "".-.-.-"",
  "","": ""--..--"",
  ""?"": ""..--.."",
  ""!"": ""-.-.--"",
  ""-"": ""-....-"",
  ""/"": ""-..-."",
  ""@"": "".--.-."",
  ""("": ""-.--."",
  "")"": ""-.--.-""
}";
        }
    }
}