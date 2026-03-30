using System.Collections.Generic;
using static Lab8.Purple.Task5;

namespace Lab8.Purple
{
    public class Task5
    {
        public struct Response
        {
            private string _animal;
            private string _charactertrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _charactertrait;
            public string Concept => _concept;

            public Response(string animal, string charactertrait, string concept)
            {
                _animal = animal;
                _charactertrait = charactertrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || responses.Length == 0) return 0;
                int k = 0;
                if (questionNumber == 1)
                {
                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (responses[i].Animal == _animal)
                        {
                            k++;
                        }
                    }
                }
                else if (questionNumber == 2)
                {
                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (responses[i].CharacterTrait == _charactertrait)
                        {
                            k++;
                        }
                    }
                }
                else if (questionNumber == 3)
                {
                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (responses[i].Concept == _concept)
                        {
                            k++;
                        }
                    }
                }
                return k;
            }
            public void Print()
            {
                Console.WriteLine(_animal);
                Console.WriteLine(_charactertrait);
                Console.WriteLine(_concept);
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

            public void Add(string[] answers)
            {
                Response newAnswer = new Response(answers[0], answers[1], answers[2]);
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = newAnswer;
            }
            public string[] GetTopResponses(int question)
            {
                if (_responses.Length == 0 || _responses == null) return null;
                string[] ans = new string[0];
                if (question == 1)
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        Array.Resize(ref ans, ans.Length + 1);
                        ans[^1] = _responses[i].Animal;
                    }
                }
                else if (question == 2)
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        Array.Resize(ref ans, ans.Length + 1);
                        ans[^1] = _responses[i].CharacterTrait;
                    }
                }
                else if (question == 3)
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        Array.Resize(ref ans, ans.Length + 1);
                        ans[^1] = _responses[i].Concept;
                    }
                }

                int[] ans_kol = new int[ans.Length];
                for (int i = 0; i < ans.Length; i++)
                {
                    int k = 1;
                    if (ans[i] != null)
                    {
                        for (int j = i + 1; j < ans.Length; j++)
                        {
                            if (ans[i] == ans[j])
                            {
                                k++;
                                ans[j] = null;
                            }
                        }
                        ans_kol[i] = k;
                    }


                }
                int[] temp_kol = new int[ans_kol.Length];
                Array.Copy(ans_kol, temp_kol, ans_kol.Length);
                string[] ans_fin = new string[0];
                for (int i = 0; i < 5; i++)
                {
                    if (ans_kol.Max() == 0) break;
                    for (int j = 0; j < ans_kol.Length; j++)
                    {
                        if (ans_kol[j] == ans_kol.Max() && ans[j] != null)
                        {
                            Array.Resize(ref ans_fin, ans_fin.Length + 1);
                            ans_fin[i] = ans[j];
                            ans_kol[j] = 0;
                            break;

                        }
                    }
                }
                Console.WriteLine(string.Join(" ", ans));
                Console.WriteLine(string.Join(" ", ans_kol));
                Console.WriteLine(string.Join(" ", ans_fin));
                return ans_fin;

            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_responses);
            }
        }
        public class Report
        {
            private static int _num;
            private Research[] _researches;

            public Research[] Researches => _researches;

            static Report()
            {
                _num = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }
            public Research MakeResearch()
            {
                int X = _num;
                _num += 1;
                var MM = DateTime.Now.Month;
                var YY = (DateTime.Now.Year)%100;
                var name = $"No_{X}_{MM:D2}/{YY}";
                var a = new Research(name);
                Array.Resize(ref _researches, _researches.Length+1);
                _researches[^1] = a;
                return a;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                string[] ans = new string[0];
                double[] ans_kol = new double[ans.Length];
                int kol_vse = 0;
                foreach (var reserche in _researches)
                {

                    if (question == 1)
                    {
                        for (int i = 0; i < reserche.Responses.Length; i++)
                        {
                            Array.Resize(ref ans, ans.Length + 1);
                            ans[^1] = reserche.Responses[i].Animal;
                        }
                    }
                    else if (question == 2)
                    {
                        for (int i = 0; i < reserche.Responses.Length; i++)
                        {
                            Array.Resize(ref ans, ans.Length + 1);
                            ans[^1] = reserche.Responses[i].CharacterTrait;
                        }
                    }
                    else if (question == 3)
                    {
                        for (int i = 0; i < reserche.Responses.Length; i++)
                        {
                            Array.Resize(ref ans, ans.Length + 1);
                            ans[^1] = reserche.Responses[i].Concept;
                        }
                    }
                    Console.WriteLine(string.Join(", ", ans));
                    Console.WriteLine();
                }
                ans_kol = new double[ans.Length];
                for (int i = 0; i < ans.Length; i++)
                {
                    int k = 1;
                    if (ans[i] != null)
                    {
                        for (int j = i + 1; j < ans.Length; j++)
                        {
                            if (ans[i] == ans[j])
                            {
                                    
                                k++;
                                ans[j] = null;
                                
                            }
                        }
                        ans_kol[i] = k;
                        kol_vse += k;
                    }


                }
                ans = ans.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                ans_kol = ans_kol.Where(n => n != 0).ToArray();
                Console.WriteLine(string.Join(", ", ans));
                Console.WriteLine(string.Join(", ", ans_kol));


                (string, double)[] ans_fin = new (string, double)[ans.Length];
                ans.OrderBy(ans => ans_kol).ToArray();
                for (int i = 0; i < ans.Length; i++)
                {
                    ans_fin[i] = (ans[i], ans_kol[i]/(double)kol_vse*100);
                }
                return ans_fin;


            }



        }

    }
}