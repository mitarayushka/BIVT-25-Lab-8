using System;
using System.Linq;

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

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                string myAnswer = questionNumber == 1 ? _animal :
                                  questionNumber == 2 ? _characterTrait : _concept;

                if (string.IsNullOrEmpty(myAnswer)) return 0;

                int count = 0;
                foreach (Response r in responses)
                {
                    string other = questionNumber == 1 ? r._animal :
                                   questionNumber == 2 ? r._characterTrait : r._concept;
                    if (myAnswer == other) count++;
                }
                return count;
            }

            public void Print()
            {
                Console.WriteLine(Animal);
                Console.WriteLine(CharacterTrait);
                Console.WriteLine(Concept);
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses => _responses;

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] responses)
            {
                Response newResponse = new Response(responses[0], responses[1], responses[2]);
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = newResponse;
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null || _responses.Length == 0) return new string[0];

                string[] allAnswers = new string[_responses.Length];
                for (int i = 0; i < _responses.Length; i++)
                {
                    allAnswers[i] = question == 1 ? _responses[i].Animal :
                                    question == 2 ? _responses[i].CharacterTrait :
                                                    _responses[i].Concept;
                }

                string[] unique = allAnswers.Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int[] counts = new int[unique.Length];

                for (int i = 0; i < unique.Length; i++)
                {
                    for (int j = 0; j < _responses.Length; j++)
                    {
                        string val = question == 1 ? _responses[j].Animal :
                                     question == 2 ? _responses[j].CharacterTrait :
                                                     _responses[j].Concept;
                        if (val == unique[i]) { counts[i] = _responses[j].CountVotes(_responses, question); break; }
                    }
                }

                for (int i = 0; i < unique.Length - 1; i++)
                    for (int j = 0; j < unique.Length - i - 1; j++)
                        if (counts[j] < counts[j + 1])
                        {
                            (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]);
                            (unique[j], unique[j + 1]) = (unique[j + 1], unique[j]);
                        }

                int size = Math.Min(5, unique.Length);
                string[] result = new string[size];
                Array.Copy(unique, result, size);
                return result;
            }

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(string.Join(", ", Responses));
            }
        }

        public class Report
        {
            private Research[] _researches;
            private static int _counter;

            public Research[] Researches => _researches;

            static Report()
            {
                _counter = 1;
            }

            public Report()
            {
                _researches = new Research[0];
            }

            public Research MakeResearch()
            {
                string name = "No_" + _counter + "_"
                    + DateTime.Now.Month.ToString("D2") + "/"
                    + (DateTime.Now.Year % 100).ToString("D2");
                _counter++;

                Research research = new Research(name);
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = research;
                return research;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                string[] uniqueAnswers = new string[0];
                int[] counts = new int[0];
                int total = 0;

                for (int i = 0; i < _researches.Length; i++)
                {
                    Response[] responses = _researches[i].Responses;
                    if (responses == null) continue;
                    for (int j = 0; j < responses.Length; j++)
                    {
                        string ans = question == 1 ? responses[j].Animal :
                                     question == 2 ? responses[j].CharacterTrait :
                                                     responses[j].Concept;
                        if (string.IsNullOrEmpty(ans)) continue;
                        total++;

                        int idx = -1;
                        for (int k = 0; k < uniqueAnswers.Length; k++)
                            if (uniqueAnswers[k] == ans) { idx = k; break; }

                        if (idx == -1)
                        {
                            Array.Resize(ref uniqueAnswers, uniqueAnswers.Length + 1);
                            Array.Resize(ref counts, counts.Length + 1);
                            uniqueAnswers[uniqueAnswers.Length - 1] = ans;
                            counts[counts.Length - 1] = 1;
                        }
                        else
                        {
                            counts[idx]++;
                        }
                    }
                }

                if (total == 0) return new (string, double)[0];

                for (int i = 0; i < uniqueAnswers.Length - 1; i++)
                    for (int j = 0; j < uniqueAnswers.Length - i - 1; j++)
                        if (counts[j] < counts[j + 1])
                        {
                            int tmpC = counts[j]; counts[j] = counts[j + 1]; counts[j + 1] = tmpC;
                            string tmpS = uniqueAnswers[j]; uniqueAnswers[j] = uniqueAnswers[j + 1]; uniqueAnswers[j + 1] = tmpS;
                        }

                (string, double)[] result = new (string, double)[uniqueAnswers.Length];
                for (int i = 0; i < uniqueAnswers.Length; i++)
                    result[i] = (uniqueAnswers[i], (double)counts[i] / total * 100.0);
                return result;
            }
        }
    }
}
