using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodeDecodeLibrary;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using static EncodeDecodeLibraryTests.TestConstants;

namespace EncodeDecodeLibraryTests
{
    /// <summary>Тестирование функций кодирования строк</summary>
    [TestClass]
    public class MiepStringEncodingTests
    {
        #region Повторяющая часть тестов
        /// <summary>Тест на кодирование строки</summary>
        /// <param name="input">входная строка</param>
        /// <param name="expected">ожидаемая строка</param>
        /// <param name="message">текст сообщения об ошибке</param>
        private void StringEncodingTest(string input, string expected, string message)
        {
            string actual = Library.Encode(input);
            Assert.AreEqual(expected, actual, message);
        }
        #endregion

        #region Сами тесты
        /// <summary>Кодирование латинского алфавита</summary>
        [TestMethod]
        public void EngAlphabetStringEncoding()
        {
            StringEncodingTest(DEC_ENG_ALPHABET, ENC_ENG_ALPHABET, "Encoding of Latin Alphabet (string) decoding is incorrect");
        }

        /// <summary>Кодирование русского алфавита</summary>
        [TestMethod]
        public void RusAlphabetStringEncoding()
        {
            StringEncodingTest(DEC_RUS_ALPHABET, ENC_RUS_ALPHABET, "Encoding of Cyrillic Alphabet (string) decoding is incorrect");
        }

        /// <summary>Кодирование спец. символов</summary>
        [TestMethod]
        public void SpecialSymbolsStringEncoding()
        {
            StringEncodingTest(DEC_SPEC_SYMBOLS, ENC_SPEC_SYMBOLS, "Encoding of Special Symbols (string) decoding is incorrect");
        }

        /// <summary>Кодирование других символов</summary>
        [TestMethod]
        public void OtherSymbolsStringEncoding()
        {
            StringEncodingTest(DEC_OTHER_SYMBOLS, ENC_OTHER_SYMBOLS, "Encoding of Other Symbols (string) decoding is incorrect");
        }
        #endregion
    }
}
