using System.Xml.Serialization;

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
            public int[] Marks => (int[])_marks.Clone();

            public int Result
            {
                get
                {
                    int result = _marks.Sum() - _marks.Min() - _marks.Max() + 60;
                    result += (_distance - _target) * 2;
                    if (result < 0 || _distance == 0) result = 0;
                    return result;
                }
            }
            public Participant(string Name, string Surname)
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname)) throw new Exception("Null constructor");
                _name = Name;
                _surname = Surname;
                _marks = [0, 0, 0, 0, 0];
                _distance = 0;
                _target = 0;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                _target = target;
                bool isA = true;
                if (distance > 0 && marks.Length == 5)
                {
                    for (int x = 0; x < marks.Length; x++)
                        if (marks[x] < 0 || marks[x] > 20)
                        {
                            isA = false;
                            break;
                        }
                    if (isA)
                    {
                        _distance = distance;
                        for (int x = 0; x < marks.Length; x++)
                            _marks[x] = marks[x];
                    }
                }
            }
            public static void Sort(Participant[] array)
            {
                Participant temp;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].Result > array[j].Result)
                        {
                            temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя:{_name}\n" +
                    $"Фамилия:{_surname}\n" +
                    $"Дистанция прыжка:{_distance}");
                Console.WriteLine($"Оценки Судей:{string.Join(' ', _marks)}");
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

            public SkiJumping(string Name, int Standard)
            {
                _name = Name;
                _standard = Standard;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                foreach (Participant el in participants)
                    Add(el);
            }
            public void Jump(int distance, int[] marks)
            {
                for(int x = 0; x < _participants.Length; x++)
                {
                    if (_participants[x].Distance == 0)
                    {
                        _participants[x].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Название: {_name}");
                Console.WriteLine($"Дистанции: {_standard}");
                Console.WriteLine("Участники:");

                foreach (Participant el in _participants)
                {
                    el.Print();
                    Console.WriteLine();
                }
            }
        }
        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }
        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}