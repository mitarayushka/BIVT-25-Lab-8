namespace Lab8.Green
{
    public class Task4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Jumps => _jumps.ToArray();
            public double BestJump => _jumps.Max();

            public void UpdateJumps(double[] jumps)
            {
                _jumps = jumps;
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
            }

            public void Jump(double result)
            {
                for (int i = 0; i < _jumps.Length; i++)
                {
                    if (_jumps[i] == 0)
                    {
                        _jumps[i] = result;
                        break;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                Array.Sort(array, (a, b) => b.BestJump.CompareTo(a.BestJump));
            }

            public void Print()
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Surname: {Surname}");
                Console.WriteLine($"Jumps: {Jumps}");
                Console.WriteLine($"Best jump: {BestJump}");
            }
        }

        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;
            
            public string Name => _name;
            public Participant[] Participants => _participants;

            protected Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                int startIdx = _participants.Length;
                Array.Resize(ref _participants, startIdx + participants.Length);
                for (int i = 0; i < participants.Length; i++)
                {
                    _participants[startIdx + i] = participants[i];
                }
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index);
            
            public void Print(){}
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long Jump"){}
            
            public override void Retry(int index)
            {
                double bestJump = Participants[index].BestJump;
                Participants[index].UpdateJumps(new[] {bestJump, 0d, 0d});
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High Jump"){}

            public override void Retry(int index)
            {
                double[] jumps = Participants[index].Jumps;
                jumps[2] = 0.0;
                
                Participants[index].UpdateJumps(jumps);
            }
        }
        
    }
}
