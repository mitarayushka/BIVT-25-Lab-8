namespace Lab8.Blue
{
    public class Task1
    {
        public class Response(string name)
        {
            readonly string name = name;

            public string Name => name;
            public int Votes { get; protected set; }

            virtual public int CountVotes(Response[] responses)
            {
                this.Votes = responses.Count(Same);

                for (int i = 0; i < responses.Length; i += 1)
                {
                    ref var r = ref responses[i];
                    if (null == r) { continue; }
                    if (Same(r)) { r.Votes = this.Votes; }
                }

                return this.Votes;
            }

            virtual public void Print() => Console.WriteLine(this.ToString());

            bool Same(Response other) => null != other && this.name == other.name;

            override public string ToString() =>
                $"Response{{name: \"{name}\", Votes: {Votes}}}";
        }

        public class HumanResponse(string n, string surname) : Response(n)
        {
            readonly string surname = surname;
            public string Surname => surname;

            bool Same(Response other) => null != other && this.Name == other.Name && this.surname == ((HumanResponse)other).Surname;

            override public int CountVotes(Response[] responses)
            {
                this.Votes = responses.Count(Same);

                for (int i = 0; i < responses.Length; i += 1)
                {
                    if (null == responses[i]) { continue; }
                    if (Same((HumanResponse)responses[i])) { ((HumanResponse)responses[i]).Votes = this.Votes; }
                }

                return this.Votes;
            }

            override public void Print() => Console.WriteLine(this.ToString());

            override public string ToString() =>
                $"Response{{name: \"{Name}\", surname: \"{Surname}\", Votes: {Votes}}}";

        }
    }
}