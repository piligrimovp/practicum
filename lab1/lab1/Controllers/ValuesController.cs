using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace lab1.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Route("/")]
        public string Get()
        {
            string[] map =
            {
                "Число прописью - /ntw/123",
                "Решение квадратного уравнения - /equation?a=1&b=0&c=0",
            };
            return JsonConvert.SerializeObject(map);
        }

        /// <example>/ntw/123</example>
        /// <summary>
        /// Перевод значения числа в пропись
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Значение number прописью</returns>
        [Route("ntw/{number}")]
        public string GetNumberToWord(int number)
        {
            return JsonConvert.SerializeObject(StringHelper.Str(number));
        }

        ///<example>/equation?a=1&b=0&c=0</example>
        /// <summary>
        /// Решениие квадратного уравнения
        /// </summary>
        /// <param name="a">Коэффициент x^2</param>
        /// <param name="b">Коэффициент x</param>
        /// <param name="c">Свободный коэффициент</param>
        /// <returns>Корни квадратного уравнения</returns>
        [Route("equation")]
        public string GetQuadraticEquation(string a, string b, string c)
        {
            string answerFail = "Параметры не являются числами";
            double _a, _b, _c;
            if (double.TryParse(a, out _a)) {}
            else
            {
                return JsonConvert.SerializeObject(answerFail);
            }
            if (double.TryParse(b, out _b)) {}
            else
            {
                return JsonConvert.SerializeObject(answerFail);
            }
            if (double.TryParse(c, out _c)) {}
            else
            {
                _c = 0;
            }

            var Roots = QuadraticEquation.Roots(_a, _b, _c);
            string answer = "";
            if(Roots == null)
            {
                answer = JsonConvert.SerializeObject("Действительных корней нет");
            } else
            {
                answer = JsonConvert.SerializeObject(Roots);
            }
            return answer;
        }

    }
}
