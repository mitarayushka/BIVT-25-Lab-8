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
            private bool _jump_yes_or_no;
            private int _target;


            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            internal bool Jump_yes_or_no => _jump_yes_or_no;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    return _marks.ToArray();
                }
            }


            public int Result
            {
                get
                {
                    if (_marks == null) return 0;
                    int res = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        res += _marks[i];
                    }
                    res -= (_marks.Max() + _marks.Min());
                    int locres = 60+res;
                    if (_distance - _target >= 0)
                    {
                        locres += (_distance - _target) * 2;
                    }
                    else
                    {
                        if ((locres + (_distance - _target) * 2) >= 0)
                        {
                            locres += (_distance - _target) * 2;
                        }
                        else
                        {
                            locres = 0;
                        }
                    }

                    return locres;

                }
            }

            public Participant(string name, string surname)
            {

                _name = name;
                _surname = surname;
                _marks = new int[5];
                _jump_yes_or_no = false;
                _target = 120;
                _distance = 0;
                
            }
            public void Jump(int distance, int[] marks, int target)
            {

                if (_marks == null || _marks.Length != marks.Length) return;
                _distance = distance;
                _target = target;
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
                _jump_yes_or_no = true;
            }
            public static void Sort(Participant[] array)
            {
                foreach (Participant participant in array)
                {
                    Console.Write(participant.Result + " ");
                }
                Console.WriteLine();
                if (array == null || array.Length == 0) return;
                var s = array.OrderByDescending(participant => participant.Result).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = s[i];
                }
                foreach (Participant participant in array)
                {
                    Console.Write(participant.Result + " ");
                }
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
                Console.WriteLine(Result);


            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }

            public void Add(Participant debil)
            {
                Array.Resize(ref _participants, _participants.Length+1);
                _participants[^1] = debil;
            }
            public void Add(Participant[] debili)
            {
                foreach (Participant debil in debili)
                {
                    Add(debil);
                }
            }
            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Jump_yes_or_no == false)
                    {
                        _participants[i].Jump(distance, marks, Standard);
                        break;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_standard);
                Console.WriteLine(_participants);
            }

        }
        public class JuniorSkiJumping: SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}