namespace EncodeDecodeLibraryTests
{
    /// <summary>Константы, используемые в тестах</summary>
    public static class TestConstants
    {
        public const string DEC_RUS_ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string ENC_RUS_ALPHABET = "315328326317307318319320312321322323330329313314305308316309311327306325310324005006007008009010011012013014015016017018019020021022023025030032033036040054";
        public const string DEC_ENG_ALPHABET = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫБЭЮЯ";
        public const string ENC_ENG_ALPHABET = "203208210237212216218219222201225227220224229239232214233207206209235228223213234217221236205202230089094095096097099101102104105106107109110111112113115271245244248273267262252272255259094243241269";
        public const string DEC_SPEC_SYMBOLS = "()-";
        public const string ENC_SPEC_SYMBOLS = "003004000";
        public const string DEC_OTHER_SYMBOLS = "_";
        public const string ENC_OTHER_SYMBOLS = " _ ";
        public const string ENC_WRONG_SIZE_STRING = "1311";
        public const string DEC_WRONG_SIZE_STRING = "1311";
        public const string ENC_WRONG_FORMAT_STRING = "131 02";
        public const string DEC_WRONG_FORMAT_STRING = "131 02";
        public const string ENC_WRONG_CODE_STRING = "131000";
        public const string DEC_WRONG_CODE_STRING = "131000";
    }
}
