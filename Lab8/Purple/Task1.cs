namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {
            private string _name; private string _surname;
            private double[] _coefs; private int[,] _marks;
            private double _total; private int _jumps;

            public string Name => _name; public string Surname => _surname;
            public double TotalScore
            {                   //свойства - доступ и использование полей пользов.
                get
                {
                    double[] m = new double[4];
                    for (int i = 0; i < 4; i++)
                    {
                        int sum = 0, min = int.MaxValue, max = int.MinValue;
                        for (int j = 0; j < 7; j++)
                        {
                            if (_marks[i, j] < min)
                                min = _marks[i, j];
                            if (_marks[i, j] > max)
                                max = _marks[i, j];
                            sum += _marks[i, j];
                        }
                        m[i] = sum - (min + max);
                    }
                    for (int j = 0; j < 4; j++)
                        m[j] *= _coefs[j];
                    _total = m.Sum();
                    return _total;
                }
            }
            public double[] Coefs => (double[])_coefs.Clone(); public int[,] Marks => (int[,])_marks.Clone();
            public Participant(string name, string surname)
            {//конструктор - позволяет взять шаблон и создать конкретный обьектс конкретными значениями
                //задает правила того, как создаются обьекты
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _total = -1.0;
                _jumps = 0;
            }
            public void SetCriterias(double[] coefs)
            {
                if (coefs.Length != 4) return;
                for (int i = 0; i < coefs.Length; i++)
                    _coefs[i] = coefs[i];
            }
            public void Jump(int[] marks)
            {
                if (marks.Length != 7) return;
                if (_jumps >= 4) return;
                for (int i = 0; i < 7; i++)
                    _marks[_jumps, i] = marks[i];
                _jumps++;
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                    for (int j = 0; j < array.Length - i - 1; j++)
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
            }
            public void Print()
            {
                Console.Write($"Name: {Name}\nSurname: {Surname}\nTotal score: {TotalScore}\n\n");
            }
        }








        public class Judge
        {
            private string _name; private int[] _marks; private int _k;
            public string Name => _name;
            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks;
                _k = 0;
            }
            public int CreateMark()
            {
                if (_marks == null) return 0;
                int mark = _marks[_k++];
                if (_k >= _marks.Length) _k = 0;
                return mark;
            }
            public void Print()
            { Console.Write($"Name: {_name}\nMarks: {string.Join(" ", _marks)}\n\n"); }
        }






        public class Competition
        {
            private Judge[] _judges; private Participant[] _participants;
            public Judge[] Judges => (Judge[])_judges.Clone();
            public Participant[] Participants => (Participant[])_participants.Clone();
            public Competition(Judge[] judges)
            {
                _judges = judges; _participants = Array.Empty<Participant>();
                                                                //создание пустого массива без выделения памяти.
            }
            public void Evaluate(Participant jumper)
            {
                int[] marks=new int[_judges.Length];
                for (int i = 0; i < marks.Length; i++)
                     marks[i] = _judges[i].CreateMark();
                jumper.Jump(marks);
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1]=participant;
                Evaluate(participant);
            }
            public void Add(Participant[] participants)
            {
                foreach(var participant in participants)
                    Add(participant);
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}
