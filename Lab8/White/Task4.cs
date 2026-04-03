namespace Lab8.White
{
    public class Task4
    {
        public class Human
        {
            //Приватные поля
            private string _name;
            private string _surname;

            //Свойства
            public string Name => _name;
            public string Surname => _surname;

            //Конструктор 
            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }
            //Виртуальный метод
            public virtual void Print()
            {
                return;
            }
        }
        public class Participant : Human
        {
            //Поля
            private double[] _scores;
            private static int _count;
            //Статические свойства
            public static int Count => _count;
            //Свойства
            public double[] Scores
            {
                get
                {
                    if (_scores == null || _scores.Length == 0) return new double[0];
                    double[] copy = new double[_scores.Length];
                    for (int i = 0; i < _scores.Length; i++) copy[i] = _scores[i];
                    return copy;
                }
            }
            public double TotalScore
            {
                get
                {
                    if (_scores == null || _scores.Length == 0) return 0;
                    double s = 0;
                    for (int i = 0; i < _scores.Length; i++) s += _scores[i];
                    return s;
                }
            }
            //Статический конструктор
            static Participant()
            {
                _count = 0;
            }
            public Participant(string name, string surname) : base(name, surname)
            {
                _scores = new double[0]; //пустой массив результатов
                _count++; //увеличиваем общее количество участников
            }
            //Методы
            public void PlayMatch(double result)
            {
                if (result == 0 || result == 0.5 || result == 1)
                {
                    if (_scores == null) _scores = new double[0];
                    Array.Resize(ref _scores, _scores.Length + 1);
                    _scores[_scores.Length - 1] = result;
                }
            }
            //Статический метод сортировки массива участников по убыванию
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            //Переопределение виртуального метода
            public override void Print()
            {
                return;
            }
        }

    }
}
