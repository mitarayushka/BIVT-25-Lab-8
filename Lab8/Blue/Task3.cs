using System;
using System.Linq;

namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            // ПОЛЯ 

            private string _name;        
            private string _surname;     
            protected int[] _penalties; 

            // СВОЙСТВА

            public string Name => _name; 
           

            public string Surname => _surname; 


            public int[] Penalties
            {
                get
                {
                    //  возвращаем копию массива штрафов 
                    if (_penalties == null) return null;

                    int[] copy = new int[_penalties.Length];
                    Array.Copy(_penalties, copy, _penalties.Length);
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    // считаем и возвращаем сумму всех штрафов
                    if (_penalties == null) return 0;

                    int sum = 0;
                    foreach (int p in _penalties) sum += p;
                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    //удалён ли участник 
                    if (_penalties == null) return false;

                    foreach (int p in _penalties)
                        if (p == 10) return true;

                    return false;
                }
            }

            //  КОНСТРУКТОР 

            public Participant(string name, string surname)
            {
                
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }

            //  МЕТОДЫ 

            public virtual void PlayMatch(int time)
            {
                //  добавляем новый штраф участнику
                if (_penalties == null) return;

                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                // сортировка по штрафам
                if (array == null || array.Length < 2) return;

                Array.Sort(array, (x, y) => x.Total.CompareTo(y.Total));
            }

            public void Print()
            {
                
            }
        }

        // наследник баскетболист

        public class BasketballPlayer : Participant
        {
            // конструктор
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int fouls)
            {
                // добавляет штраф
                if (fouls < 0 || fouls > 5) return;

                base.PlayMatch(fouls);
            }

            public override bool IsExpelled
            {
                get
                {
                    // свойство

                    if (_penalties == null || _penalties.Length == 0) return false;

                    int count5Fouls = 0;
                    foreach (int f in _penalties)
                        if (f == 5) count5Fouls++;

                    
                    bool cond1 = (double)count5Fouls / _penalties.Length > 0.10;

                    
                    bool cond2 = Total > _penalties.Length * 2;

                    return cond1 || cond2;
                }
            }
        }

        // наследник хоккеист

        public class HockeyPlayer : Participant
        {
            // ПОЛЯ 

            private static int _playersCount = 0;      
            private static int _totalPenaltiesTime = 0; 

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                // конструктор
                _playersCount++;
            }

            public override void PlayMatch(int time)
            {
                // метод. добавляем штраф и учитываем его в общей статистике
                base.PlayMatch(time);
                _totalPenaltiesTime += time;
            }

            public override bool IsExpelled
            {
                get
                {
                    // свойство

                    if (_penalties == null || _penalties.Length == 0 || _playersCount == 0)
                        return false;

                    
                    bool has10Min = base.IsExpelled;

                    
                    double threshold = (_totalPenaltiesTime / (double)_playersCount) * 0.10;
                    bool tooMuchTime = Total > threshold;

                    return has10Min || tooMuchTime;
                }
            }
        }
    }
}
