namespace KGPhoneHelper
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class PhoneHelper
    {
        private static readonly string _basekod = "996";

        /// <summary>
        /// Коды оператора O!
        /// </summary>
        public static readonly List<string> Okods = new List<string>
        {
            "700","701","702","703","704","705","706","707","708","709",
            "500","501","502","503","504","505","506","507","508","509"
        };

        /// <summary>
        /// Коды оператора Megacom
        /// </summary>
        public static readonly List<string> Megacomkods = new List<string>
        {
            "550", "551", "552", "553", "554", "555", "556", "557", "558", "559"
        };

        /// <summary>
        /// Коды оператора Beeline
        /// </summary>
        public static readonly List<string> Beelinekods = new List<string>
        {
            "770", "771", "772", "773", "774", "775", "776", "777", "778", "779"
        };

        /// <summary>
        /// Возвращает Оператора которому принадлежит телефон по коду если передан валидный номер иначе неопределенного опереатора
        /// </summary>
        /// <param name="phone">Телефон</param>
        /// <returns></returns>
        public static Operator GetOperatorByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return Operator.Unknown;

            phone = GetFormatedPhone(phone);

            if (string.IsNullOrWhiteSpace(phone)) return Operator.Unknown;

            var operatorKod = phone.Substring(3, 3);

            if (Okods.Contains(operatorKod)) return Operator.O;
            if (Megacomkods.Contains(operatorKod)) return Operator.Megacom;
            if (Beelinekods.Contains(operatorKod)) return Operator.Beeline;

            return Operator.Unknown;
        }

        /// <summary>
        /// Возвращает телефон в формате 996555123456 если передан валидный номер иначе пустую строку
        /// </summary>
        /// <param name="phone">Телефон</param>
        /// <returns></returns>
        public static string GetFormatedPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;
            phone = new Regex("\\D").Replace(phone, "");

            if (phone.StartsWith("+") && phone.Length == 13)
                return phone.Substring(1);
            if (phone.StartsWith(_basekod) && phone.Length == 12)
                return phone;
            if (phone.StartsWith("0") && phone.Length == 10)
                return phone.Substring(1).Insert(0, _basekod);
            if (phone.Length == 9 && phone.StartsWith("7") || phone.StartsWith("5"))
                return phone.Insert(0, phone);
            return string.Empty;
        }

        /// <summary>
        /// Проверяет переданный телефон на соответствие формату 996555123456
        /// </summary>
        /// <param name="phone">Телефон</param>
        /// <returns> </returns>
        public static bool IsFormatedPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return GetFormatedPhone(phone) != string.Empty;
        }

    }
}
