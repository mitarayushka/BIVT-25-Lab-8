using System.Xml.Linq;

namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            // поля
            private string _name;
            protected int _votes;

            // свойства
            public string Name => _name;
            public int Votes => _votes;

            // конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
            public virtual int CountVotes(Response[] responses) //массив структуры
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == _name)
                    {
                        count++;
                    }
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == _name)
                    {
                        responses[i]._votes = count;
                    }
                }
                _votes = count;
                return _votes;
            }
            public virtual void Print()
            {
                return;
            }
        }
        public class HumanResponse : Response
        {
            // поля
            private string _surname;

            //свойства
            public string Surname => _surname;

            //конструктро
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }
            public override int CountVotes(Response[] responses)
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == Name && ((HumanResponse)responses[i])._surname == _surname)
                    {
                        count++;
                    }
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == Name && ((HumanResponse)responses[i])._surname == _surname)
                    {
                        ((HumanResponse)responses[i])._votes = count;
                    }
                }
                _votes = count;
                return _votes;
            }
            public override void Print()
            {
                return;
            }
        }
    }
}
        
