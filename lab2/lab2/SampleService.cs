using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using lab2.Models;

namespace lab2
{
    public class SampleService : ISampleService
    {
        public string Test(string s)
        {
            Console.WriteLine("Тест метода");
            return s;
        }

        public Client TestClient(Client inputClient)
        {
            return inputClient;
        }

        public void XmlMethod(XElement xml)
        {
            Console.WriteLine(xml.ToString());
        }
    }
}
