using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncodeDecodeLibrary
{
    /// <summary>Двунаправленный словарь</summary>
    public class BidirectionalDictionary
    {
        #region закрытые поля для хранения данных
        /// <summary>список </summary>
        private Dictionary<int, char> values;
        private Dictionary<char, int> keys;
        #endregion

        #region конструктор и инициализация
        public BidirectionalDictionary(Dictionary<int,char> dictionary)
        {
            Init(dictionary);
        }

        /// <summary>Инициализирует двунаправленный словарь, задавая начальные значения в виде словаря</summary>
        /// <param name="dictionary">словарь начальных значений</param>
        private void Init(Dictionary<int,char> dictionary)
        {
            values = dictionary;
            keys = new Dictionary<char, int>();
            values.ToList().ForEach(item => keys.Add(item.Value, item.Key));
        }
        #endregion

        #region свойства для доступа к данным
        /// <summary>Возвращает значение в виде символа по ключу</summary>
        /// <param name="key">ключ</param>
        /// <param name="def">что вернуть, если нет значений стаким ключом (по умолчанию null)</param>
        /// <returns>значение в виде строки</returns>
        public char? Value(int key, char? def=null)
        {
            if (values.TryGetValue(key, out char result))
                return result;
            else
                return def;
        }

        /// <summary>Возвращает значение в виде строки по ключу</summary>
        /// <param name="key">ключ</param>
        /// <param name="def">что вернуть, если нет значений стаким ключом (по умолчанию пустая строка)</param>
        /// <returns>значение в виде строки</returns>
        public string StrVal(int key, string def = "") => Value(key)?.ToString() ?? def;

        /// <summary>Возвращает ключ по значению в виде символа</summary>
        /// <param name="value">значение</param>
        /// <param name="def">что вернуть, если значение не найдено (по умолчанию null)</param>
        /// <returns>ключ</returns>
        public int? Key(char value, int? def = null)
        {
            if (keys.TryGetValue(value, out int result))
                return result;
            else
                return def;
        }

        /// <summary>Возвращает ключ по значению в виде строки</summary>
        /// <param name="value">значение</param>
        /// <param name="def">что вернуть, если значение не найдено (по умолчанию null)</param>
        /// <returns>ключ</returns>
        public int? Key(string value, int? def = null)
        {
            if (value.Length == 0)
                return def;
            else
                return Key(value[0]);
        }
        #endregion

    }
}
