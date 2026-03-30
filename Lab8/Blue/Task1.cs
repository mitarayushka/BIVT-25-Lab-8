using System;

namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            private string _name;
            public string Name => _name;

            protected int _votes;
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual void CountVotes(Response[] responses)
            {
                if (responses == null) return;

                int count = 0;

                foreach (var r in responses)
                {
                    if (r != null && r.Name == Name)
                    {
                        count++;
                    }
                }

                foreach (var r in responses)
                {
                    if (r != null && r.Name == Name)
                    {
                        r._votes = count;
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name} = {Votes}");
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

            public override void CountVotes(Response[] responses)
            {
                if (responses == null) return;

                int count = 0;

                foreach (var r in responses)
                {
                    if (r is HumanResponse hr && hr.Name == Name && hr.Surname == Surname)
                    {
                        count++;
                    }
                }

                foreach (var r in responses)
                {
                    if (r is HumanResponse hr && hr.Name == Name && hr.Surname == Surname)
                    {
                        hr._votes = count;
                    }
                }
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname} = {Votes}");
            }
        }
    }
}