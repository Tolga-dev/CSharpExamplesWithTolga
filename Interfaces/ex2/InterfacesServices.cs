
namespace Interfaces.ex2
{
    public interface IWeatherService
    {
        WeatherData GetWeather(string location);
    }

    public class WeatherData
    {
        public string Location { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Location: {Location}, Description: {Description}";
        }
    }

    public class MapService : IWeatherService
    {
        public WeatherData GetWeather(string location)
        {
            return new WeatherData()
            {
                Location = location,
                Description = "Cloudly"
            };
        }
    }

    public class Program
    {

        public void Runner()
        {
            IWeatherService weatherService = new MapService();
            WeatherData weatherData = weatherService.GetWeather("Turkey");
            Console.WriteLine(weatherData);
        }
    }
}


namespace Interfaces.ex2
{
    
    public interface IDialogueService
    {
        DialogueData GetDialogueData(string data);
    
    }
    
    public class DialogueData
    {
        public string? person1;
        public Dictionary<int, string>? Message { get; set; }

        public DialogueData()
        {
            Message = new Dictionary<int, string>();
        }

        public override string ToString()
        {
            return Message.Aggregate(string.Empty, (current, m) => current + $"[{person1}]: {m.Value}\n");
        }
    }
    
    public class OpenDialogueService : IDialogueService
    {
        private Dictionary<string, Dictionary<int, string>?> dialogueData;
        
        public OpenDialogueService()
        {
            dialogueData = new Dictionary<string, Dictionary<int, string>?>
            {
                {
                    "A", new Dictionary<int, string>
                    {
                        { 1, "A" },
                        { 2, "AB" },
                        { 3, "ABC" },
                        { 4, "ABCD" },
                    }
                },
                {
                    "B", new Dictionary<int, string>
                    {
                        { 1, "Q" },
                        { 2, "QW" },
                        { 3, "QWT" },
                        { 4, "QWTE" },
                    }
                },
            };
        }
        
        public DialogueData GetDialogueData(string name)
        {
            if (!dialogueData.TryGetValue(name, out var messages)) return new DialogueData();
          
            var dialogue = new DialogueData
            {
                person1 = name,
                Message = messages
            };

            return dialogue;

        }
    }
    
    public class RunnerClass
    {
        public void Runner()
        {
            IDialogueService service = new OpenDialogueService();

            DialogueData dialogueDataWithA = service.GetDialogueData("A");
            DialogueData dialogueDataWithB = service.GetDialogueData("B");
            
            Console.WriteLine(dialogueDataWithA);
            Console.WriteLine(dialogueDataWithB);

            const int messageId = 1;
            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine($"[A]: {GetLine(messageId+i,dialogueDataWithA)}");
                Console.WriteLine($"[B]: {GetLine(messageId+i,dialogueDataWithB)}");
            }
            
        }

        private static string GetLine(int id, DialogueData dialogueData)
        {
            return dialogueData.Message.TryGetValue(id, out var specificMessage) ? specificMessage : "not found!";
        }

    }

}


