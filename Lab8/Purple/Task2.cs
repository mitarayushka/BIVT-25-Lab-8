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
            private bool _isJumped;
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[5];
                    int[] Marks = new int[_marks.Length];
                    Array.Copy(_marks, Marks, _marks.Length);
                    return Marks;
                }
            }

            public int Result
            {
                get
                {
                    int result = 0;
                    int max = Int32.MinValue, min = Int32.MaxValue;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        result += _marks[i];
                        if (_marks[i] > max)
                            max = _marks[i];
                        if (_marks[i] < min)
                            min = _marks[i];
                    }

                    result -= max + min;
                    if (_distance >= _target)
                        result += (_distance - _target) * 2 + 60;
                    else 
                        result -= (_target - _distance) * 2 - 60;
                    if (result < 0 || result == 60)
                        result = 0;
                    return result;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }

            public void Jump(int distance, int[] marks, int target)
            { 
                for (int i = 0; i < _marks.Length; i++)
                    _marks[i] = marks[i];
                _distance = distance;
                _target = target;
                _isJumped = true;
            }

            public static void Sort(Participant[] array)
            {
                Participant[] sortedArray = new Participant[array.Length];
                sortedArray = array.OrderByDescending(x => x.Result).ToArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = sortedArray[i];
            }

            
            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname}");
                Console.WriteLine($"Distance: {_distance}");
                Console.WriteLine($"Marks: {string.Join(", ", _marks)}");
                Console.WriteLine($"Result: {Result}");
            }
        
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            public string Name => _name;
            public int Standard => _standard;

            public Participant[] Participants
            {
                get
                {
                    if (_participants == null) return new Participant[0];
                    return _participants;
                }
            }

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            } 

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Standard: {_standard}, Participants: {string.Join(", ", Participants)}");
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) {}
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) {}
        }
    }
}
