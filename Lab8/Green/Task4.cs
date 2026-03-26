namespace Lab8.Green
{
    public class Task4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;
            private int _countJump;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Jumps
            {
                get
                {
                    if (_jumps == null) return new double[0];
                    double[] copy = new double[_jumps.Length];
                    for (int i = 0; i < _jumps.Length; i++)
                    {
                        copy[i] = _jumps[i];
                    }
                    return copy;
                }
            }

            public double BestJump
            {
                get
                {
                    if (_jumps == null || _jumps.Length == 0) return 0;
                    double best = _jumps[0];
                    for (int i = 1; i < _jumps.Length; i++)
                    {
                        if (_jumps[i] > best) best = _jumps[i];
                    }
                    return best;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
                _countJump = 0;
            }

            public void Jump(double result)
            {
                if (_countJump >= 3) return;
                _jumps[_countJump] = result;
                _countJump++;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }
            public void Print()
            {
                string jumpsStr = string.Join(", ", Jumps);
                Console.WriteLine($"Спортсмен: {Name} {Surname}, попытки: {jumpsStr}, лучший: {BestJump:F2}");
            }
        }

        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;

            public string Name => _name;
            public Participant[] Participants => _participants;
            protected Participant[] ParticipantsArray => _participants;

            protected Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Participant[] newArray = new Participant[_participants.Length+1];
                for (int i = 0; i < _participants.Length; i++)
                    newArray[i] = _participants[i];
                newArray[_participants.Length ] = participant;
                _participants = newArray;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                Participant[] newArray = new Participant[_participants.Length + participants.Length];
                for (int i =0; i< _participants.Length; i++)
                {
                    newArray[i] = _participants[i];
                }
                for (int i = 0; i<participants.Length; i++)
                {
                    newArray[_participants.Length + i] = participants[i];
                }
                _participants = newArray;
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index);

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Participants);
            }
        }


        public class LongJump: Discipline
        {
            public LongJump() : base("Long jump") { }

            public override void Retry(int index)
            {
                if (index<0 || index>= ParticipantsArray.Length) return;
                Participant p = ParticipantsArray[index];
                double best = p.BestJump;
                if (best == 0 && p.Jumps.Length > 0 && p.Jumps[0] == 0) return; 

                Participant newP = new Participant(p.Name, p.Surname);
                newP.Jump(best);   
                ParticipantsArray[index] = newP;
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High jump") { }

            public override void Retry(int index)
            {
                if (index < 0 || index >= ParticipantsArray.Length) return;
                Participant p = ParticipantsArray[index];
                double[] jumps = p.Jumps;
                int count = 0;
                for (int i = 0; i < jumps.Length; i++)
                    if (jumps[i] != 0) count++;
                if (count == 0) return;

                Participant newP = new Participant(p.Name, p.Surname);
                for (int i = 0; i < count - 1; i++)
                    newP.Jump(jumps[i]);
                ParticipantsArray[index] = newP;
            }
        }
    }
}
