using System.Reflection.PortableExecutable;

namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            //поля
            private string _name;
            protected int _votes;

            //свойства
            public string Name => _name;
            public int Votes => _votes;

            //конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == Name)
                    {
                        count++;
                    }
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == Name)
                    {
                        responses[i]._votes = count;
                    }
                }
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"имя: {Name}, голоса: {Votes}");
            }
        }
        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;

                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    HumanResponse hr = responses[i] as HumanResponse;

                    if (hr != null && hr.Name == Name && hr.Surname == Surname)
                    {
                        count++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    HumanResponse hr = responses[i] as HumanResponse;
                    if (hr != null && hr.Name == Name && hr.Surname == Surname)
                    {
                        hr._votes = count;
                    }
                }
                return count;
            }
            public override void Print()
            {
                Console.WriteLine($"имя: {Name}, фамилия: {Surname}, голоса: {Votes}");
            }
        }
    }

}
