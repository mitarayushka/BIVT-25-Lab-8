namespace Lab8.Green
{
    public class Task1
    {
        public abstract class Participant
        {
            private string _lastname;
            private string _group;
            private string _couchlm;
            private double _result;
            protected int _norm;
            private static int _passed;
            public static int PassedTheStandard => _passed;
            public string Surname => _lastname;
            public string Group => _group;
            public string Trainer => _couchlm;
            public double Result => _result;
            static Participant()
            {
                
                _passed = 0;
            }
            public bool HasPassed
            {
                get
                {
                    if (_result <= _norm)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public Participant(string name, string group, string trainer)
            {
                _lastname = name;
                _group = group;
                _couchlm = trainer;
                _result = 0;
            }
            
            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                if (participants == null)
                {
                    return new Participant[0];
                }
                var a = new Participant[0];
                foreach(var f in participants)
                {
                    if (f.GetType()==participantType && f.Trainer == trainer)
                    {
                        Array.Resize(ref a, a.Length + 1);
                        a[a.Length-1] = f;
                    }
                }
                return a;
            }
            public void Run(double result)
            {
                if (_result > 0)
                {
                    return;
                }
                _result = result;
                if (result <= _norm)
                {
                    _passed++;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Фамилия: {_lastname}");
                Console.WriteLine($"Группа: {_group}");
                Console.WriteLine($"Тренер: {_couchlm}");
                Console.WriteLine($"Результат:{_result}");
                Console.WriteLine($"Норматив: {_norm} сек");


            }
        }
        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _norm = 12;
            }
        }

        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _norm = 90;
            }
        }
    }
}
