namespace Lab8.Green
{
    public class Task4
    {
        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;

            public string Name => _name;
            public Participant[] Participants => _participants.ToArray();

            protected Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Participant[] newArray = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    newArray[i] = _participants[i];
                }
                newArray[_participants.Length] = participant;
                _participants = newArray;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                Participant[] newArray = new Participant[_participants.Length + participants.Length];

                for (int i = 0; i < _participants.Length; i++)
                {
                    newArray[i] = _participants[i];
                }

                for (int i = 0; i < participants.Length; i++)
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

            public void Print() { }

            protected void UpdateParticipant(int index, Participant updated)
            {
                _participants[index] = updated;
            }
            protected Participant GetIndex(int index)
            {
                if (index < 0 || index >= _participants.Length)
                {
                    return new Participant();
                }
                return _participants[index];
            }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump") { }

            public override void Retry(int index)
            {
                Participant participant = GetIndex(index);
                double[] jumps = participant.Jumps;
                double bestJump = participant.BestJump;
                double[] newJumps = new double[3];
                newJumps[0] = bestJump;

                Participant newParticipant = new Participant(participant.Name, participant.Surname);

                Participant temp = new Participant(participant.Name, participant.Surname);
                temp.Jump(bestJump);
                UpdateParticipant(index, temp);
            }
        }
        public class HighJump : Discipline
        {
            public HighJump() : base("HighJump") { }

            public override void Retry(int index)
            {
                Participant participant = GetIndex(index);
                double[] currentJumps = participant.Jumps;
                int LastJump = -1;
                for (int i = 0; i < currentJumps.Length; i++)
                {
                    if (currentJumps[i] != 0) LastJump = i;
                }
                if (LastJump == -1) return;

                Participant newParticipant = new Participant(participant.Name, participant.Surname);

                for (int i = 0; i < LastJump; i++)
                {
                    if (currentJumps[i] != 0) newParticipant.Jump(currentJumps[i]);
                }
                UpdateParticipant(index, newParticipant);
            }
        }

        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Jumps => _jumps.ToArray();

            public double BestJump
            {
                get
                {
                    double bestJump = _jumps[0];
                    for (int i = 0; i < _jumps.Length; i++)
                    {
                        if (_jumps[i] > bestJump) bestJump = _jumps[i];
                    }
                    return bestJump;
                }
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
                    if (_jumps[i] == 0) { _jumps[i] = result; break; }

                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
                Console.WriteLine(BestJump);
            }
        }
    }
}
