using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp25
{
    class Program
    {
        static void Main(string[] args)
        {
            // Обобщения 
            
            Human human = new Human { Name = "Эльвира" };
            string result =  human.Serialize();
            Console.WriteLine(result);
            Animal<bool> animal = new Animal<bool> 
            { Age = 10, Name = "Бобик",
                GenericProperty = true };
            Console.WriteLine(animal.Serialize());
        }
    }
    
    public class Human
    { 
        public string Name { get; set; }
        private double Weight = 10;
    }

    public class Animal<T>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public T GenericProperty { get; set; }
    }

    public static class Extension
    {
        public static string Serialize<T>(this T obj)
        {
            string result = null;
            DataContractJsonSerializer jsonSerializer =
                   new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream())
            using (var br = new BinaryReader(ms))
            {
                jsonSerializer.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                result = Encoding.UTF8.GetString(br.ReadBytes((int)ms.Length));
            }
            return result;
        }
    }
}
