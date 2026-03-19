using System;
using System.Linq;
using static Lab8.Purple.Task5;

namespace Lab8.Purple
{
    public class Task5
    {
        public struct Response
            {
                // Animal, CharacterTrait, Concept.
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
          
            // метод который считает, сколько ответов на выбранный вопрос(от 1 до 3) из списка анкет совпадает с текущей анкетой
            public int CountVotes(Response[] responses, int questionNumber)
                {
                    int count = 0;
                    if (questionNumber == 1)
                    {
                        for (int i = 0; i < responses.Length; i++)
                        {
                            if (_animal == responses[i]._animal)
                            {
                                count++;
                            }
                        }
                    }
                    if (questionNumber == 2)
                    {
                        for (int i = 0; i < responses.Length; i++)
                        {
                            if (_charactertrait == responses[i]._charactertrait)
                            {
                                count++;
                            }
                        }
                    }
                    if (questionNumber == 3)
                    {
                        for (int i = 0; i < responses.Length; i++)
                        {
                            if (_concept == responses[i]._concept)
                            {
                                count++;
                            }
                        }
                    }
                    return count;
                }
                public void Print()
                {
                    Console.WriteLine(_animal);
                }
            }
            public struct Research
            {
                //  Name, Responses. 
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
                    if (answers == null || answers.Length != 3) return;
                    Array.Resize(ref _responses, _responses.Length + 1);
                    _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
                }
                //который возвращает до 5 наиболее часто встречающихся ответов по указанному вопросу.
                public string[] GetTopResponses(int question)
                {
                    string[] result = new string[5];
                    if (question == 1)
                    {
                        // новый массив для ответов 
                        string[] top = new string[0];

                        for (int i = 0; i < _responses.Length; i++)
                        {
                            if (_responses[i].Animal != null)
                            {
                                Array.Resize(ref top, top.Length + 1);
                                top[top.Length - 1] = _responses[i].Animal;
                            }
                        }
                        // массив для подсчета сколько раз отвечади также
                        int[] count = new int[top.Length];
                        for (int i = 0; i < count.Length; i++)
                        {
                            if (_responses[i].Animal != null)
                            {
                                count[i] = _responses[i].CountVotes(_responses, question);
                            }
                        }
                        // цикл для замены похожих 
                        for (int i = 0; i < count.Length; i++)
                        {
                            for (int j = 0; j < count.Length; j++)
                            {
                                if (i != j & top[i] == top[j])
                                {
                                    top[j] = null;
                                    count[j] = -10;
                                }
                            }
                        }
                        int k = 0;
                        // подсчитываем сколкьо всего там разных ответов
                        for (int i = 0; i < count.Length; i++)
                        {
                            if (top[i] != null)
                            {
                                k++;
                            }
                        }
                        // создаем новые массивы где будут только различные без повторяющихся
                        string[] top2 = new string[k];
                        int[] count2 = new int[k];
                        int c = 0;
                        for (int i = 0; i < top.Length; i++)
                        {
                            if (top[i] != null)
                            {
                                top2[c] = top[i];
                                count2[c] = count[i];
                                c++;
                            }
                        }
                        // сортирую по количеству два массива
                        for (int i = 0; i < count2.Length; i++)
                        {
                            for (int j = 1; j < count2.Length; j++)
                            {
                                if (count2[j] > count2[j - 1])
                                {
                                    (top2[j], top2[j - 1]) = (top2[j - 1], top2[j]);
                                    (count2[j], count2[j - 1]) = (count2[j - 1], count2[j]);
                                }
                            }
                        }
                        if (top2.Length < 5)
                        {
                            result = new string[top2.Length];
                        }
                        for (int i = 0; i < result.Length; i++)
                        {
                            result[i] = top2[i];
                        }
                    }
                    if (question == 2)
                    {
                        string[] top = new string[0];

                        for (int i = 0; i < _responses.Length; i++)
                        {
                            if (_responses[i].Animal != null)
                            {
                                Array.Resize(ref top, top.Length + 1);
                                top[top.Length - 1] = _responses[i].CharacterTrait;
                            }
                        }
                        int[] count = new int[top.Length];
                        for (int i = 0; i < count.Length; i++)
                        {
                            if (_responses[i].Animal != null)
                            {
                                count[i] = _responses[i].CountVotes(_responses, question);
                            }
                        }
                        for (int i = 0; i < count.Length; i++)
                        {
                            for (int j = 0; j < count.Length; j++)
                            {
                                if (i != j & top[i] == top[j])
                                {
                                    top[j] = null;
                                    count[j] = -10;
                                }
                            }
                        }
                        int k = 0;
                        for (int i = 0; i < count.Length; i++)
                        {
                            if (top[i] != null)
                            {
                                k++;
                            }
                        }
                        string[] top2 = new string[k];
                        int[] count2 = new int[k];
                        int c = 0;
                        for (int i = 0; i < top.Length; i++)
                        {
                            if (top[i] != null)
                            {
                                top2[c] = top[i];
                                count2[c] = count[i];
                                c++;
                            }
                        }
                        for (int i = 0; i < count2.Length; i++)
                        {
                            for (int j = 1; j < count2.Length; j++)
                            {
                                if (count2[j] > count2[j - 1])
                                {
                                    (top2[j], top2[j - 1]) = (top2[j - 1], top2[j]);
                                    (count2[j], count2[j - 1]) = (count2[j - 1], count2[j]);
                                }
                            }
                        }
                        if (top2.Length < 5)
                        {
                            result = new string[top2.Length];
                        }
                        for (int i = 0; i < result.Length; i++)
                        {
                            result[i] = top2[i];
                        }
                    }
                    if (question == 3)
                    {
                        string[] top = new string[0];

                        for (int i = 0; i < _responses.Length; i++)
                        {
                            if (_responses[i].Animal != null)
                            {
                                Array.Resize(ref top, top.Length + 1);
                                top[top.Length - 1] = _responses[i].Concept;
                            }
                        }
                        int[] count = new int[top.Length];
                        for (int i = 0; i < count.Length; i++)
                        {
                            if (_responses[i].Animal != null)
                            {
                                count[i] = _responses[i].CountVotes(_responses, question);
                            }
                        }
                        for (int i = 0; i < count.Length; i++)
                        {
                            for (int j = 0; j < count.Length; j++)
                            {
                                if (i != j & top[i] == top[j])
                                {
                                    top[j] = null;
                                    count[j] = -10;
                                }
                            }
                        }
                        int k = 0;
                        for (int i = 0; i < count.Length; i++)
                        {
                            if (top[i] != null)
                            {
                                k++;
                            }
                        }
                        string[] top2 = new string[k];
                        int[] count2 = new int[k];
                        int c = 0;
                        for (int i = 0; i < top.Length; i++)
                        {
                            if (top[i] != null)
                            {
                                top2[c] = top[i];
                                count2[c] = count[i];
                                c++;
                            }
                        }
                        for (int i = 0; i < count2.Length; i++)
                        {
                            for (int j = 1; j < count2.Length; j++)
                            {
                                if (count2[j] > count2[j - 1])
                                {
                                    (top2[j], top2[j - 1]) = (top2[j - 1], top2[j]);
                                    (count2[j], count2[j - 1]) = (count2[j - 1], count2[j]);
                                }
                            }
                        }
                        if (top2.Length < 5)
                        {
                            result = new string[top2.Length];
                        }
                        for (int i = 0; i < result.Length; i++)
                        {
                            result[i] = top2[i];
                        }
                    }
                    return result;

                }

