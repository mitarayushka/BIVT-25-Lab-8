using System;

namespace Lab8.White
{
    public class Task5
    {
        public struct Match
        {
            private int goals;
            private int misses;

            public int Goals => goals;
            public int Misses => misses;
            public int Difference => goals - misses;
            public int Score
            {
                get
                {
                    if (goals > misses) return 3;
                    if (goals == misses) return 1;
                    return 0;
                }
            }

            public Match(int goals, int misses)
            {
                this.goals = goals;
                this.misses = misses;
            }

            public void Print()
            {
                Console.WriteLine($"Goals: {goals}, Misses: {misses}, Difference: {Difference}, Score: {Score}");
            }
        }

        // Team 改为抽象类
        public abstract class Team
        {
            protected string name;
            protected Match[] matches;
            protected int matchCount;

            public string Name => name;
            public Match[] Matches
            {
                get
                {
                    return matches;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (matches == null || matchCount == 0) return 0;

                    int total = 0;
                    for (int i = 0; i < matchCount; i++)
                    {
                        total += matches[i].Score;
                    }
                    return total;
                }
            }

            public int TotalDifference
            {
                get
                {
                    if (matches == null || matchCount == 0) return 0;

                    int total = 0;
                    for (int i = 0; i < matchCount; i++)
                    {
                        total += matches[i].Difference;
                    }
                    return total;
                }
            }

            public Team(string name)
            {
                this.name = name;
                matches = new Match[10];
                matchCount = 0;
            }

            // PlayMatch 改为虚方法
            public virtual void PlayMatch(int goals, int misses)
            {
                if (matches == null)
                {
                    matches = new Match[10];
                    matchCount = 0;
                }

                if (matchCount >= matches.Length)
                {
                    Array.Resize(ref matches, matches.Length * 2);
                }

                matches[matchCount] = new Match(goals, misses);
                matchCount++;
            }

            public static void SortTeams(Team[] teams)
            {
                if (teams == null) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        bool needSwap = false;

                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            needSwap = true;
                        }
                        else if (teams[j].TotalScore == teams[j + 1].TotalScore)
                        {
                            if (teams[j].TotalDifference < teams[j + 1].TotalDifference)
                            {
                                needSwap = true;
                            }
                        }

                        if (needSwap)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {name}, TotalScore: {TotalScore}, TotalDifference: {TotalDifference}");
            }
        }

        // ManTeam 类
        public class ManTeam : Team
        {
            private ManTeam derby;

            public ManTeam Derby => derby;

            // 构造函数：接受名称和可选的德比队（默认为 null）
            public ManTeam(string name, ManTeam derby = null) : base(name)
            {
                this.derby = derby;
            }

            // 重载的 PlayMatch 方法
            public void PlayMatch(int goals, int misses, ManTeam team)
            {
                // 如果比赛是与德比队进行的
                if (team != null && derby != null && team == derby)
                {
                    // 增加进球数（将进球数加1后调用基类方法）
                    base.PlayMatch(goals + 1, misses);
                }
                else
                {
                    // 普通比赛
                    base.PlayMatch(goals, misses);
                }
            }
        }

        // WomanTeam 类
        public class WomanTeam : Team
        {
            private int[] penalties;
            private int penaltyCount;

            public int[] Penalties
            {
                get
                {
                    if (penalties == null || penaltyCount == 0)
                        return new int[0];

                    int[] result = new int[penaltyCount];
                    Array.Copy(penalties, result, penaltyCount);
                    return result;
                }
            }

            public int TotalPenalties
            {
                get
                {
                    if (penalties == null || penaltyCount == 0)
                        return 0;

                    int total = 0;
                    for (int i = 0; i < penaltyCount; i++)
                    {
                        total += penalties[i];
                    }
                    return total;
                }
            }

            public WomanTeam(string name) : base(name)
            {
                penalties = new int[10];
                penaltyCount = 0;
            }

            // 重写 PlayMatch 方法
            public override void PlayMatch(int goals, int misses)
            {
                // 调用基类的 PlayMatch 记录比赛
                base.PlayMatch(goals, misses);

                // 如果失球数大于进球数，添加罚分
                if (misses > goals)
                {
                    int penalty = misses - goals;

                    // 动态扩展数组
                    if (penaltyCount >= penalties.Length)
                    {
                        Array.Resize(ref penalties, penalties.Length * 2);
                    }

                    penalties[penaltyCount] = penalty;
                    penaltyCount++;
                }
            }
        }
    }
}
