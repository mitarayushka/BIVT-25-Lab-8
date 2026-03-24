using System.Text;

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
            private bool _wasJump;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            internal bool WasJump => _wasJump;
            // доступен в текущей сборке
            public int[] Marks
            {
                get
                {
                    int[] copyMarks = new int[_marks.Length];
                    Array.Copy(_marks, copyMarks, _marks.Length);
                    return copyMarks;
                }
            }

            public int Result
            {
                get
                {
                    if (!_wasJump) return 0;

                    int resSum = 0;
                    int sumPointsForDistnce = 0;
                    Array.Sort(_marks);
                    for (int i = 1; i < _marks.Length - 1; i++)
                    {
                        resSum += _marks[i];
                    }
                    const int pointsForExampleDisnance = 60;

                    int curDistance = _distance - _target;
                    // _distance > 90 должна быть потому что если спортсмен получит штраф 30 баллов,то 30 * 2 = 60
                    // А 60 - 60 = 0, соотвественно, если еще меньше не 90 то он уйдет в -, в условии написано что меньше 0 не может быть
                    if (_distance > 90)
                        //если дистанция получится отрицательная, то просто знак + перебьет - и будет все ок)
                        sumPointsForDistnce = pointsForExampleDisnance + curDistance * 2;

                    resSum += sumPointsForDistnce;
                    return resSum;

                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _target = 120;
                _wasJump = false;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (_marks == null) return;
                _target = target;
                _distance = distance;
                _wasJump = true;

                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }

            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Participant[] sorted = array.OrderByDescending(x => x.Result).ToArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = sorted[i];
            }

            public void Print() { }
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

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || _participants == null) return;
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (!_participants[i].WasJump)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine("all nice with class");
            }

        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100)
            {

            }

        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150)
            {

            }
        }
    }
}

