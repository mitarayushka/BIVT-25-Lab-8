namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            // поля 
            protected string _name;
            protected string _surname;
            protected double _time;
            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            // конструктор 
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }
            public void Run(double time)
            {
                _time = time;
            }
            public void Print()
            {
                Console.WriteLine(_name);
            }
            // который сортирует массив спортсменов по возрастанию времени забега.
            public static void Sort(Sportsman[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].Time > array[j].Time)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
        }
            // по 2: первый принимает имя и фамилию, а второй – имя, фамилию и время забега
            // Создать классы–наследники от Sportsman: SkiMan и SkiWoman. 
            public class SkiMan: Sportsman
            { 
                public SkiMan(string name, string surname) : base(name, surname)
                {

                }
                public SkiMan(string name, string surname, double time) : base(name, surname)
                {
                    Run(time);
                }
            }
            // Во втором конструкторе вызывать метод Run для выставления времени.
            public class SkiWoman : Sportsman
            {
                public SkiWoman(string name, string surname) : base(name, surname)
                {
                  
                }
                public SkiWoman(string name, string surname, double time) : base(name, surname)
                {
                    Run(time);
                }
            }
            public class Group
            {
                // поля Name и Sportsmen.
                private string _name;
                private Sportsman[] _sportsmen;
                public string Name => _name;
                public Sportsman[] Sportsmen => _sportsmen;
                // первый конструктор
                ////            Первый конструктор должен принимать название группы и инициализировать массив
                ////спортсменов.Второй конструктор должен принимать в себя группу и копировать её название и всех
                ////её спортсменов в свой массив спортсменов.
                public Group(string name)
                {
                    _name = name;
                    _sportsmen = new Sportsman[0];
                }
                // второй конструктор 
                public Group(Group group)
                {
                    _name = group.Name;
                    Sportsman[] array = new Sportsman[group._sportsmen.Length];
                    for (int i = 0; i < group._sportsmen.Length; i++)
                    {
                        array[i] = group._sportsmen[i];
                    }

                    _sportsmen = array;

                }
                // метод
                public void Add(Sportsman sportsman)
                {
                    Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                    _sportsmen[_sportsmen.Length - 1] = sportsman;
                }
                public void Add(Sportsman[] sportsman)
                {
                    for (int i = 0; i < sportsman.Length; i++)
                    {
                        Add(sportsman[i]);
                    }
                }
                public void Add(Group group)
                {
                    for (int i = 0; i < group._sportsmen.Length; i++)
                    {
                        Add(group._sportsmen[i]);
                    }
                }
                //метод для сортировки массива участников группы 
                public void Sort()
                {
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        for (int j = 1; j < _sportsmen.Length; j++)
                        {
                            if (_sportsmen[j - 1].Time > _sportsmen[j].Time)
                            {
                                (_sportsmen[j - 1], _sportsmen[j]) = (_sportsmen[j], _sportsmen[j - 1]);
                            }
                        }
                    }
                }

                public static Group Merge(Group group1, Group group2)
                {
                    Group merge = new Group("Финалисты");
                    Sportsman[] sportsman = new Sportsman[group1._sportsmen.Length + group2._sportsmen.Length];
                    for (int i = 0; i < group1._sportsmen.Length; i++)
                    {
                        sportsman[i] = group1._sportsmen[i];
                    }
                    int k = 0;
                    for (int i = group1._sportsmen.Length; i < sportsman.Length; i++)
                    {
                        sportsman[i] = group2._sportsmen[k];
                        k++;
                    }
                    for (int i = 0; i < sportsman.Length; i++)
                    {
                        for (int j = 1; j < sportsman.Length; j++)
                        {
                            if (sportsman[j - 1].Time >= sportsman[j].Time)
                            {
                                (sportsman[j - 1], sportsman[j]) = (sportsman[j], sportsman[j - 1]);
                            }
                        }
                    }

                    merge._sportsmen = sportsman;
                    return merge;
                }
                // который разделяет массив спортсменов группы на массивы лыжников и лыжниц
                public void Split(out Sportsman[] men, out Sportsman[] women)
                {
                   
                    int m = 0;
                    int w = 0;
                    for (int i = 0; i < _sportsmen.Length; i ++)
                    {
                        if (_sportsmen[i] is SkiMan)
                        {
                            m++;
                        }
                        if (_sportsmen[i] is SkiWoman)
                        {
                            w++;
                        }
                    }
                    men = new Sportsman[m];
                    women = new Sportsman[w];
                    int k = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i] is SkiMan)
                        {
                            men[k++] = _sportsmen[i];
                        }
                    }
                    k = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i] is SkiWoman)
                        {
                            women[k++] = _sportsmen[i];
                        }
                    }
                }
            //сортирует спортсменов группы по времени и выстраивает в порядке: лыжник, лыжница, лыжник, лыжница и т д.начиная от самого быстрого спортсмена или спортсменки.
                public void Shuffle()
            {
                Sportsman[] men;
                Sportsman[] women;
                Split(out men, out women);

                for (int i = 0; i < men.Length; i++)
                {
                    for (int j = 1; j < men.Length; j++)
                    {
                        if (men[j].Time < men[j - 1].Time)
                        {
                            (men[j], men[j - 1]) = (men[j - 1], men[j]);
                        }
                    }
                }
                for (int i = 0; i < women.Length; i++)
                {
                    for (int j = 1; j < women.Length; j++)
                    {
                        if (women[j].Time<women[j - 1].Time)
                        {
                            (women[j], women[j - 1]) = (women[j - 1], women[j]);
                        }
                    }
                }
                double mxwomen = 0;
                double mxmen = 0;
                for (int i = 0; i < women.Length;i++)
                {
                    if (mxwomen  < women[i].Time)
                    {
                        mxwomen= women[i].Time;
                    }
                }
                for (int i = 0; i < men.Length; i++)
                {
                    if (mxmen < men[i].Time)
                    {
                        mxmen = men[i].Time;
                    }
                }
                int m = 0;
                int w = 0;
                if (mxwomen > mxmen)
                {

                    if (women.Length < men.Length)
                    {
                        for (int i = 0; i < _sportsmen.Length; i++)
                        {
                            if ((i % 2 == 0 || i == 0) & w < women.Length)
                            {
                                _sportsmen[i] = women[w++];

                            }
                            else
                            {
                                _sportsmen[i] = men[m++];
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _sportsmen.Length; i++)
                        {
                            if ((i % 2 == 0 || i == 0) & w < men.Length)
                            {
                                _sportsmen[i] = women[w++];
                            }
                            else if ((i % 2 != 0) & m < men.Length)
                            {
                                _sportsmen[i] = men[m++];
                            }
                            else
                            {
                                _sportsmen[i] = women[w++];
                            }

                        }
                    }
                }
                if (mxmen > mxwomen)
                {
                    if (women.Length > men.Length)
                    {
                        for (int i = 0; i < _sportsmen.Length; i++)
                        {
                            if ((i % 2 == 0 || i == 0) & m < men.Length)
                            {
                                _sportsmen[i] = men[m++];

                            }
                            else
                            {
                                _sportsmen[i] = women[w++];
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _sportsmen.Length; i++)
                        {
                            if ((i % 2 == 0 || i == 0) & m < women.Length)
                            {
                                _sportsmen[i] = men[m++];
                            }
                            else if ((i % 2 != 0) & w < women.Length)
                            {
                                _sportsmen[i] = women[w++];
                            }
                            else
                            {
                                _sportsmen[i] = men[m++];
                            }

                        }
                    }
                }



            }
                public void Print()
                {
                    Console.WriteLine(_name);
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        Console.Write(_sportsmen[i]);
                    }
                }
            }

        

    }
}
