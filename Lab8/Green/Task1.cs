using System.Collections.Specialized;
using System.Xml.Linq;

namespace Lab8.Green
{
    public class Task1
    {
        public abstract class Participant
        {
            // Поля
            protected string _surname;
            protected string _group;
            protected string _trainer;
            protected double _result;
            protected double _norma;
            protected static int _passed;

            // Свойства
            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;
            public static int PassedTheStandard => _passed;
            public bool HasPassed
            {
                get
                {
                    if (_result == 0)
                        return false;
                    else
                    {
                        if (_result <= _norma)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            
            // Конструктор
            public Participant(string surname, string group, string trainer)
            {
                _surname = surname;
                _group = group;
                _trainer = trainer;
                _result = 0;
            }
            protected abstract void SetNorma();

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                if (participants == null || trainer == null)
                    return new Participant[0];
                int count = 0;
                // Считаем, сколько подходящих участников
                for (int i = 0; i < participants.Length; i++)
                {
                    Participant p = participants[i];
                    if (p != null)
                    {
                        if (p.GetType() == participantType && p.Trainer == trainer)
                        {
                            count++;
                        }
                    }
                }
                Participant[] result = new Participant[count];
                int ind = 0;
                for (int i = 0; i <  participants.Length; i++)
                {
                    Participant p = participants[i];
                    if (p != null)
                    {
                        if (p.GetType() == participantType && p.Trainer == trainer)
                        {
                            result[ind] = p;
                            ind++;
                        }
                    }
                }
                return result;
            } 
            public void Run(double result)
            {
                if (_result == 0)
                {
                    _result = result;
                    if (result <= _norma)
                    {
                        _passed++;
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"Фамилия: {_surname}");
                Console.WriteLine($"Группа: {_group}");
                Console.WriteLine($"Тренер: {_trainer}");
                if (_result == 0)
                {
                    Console.WriteLine("Резултат: ещё не прошел");
                }
                else
                {
                    Console.WriteLine($"Результат: {_result} сек");
                    Console.WriteLine($"Норматив: {(_result <= _norma ? "прошел" : "не прошел")}");
                }
                Console.WriteLine();
            }
        }
        // Класс наследник для забега на 100 метров
        public class Participant100M : Participant
        {
            // Конструктор
            public Participant100M(string name, string group, string trainer) : base(name, group, trainer)
            {
                SetNorma();
            }
            protected override void SetNorma()
            {
                _norma = 12;
            }
            // Добавляем информацию о дистанции
            public override void Print()
            {
                Console.WriteLine("=== Забег на 100 метров ===");
                base.Print();
            }
        }
        // Класс-наследник для забега на 500 метров
        public class Participant500M : Participant
        {
            // Конструктор
            public Participant500M(string name, string group, string trainer) : base(name, group, trainer)
            {
                SetNorma();
            }
            protected override void SetNorma()
            {
                _norma = 90;
            }
            public override void Print()
            {
                Console.WriteLine("=== Забег на 500 метров ===");
                base.Print();
            }
        }
    }
}
