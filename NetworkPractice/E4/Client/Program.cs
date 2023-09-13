using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Text.Json;
using NetworkPractice.E4.Server;

namespace NetworkPractice.E4.Client;

public class Program
{
	public static void Runner()
	{
		
		try
		{
			TcpClient client = new TcpClient("127.0.0.1", 8052);
			Console.WriteLine("Connected to server.");
			NetworkStream stream = client.GetStream();
			Console.Write("Enter your name: ");
			string clientName = Console.ReadLine();
			SendInitialName(stream, clientName);
			
			// Start a thread to receive and display messages from the server
			var receiveThread = new System.Threading.Thread(() =>
			{
				while (true)
				{
					byte[] bytes = new byte[1024];
					int bytesRead = stream.Read(bytes, 0, bytes.Length);
					string message = Encoding.ASCII.GetString(bytes, 0, bytesRead);
					Console.WriteLine(message);
				}
			});

			var sendThread = new System.Threading.Thread(
				() =>
				{
					// Allow the user to send messages to the server
					while (true)
					{
						string message = Console.ReadLine();
						SendMessage(stream, clientName, message);
						Thread.Sleep(1000);

						Vector3 original = new Vector3(0, 1, 1);
						SerializedVector3 serializedVector3 = new SerializedVector3(original);
						string positionJson = serializedVector3.ToJson();
						Console.WriteLine(positionJson);
						SendMessage(stream, clientName,positionJson);

					}

				});
			
			receiveThread.Start();
			sendThread.Start();
			
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	static void SendInitialName(NetworkStream stream, string name)
	{
		byte[] nameBytes = Encoding.ASCII.GetBytes(name);
		stream.Write(nameBytes, 0, nameBytes.Length);
	}

	static void SendMessage(NetworkStream stream, string sender, string message)
	{
		string fullMessage = $"{sender}: {message}";
		byte[] messageBytes = Encoding.ASCII.GetBytes(fullMessage);
		stream.Write(messageBytes, 0, messageBytes.Length);
	}

	
}

[Serializable]
public class SerializedVector3
{
	public float X { get; set; }
	public float Y { get; set; }
	public float Z { get; set; }

	public SerializedVector3() { }

	public SerializedVector3(Vector3 vector3)
	{
		X = vector3.X;
		Y = vector3.Y;
		Z = vector3.Z;
	}

	public Vector3 ToVector3()
	{
		return new Vector3(X, Y, Z);
	}

	public string ToJson()
	{
		return "VECTOR3:"+JsonSerializer.Serialize(this);
	}

	public static SerializableVector3? FromJson(string json)
	{
		return JsonSerializer.Deserialize<SerializableVector3>(json);
	}

}