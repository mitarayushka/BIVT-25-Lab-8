using System;

namespace Lab8.White
{
    public class Task4
    {
        public class Human
        {
            private string _name;
            private string _surname;

            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}");
            }
        }

        public class Participant : Human
        {
            private static int _count;
            private int[] _jumps;

            static Participant()
            {
                _count = 0;
            }

            public Participant(string name, string surname, int[] jumps) : base(name, surname)
            {
                _jumps = jumps;
                _count++;
            }

            public static int Count => _count;

            public override void Print()
            {
                base.Print();
                Console.WriteLine($"Прыжки: {string.Join(", ", _jumps)}");
            }
        }

        public static void Main(string[] args)
        {
            Participant p1 = new Participant("Иван", "Иванов", new int[] { 5, 6 });
            Participant p2 = new Participant("Петр", "Петров", new int[] { 4, 5 });

            p1.Print();
            p2.Print();

            Console.WriteLine($"Всего участников: {Participant.Count}");
        }
    }
}
