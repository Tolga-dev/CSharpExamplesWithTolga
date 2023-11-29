using System.Collections.Specialized;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using Delegates.E0;
using Delegates.E1;


namespace Delegates
{
    namespace E0
    {
        
        public class Delegate
        {
            private delegate void DelegateFunction(int a);

            private void FooDel(DelegateFunction delegateFunction)
            {
                delegateFunction(31);
            }

            private void Foo(int a)
            {
                Console.WriteLine("Foo " + a);                   
            }

            private void Bar(int b)
            {
                Console.WriteLine("BAR " + b);
            }
            public void Runner()
            {
                FooDel(Foo);
                FooDel(Bar);
            }
            
        }
    }
    namespace E1
    {
        public class Delegate
        {
            private delegate T Foo<T>(T val) where T : INumber<T>;
            private delegate int MultiDel(int n);
            
            private static int Multi(int n)
            {
                Console.WriteLine(n);
                return n;
            }
            
            private static T FooFunction<T>(T val)
            {
                return val;
            }
            private T FooFunctionNonStatic<T>(T val) // same behaviour with static
            {
                return val;
            }
            
            public void Runner()
            {
                /*
                {
                    Foo<int> handler = FooFunction<int>;
                    Foo<float> handler2 = FooFunction<float>;

                    Console.WriteLine(FooFunction<Foo<int>>(handler)(31));

    //                Console.WriteLine(handler(31)); // 31

                }
                */

                /*
                {
                    Foo<int> handler = FooFunction<int>;
                    Foo<int> handler2 = FooFunction<int>;
                    Foo<int> handler3 = handler + handler2;
                    
                    Console.WriteLine(handler(31)); // 31
                    
                }
                */
                {
                    MultiDel foo = Multi;
                    MultiDel bar = Multi;
                    MultiDel sum = foo + bar;
                    Console.WriteLine(sum(31)); // 31 31 31
                }
                
            }

        }
        
    }
    namespace E2
    {

        public class Delegate
        {
            private delegate T Foo<T>(T val) where T : INumber<T>;

            private static T FooFunction<T>(T val)
            {
                return val;
            }

            public void Runner()
            {
                // Instantiate Del by using a lambda expression.
                Foo<int> del4 = name => 5;
                

            }
        }
    }
    namespace E3
    {
        public class PlayerStats
        {
            public int kill;
            public string name= "";
            public int capturedFlag;
        }

        public class GameOverState
        {
            private delegate int ScoreDel(PlayerStats stats);

            private void GameOver(PlayerStats[] players)
            {
                var playerMostKill = GetPlayerNameTopScore(players, stats => stats.kill);
                var playerMostCapturedFlag = GetPlayerNameTopScore(players, stats => stats.capturedFlag);
                
            }

            private string GetPlayerNameTopScore(PlayerStats[] players, ScoreDel scoreDel)
            {
                var name = "";
                var bestScore = 0;

                foreach (var stat in players)
                {
                    var score = scoreDel(stat);
                    if (score <= bestScore) continue;
                    bestScore = score;
                    name = stat.name;
                }

                return name;
            }

        }
        
    }
    namespace E5
    {
        public delegate void PlayerAction();
        
        public class Delegate
        {
            
            public event PlayerAction onDefence;
            public event PlayerAction onAttack;
            
            private void Defence()
            {
                Console.WriteLine("defence");
                onDefence?.Invoke();
            }

            private void Attack()
            {
                Console.WriteLine("attack");
                onAttack?.Invoke();   
            }
            
            public void Runner()
            {
                void attack() => Console.WriteLine("Play ATTACK sound");
                void defence() => Console.WriteLine("Play Defence sound");

                onDefence += defence;
                onAttack += attack;
                
                Defence();
                Attack();
                onAttack -= attack;
                Attack();
            }
            
        }
    }
    namespace E4
    {
        public delegate void PlayerOnMagicAction();

        public class Delegate
        {

            public event PlayerOnMagicAction onMagic;
            private int _health = 100;

            private void OnMagic()
            {
                Console.WriteLine("On Magic");

                onMagic?.Invoke();
            }

            private void healthDarkMagic(int a)
            {
                Console.WriteLine("Get dark life");
                _health -= a;

            }

            private void healthBoostMagic(int a)
            {
                Console.WriteLine("Get extra life");
                _health += a;
            }


            public void Runner()
            {
                Console.WriteLine(_health);
                PlayerOnMagicAction darkAction = delegate { healthDarkMagic(10); };
                PlayerOnMagicAction boostAction = delegate { healthBoostMagic(10); };

                onMagic += darkAction;
                OnMagic();
                onMagic -= darkAction;
                OnMagic();
                onMagic += boostAction;
                OnMagic();
                onMagic -= boostAction;
                OnMagic();
            }

        }
    }
    namespace E6
    {
        public interface IStat
        {
            public int health { get; set;}
            int GetHealth();
        }
        public class Player : IStat
        {
            public int health { get; set; }

            public int GetHealth()
            {
                return health;
            }
        }

        public class Enemy : IStat
        {
            public int health { get; set; }

            public int GetHealth()
            {
                return health;
            }
        }
        
        public delegate void PlayerOnMagicAction(IStat entity, int a);

        public class Delegate // action specific classtaki can i azalticak, ve eventle yazdiralacak
        {
            public event PlayerOnMagicAction healthEvent;

            public void HealthController(IStat stat, int i)
            {
                healthEvent?.Invoke(stat, i);
            }
            
            public void Runner()
            {
                IStat player = new Player();
                IStat enemy = new Enemy();

                PlayerOnMagicAction darkAction = (IStat entity, int a) =>
                {
                    entity.health -= a;
                    Console.WriteLine("{0}, {1}", entity, a);
                };

                healthEvent += darkAction;

                HealthController(player, 10);
                HealthController(enemy, 10);

                healthEvent -= darkAction;
                
                HealthController(player, 10);
                HealthController(enemy, 10);
                
            }

        }
        
        public class Delegate2  
        {
            public event Action<IStat, int> scoreUpdatedEntity;

            private void UpdateScore(IStat entity, int Score)
            {
                entity.health += Score;
                scoreUpdatedEntity?.Invoke(entity, Score);
            }
            private void OnScoreUpdated(IStat entity, int newScore)
            {
                Console.WriteLine($"UI: {entity}'s score updated to {newScore}");
            }

            public void Runner()
            {
                IStat player = new Player();
                IStat enemy = new Enemy();
                
                scoreUpdatedEntity += OnScoreUpdated;
                
                UpdateScore(player,10);
                UpdateScore(player,10);
                UpdateScore(enemy,10);

                scoreUpdatedEntity -= OnScoreUpdated;
                UpdateScore(enemy,10);



            }
            
            

        }

    }
    


}


public class Program
{

    static void Main(string[] args)
    {
        
//        Delegates.E2.Delegate m = new Delegates.E2.Delegate();
//        m.Runner();
        Delegates.E6.Delegate2 m = new Delegates.E6.Delegate2();
        m.Runner();

    }
}