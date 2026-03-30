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

            public virtual void CountVotes(Response[] responses)
            {
                int count = 0;
                foreach (var response in responses)
                {
                    if (response.Name == _name)
                    {
                        count++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                    if (responses[i].Name == _name)
                        responses[i]._votes = count;
                
            }

            public virtual void Print() { }
            
        }
        

        public class HumanResponse : Response
        {
            readonly string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override void CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0)
                    return;

                int count = 0;

                foreach (var response in responses)
                    if (response is HumanResponse hr)
                        if (hr.Name == this.Name && hr.Surname == this.Surname)
                            count++;

                foreach (var response in responses)
                    if (response is HumanResponse hr)
                        if (hr.Name == this.Name && hr.Surname == this.Surname)
                            hr._votes = count;
                
                
            }

            public override void Print()  { }
        }

    }
}