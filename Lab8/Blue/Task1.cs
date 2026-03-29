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
                int count = 0;

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == this.Name)
                    {
                        count++;
                    }
                }

                _votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name}, {Votes}");
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
                _votes = 0;
            }

            public override int CountVotes(Response[] responses)
            {
                int count = 0;

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse)
                    {
                        HumanResponse other = (HumanResponse)responses[i];

                        if (other.Name == this.Name && other.Surname == this.Surname)
                        {
                            count++;
                        }
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse)
                    {
                        HumanResponse other = (HumanResponse)responses[i];

                        if (other.Name == this.Name && other.Surname == this.Surname)
                        {
                            other._votes = count;
                        }
                    }
                }

                return count;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name}, {Surname}, {Votes}");
            }
        }
    }
}