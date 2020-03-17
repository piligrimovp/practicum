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
                "День недели заданной даты - /day-week?date=17.03.2020",
                "Число Фибоначчи - /fibonacci/1",
                "Название региона - /region/72",
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
        public string GetQuadraticEquation(double a, double b, double c = 0)
        {
            var Roots = QuadraticEquation.Roots(a, b, c);
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

        /// <summary>
        /// Возвращает день недели по заданной дате
        /// </summary>
        /// <param name="date">дата в формате день.месяц.год</param>
        /// <returns></returns>
        [Route("day-week")]
        public ActionResult<string> GetDayOfWeek(string date)
        {
            string[] days =
            {
                "Воскресенье",
                "Понедельник",
                "Вторник",
                "Среда",
                "Четверг",
                "Пятница",
                "Суббота",
            };
            if(DateTime.TryParse(date, out DateTime dt))
            {
                return JsonConvert.SerializeObject(days[(int)dt.DayOfWeek]);
            }
            return JsonConvert.SerializeObject("Некорректная дата");
            
        }

        /// <summary>
        /// Возвращает число Фибоначчи
        /// </summary>
        /// <param name="index">порядковый номер числа</param>
        /// <returns></returns>
        [Route("fibonacci/{index}")]
        public string GetFibonacci(int index)
        {
            bool neg = false;
            if(index < 0)
            {
                neg = true;
                index = -index;
            }
           
            double a = Math.Pow((1 + Math.Sqrt(5)) / 2, index);
            double b = Math.Pow((1 - Math.Sqrt(5)) / 2, index);

            double answer = (a - b) / Math.Sqrt(5);

            if(neg && index % 2 == 0)
            {
                answer = -answer;
            }

            return JsonConvert.SerializeObject(Math.Round(answer));
        }

        /// <summary>
        /// Возвращает название региона
        /// </summary>
        /// <param name="number">Код региона</param>
        /// <returns></returns>
        [Route("region/{number}")]
        public ActionResult<string> GetNameRegion(int number)
        {
            if(number < 0 || number >= Regions.regions.Length || Regions.regions[number] == null)
            {
                return BadRequest(JsonConvert.SerializeObject("Регион не найден"));
            }
            return Content(JsonConvert.SerializeObject(Regions.regions[number]));
        }

    }
}
