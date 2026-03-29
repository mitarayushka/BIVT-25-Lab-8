namespace Lab8.Green
{
    public class Task1
    {
        public abstract class Participant
        {
            //поля
            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;
            private bool _hasPassed;
            protected double STANDARD_RESULT;
            private static int _passed;


            //свойства
            public string Surname
            {
                get { return _surname; } // только для чтения
            }
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;

            public bool HasPassed => (_result > 0 && _result <= STANDARD_RESULT);
            //
            public static int PassedTheStandard => _passed;

            //Конструктор

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
                _hasPassed = false;

            }
            public void Run(double result)
            {
                if (_result == 0 && result > 0)
                {
                    _result = result;
                    if (result <= STANDARD_RESULT)
                        _passed++;
                }
            }

            public void Print()
            {
                Console.WriteLine(Surname, Group, Trainer, Result, HasPassed);
            }

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                if (participants == null || participantType == null)
                    return new Participant[0];

                int count = 0;
                foreach (var participant in participants)
                {
                    if (participant != null && participant.GetType() == participantType && participant.Trainer == trainer)
                    {
                        count++;
                    }
                }

                Participant[] result = new Participant[count];
                int index = 0;
                foreach (var participant in participants)
                {
                    if (participant != null && participant.GetType() == participantType && participant.Trainer == trainer)
                    {
                        result[index++] = participant;
                    }
                }

                return result;
            }


        }

        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base (surname, group, trainer)
            {
                STANDARD_RESULT = 12;
            }
        }
        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                STANDARD_RESULT = 90;
            }
        }
    }
}

