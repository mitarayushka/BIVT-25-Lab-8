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

            }
            virtual public int CountVotes(Response[] responses)
            {
                int count_votes = 0;
                foreach (Response response in responses)
                {
                    if (Name == response.Name)
                    {
                        count_votes++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (Name == responses[i].Name)
                    {
                        responses[i]._votes = count_votes;
                    }
                }

                return Votes;
            }
            virtual public void Print()
            {
                Console.WriteLine($"Имя: {Name} Голоса: {Votes}\n");
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
                int count_votes = 0;
                foreach (HumanResponse response in responses)
                {
                    if (Name == response.Name && Surname == response.Surname)
                    {
                        count_votes++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (Name == responses[i].Name && Surname == ((HumanResponse)responses[i]).Surname)
                    {
                        ((HumanResponse)responses[i])._votes = count_votes;
                    }
                }
                return count_votes;
            }

            public override void Print()
            {
                Console.WriteLine($"Имя: {Name} Фамилия: {Surname}\n Голоса: {Votes}\n");
            }
        }
    }
}
