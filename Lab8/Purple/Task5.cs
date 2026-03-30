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
                if (responses == null || responses.Length == 0) return 0;

                string targetAnswer = "";

                
                if (questionNumber == 1)
                {
                    targetAnswer = _animal;
                }
                else if (questionNumber == 2)
                {
                    targetAnswer = _characterTrait;
                }
                else if (questionNumber == 3)
                {
                    targetAnswer = _concept;
                }
                else
                {
                    return 0;
                }

               
                if (targetAnswer == null || targetAnswer.Length == 0) return 0;

                int count = 0;

                
                for (int i = 0; i < responses.Length; i++)
                {
                    string currentAnswer = "";

                    
                    if (questionNumber == 1)
                    {
                        currentAnswer = responses[i]._animal;
                    }
                    else if (questionNumber == 2)
                    {
                        currentAnswer = responses[i]._characterTrait;
                    }
                    else if (questionNumber == 3)
                    {
                        currentAnswer = responses[i]._concept;
                    }

                    if (currentAnswer == targetAnswer)
                    {
                        count++;
                    }
                }

                return count;
            }

            public void Print()
            {
                Console.WriteLine($"Животное: {_animal}");
                Console.WriteLine($"Черта характера: {_characterTrait}");
                Console.WriteLine($"Понятие/предмет: {_concept}");
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses; 
            private int _count;

            public string Name => _name;

            public Response[] Responses
            {
                get
                {
                    Response[] copy = new Response[_count];
                    Array.Copy(_responses, copy, _count);
                    return copy;
                }
            }

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0]; 
                _count = 0;
            }

            public void Add(string[] answers)
            {
                if (answers == null || answers.Length < 3) return;

                Response newResponse = new Response(answers[0], answers[1], answers[2]);

                Response[] newResponses = new Response[_responses.Length + 1];

                for (int i = 0; i < _responses.Length; i++)
                {
                    newResponses[i] = _responses[i];
                }

                newResponses[_responses.Length] = newResponse;

                _responses = newResponses;
                _count++;
            }


            public string[] GetTopResponses(int question)
            {
                string[] answers = new string[_responses.Length];
                for (int i = 0; i < _responses.Length; i++)
                {
                    if (question == 1)
                        answers[i] = _responses[i].Animal;
                    else if (question == 2)
                        answers[i] = _responses[i].CharacterTrait;
                    else if (question == 3)
                        answers[i] = _responses[i].Concept;
                }

                string[] unique = new string[answers.Length];
                int[] counts = new int[answers.Length];
                int uniqueCount = 0;

                for (int i = 0; i < answers.Length; i++)
                {
                    if (answers[i] == null || answers[i].Length == 0) continue;

                    int foundIndex = -1;
                    for (int j = 0; j < uniqueCount; j++)
                    {
                        if (unique[j] == answers[i])
                        {
                            foundIndex = j;
                            break;
                        }
                    }

                    if (foundIndex == -1)
                    {
                        unique[uniqueCount] = answers[i];
                        counts[uniqueCount] = 1;
                        uniqueCount++;
                    }
                    else
                    {
                        counts[foundIndex]++;
                    }
                }

                for (int i = 0; i < uniqueCount - 1; i++)
                {
                    for (int j = 0; j < uniqueCount - i - 1; j++)
                    {
                        if (counts[j] < counts[j + 1])
                        {
                            // Меняем местами уникальные ответы
                            string tempAnswer = unique[j];
                            unique[j] = unique[j + 1];
                            unique[j + 1] = tempAnswer;

                            // Меняем местами частоты
                            int tempCount = counts[j];
                            counts[j] = counts[j + 1];
                            counts[j + 1] = tempCount;
                        }
                    }
                }

                int resultSize = Math.Min(uniqueCount, 5);
                string[] result = new string[resultSize];
                for (int i = 0; i < resultSize; i++)
                {
                    result[i] = unique[i];
                }

                return result;
            }

            public void Print()
            {
                Console.WriteLine($"Исследование: {_name}");
                Console.WriteLine($"Всего ответов: {_count}");
                for (int i = 0; i < _responses.Length; i++)
                {
                    Response response = _responses[i];
                    response.Print();
                }
            }
        }

        public class Report
        {
            private Research[] _researches; 
            private static int _idCounter;

            public Research[] Researches => _researches;

            static Report()
            {
                _idCounter = 1;
            }

            public Report()
            {
                _researches = new Research[0]; 
            }

            public Research MakeResearch()
            {
                string date = DateTime.Now.ToString("MM/yy");
                string name = $"No_{_idCounter}_{date}";
                _idCounter++;

                Research newResearch = new Research(name);
                Research[] newResearches = new Research[_researches.Length + 1];

                for (int i = 0; i < _researches.Length; i++)
                {
                    newResearches[i] = _researches[i];
                }

                newResearches[_researches.Length] = newResearch;
                _researches = newResearches;

                return newResearch;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                string[] allAnswers = new string[0];
                int count = 0;

                for (int i = 0; i < _researches.Length; i++)
                {
                    Response[] responses = _researches[i].Responses;
                    for (int j = 0; j < responses.Length; j++)
                    {
                        string answer = null;

                        if (question == 1)
                        {
                            answer = responses[j].Animal;
                        }
                        else if (question == 2)
                        {
                            answer = responses[j].CharacterTrait;
                        }
                        else if (question == 3)
                        {
                            answer = responses[j].Concept;
                        }

                        if (answer != null && answer.Length > 0)
                        {
                            Array.Resize(ref allAnswers, allAnswers.Length + 1);
                            allAnswers[count++] = answer;
                        }
                    }
                }

                string[] uniqueAnswers = new string[count];
                int uniqueCount = 0;

                for (int i = 0; i < count; i++)
                {
                    if (Array.IndexOf(uniqueAnswers, allAnswers[i]) == -1)
                    {
                        uniqueAnswers[uniqueCount++] = allAnswers[i];
                    }
                }

                (string, double)[] result = new (string, double)[uniqueCount];
                for (int i = 0; i < uniqueCount; i++)
                {
                    int answerCount = 0;
                    for (int j = 0; j < count; j++)
                    {
                        if (allAnswers[j] == uniqueAnswers[i])
                        {
                            answerCount++;
                        }
                    }
                    result[i] = (uniqueAnswers[i], (double)answerCount / count * 100);
                }

                return result;
            }

        }
    }
}
