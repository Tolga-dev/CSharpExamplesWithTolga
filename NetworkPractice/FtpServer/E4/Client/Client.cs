using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPractice.FtpServer.E4.Client;

public class Client
{
    private TcpClient _controlClient;
    private TcpListener _passiveListener;

    private NetworkStream _controlStream;
    private StreamReader _controlReader;
    private StreamWriter _controlWriter;

    private string _username;
    private string _transferType;
    public Client(TcpClient client)
    {
        _controlClient = client;

        _controlStream = _controlClient.GetStream();

        _controlReader = new StreamReader(_controlStream);
        _controlWriter = new StreamWriter(_controlStream);
        
        IPAddress localAddress = ((IPEndPoint)_controlClient.Client.LocalEndPoint).Address;

        _passiveListener = new TcpListener(localAddress, 0);
        _passiveListener.Start();
    }

    public void HandleClient(object obj)
    {
        _controlWriter.WriteLine("220 Service Ready.");
        _controlWriter.Flush();
        string line;

        try
        {
            while (!string.IsNullOrEmpty(line = _controlReader.ReadLine()))
            {
                Console.WriteLine(line);
                string response = null;

                string[] command = line.Split(' ');
                foreach (var s in command)
                {
                    Console.WriteLine(s);
                }
                
                string cmd = command[0].ToUpperInvariant();
                Console.WriteLine(cmd);
                
                string arguments = command.Length > 1 ? line.Substring(command[0].Length + 1) : null;
                Console.WriteLine(arguments);

                if (string.IsNullOrWhiteSpace(arguments))
                    arguments = null;
    
                
                if (response == null)
                {
                    switch (cmd)
                    {
                        case "USER":
                            response = User(arguments);
                            break;
                        case "PASS":
                            response = Password(arguments);
                            break;
                        case "CWD":
                            response = ChangeWorkingDirectory(arguments);
                            break;
                        case "CDUP":
                            response = ChangeWorkingDirectory("..");
                            break;
                        case "PWD":
                            response = "257 \"/\" is current directory.";
                            break;
                        case "QUIT":
                            response = "221 Service closing control connection";
                            break;
                        case "TYPE":
                            string[] splitArgs = arguments.Split(' ');
                            response = Type(splitArgs[0], splitArgs.Length > 1 ? splitArgs[1] : null);
                            break;
                        case "PORT":
                            response = Port(arguments);
                            break;
                        case "PASV":
                            response = Passive();
                            break;
                        case "LIST":
                            response = List();
                            break;
                        case "RETR":
                            response = RetrieveFile(arguments);
                            break;
                        
                        
                        default:
                            response = "502 Command not implemented";
                            break;
                    }
                }

                if (_controlClient == null || !_controlClient.Connected)
                {
                    break;
                }
                else
                {
                    _controlWriter.WriteLine(response);
                    _controlWriter.Flush();

                    if (response.StartsWith("221"))
                    {
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    private string Type(string typeCode, string formatControl)
    {
        string response = "500 ERROR";

        switch (typeCode)
        {
            case "A":
            case "I":
                _transferType = typeCode;
                response = "200 OK";
                break;
            case "E":
            case "L":
            default:
                response = "504 Command not implemented for that parameter.";
                break;
        }

        if (formatControl != null)
        {
            switch (formatControl)
            {
                case "N":
                    response = "200 OK";
                    break;
                case "T":
                case "C":
                default:
                    response = "504 Command not implemented for that parameter.";
                    break;
            }
        }

        return response;
    }
    
    #region FTP Commands

    private string User(string username)
    {
        _username = username;

        return "331 Username ok, need password";
    }

    private string Password(string password)
    {
        if (true)
        {
            return "230 User logged in";
        }
        else
        {
            return "530 Not logged in";
        }
    }
    private string RetrieveFile(string fileName)
    {
        try
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            if (File.Exists(filePath))
            {
                _controlWriter.WriteLine("150 Opening data connection for RETR");
                _controlWriter.Flush();

                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        _controlClient.GetStream().Write(buffer, 0, bytesRead);
                    }
                }

                return "226 Transfer complete";
            }
            else
            {
                return "550 File not found";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "550 Error retrieving file";
        }
    }


    private string ChangeWorkingDirectory(string pathname)
    {
        return "250 Changed to new directory";
    }
    
    private string Port(string hostname)
    {
        
        string[] ipAndPort = hostname.Split(',');

        byte[] ipAddress = new byte[4];
        byte[] port = new byte[2];

        for (int i = 0; i < 4; i++)
        {
            ipAddress[i] = Convert.ToByte(ipAndPort[i]);
        }

        for (int i = 4; i < 6; i++)
        {
            port[i - 4] = Convert.ToByte(ipAndPort[i]);
        }

        if (BitConverter.IsLittleEndian)
            Array.Reverse(port);
        
        BitConverter.ToInt16(port, 0);
        
        return "200 Data Connection Established";
    }

    private string Passive()
    {
        
        IPAddress localIp = ((IPEndPoint)_controlClient.Client.LocalEndPoint).Address;

        _passiveListener = new TcpListener(localIp, 0);
        _passiveListener.Start();

        IPEndPoint passiveListenerEndpoint = (IPEndPoint)_passiveListener.LocalEndpoint;

        byte[] address = passiveListenerEndpoint.Address.GetAddressBytes();
        short port = (short)passiveListenerEndpoint.Port;

        byte[] portArray = BitConverter.GetBytes(port);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(portArray);
        
        return string.Format("227 Entering Passive Mode ({0},{1},{2},{3},{4},{5})", address[0], address[1], address[2], address[3], portArray[0], portArray[1]);
    }
    private string List()
    {
        try
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());

            // Combine files and directories and format them
            string fileList = string.Join(Environment.NewLine, files.Concat(dirs).Select(item => Path.GetFileName(item)));

            // Send the directory listing to the client
            using (TcpClient dataClient = _passiveListener.AcceptTcpClient())
            using (NetworkStream dataStream = dataClient.GetStream())
            using (StreamWriter dataWriter = new StreamWriter(dataStream, Encoding.ASCII))
            {
                dataWriter.Write(fileList);
                dataWriter.Flush();
            }

            return "226 Transfer complete";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "550 Error listing directory";
        }
    }


    #endregion
}