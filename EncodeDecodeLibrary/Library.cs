using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace EncodeDecodeLibrary
{
    /// <summary>Содержит методы для работы с базой Спрут МИЭП</summary>
    public static class Library
    {
        #region Поля и Свойства
        /// <summary>Словарь для кодирования/декодирования</summary>
        private static BidirectionalDictionary Secret { get; set; }
        #endregion

        #region Статический конструктор (содержит ключ)
        static Library()
        {
            Secret = new BidirectionalDictionary(new Dictionary<int, char>
                {
                    // В данном случае при всех предполагаемых развитиях событий словарь
                    // - не представляет ценности, т.к. подбор значений будет быстрее их извлечения из библиотеки
                    // - не будет меняться без полной переработки клиентского ПО (и вероятность такой переработки близка к нулю)
                    [0] = '-', [2] = ' ', [3] = '(', [4] = ')', [5] = 'A', [6] = 'B', [7] = 'C', [8] = 'D', [9] = 'E', [10] = 'F', [11] = 'G', [12] = 'H',
                    [13] = 'I', [14] = 'J', [15] = 'K', [16] = 'L', [17] = 'M', [18] = 'N', [19] = 'O', [20] = 'P', [21] = 'Q', [22] = 'R', [23] = 'S', [25] = 'T',
                    [30] = 'U', [32] = 'V', [33] = 'W', [36] = 'X', [40] = 'Y', [54] = 'Z', [89] = 'А', [94] = 'Б', [95] = 'В', [96] = 'Г', [97] = 'Д', [99] = 'Е',
                    [101] = 'Ё', [102] = 'Ж', [104] = 'З', [105] = 'И', [106] = 'Й', [107] = 'К', [109] = 'Л', [110] = 'М', [111] = 'Н', [112] = 'О', [113] = 'П', [115] = 'Р',
                    [271] = 'С', [245] = 'Т', [244] = 'У', [248] = 'Ф', [273] = 'Х', [267] = 'Ц', [262] = 'Ч', [252] = 'Ш', [272] = 'Щ', [255] = 'Ъ', [259] = 'Ы', [275] = 'Ь',
                    [243] = 'Э', [241] = 'Ю', [269] = 'Я', [315] = 'a', [328] = 'b', [326] = 'c', [317] = 'd', [307] = 'e', [318] = 'f', [319] = 'g', [320] = 'h',
                    [312] = 'i', [321] = 'j', [322] = 'k', [323] = 'l', [330] = 'm', [329] = 'n', [313] = 'o', [314] = 'p', [305] = 'q', [308] = 'r', [316] = 's',
                    [309] = 't', [311] = 'u', [327] = 'v', [306] = 'w', [325] = 'x', [310] = 'y', [324] = 'z', [203] = 'а', [208] = 'б', [210] = 'в', [237] = 'г', [212] = 'д',
                    [216] = 'е', [218] = 'ё', [219] = 'ж', [222] = 'з', [201] = 'и', [225] = 'й', [227] = 'к', [220] = 'л', [224] = 'м', [229] = 'н', [239] = 'о', [232] = 'п',
                    [214] = 'р', [233] = 'с', [207] = 'т', [206] = 'у', [209] = 'ф', [235] = 'х', [228] = 'ц', [223] = 'ч', [213] = 'ш', [234] = 'щ', [217] = 'ъ', [221] = 'ы',
                    [236] = 'ь', [205] = 'э', [202] = 'ю', [230] = 'я'
                }
            );
        }
        #endregion

        #region Публичные методы SQL (для подключения в SQL Server)

        #region декодирование SQL строки
        /// <summary>Декодирует SQL-строку из трехсимвольной кодировки Спрут</summary>
        /// <param name="sqlSeqStr">SQL-строка в трехсимвольной кодировке</param>
        /// <returns>Раскодированная SQL-строка</returns>
        [SqlFunction(
            DataAccess = DataAccessKind.None,
            IsDeterministic=true,
            IsPrecise = true)]
        public static SqlString Decode(SqlString sqlSeqStr)
        {
            return new SqlString(Library.Decode(sqlSeqStr.ToString()));
        }
        #endregion

        #region кодирование SQL строки
        /// <summary>Кодирует SQL-строку в трехсимвольную кодировку Спрут</summary>
        /// <param name="sqlSeqStr">SQL-строка в трехсимвольной кодировке</param>
        /// <returns>Закодированная SQL-строка</returns>
        [SqlFunction(
            DataAccess = DataAccessKind.None,
            IsDeterministic = true,
            IsPrecise = true)
        ]
        public static SqlString Encode(SqlString sqlSeqStr)
        {
            return new SqlString(Library.Encode(sqlSeqStr.ToString()));
        }
        #endregion

        #region преобразование закодированной SQL строки в нижний регистр
        /// <summary>Преобразует закодированную SQL-строку в нижний регистр</summary>
        /// <param name="sqlSeqStr">SQL-строка в трехсимвольной кодировке</param>
        /// <returns>Закодированная SQL-строка в нижнем регистре</returns>
        [SqlFunction(
            DataAccess = DataAccessKind.None,
            IsDeterministic = true,
            IsPrecise = true)
        ]
        public static SqlString EncodedToLower(SqlString sqlSeqStr)
        {
            return new SqlString(
                Library.Encode(
                    Library.Decode(
                        sqlSeqStr.ToString()
                    ).ToLower()
                )
            );
        }
        #endregion

        #endregion

        #region Остальные публичные методы

        #region декодирование
        /// <summary>Декодирует строку по словарю</summary>
        /// <param name="seqStr">Строка для декодирования</param>
        /// <returns>Декодированная строка</returns>
        public static string Decode(string seqStr)
        {
            if ((seqStr.Length > 0) && (seqStr.Length % 3 == 0))
            {
                #region Объявляем объект, в котором мы будем собирать строку посимвольно
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                #endregion
                #region Декодируем строку, разбивая на трехсимвольные блоки
                for (int i = 0; i < seqStr.Length; i += 3)
                {
                    try
                    {
                        sb.Append(DecodeSymbol(seqStr.Substring(i, 3)));
                    }
                    catch
                    {
                        return seqStr;
                    }
                }
                #endregion
                return sb.ToString();
            }
            else
            {
                return seqStr;
            }
        }

        /// <summary>Декодирует символ из трехсимвольной последовательности</summary>
        /// <param name="seqShr">Трехсимвольная последовательность</param>
        /// <returns>Строка результата</returns>
        public static char DecodeSymbol(string seqShr)
        {
            ;
            if (seqShr.Length == 3)
            {
                #region экранированные пробелами символы сохраняем как есть
                if (seqShr[0] == ' ' && seqShr[2] == ' ')
                {
                    return seqShr[1];
                }
                #endregion
                #region трехсимвольные блоки из трех цифр преобразуем в символы по словарю
                else if (char.IsDigit(seqShr[0]) && char.IsDigit(seqShr[1]) && char.IsDigit(seqShr[2]))
                {
                    char? tmp = Library.Secret.Value(short.Parse(seqShr));
                    if (tmp != null)
                        return tmp ?? '!';
                    else
                        throw new ArgumentOutOfRangeException();
                }
                #endregion
                #region иначе ругаемся на неверный формат
                else
                {
                    throw new ArgumentException(message: "Supports ' c ' or 'ddd' format, where 'c' is any charecter and 'd' is any digit!");
                }
                #endregion
            }
            else
            {
                #region иначе ругаемся на неверную длину
                throw new ArgumentException(message: "Works with strings 3 characters long only!");
                #endregion
            }

        }
        #endregion

        #region кодирование
        /// <summary>Кодирует строку по словарю</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            #region Объявляем объект, в котором мы будем собирать строку посимвольно
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            #endregion

            str.ToList().ForEach(chr => sb.Append(Library.EncodeSymbol(chr)));
            return sb.ToString();
        }

        /// <summary>Кодирует символ в трохсимвльную последовательность по словарю</summary>
        /// <param name="chr">символ для кодирования</param>
        /// <returns>Строка с закодированным символом</returns>
        public static string EncodeSymbol(char chr)
        {
            int? code = Library.Secret.Key(chr);
            if (code != null)
                return String.Format("{0:d3}", code);
            else
                return $" {chr} ";
        }
        #endregion

        #endregion

    }
}
