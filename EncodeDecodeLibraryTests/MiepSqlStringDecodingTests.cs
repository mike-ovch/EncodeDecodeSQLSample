using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodeDecodeLibrary;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using static EncodeDecodeLibraryTests.TestConstants;

namespace EncodeDecodeLibraryTests
{
    /// <summary>Тестирование функций декодирования SQL-строк</summary>
    [TestClass]
    public class MiepSqlStringDecodingTests
    {
        #region Повторяющая часть тестов
        /// <summary>Тест на декодирование SQL-строки</summary>
        /// <param name="input">входная SQL-строка</param>
        /// <param name="expected">ожидаемая SQL-строка</param>
        /// <param name="message">текст сообщения об ошибке</param>
        private void SqlStringDecodingTest(SqlString input, SqlString expected, string message)
        {
            SqlString actual = Library.Decode(input);
            Assert.AreEqual(expected, actual, message);
        }
        #endregion

        #region Сами тесты
        /// <summary>Декодирование латинского алфавита</summary>
        [TestMethod]
        public void EngAlphabetSqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(ENC_ENG_ALPHABET),
                                  new SqlString(DEC_ENG_ALPHABET),
                                  "Decoding of Latin Alphabet (SQL-string) decoding is incorrect");
        }

        /// <summary>Декодирование русского алфавита</summary>
        [TestMethod]
        public void RusAlphabetSqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(ENC_RUS_ALPHABET),
                                  new SqlString(DEC_RUS_ALPHABET),
                                  "Decoding of Cyrillic Alphabet (SQL-string) decoding is incorrect");
        }

        /// <summary>Декодирование спец. символов</summary>
        [TestMethod]
        public void SpecialSymbolsSqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(ENC_SPEC_SYMBOLS),
                                  new SqlString(DEC_SPEC_SYMBOLS),
                                  "Decoding of Special Symbols (SQL-string) decoding is incorrect");
        }

        /// <summary>Декодирование других символов</summary>
        [TestMethod]
        public void OtherSymbolsSqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(ENC_OTHER_SYMBOLS),
                                  new SqlString(DEC_OTHER_SYMBOLS),
                                  "Decoding of Other Symbols (SQL-string) decoding is incorrect");
        }


        /// <summary>Декодирование строки с неверным количеством символов</summary>
        [TestMethod]
        public void WrongSizeSqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(ENC_WRONG_SIZE_STRING),
                                  new SqlString(DEC_WRONG_SIZE_STRING),
                                  "Wrong Size String (SQL-string) decoding is incorrect");
        }

        /// <summary>Декодирование неформатной строки</summary>
        [TestMethod]
        public void WrongFormatSqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(ENC_WRONG_FORMAT_STRING),
                                  new SqlString(DEC_WRONG_FORMAT_STRING),
                                  "Wrong Format String (string) decoding is incorrect");
        }

        /// <summary>Декодирование строки с неверным кодом в одном символе</summary>
        [TestMethod]
        public void WrongCodeSqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(ENC_WRONG_CODE_STRING),
                                  new SqlString(DEC_WRONG_CODE_STRING), 
                                  "Wrong Format String (string) decoding is incorrect");
        }

        /// <summary>Декодирование пустой строки</summary>
        [TestMethod]
        public void EmptySqlStringDecoding()
        {
            SqlStringDecodingTest(new SqlString(""),
                                  new SqlString(""),
                                  "Wrong Format String (string) decoding is incorrect");
        }
        #endregion
    }
}
