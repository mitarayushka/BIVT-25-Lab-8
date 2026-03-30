using System;

namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public string Name => _name;
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null)
                {
                    _votes = 0;
                    return 0;
                }

                int count = 0;

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] != null &&
                        responses[i].Name == Name)
                        count++;
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] != null &&
                        responses[i].Name == Name)
                        responses[i]._votes = count;
                }

                _votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name}: {Votes} голосов");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname => _surname;

            public HumanResponse(string name, string surname)
                : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null)
                {
                    _votes = 0;
                    return 0;
                }

                int count = 0;

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse h)
                        if (h.Name == Name && h.Surname == Surname)
                            count++;
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse h)
                        if (h.Name == Name && h.Surname == Surname)
                            h._votes = count;
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
