using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodeDecodeLibrary;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using static EncodeDecodeLibraryTests.TestConstants;

namespace EncodeDecodeLibraryTests
{
    /// <summary>Тестирование функций декодирования строк</summary>
    [TestClass]
    public class MiepStringDecodingTests
    {
        #region Повторяющая часть тестов
        /// <summary>Тест на декодирование строки</summary>
        /// <param name="input">входная строка</param>
        /// <param name="expected">ожидаемая строка</param>
        /// <param name="message">текст сообщения об ошибке</param>
        private void StringDecodingTest(string input, string expected, string message)
        {
            string actual = Library.Decode(input);
            Assert.AreEqual(expected, actual, message);
        }
        #endregion

        #region Сами тесты
        /// <summary>Декодирование латинского алфавита</summary>
        [TestMethod]
        public void EngAlphabetStringDecoding()
        {
            StringDecodingTest(ENC_ENG_ALPHABET, DEC_ENG_ALPHABET, "Latin Alphabet (string) decoding is incorrect");
        }

        /// <summary>Декодирование русского алфавита</summary>
        [TestMethod]
        public void RusAlphabetStringDecoding()
        {
            StringDecodingTest(ENC_RUS_ALPHABET, DEC_RUS_ALPHABET, "Cyrillic Alphabet (string) decoding is incorrect");
        }

        /// <summary>Декодирование спец. символов</summary>
        [TestMethod]
        public void SpecialSymbolsStringDecoding()
        {
            StringDecodingTest(ENC_SPEC_SYMBOLS, DEC_SPEC_SYMBOLS, "Special Symbols (string) decoding is incorrect");
        }

        /// <summary>Декодирование других символов</summary>
        [TestMethod]
        public void OtherSymbolsStringDecoding()
        {
            StringDecodingTest(ENC_OTHER_SYMBOLS, DEC_OTHER_SYMBOLS, "Other Symbols (string) decoding is incorrect");
        }

        /// <summary>Декодирование строки с неверным количеством символов</summary>
        [TestMethod]
        public void WrongSizeStringDecoding()
        {
            StringDecodingTest(ENC_WRONG_SIZE_STRING, DEC_WRONG_SIZE_STRING, "Wrong Size String (string) decoding is incorrect");
        }

        /// <summary>Декодирование неформатной строки</summary>
        [TestMethod]
        public void WrongFormatStringDecoding()
        {
            StringDecodingTest(ENC_WRONG_FORMAT_STRING, DEC_WRONG_FORMAT_STRING, "Wrong Format String (string) decoding is incorrect");
        }

        /// <summary>Декодирование строки с неверным кодом в одном символе</summary>
        [TestMethod]
        public void WrongCodeStringDecoding()
        {
            StringDecodingTest(ENC_WRONG_CODE_STRING, DEC_WRONG_CODE_STRING, "Wrong Format String (string) decoding is incorrect");
        }

        /// <summary>Декодирование пустой строки</summary>
        [TestMethod]
        public void EmptyStringDecoding()
        {
            StringDecodingTest("", "", "Wrong Format String (string) decoding is incorrect");
        }
        #endregion
    }
}
