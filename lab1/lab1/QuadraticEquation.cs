using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab1
{
    /// <summary>
    /// Квадратное уравнение
    /// </summary>
    public class QuadraticEquation
    {
        /// <summary>
        /// Нахождение корней квадратного уравнения
        /// </summary>
        /// <param name="a">Коэффициент x^2</param>
        /// <param name="b">Коэффициент x</param>
        /// <param name="c">Свободный коэффициент</param>
        /// <returns>Корни квадратного уравнения</returns>
        public static object Roots(double a, double b, double c)
        {
            double[] answer = null;
            double D = b * b - 4 * a * c;

            if(D >= 0)
            {
                answer = new double[2]
                {
                    (-b + Math.Sqrt(D)) / (2 * a),
                    (-b - Math.Sqrt(D)) / (2 * a),
                };
            }
            
            return answer;
        }
    }
}
