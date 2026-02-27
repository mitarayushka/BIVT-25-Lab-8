namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks => _marks == null ? Array.Empty<int>() : (int[])_marks.Clone();

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = [0, 0, 0, 0, 0];
                _target = 0;
            }

            public int Result {
                get
                {
                    if (_target == 0) return 0;

                    int[] array = (int[])_marks.Clone();
                    Array.Sort(array);
                    int sum = 0;

                    for (int i = 1; i < array.Length - 1; i++)
                    {
                        sum += array[i];
                    }

                    return sum + 60 + (_distance - _target) * 2;
                }
            }
            
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null) return;

                _distance = distance;
                _marks = (int[])marks.Clone();
                _target = target;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Result < array[j + 1].Result)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_surname} | {Result}");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            private int _index = 0;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants == null ? Array.Empty<Participant>() : _participants;

            public SkiJumping (string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = Array.Empty<Participant>();
            }

            public void Add (Participant participant)
            {
                Participant[] array = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                {
                    array[i] = _participants[i];
                }

                array[array.Length - 1] = participant;
                _participants = array;
            }

            public void Add (Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            public void Jump(int distance, int[] marks)
            {
                if (_index < 0 || _index >= _participants.Length || marks == null) return;
                if (_participants == null || marks == null) return;

                _participants[_index++].Jump(distance, marks, _standard);
            }

            public void Print()
            {
                Console.WriteLine();
            }
        }

        public class JuniorSkiJumping : SkiJumping {
            public JuniorSkiJumping() : base("100m", 100) {}
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) {}
        }
    }
}
