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

            protected double _standard;
            private static int _passed;

            static Participant()
            {
                _passed = 0;
            }

            public Participant(string surname, string group, string trainer)
            {
                _surname = surname == null ? "" : surname;
                _group = group == null ? "" : group;
                _trainer = trainer == null ? "" : trainer;
                _result = 0;
                _standard = 0;
            }

            public string Surname { get { return _surname; } }
            public string Group { get { return _group; } }
            public string Trainer { get { return _trainer; } }
            public double Result { get { return _result; } }
            public static int PassedTheStandard { get { return _passed; } }

            public bool HasPassed
            {
                get
                {
                    if (_result == 0)
                        return false;

                    return _result <= _standard;
                }
            }

            public void Run(double newResult)
            {
                if (_result != 0)
                    return;

                _result = newResult;

                if (HasPassed)
                    _passed++;
            }

            public static Participant[] GetTrainerParticipants(Participant[] participants, System.Type participantType, string trainer)
            {
                if (participants == null || participantType == null)
                    return new Participant[0];

                string trainerValue = trainer == null ? "" : trainer;
                int count = 0;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null &&
                        participants[i].GetType() == participantType &&
                        participants[i].Trainer == trainerValue)
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
                        participants[i].Trainer == trainerValue)
                    {
                        result[index] = participants[i];
                        index++;
                    }
                }

                return result;
            }

            public void Print()
            {
                System.Console.WriteLine($"{Surname} {Group} {Trainer} {Result} {HasPassed}");
            }
        }

        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer)
                : base(surname, group, trainer)
            {
                _standard = 12;
            }
        }

        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer)
                : base(surname, group, trainer)
            {
                _standard = 90;
            }
        }
    }
}
