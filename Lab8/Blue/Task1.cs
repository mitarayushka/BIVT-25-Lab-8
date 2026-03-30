namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public string Name
            {
                get { return _name; }
            }

            public int Votes
            {
                get { return _votes; }
            }

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
                    if (responses[i] != null && responses[i].Name == _name)
                    {
                        count++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] != null && responses[i].Name == _name)
                    {
                        responses[i]._votes = count;
                    }
                }

                _votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name}: {_votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;

            public string Surname
            {
                get { return _surname; }
            }

            public HumanResponse(string name, string surname) : base(name)
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
                    HumanResponse human = responses[i] as HumanResponse;

                    if (human != null && human.Name == Name && human.Surname == _surname)
                    {
                        count++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    HumanResponse human = responses[i] as HumanResponse;

                    if (human != null && human.Name == Name && human.Surname == _surname)
                    {
                        human._votes = count;
                    }
                }

                _votes = count;
                return count;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {_surname}: {Votes}");
            }
        }
    }
}
