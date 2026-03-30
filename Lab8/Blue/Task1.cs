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
                if (responses == null) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] != null && responses[i].Name == this._name)
                    {
                        count++;
                    }
                }
                _votes = count;
                return count;
            }
            public virtual void Print()
            {
                Console.WriteLine($"{Name} {Votes}");
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
                    HumanResponse humanResp = responses[i] as HumanResponse;
                    if (humanResp != null && humanResp.Name == this.Name && humanResp.Surname == this.Surname)
                    {
                        count++;
                    }
                }
                _votes = count;
                return count;
            }
            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Votes}");
            }
        }
    }
}
