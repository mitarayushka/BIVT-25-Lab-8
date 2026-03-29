using System;

namespace Lab8.White
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

        // Метод для получения массива прошедших участников
        public static Participant[] GetPassed(Participant[] participants)
        {
            int count = 0;
            foreach (var p in participants)
            {
                if (p.IsPassed) count++;
            }

            Participant[] result = new Participant[count];
            int index = 0;
            foreach (var p in participants)
            {
                if (p.IsPassed) result[index++] = p;
            }
            return result;
        }
    }

    public class Task2
    {
        public static void Main(string[] args)
        {
            Participant[] participants = new Participant[] 
            { 
                new Participant("Иван", "Иванов", 2.5, 2.8), 
                new Participant("Петр", "Петров", 3.1, 2.0), 
                new Participant("Сидор", "Сидоров", 3.5, 3.6) 
            };

            Participant[] passed = Participant.GetPassed(participants);

            Console.WriteLine($"Всего участников: {participants.Length}");
            Console.WriteLine($"Прошли норматив: {passed.Length}");
        }
    }
}
