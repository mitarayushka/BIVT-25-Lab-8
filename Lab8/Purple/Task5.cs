using System.ComponentModel;
using System.Globalization;
using static Lab8.Purple.Task4;

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

            public Response(string Animal, string CharacterTrait, string Concept)
            {
                if (!string.IsNullOrWhiteSpace(Animal) || !string.IsNullOrWhiteSpace(CharacterTrait) || !string.IsNullOrWhiteSpace(Concept))
                {
                    _animal = Animal;
                    _characterTrait = CharacterTrait;
                    _concept = Concept;
                }
                else
                    throw new Exception("ERROR! See the details:https://ih1.redbubble.net/image.3386060102.8005/bg,f8f8f8-flat,750x,075,f-pad,750x1000,f8f8f8.jpg");
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                int count = 0;
                for (int x = 0; x < responses.Length; x++)
                {
                    switch (questionNumber)
                    {
                        case 1:
                            if (!string.IsNullOrEmpty(responses[x]._animal))
                                count++;
                            break;
                        case 2:
                            if (!string.IsNullOrEmpty(responses[x]._characterTrait))
                                count++;
                            break;
                        case 3:
                            if (!string.IsNullOrEmpty(responses[x]._concept))
                                count++;
                            break;
                        default:
                            return 0;
                    }
                }
                return 0;
            }


            public void Print()
            {
                Console.WriteLine("Ваши ответы на вопросы:" +
                    $"Какое животное Вы связываете с Японией и японцами:{_animal}" +
                    $"Какая черта характера присуща японцам больше всего:{_characterTrait}" +
                    $"Какой неодушевленный предмет или понятие Вы связываете с Японией:{_concept}");
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses => (Response[])_responses.Clone();

            public Research(string Name)
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    _name = Name;
                    _responses = [];
                }
            }
            public string[] GetTopResponses(int question)
            {
                if (question > 0 || question < 4)
                {
                    Dictionary<string, int> count = new Dictionary<string, int>();

                    for (int x = 0; x < _responses.Length; x++)
                    {
                        string answer = "";

                        switch (question)
                        {
                            case 1:
                                answer = _responses[x].Animal;
                                break;
                            case 2:
                                answer = _responses[x].CharacterTrait;
                                break;
                            case 3:
                                answer = _responses[x].Concept;
                                break;
                            default:
                                break;
                        }
                        if (string.IsNullOrWhiteSpace(answer))
                            continue;

                        if (count.ContainsKey(answer))
                            count[answer]++;
                        else
                            count[answer] = 1;
                    }
                    return count.OrderByDescending(x => x.Value).Take(5).Select(x => x.Key).ToArray();
                }
                else
                {
                    return new string[0];
                }
            }

            public void Add(string[] answers)
            {
                if (answers.Length > 0 || answers.Length < 4)
                {
                    Array.Resize(ref _responses, _responses.Length + 1);
                    _responses[_responses.Length - 1] = new Response(answers[0], answers.Length > 1 ? answers[1] : "", answers.Length > 2 ? answers[2] : "");
                }
            }

            public void Print()
            {
                Console.WriteLine("Добавте анкеты чтобы структура работала!");
            }
        }
        public class Report
        {
            private Research[] _researches;
            public Research[] Researches => _researches;

            private static int _id;

            static Report()
            {
                _id = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }
            private void Add(Research research)
            {
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = research;
            }
            public Research MakeResearch()
            {
                DateTime date = DateTime.Now;
                Research research = new Research($"No_{_id++}_{date.ToString("MM")}/{date.ToString("YY")}");
                Add(research);
                return research;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                Dictionary<string, int> counts = new Dictionary<string, int>();
                int count = 0, countAnswers = 0;
                foreach (var research in _researches)
                {
                    foreach (var response in research.Responses)
                    {
                        string answer = "";
                        switch (question)
                        {
                            case 1:
                                answer = response.Animal;
                                break;
                            case 2:
                                answer = response.CharacterTrait;
                                break;
                            case 3:
                                answer = response.Concept;
                                break;
                            default:
                                break;
                        }
                        if (string.IsNullOrWhiteSpace(answer))
                            continue;

                        count++;
                        if (counts.ContainsKey(answer))
                            counts[answer]++;
                        else
                        {
                            counts[answer] = 1;
                            countAnswers++;
                        }
                    }
                }

                if (count == 0)
                    return new (string, double)[0];

                (string, double)[] output = new (string, double)[countAnswers];
                countAnswers = 0;
                foreach (var el in counts)
                {
                    output[countAnswers++] = (el.Key, ((double)el.Value / count) * 100);
                }
                return output;
            }
        }
    }
}