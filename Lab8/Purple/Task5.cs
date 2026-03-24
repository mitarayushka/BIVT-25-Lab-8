using System.ComponentModel.DataAnnotations;

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
                int counter = 0;

                if (questionNumber == 1)
                {
                    foreach (var response in responses)
                    {
                        if (response._animal == null) continue;
                        if (response._animal == _animal)
                            counter++;
                    }
                }
                if (questionNumber == 2)
                {
                    foreach (var response in responses)
                    {
                        if (response._characterTrait == null) continue;
                        if (response._characterTrait == _characterTrait)
                            counter++;
                    }
                }
                if (questionNumber == 3)
                {
                    foreach (var response in responses)
                    {
                        if (response._concept == null) continue;
                        if (response._concept == _concept)
                            counter++;
                    }
                }
                return counter;
            }

            public void Print() { }
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
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[^1] = new Response(answers[0], answers[1], answers[2]);
            }

            private string[] SortedQuationByPopulalarAnswersAndChooseFivePeopleOrLess(string[] array)
            {
                // 1. Создаем копию массива
                string[] copyArray = new string[array.Length];
                // 2. Удаляем null из массива
                array = array.Where(x => x != null).ToArray();
                Array.Copy(array, copyArray, array.Length);
                string[] copyArrayWithOutNull = copyArray.Where(x => x != null).ToArray();

                // 3. Добавим в конце каждого элемента, # для разделения и число повторов.
                for (int i = 0; i < copyArrayWithOutNull.Length; i++)
                {
                    copyArrayWithOutNull[i] += "#" + array.Count(x => x == array[i]);
                }

                // 4. Сортируем массив по убыванию количества повторов
                var sortedArray = copyArrayWithOutNull.OrderByDescending(x => { var parts = x.Split('#'); return int.Parse(parts[^1]); }).ToArray();

                // 5. Удаляем раннее добавленные # и число повторов

                var cleaned = new string[sortedArray.Length];
                for (int i = 0; i < cleaned.Length; i++)
                {
                    string el = sortedArray[i];
                    cleaned[i] = el.Substring(0, el.LastIndexOf("#"));
                }

                // 6. Удаляем повторяющиеся элементы. 
                var sortedArrayWithDeleteRepetitions = cleaned.Distinct().ToArray();

                // 7. Делаем обрез массива до 5 самых популярных если длина позволяет иначе возвращаем все элементы
                if (sortedArrayWithDeleteRepetitions.Length >= 5)
                {
                    string[] resArray = new string[5];
                    for (int i = 0; i < resArray.Length; i++)
                    {
                        resArray[i] = sortedArrayWithDeleteRepetitions[i];
                    }
                    return resArray;
                }

                return sortedArrayWithDeleteRepetitions;
            }
            public string[] GetTopResponses(int question)
            {
                string[] answers = new string[5];
                if (question == 1)
                {
                    string[] animalArray = new string[_responses.Length];
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        animalArray[i] = _responses[i].Animal;
                    }
                    string[] sortedByPopularAnswerAndChooseFivePeopleOrLessArray = SortedQuationByPopulalarAnswersAndChooseFivePeopleOrLess(animalArray);
                    return sortedByPopularAnswerAndChooseFivePeopleOrLessArray;
                }
                if (question == 2)
                {
                    string[] characterTraitArray = new string[_responses.Length];
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        characterTraitArray[i] = _responses[i].CharacterTrait;
                    }
                    string[] sortedByPopularAnswerAndChooseFivePeopleOrLessArray = SortedQuationByPopulalarAnswersAndChooseFivePeopleOrLess(characterTraitArray);
                    return sortedByPopularAnswerAndChooseFivePeopleOrLessArray;
                }
                if (question == 3)
                {
                    string[] conceptArray = new string[_responses.Length];
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        conceptArray[i] = _responses[i].Concept;
                    }
                    string[] sortedByPopularAnswerAndChooseFivePeopleOrLessArray = SortedQuationByPopulalarAnswersAndChooseFivePeopleOrLess(conceptArray);
                    return sortedByPopularAnswerAndChooseFivePeopleOrLessArray;

                }
                return answers;
            }

            public void Print() { }
        }
        public class Report
        {
            private Research[] _researches;
            private static int _numberFutureExp;

            public Research[] Researches => _researches;

            static Report()
            {
                _numberFutureExp = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }

            public Research MakeResearch()
            {
                DateTime report = DateTime.Now;
                Research research = new Research($"No_{_numberFutureExp}_{report.Month:D2}/{report.Year % 100}");
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[^1] = research;
                _numberFutureExp++;
                return research;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                double allNoNullAnswersByOneQuestion = 0;
                string[] answers = new string[0];
                foreach (var research in _researches)
                {
                    foreach (var response in research.Responses)
                    {
                        if (question == 1)
                        {
                            if (response.Animal != null)
                            {

                                allNoNullAnswersByOneQuestion++;
                                Array.Resize(ref answers, answers.Length + 1);
                                answers[answers.Length - 1] = response.Animal;
                            }

                        }
                        else if (question == 2)
                        {
                            if (response.CharacterTrait != null)
                            {
                                allNoNullAnswersByOneQuestion++;
                                Array.Resize(ref answers, answers.Length + 1);
                                answers[answers.Length - 1] = response.CharacterTrait;
                            }

                        }
                        else if (question == 3)
                        {
                            if (response.Concept != null)
                            {
                                allNoNullAnswersByOneQuestion++;
                                Array.Resize(ref answers, answers.Length + 1);
                                answers[answers.Length - 1] = response.Concept;
                            }

                        }
                    }
                }

                answers = answers.Distinct().ToArray();
                int[] numOfAnswers = new int[answers.Length];
                foreach (var research in _researches)
                {
                    foreach (var response in research.Responses)
                    {
                        for (int i = 0; i < answers.Length; i++)
                        {
                            if (question == 1)
                            {
                                if (response.Animal != null && response.Animal == answers[i])
                                {
                                    numOfAnswers[i]++;
                                }
                            }
                            if (question == 2)
                            {
                                if (response.CharacterTrait != null && response.CharacterTrait == answers[i])
                                {
                                    numOfAnswers[i]++;
                                }
                            }
                            if (question == 3)
                            {
                                if (response.Concept != null && response.Concept == answers[i])
                                {
                                    numOfAnswers[i]++;
                                }
                            }
                        }
                    }
                }

                (string, double)[] res = new (string, double)[answers.Length];
                for (int i = 0; i < res.Length; i++)
                {
                    double percent = numOfAnswers[i] * 100.0 / allNoNullAnswersByOneQuestion;
                    res[i] = (answers[i], percent);
                }
                return res;
            }
        }
    }
}
