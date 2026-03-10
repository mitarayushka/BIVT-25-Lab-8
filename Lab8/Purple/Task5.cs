namespace Lab8.Purple
{
    public class Task5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response 
                (
                    string animal,
                    string characterTrait,
                    string concept
                )
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }
            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null) return 0;
                if (questionNumber<1 || questionNumber>3) return 0;
                Func<Response,string> selector = questionNumber switch
                {
                    1 => r => r.Animal,
                    2 => r => r.CharacterTrait,
                    3 => r => r.Concept,
                };
                string myValue = selector(this);
                int count = responses.Count(r=> selector(r)==myValue);
                return count;
            }
            public void Print()
            {
                System.Console.WriteLine("_________Response___________");
                System.Console.WriteLine($"Animal: {_animal}");
                System.Console.WriteLine($"CharacterTrait: {_characterTrait}");
                System.Console.WriteLine($"Concept: {_concept}");
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses => (Response[])_responses.Clone();

            public Research (string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                if (answers == null) return;
                Array.Resize(ref _responses,_responses.Length+1);
                var response = new Response(answers[0],answers[1],answers[2]);
                _responses[^1] = response;
            }

            
            public  string[]  GetTopResponses(int question)
            {
                if (question < 1 || question > 3) return new string[0];
                string[] all = new string[0];
                
                foreach (var response in _responses)
                {
                    string value = question switch
                    {
                        1 => response.Animal,
                        2 => response.CharacterTrait,
                        3 => response.Concept,
                    };
                    if (!string.IsNullOrEmpty(value))
                    {
                        Array.Resize(ref all,all.Length+1);
                        all[^1] = value;
                    }
                }
                
                string[] unique = all.Distinct().ToArray();
                int[] countUnique = new int[unique.Length];
                for (int i = 0; i < unique.Length; i++)
                    countUnique[i] = all.Count(a => a==unique[i]);

                int l = 1;
                while (l < unique.Length)
                {
                    if (l==0 || countUnique[l-1]>=countUnique[l]) l++;
                    else
                    {
                        (countUnique[l-1],countUnique[l])=(countUnique[l],countUnique[l-1]);
                        (unique[l-1],unique[l])=(unique[l],unique[l-1]);
                        l--;
                    }
                }

                if (unique.Length <= 5) return unique;
                else
                {
                    string[] onlyFive = new string[5];
                    Array.Copy(unique,0,onlyFive,0,5);
                    return onlyFive;
                }               
            }

            public void Print()
            {
                System.Console.WriteLine("___________Research__________");
                System.Console.WriteLine($"Name: {_name}");
                for (int i = 0; i < _responses.Length; i++)
                    System.Console.WriteLine($"Response{i}: {_responses[i].Animal}; {_responses[i].CharacterTrait}; {_responses[i].Concept};");
                
            }
        }

        public class Report
        {
            private Research[] _researches;
            private static int _countId;
            public Research[] Researches => _researches;
            static Report() => _countId = 1;
            public Report()
            {
                _researches = new Research[0];
            }
            public Research MakeResearch()
            {
                int id = _countId++;
                var newResearch = new Research($"No_{id}_{DateTime.Now.Month}/{DateTime.Now.Year}");
                Array.Resize(ref _researches,_researches.Length+1);
                _researches[^1] = newResearch;
                return newResearch;
            }
            public  (string,  double)[]  GetGeneralReport(int question)
            {
                if (question<1 || question>3) return new (string,  double)[0];

                string[] all = new string[0];
                foreach (var research in _researches)
                {
                    foreach (var response in research.Responses)
                    {
                        string value = question switch
                        {
                            1 => response.Animal,
                            2 => response.CharacterTrait,
                            3 => response.Concept,
                        };
                        if (!string.IsNullOrEmpty(value))
                        {
                            Array.Resize(ref all,all.Length+1);
                            all[^1] = value;
                        }
                    }
                }

                int total = all.Length;
                if (total==0) return new (string,  double)[0];

                string[] unique = all.Distinct().ToArray();
                int[] countUnique = new int[unique.Length];
                for (int i =0;i<unique.Length;i++)
                    countUnique[i] = all.Count(a=> a==unique[i]);

                (string,  double)[] answer = new (string,  double)[unique.Length];
                for (int i = 0; i < unique.Length; i++)
                {
                    double percent = countUnique[i]/(double)total * 100.0;
                    answer[i] = (unique[i],percent);
                }

                return answer;
            }
        }
    }
}