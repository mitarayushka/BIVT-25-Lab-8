using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SeaAnimal animal1 = new SeaAnimal("Sea turtle", "Reptiles");
            Shark shark1 = new Shark("White Shark", "Cartilaginous fish", 4.7);
            animal1.eat();
            shark1.eat();
            shark1.stop();

            SeaAnimal hiddenShark = new Shark("Tiger Shark", "Cartilaginous fish", 3.6);
            hiddenShark.eat();
            hiddenShark.stop();

            SeaAnimal[] BadBand = new SeaAnimal[] { animal1, shark1, hiddenShark};
            for (int i=0; i < BadBand.Length; i++)
            {
                if (BadBand[i] is Shark)
                {
                    Shark s = BadBand[i] as Shark;
                    Console.WriteLine(s.Size);
                }
            }
            for (int i = 0; i < BadBand.Length; i++)
            {
                Shark s = BadBand[i] as Shark;
                //Shark s = (Shark)BadBand[i];
                if (s!=null )
                {
                    Console.WriteLine(s.Weight);
                }
            }
        }
    }

    public class SeaAnimal
    {
        protected string _name;
        protected string _type;
        public string Name => _name;
        public string Type => _type;
        public SeaAnimal(string name, string type)
        {
            _name = name;
            _type = type;
        }
        public virtual void eat()
        {
            Console.WriteLine("amamamamam");
        }
        public void stop()
        {
            Console.WriteLine($"{_name} stops");
        }

    }
    public class Shark: SeaAnimal
    {
        private double _size;
        private double _weight;
        public double Size=> _size;
        public double Weight=>_weight;
        public Shark(string name, string type, double size): base(name, type)
        {
            _name = name;
            _type = type;
            _size = size;
        }
        public Shark(string name, string type, double size,double weight) : this(name, type, size)
        {
            _weight = weight;
        }
        public override void eat()  //Переопределение
        {
            Console.WriteLine("you were eaten by shark amamam");
        }
        public new void stop() // Сокрытие
        {
            Console.WriteLine($"{_name} die");
        }

    }
}
