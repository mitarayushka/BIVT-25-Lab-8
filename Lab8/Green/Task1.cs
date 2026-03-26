namespace Lab8.Green
{
    public class Task1
    {
        public abstract class Participant
        {
            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;
            private bool _runCalled;

            protected double _standard;
            private static int _passed;

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
                _runCalled = false;
            }

            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;

            public double Result => _result;

            public static int PassedTheStandard => _passed;

            public bool HasPassed
            {
                get
                {
                    if (!_runCalled) return false;
                    return _result <= _standard;
                }
            }

            public void Run(double result)
            {
                if (_runCalled) return;
                _result = result;
                _runCalled = true;
                if (_result <= _standard)
                {
                    _passed++;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Участница: {Surname}, группа: {Group}, тренер: {Trainer}, результат: {Result}, прошла: {HasPassed}");
            }

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                if (participants == null) return new Participant[0];

                int count = 0;
                foreach (var p in participants)
                {
                    if (p != null && p.GetType() == participantType && p.Trainer == trainer) count++;
                }

                var result = new Participant[count];
                int ind = 0;
                foreach (var p in participants)
                {
                    if (p != null && p.GetType() == participantType && p.Trainer == trainer)
                        result[ind++] = p;
                }

                return result;
            }
        }

        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 12;
            }
        }

        public class Participant500M: Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 90;
            }
        }
    }
}