                public void Print()
                {
                    Console.WriteLine(_name);

                }


            }

            public class Report
        {
            // с полем для массива исследований и уникальным полем для нумерации будущих исследований.
            // Researches
            private Research[] _researches;
            private static int _count;

            // Создать в классе Report свойство Researches для чтения поля исследований
            public Research[] Researches => _researches;


            //Статический конструктор должен инициализировать нумерацию исследований с единицы.
            static Report()
            {
                _count = 1;

            }


            //Конструктор должен инициализировать пустой массив исследований.
            public Report()
            {
                _researches = new Research[0];
            }
            // который начинает новое исследование и называет его в следующем виде: No_X_MM/YY, где X – порядковый номер
            //исследования, MM – месяц создания отчета(брать из DateTime) YY – последние 2 цифры года
            //создания отчета(брать из DateTime). Исследование добавлять в список и возвращать на выходе из метода.
            public Research MakeResearch()
            {
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = new Research("No_" + _count + "_" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                _count++;
                return _researches[_researches.Length - 1];
            }

            // массив кортежей данных, собранный среди всех исследований по выбранному вопросу, в виде: ответ + % этих ответов от общего количества(непустых) ответов.
            public (string, double)[] GetGeneralReport(int question)
            {

                string[] otvet = new string[0];

                int k1 = 0;
                for (int i = 0; i < _researches.Length; i++)
                {
                    for (int j = 0; j < _researches[i].Responses.Length; j++)
                    {
                        k1++;
                    }
                }
                Response[] r = new Response[k1];
                k1 = 0;
                for (int i = 0; i < _researches.Length; i++)
                {
                    for (int j = 0; j < _researches[i].Responses.Length; j++)
                    {
                        r[k1++] = _researches[i].Responses[j];
                    }
                }

                // хранятся различные ответы 
                GetTopResponses(question, r, ref otvet);

                (string, double)[] ans = new (string, double)[otvet.Length];
                // теперь нужно подсчитать сколько всего не null ответов 
                // подсчитать сколько ответов одного варианта
                double[] colvo = new double[otvet.Length];
                int k = 0;

                for (int i = 0; i < otvet.Length; i++)
                {
                    // сколько ответов одного варианта во все массиве 
                    colvo[i] = CountVotes(r, question, otvet[i]);
                }

                double itog = 0;
                //считает все не null
                for (int i = 0; i < r.Length; i++)
                {


                    if (question == 1)
                    {
                        if (r[i].Animal != null)
                        {
                            itog++;
                        }
                    }
                    if (question == 2)
                    {
                        if (r[i].CharacterTrait != null)
                        {
                            itog++;
                        }
                    }
                    if (question == 3)
                    {
                        if (r[i].Concept != null)
                        {
                            itog++;
                        }
                    }


                }
                for (int i = 0; i < ans.Length; i++)
                {
                    ans[i] = (otvet[i], (colvo[i] / itog) * 100);
                }

                return ans;

            }
            private string[] GetTopResponses(int question, Response[] _responses, ref string[] top2)
            {

                if (question == 1)
                {
                    // новый массив для ответов 
                    string[] top = new string[0];

                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].Animal != null)
                        {
                            Array.Resize(ref top, top.Length + 1);
                            top[top.Length - 1] = _responses[i].Animal;
                        }
                    }
                    // массив для подсчета сколько раз отвечади также
                    int[] count = new int[top.Length];
                    for (int i = 0; i < count.Length; i++)
                    {
                        if (_responses[i].Animal != null)
                        {
                            count[i] = _responses[i].CountVotes(_responses, question);
                        }
                    }
                    // цикл для замены похожих 
                    for (int i = 0; i < count.Length; i++)
                    {
                        for (int j = 0; j < count.Length; j++)
                        {
                            if (i != j & top[i] == top[j])
                            {
                                top[j] = null;
                                count[j] = -10;
                            }
                        }
                    }
                    int k = 0;
                    // подсчитываем сколкьо всего там разных ответов
                    for (int i = 0; i < count.Length; i++)
                    {
                        if (top[i] != null)
                        {
                            k++;
                        }
                    }
                    // создаем новые массивы где будут только различные без повторяющихся
                    top2 = new string[k];
                    int[] count2 = new int[k];
                    int c = 0;
                    for (int i = 0; i < top.Length; i++)
                    {
                        if (top[i] != null)
                        {
                            top2[c] = top[i];
                            count2[c] = count[i];
                            c++;
                        }
                    }


                }
                if (question == 2)
                {
                    string[] top = new string[0];

                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].CharacterTrait != null)
                        {
                            Array.Resize(ref top, top.Length + 1);
                            top[top.Length - 1] = _responses[i].CharacterTrait;
                        }
                    }
                    int[] count = new int[top.Length];
                    for (int i = 0; i < count.Length; i++)
                    {
                        if (_responses[i].Animal != null)
                        {
                            count[i] = _responses[i].CountVotes(_responses, question);
                        }
                    }
                    for (int i = 0; i < count.Length; i++)
                    {
                        for (int j = 0; j < count.Length; j++)
                        {
                            if (i != j & top[i] == top[j])
                            {
                                top[j] = null;
                                count[j] = -10;
                            }
                        }
                    }
                    int k = 0;
                    for (int i = 0; i < count.Length; i++)
                    {
                        if (top[i] != null)
                        {
                            k++;
                        }
                    }
                    top2 = new string[k];
                    int[] count2 = new int[k];
                    int c = 0;
                    for (int i = 0; i < top.Length; i++)
                    {
                        if (top[i] != null)
                        {
                            top2[c] = top[i];
                            count2[c] = count[i];
                            c++;
                        }
                    }


                }
                if (question == 3)
                {
                    string[] top = new string[0];

                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].Concept != null)
                        {
                            Array.Resize(ref top, top.Length + 1);
                            top[top.Length - 1] = _responses[i].Concept;
                        }
                    }
                    int[] count = new int[top.Length];
                    for (int i = 0; i < count.Length; i++)
                    {
                        if (_responses[i].Animal != null)
                        {
                            count[i] = _responses[i].CountVotes(_responses, question);
                        }
                    }
                    for (int i = 0; i < count.Length; i++)
                    {
                        for (int j = 0; j < count.Length; j++)
                        {
                            if (i != j & top[i] == top[j])
                            {
                                top[j] = null;
                                count[j] = -10;
                            }
                        }
                    }
                    int k = 0;
                    for (int i = 0; i < count.Length; i++)
                    {
                        if (top[i] != null)
                        {
                            k++;
                        }
                    }
                    top2 = new string[k];
                    int[] count2 = new int[k];
                    int c = 0;
                    for (int i = 0; i < top.Length; i++)
                    {
                        if (top[i] != null)
                        {
                            top2[c] = top[i];
                            count2[c] = count[i];
                            c++;
                        }
                    }


                }
                return top2;

            }
            private int CountVotes(Response[] responses, int questionNumber, string _responses)
            {
                string _animal = _responses;
                int count = 0;
                if (questionNumber == 1)
                {

                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (_animal == responses[i].Animal)
                        {
                            count++;
                        }
                    }
                }
                if (questionNumber == 2)
                {
                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (_animal == responses[i].CharacterTrait)
                        {
                            count++;
                        }
                    }
                }
                if (questionNumber == 3)
                {
                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (_animal == responses[i].Concept)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
    }
}
