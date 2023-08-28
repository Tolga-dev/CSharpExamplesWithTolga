
/*
In C#, covariance and contravariance enable implicit reference conversion 
for array types,
    delegate types,
    and generic type arguments.
*/

namespace AdvancedCsGenerics
{
    
    internal interface IAnimal
    {}
    
    public class Animal : IAnimal
    {}
    
    public class Dog : Animal
    {}
    public class Cat : Animal
    {}
    
    //Covariance allows a more derived (more specific) type to be treated as its less derived (less specific) base type. 
    public class Covariance
    {
        public void Foo()
        {
            IEnumerable<Animal> adoptedAnimals = new List<Dog>();
            
            IEnumerable<Animal> willAdoptedAnimals = new List<Cat>();
//            IEnumerable<Dog> dogs = new List<Animal>(); error! 
            adoptedAnimals = willAdoptedAnimals;
        }

        public void Bar()
        {
            IEnumerable<string> strings = new List<string>() { "abs", "kml" };
            IEnumerable<object> objects = strings;
            foreach (var item in objects)
            {
                Console.WriteLine(item);
            }
        }
    }    
}

namespace Contravariance
{
    internal interface IDataWriter<in T>
    {
        void Write(T data);
    }
    internal class Animal{}
    internal class Dog : Animal{}

    internal class AnimalWrite : IDataWriter<Animal>
    {
        public void Write(Animal data)
        {
            Console.WriteLine("Writing data {0}", data);
        }
    }

    internal class Contravariance
    {
        private void Foo()
        {
            IDataWriter<Animal> animal = new AnimalWrite();
            IDataWriter<Dog> dogWriter = animal;
            
            dogWriter.Write(new Dog());
        }

        public void Runner()
        {
            Foo();            
        }
        
    }

}

// regarding the next example, an application that demonstrates the concepts discussed in the file
namespace Exercise
{
    public abstract class Unit
    {
        public abstract string Type();
    }
    // covariant type
    public interface IUnitProcessor<T> where T : Unit
    {
        T Process(T shape);
    }

    public class PlayerProcessor : IUnitProcessor<Player>
    {
        public Player Process(Player unit)
        {
            return new Player("Player");
        }
    }
    public class EnemyProcessor : IUnitProcessor<Enemy>
    {
        public Enemy Process(Enemy unit)
        {
            return new Enemy("Player");
        }
    }
    
    public class Player : Unit
    {
        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
        }

        public override string Type()
        {
            return Name;
        }
    }
    public class Enemy : Unit
    {
        public string Name { get; set; }

        public Enemy(string name)
        {
            Name = name;
        }

        public override string Type()
        {
            return Name;
        }
    }

    public class RunnerClass
    {
        
        public static List<T> ProcessShapes<T>(IEnumerable<T> units, IUnitProcessor<T> processor) where T : Unit
        {
            return units.Select(processor.Process).ToList();
        }
        
        public void Runner()
        {
            var players = new List<Player>()
            {
                new Player("Player 1"),
                new Player("Player 2")
            };
            var enemies = new List<Enemy>()
            {
                new Enemy("Enemy 1"),
                new Enemy("Enemy 2")
            };

            IUnitProcessor<Enemy> enemyProcessor = new EnemyProcessor();
            IUnitProcessor<Player> playerProcessor = new PlayerProcessor();
            
            var playersList = ProcessShapes(players, playerProcessor);
            var enemiesList = ProcessShapes(enemies, enemyProcessor);
            
            foreach (var player in playersList)
            {
                Console.WriteLine($" {player.Name}");
            }
            foreach (var enemy in enemiesList)
            {
                Console.WriteLine($"{enemy.Name}");
            }


            
        }
    }
    
}