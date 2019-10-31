using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodeDecodeLibrary;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using static EncodeDecodeLibraryTests.TestConstants;

namespace EncodeDecodeLibraryTests
{
    /// <summary>Тестирование функций кодирования SQL-строк</summary>
    [TestClass]
    public class MiepSqlStringEncodingTests
    {
        #region Повторяющая часть тестов
        /// <summary>Тест на кодирование SQL-строки</summary>
        /// <param name="input">входная SQL-строка</param>
        /// <param name="expected">ожидаемая SQL-строка</param>
        /// <param name="message">текст сообщения об ошибке</param>
        private void SqlStringEncodingTest(SqlString input, SqlString expected, string message)
        {
            SqlString actual = Library.Encode(input);
            Assert.AreEqual(expected, actual, message);
        }
        #endregion

        #region Сами тесты
        /// <summary>Кодирование латинского алфавита</summary>
        [TestMethod]
        public void EngAlphabetSqlStringEncoding()
        {
            SqlStringEncodingTest(new SqlString(DEC_ENG_ALPHABET),
                                  new SqlString(ENC_ENG_ALPHABET),
                                  "Encoding of Latin Alphabet (SQL-string) decoding is incorrect");
        }

        /// <summary>Кодирование русского алфавита</summary>
        [TestMethod]
        public void RusAlphabetSqlStringEncoding()
        {
            SqlStringEncodingTest(new SqlString(DEC_RUS_ALPHABET),
                                  new SqlString(ENC_RUS_ALPHABET),
                                 "Encoding of  Cyrillic Alphabet (SQL-string) decoding is incorrect");
        }

        /// <summary>Кодирование спец. символов</summary>
        [TestMethod]
        public void SpecialSymbolsSqlStringEncoding()
        {
            SqlStringEncodingTest(new SqlString(DEC_SPEC_SYMBOLS),
                                  new SqlString(ENC_SPEC_SYMBOLS),
                                  "Encoding of Special Symbols (SQL-string) decoding is incorrect");
        }

        /// <summary>Кодирование других символов</summary>
        [TestMethod]
        public void OtherSymbolsSqlStringEncoding()
        {
            SqlStringEncodingTest(new SqlString(DEC_OTHER_SYMBOLS),
                                  new SqlString(ENC_OTHER_SYMBOLS),
                                  "Encoding of Other Symbols (SQL-string) decoding is incorrect");
        }
        #endregion
    }
}
