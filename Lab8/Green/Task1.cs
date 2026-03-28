using System;

namespace Lab8.Green
{
    public class Task1
    {
        public abstract class Participant
        {
            protected string _surname;
            protected string _group;
            protected string _trainer;
            protected double _result;
            protected bool _resultCheck;
            protected double _standart;
            protected static int _passed;

            static Participant()
            {
                _passed = 0;
            }

            public Participant(string surname, string group, string trainer)
            {
                _surname = surname;
                _group = group;
                _trainer = trainer;
                _result = 0;
                _resultCheck = false;
            }

            public string Surname
            {
                get
                {
                    if (_surname == null) return string.Empty;
                    return _surname;
                }
            }

            public string Group
            {
                get
                {
                    if (_group == null) return string.Empty;
                    return _group;
                }
            }

            public string Trainer
            {
                get
                {
                    if (_trainer == null) return string.Empty;
                    return _trainer;
                }
            }

            public double Result
            {
                get
                {
                    if (_result == 0) return 0;
                    return _result;
                }
            }

            public static int PassedTheStandard
            {
                get { return _passed; }
            }

            public bool HasPassed
            {
                get
                {
                    if (!_resultCheck) return false;
                    return _result <= _standart;
                }
            }

            public void Run(double result)
            {
                if (_resultCheck == false)
                {
                    _result = result;
                    _resultCheck = true;

                    if (HasPassed)
                    {
                        _passed++;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Фамилия: {_surname}");
                Console.WriteLine($"Группа: {_group}");
                Console.WriteLine($"Тренер: {_trainer}");
                Console.WriteLine($"Результат: {_result}");
                Console.WriteLine($"Прошла норматив: {HasPassed}");
                Console.WriteLine($"Норматив: {_standart}");
                Console.WriteLine($"Всего прошли норматив: {_passed}");
            }

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {

                if (participants == null || participantType == null || trainer == null)
                    return new Participant[0];

                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null &&
                        participants[i].GetType() == participantType &&
                        participants[i].Trainer == trainer)
                    {
                        count++;
                    }
                }

                Participant[] result = new Participant[count];

                int index = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null &&
                        participants[i].GetType() == participantType &&
                        participants[i].Trainer == trainer)
                    {
                        result[index] = participants[i];
                        index++;
                    }
                }

                return result;
            }
        }

        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer)
                : base(surname, group, trainer)
            {
                _standart = 12;
            }
        }

        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer)
                : base(surname, group, trainer)
            {
                _standart = 90;
            }
        }
    }
}
