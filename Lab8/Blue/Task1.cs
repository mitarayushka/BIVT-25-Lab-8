namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            // fields
            private readonly string _name;
            protected int _votes;

            // properties
            public string Name => _name;
            public int Votes => _votes;

            // methods
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;

                int count = 0;
                foreach (var res in responses)
                {
                    if (res != null && res.Name == _name) count++;
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] != null && responses[i].Name == _name)
                        responses[i]._votes = count;
                }

                return count;
            }

            public virtual void Print()
            {
                System.Console.WriteLine($"Name: {Name}, Votes: {Votes}");
            }

            // constructor
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
        }

        public class HumanResponse : Response
        {
            private readonly string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;

                int count = 0;

                foreach (var r in responses)
                {
                    if (r is HumanResponse hr)
                    {
                        if (hr.Name == this.Name && hr.Surname == this.Surname)
                            count++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse hr && hr.Name == this.Name && hr.Surname == this.Surname)
                        hr._votes = count;
                }

                return count;
            }

            public override void Print()
            {
                System.Console.WriteLine($"Name: {Name}, Surname: {Surname}, Votes: {Votes}");
            }
        }
    }
}