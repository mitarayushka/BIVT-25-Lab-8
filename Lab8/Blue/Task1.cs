namespace Lab8.Blue
{
    public class Task1
    {
       
        public class Response
        {
            //  ПОЛЯ 
            private string _name;
            protected int _votes;

            //  СВОЙСТВА
            public string Name
            {
                get { return _name; }
            }

            public int Votes
            {
                get { return _votes; }
            }

            // КОНСТРУКТОР 
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            //  МЕТОДЫ 
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null)
                {
                    _votes = 0;
                    return 0;
                }

                int count = 0;

                // подсчет голосов
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] != null &&
                        responses[i].Name == this.Name)
                    {
                        count++;
                    }
                }

                // Обновление сост. всех найденных объектов
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] != null &&
                        responses[i].Name == this.Name)
                    {
                        responses[i]._votes = count;
                    }
                }

                _votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name}: {Votes} голосов");
            }
        }
        // НАСЛЕДНИК
        public class HumanResponse : Response
        {
            
            private string _surname;

            public string Surname
            {
                get { return _surname; }
            }

            // КОНСТРУКТОР 
            public HumanResponse(string name, string surname)
                : base(name) 
            {
                _surname = surname;
            }

            // ПЕРЕОПРЕДЕЛЕНИЕ МЕТОДОВ 
            public override int CountVotes(Response[] responses)
            {
                if (responses == null)
                {
                    _votes = 0;
                    return 0;
                }

                int count = 0;

                // Подсчет с учетом фамилии
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse human)
                    {
                        if (human.Name == this.Name &&
                            human.Surname == this.Surname)
                        {
                            count++;
                        }
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse human)
                    {
                        if (human.Name == this.Name &&
                            human.Surname == this.Surname)
                        {
                            human._votes = count;
                        }
                    }
                }

                _votes = count;
                return count;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Votes} голосов");
            }
        }
    }
}
