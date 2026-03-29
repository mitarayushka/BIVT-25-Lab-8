using System;
using System.Linq;

namespace Lab8.White
{
    public class Task2
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double _jump1;
            private double _jump2;

            // Неизменяемое статическое поле (const)
            private const int _standard = 3;

            public static int Standard => _standard;

            // Свойство статуса участника
            public bool IsPassed => _jump1 >= _standard || _jump2 >= _standard;

            // Статический конструктор
            static Participant()
            {
                // Инициализация const происходит автоматически
            }

            // Конструктор
            public Participant(string name, string surname, double jump1, double jump2)
            {
                _name = name;
                _surname = surname;
                _jump1 = jump1;
                _jump2 = jump2;
            }

            // Геттеры для полей
            public string Name => _name;
            public string Surname => _surname;
            public double Jump1 => _jump1;
            public double Jump2 => _jump2;

            // Метод фильтрации прошедших
            public static Participant[] GetPassed(Participant[] participants)
            {
                if (participants == null) return new Participant[0];
                return participants.Where(p => p.IsPassed).ToArray();
            }
        }

        public static void Main(string[] args)
        {
            Participant[] participants = new Participant[] 
            { 
                new Participant("Иван", "Иванов", 2.5, 2.5), 
                new Participant("Петр", "Петров", 3.0, 2.0), 
                new Participant("Сидор", "Сидоров", 4.0, 4.0), 
                new Participant("Алексей", "Алексеев", 2.0, 2.0) 
            };

            Console.WriteLine($"Всего участников: {participants.Length}");

            Participant[] passed = Participant.GetPassed(participants);

            Console.WriteLine($"Прошли норматив: {passed.Length}");
            foreach (var p in passed)
            {
                Console.WriteLine($"{p.Name} {p.Surname} (Лучший: {Math.Max(p.Jump1, p.Jump2)})");
            }
        }
    }
}
