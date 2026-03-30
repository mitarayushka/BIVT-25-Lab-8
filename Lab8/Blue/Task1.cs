namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            private string name;
            protected int votes;

            public string Name => name;
            public int Votes => votes;

            public Response(string name)
            {
                this.name = name;
                this.votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                int count = 0;

                foreach (Response r in responses)
                {
                    if (r.name == this.name)
                        count++;
                }

                foreach (Response r in responses)
                {
                    if (r.name == this.name)
                        r.votes = count;
                }

                votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name}: {Votes} голосов");
            }
        }

        public class HumanResponse : Response
        {
            private string surname;

            public string Surname => surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                this.surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                int count = 0;

                foreach (Response r in responses)
                {
                    if (r is HumanResponse hr &&
                        hr.Name == this.Name &&
                        hr.Surname == this.Surname)
                    {
                        count++;
                    }
                }

                foreach (Response r in responses)
                {
                    if (r is HumanResponse hr &&
                        hr.Name == this.Name &&
                        hr.Surname == this.Surname)
                    {
                        hr.votes = count;
                    }
                }

                votes = count;
                return count;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Votes} голосов");
            }
        }
    }
}
