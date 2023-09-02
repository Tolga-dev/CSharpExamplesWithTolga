namespace NetworkPractice.E1;

public class ProgramE1
{
    private static void Socket()
    {
        var generator = new SocketGenerator();
        generator.Runner("0.0.0.0",8000); //  python3 -m http.server 8000
    }
    
    public void Runner()
    {
        Socket();
    }
}