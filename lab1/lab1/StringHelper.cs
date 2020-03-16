using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    /// <summary>
    /// Класс для работы со строками
    /// </summary>
    public class StringHelper
    {
        public static string NumberInWords(int number)
        {
            return "";
        }

        /// <summary>
        /// Наименования сотен
        /// </summary>
        private static string[] hunds =
        {
            "", "сто ", "двести ", "триста ", "четыреста ", "пятьсот ", "шестьсот ", "семьсот ",
            "восемьсот ", "девятьсот "
        };

        /// <summary>
        /// Наименования дестков
        /// </summary>
        private static string[] tens =
        {
            "", "десять ", "двадцать ", "тридцать ", "сорок ", "пятьдесят ", "шестьдесят ",
            "семьдесят ", "восемьдесят ", "девяносто "
        };

        /// <summary>
        /// Перевод значения числа в пропись с учётом падежного окончания относящегося к числу существительного
        /// </summary>
        /// <param name="val">Число</param>
        /// <param name="male">Род существительного, которое относится к числу</param>
        /// <param name="nounFormOne">Форма существительного в единственном числе</param>
        /// <param name="nounFormTwo">Форма существительного от двух до четырёх</param>
        /// <param name="nounFormFive">Форма существительного от пяти и больше</param>
        /// <returns></returns>
        public static string Str(int val, bool male, string nounFormOne, string nounFormTwo, string nounFormFive)
        {
            string[] numbers =
            {
                "", "один ", "два ", "три ", "четыре ", "пять ", "шесть ",
                "семь ", "восемь ", "девять ", "десять ", "одиннадцать ",
                "двенадцать ", "тринадцать ", "четырнадцать ", "пятнадцать ",
                "шестнадцать ", "семнадцать ", "восемнадцать ", "девятнадцать "
            };

            int num = val % 1000;
            if (0 == num) return "";
            
            if (!male)
            {
                numbers[1] = "одна ";
                numbers[2] = "две ";
            }

            StringBuilder r = new StringBuilder(hunds[num / 100]);

            if (num < 0)
            {
                r.Append("минус ");
                num = Math.Abs(num);
            }

            if (num % 100 < 20)
            {
                r.Append(numbers[num % 100]);
            }
            else
            {
                r.Append(tens[num % 100 / 10]);
                r.Append(numbers[num % 10]);
            }

            r.Append(Case(num, nounFormOne, nounFormTwo, nounFormFive));

            if (r.Length != 0) r.Append(" ");
            return r.ToString();
        }
        /// <summary>
        /// Выбор правильного падежного окончания сущесвительного
        /// </summary>
        /// <param name="val">Число</param>
        /// <param name="nounFormOne">Форма существительного в единственном числе</param>
        /// <param name="nounFormTwo">Форма существительного от двух до четырёх</param>
        /// <param name="nounFormFive">Форма существительного от пяти и больше</param>
        /// <returns>Возвращает существительное с падежным окончанием, которое соответсвует числу</returns>
        public static string Case(int val, string nounFormOne, string nounFormTwo, string nounFormFive)
        {
            int t = (val % 100 > 20) ? val % 10 : val % 20;

            switch (t)
            {
                case 1: return nounFormOne;
                case 2: case 3: case 4: return nounFormTwo;
                default: return nounFormFive;
            }
        }
        /// <summary>
        /// Перевод значения числа в пропись
        /// </summary>
        /// <param name="val">Число</param>
        /// <returns>Возвращает строковую запись числа</returns>
        public static string Str(int val)
        {
            StringBuilder r = new StringBuilder();
            
            if (val < 0) {
                val = -val;
                r.Append("минус ");
            }

            int n = (int)val;

            if (0 == n) r.Append("0 ");
            if (n % 1000 != 0)
                r.Append(StringHelper.Str(n, true, "", "", ""));

            n /= 1000;

            r.Insert(0, StringHelper.Str(n, false, "тысяча", "тысячи", "тысяч"));
            n /= 1000;

            r.Insert(0, StringHelper.Str(n, true, "миллион", "миллиона", "миллионов"));
            n /= 1000;

            r.Insert(0, StringHelper.Str(n, true, "миллиард", "миллиарда", "миллиардов"));
            n /= 1000;

            r.Insert(0, StringHelper.Str(n, true, "триллион", "триллиона", "триллионов"));
            n /= 1000;

            r.Insert(0, StringHelper.Str(n, true, "триллиард", "триллиарда", "триллиардов"));

            r[0] = char.ToUpper(r[0]);

            return r.ToString().Trim();
        }
    }
}
