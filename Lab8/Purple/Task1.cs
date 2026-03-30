using System.Linq;
using System.Security.Cryptography;

namespace Lab8.Purple
{
    public class Task1
    {

        public class  Participant
        {

            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _num_jump;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs.Length == 0 || _coefs == null) return null;
                    else
                    {
                        return _coefs.ToArray();
                    }
                }


            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }


            }
            public double TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    else
                    {

                        double res = 0;

                        for (int i = 0; i < _marks.GetLength(0); i++)
                        {
                            double locmax = double.MinValue;
                            double locmin = double.MaxValue;
                            double locres = 0;
                            for (int j = 0; j < _marks.GetLength(1); j++)
                            {
                                locres += _marks[i, j];
                                if (_marks[i, j] > locmax)
                                {

                                    locmax = _marks[i, j];
                                }
                                if (_marks[i, j] < locmin)
                                {

                                    locmin = _marks[i, j];
                                }
                            }
                            res += (locres - locmax - locmin) * _coefs[i];

                        }
                        return res;
                    }
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _num_jump = 0;

            }
            public void SetCriterias(double[] coefs)
            {
                if (_coefs == null) return;
                else
                {
                    for (int i = 0; i < _coefs.Length; i++)
                    {
                        _coefs[i] = coefs[i];
                    }
                }
            }
            public void Jump(int[] marks)
            {

                if (4 > _num_jump)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _marks[_num_jump, j] = marks[j];
                    }

                }
                _num_jump++;

            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                var s = array.OrderByDescending(participant => participant.TotalScore).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = s[i];
                }

            }
            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Surname);
                Console.WriteLine(TotalScore);
            }
        }
        public class Judge
        {
            private string _name;
            private int[] _favoritemarks;
            private int num_mark;

            public string Name => _name;


            public Judge(string name, int[] marks)
            {
                _name = name;
                _favoritemarks = marks;
                num_mark = 0;
            }

            
            public int CreateMark()
            {
                if (_favoritemarks == null || _favoritemarks.Length == 0) return 0;
                if (num_mark < _favoritemarks.Length)
                {
                    num_mark++;
                    return _favoritemarks[num_mark-1];
                }
                else
                {
                    num_mark = 0;
                    return _favoritemarks[num_mark];
                }

            }
            public void Print()
            {
                Console.WriteLine(Name);
                
            }

        }
        
        public class Competition
        {
            private Judge[] _judge;
            private Participant[] _participants;

            public Judge[] Judges => _judge.ToArray();
            public Participant[] Participants => _participants.ToArray();


            public Competition(Judge[] judges)
            {
                _judge = judges.ToArray();
                _participants = new Participant[0];

            }
            public void Evaluate(Participant jumper)
            {
                if (jumper == null) return;
                int[] mark = new int[jumper.Marks.Length];
                for (int i = 0;  i < _judge.Length; i++)
                {
                    mark[i] = _judge[i].CreateMark();
                }
                jumper.Jump(mark);

            }
            public void Add(Participant chel)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = chel;
                Evaluate(_participants[^1]);
            }
            public void Add(Participant[] cheliks)
            {
                foreach (Participant chel in cheliks)
                {
                    Add(chel);
                }
                
            }
            public void Sort()
            {
                {
                    if (_participants == null || _participants.Length == 0) return;
                    var s = _participants.OrderByDescending(participant => participant.TotalScore).ToArray();
                    for (int i = 0; i < _participants.Length; i++)
                    {
                        _participants[i] = s[i];
                    }

                }
            }

        }
    }
}