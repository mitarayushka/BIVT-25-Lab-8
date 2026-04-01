using System.Runtime;
using System.Xml.Linq;
using System.Linq;

namespace Lab8.Purple
{
    public class Task5
    {
        public struct Response
        {

            //поля

            private string _animal;
            private string _characterTrait;
            private string _concept;

            //свойства

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            //конструктор

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            //методы

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber > 3 || questionNumber < 1)
                {
                    return 0;
                }
                int cnt = 0;

                if (questionNumber == 1)
                    foreach (Response r in responses)
                        cnt += Convert.ToInt32(Animal == r.Animal);
                else if (questionNumber == 2)
                    foreach (Response r in responses)
                        cnt += Convert.ToInt32(CharacterTrait == r.CharacterTrait);
                else
                    foreach (Response r in responses)
                        cnt += Convert.ToInt32(Concept == r.Concept);
                
                return cnt;
            }

            public void Print()
            {
                Console.WriteLine($"{Animal} {CharacterTrait} {Concept}");
            }
        }


        public struct Research
        {
            //поля

            private string _name;
            private Response[] _responses;

            //свойства

            public string Name => _name;
            public Response[] Responses
            {
                get
                {
                    if (_responses == null)
                    {
                        return new Response[0];
                    }
                    return _responses;
                }
            }

            //конструктор

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            //методы

            public void Add(string[] answers)
            {
                if (answers == null || answers.Length != 3)
                {
                    return;
                }
                Array.Resize(ref _responses, _responses.Length + 1);
                Response r = new Response(answers[0], answers[1], answers[2]);
                _responses[^1] = r;
            }

            public string[] GetTopResponses(int question)
            {
                if (question > 3 || question < 1 || _responses == null || _responses.Length == 0)
                {
                    return new string[0];
                }
                int cntAnswers;
                string mx = "";
                string[] answers = new string[0];
                int cntmx;
                string[] allAnswer = new string[0];

                if (question == 1)
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].Animal != "" && _responses[i].Animal != null)
                        {
                            Array.Resize(ref allAnswer, allAnswer.Length + 1);
                            allAnswer[^1] = _responses[i].Animal;
                        }
                    }
                }
                else if (question == 2)
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].CharacterTrait != "" && _responses[i].CharacterTrait != null)
                        {
                            Array.Resize(ref allAnswer, allAnswer.Length + 1);
                            allAnswer[^1] = _responses[i].CharacterTrait;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].Concept != "" && _responses[i].Concept != null)
                        {
                            Array.Resize(ref allAnswer, allAnswer.Length + 1);
                            allAnswer[^1] = _responses[i].Concept;
                        }
                    }
                }

                for(int len = 0; len < 5; len++)
                {
                    cntmx = 0;
                    mx = "";
                    foreach (string answer in allAnswer)
                    {
                        cntAnswers = allAnswer.Count(x => x == answer);
                        if (answers.Contains(answer) == false && cntAnswers > cntmx)
                        {
                            cntmx = cntAnswers;
                            mx = answer;
                        }
                    }
                    if (mx != "" && mx != null)
                    {
                        Array.Resize(ref answers, answers.Length + 1);
                        answers[^1] = mx;
                    }
                    else
                    {
                        break;
                    }
                }
                return answers;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Responses}");
            }

        }



        public class Report
        {
            private Research[] _researches;
            private static int _number; 

            public Research[] Researches
            {
                get
                {
                    if (_researches == null)
                    {
                        return new Research[0];
                    }
                    return _researches;
                }
            }
            private int Number => _number;



            static Report()
            {
                _number = 1;
            }

            public Report()
            {
                _researches = new Research[0];
            }


            public Research MakeResearch()
            {

                DateTime now = DateTime.Now;
                int X = _number;
                string MM = now.ToString("MM");
                string YY = now.ToString("YY");

                Research res = new Research($"No_{X}_{MM}/{YY}");
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[^1] = res;

                _number++;
                return res;
            }


            public (string, double)[] GetGeneralReport(int question)
            {
                if (question < 1 || question > 3 || _researches == null || _researches.Length == 0)
                {
                    return new (string, double)[0];
                }
                string[] allAnswers = new string[0];
                string answer;
                string[] unicAnswer = new string[0];
                bool isUnic;
                int[] cntAnswers;
                (string, double)[] res;
                
                for (int i = 0; i < _researches.Length; i++)
                {
                    if (_researches[i].Responses == null || _researches[i].Responses.Length == 0)
                    {
                        continue;
                    }

                    for (int j = 0; j < _researches[i].Responses.Length; j++)
                    {
                        isUnic = true;
                        switch (question)
                        {
                            case 1:
                                answer = _researches[i].Responses[j].Animal;
                                break;
                            case 2:
                                answer = _researches[i].Responses[j].CharacterTrait;
                                break;
                            case 3:
                                answer = _researches[i].Responses[j].Concept;
                                break;
                            default:
                                answer = "";
                                break;
                        }

                        if (answer == null || answer == "")
                        {
                            continue;
                        }
                        if (unicAnswer.Length > 0)
                        {
                            for (int k = 0; k < unicAnswer.Length; k++)
                            {
                                
                                if (unicAnswer[k] == answer)
                                {
                                    isUnic = false;
                                }
                            }
                        }
                        Array.Resize(ref allAnswers, allAnswers.Length + 1);
                        allAnswers[^1] = answer;
                        if (isUnic)
                        {
                            Array.Resize(ref unicAnswer, unicAnswer.Length + 1);
                            unicAnswer[^1] = answer;
                        }
                    }
                }

                if(allAnswers.Length == 0)
                {
                    return new(string, double)[0];
                }
                
                cntAnswers = new int[unicAnswer.Length];
                res = new (string, double)[unicAnswer.Length];

                for(int i = 0; i < unicAnswer.Length; i++)
                {
                    for(int j = 0; j < allAnswers.Length; j++)
                    {
                        if (allAnswers[j] == unicAnswer[i])
                        {
                            cntAnswers[i]++;
                        }
                    }
                }

                for(int i = 0; i < unicAnswer.Length; i++)
                {
                    res[i] = (unicAnswer[i], cntAnswers[i] * 100.0 / allAnswers.Length);
                }

                return res;



            }
        }
    }
}