namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {

            // Поля
            private string _name;
            private string _surname;
            protected int[] _penalties;
            // Свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penalties.Length];
                    Array.Copy(_penalties, 0, copy, 0, _penalties.Length);
                    return copy;
                }
            }
            public int Total
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        sum += _penalties[i];
                    }
                    return sum;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    bool flag = false;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 10)
                        {
                            flag = true;
                        }
                    }
                    return flag;
                }
            }
            // Конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }
            // Метод
            public virtual void PlayMatch(int fol)
            {
                //if (time != 0 && time != 2 && time != 5 && time != 10) return;
                Array.Resize(ref _penalties, _penalties.Length + 1); // void метод
                _penalties[^1] = fol;
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].Total > array[j].Total)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            public void Print()
            {
                return;
            }
        }
        public class BasketballPlayer : Participant
        {
            public override bool IsExpelled
            {
                get 
                {
                    bool flag = false; double k = 0;double sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 5)
                        {
                            k++;
                            sum += _penalties[i];
                        }
                    }
                    if (k/_penalties.Length > 0.1|| sum/_penalties.Length >=2)
                        flag = true;
                    return flag;
                }
            }
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
            }
            public override void PlayMatch(int fol)
            {
                if (fol < 0 || fol > 5)
                    return;
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = fol;
            }
        }
        public class HockeyPlayer : Participant
        {
            private static int _players;
            private static int _totalTime;

            public override bool IsExpelled
            {
                get
                {
                    bool flag = false; double sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        sum += _penalties[i];
                        if (_penalties[i] == 10)
                        {
                            flag = true;
                        }
                    }
                    if (sum > 0.1 * _totalTime/_players)
                        flag = true;
                    return flag;
                }
            }
            static HockeyPlayer()
            {
                _totalTime = 0;
                _players = 0;
            }
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }
            public override void PlayMatch(int fol)
            {
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[^1] = fol;
                _totalTime += fol;
            }
        }
    }
}
