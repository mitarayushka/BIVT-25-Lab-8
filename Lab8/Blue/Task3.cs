using System.Reflection;

namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            //поля
            private string _name;
            private string _surname;
            protected int[] _penalties;

            // свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penalties.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _penalties[i];
                    }
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
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            //конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }

            public virtual void PlayMatch(int time) //добавляет штрафное время в массив
            {
                int[] newTimes = new int[_penalties.Length + 1];
                for (int i = 0; i < _penalties.Length; i++)
                {
                    newTimes[i] = _penalties[i];
                }
                newTimes[newTimes.Length - 1] = time;
                _penalties = newTimes;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length - i; j++)
                    {
                        if (array[j - 1].Total > array[j].Total) // по возрастанию общего времени
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
                    double count = 0;
                    double sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 5)
                        {
                            count++;
                            sum += _penalties[i];
                        }
                    }
                    if (count / _penalties.Length > 0.1)
                    {
                        return true;
                    }
                    if (sum / _penalties.Length >= 2)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public override void PlayMatch(int fall)
            {
                if (fall < 0 || fall > 5)
                {
                    return;
                }
                int[] newTimes = new int[_penalties.Length + 1];
                for (int i = 0; i < _penalties.Length; i++)
                {
                    newTimes[i] = _penalties[i];
                }
                newTimes[newTimes.Length - 1] = fall;
                _penalties = newTimes;
            }
            public BasketballPlayer(string name, string surname) : base(name, surname) { }
        }
        public class HockeyPlayer : Participant
        {
            //pole
            private static int _players = 0;
            private static int _totalTime = 0;
            //svoistvo
            public override bool IsExpelled
            {
                get
                {
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 10)
                        {
                            return true;
                        }
                    }
                    double sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        sum += _penalties[i];
                    }
                    if (sum > _totalTime / _players * 0.1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public override void PlayMatch(int time)
            {
                int[] newTimes = new int[_penalties.Length + 1];
                for (int i = 0; i < _penalties.Length; i++)
                {
                    newTimes[i] = _penalties[i];
                }
                newTimes[newTimes.Length - 1] = time;
                _penalties = newTimes;
                _totalTime += time;
            }
            //konstryltor
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }
        }
    }
}
