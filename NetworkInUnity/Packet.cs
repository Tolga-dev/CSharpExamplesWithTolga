using System.Text;

namespace NetworkInUnity;

public class Packet : IDisposable
{
    private List<byte> Buffer { get; } = new List<byte>(); 
    
    private byte[]? _readBuffer;
    private int _readPos;
    private bool _buffUpdated;
    
    
    public int Length() => Buffer.Count - _readPos;
    public void Clear()
    {
        Buffer.Clear();
        _readPos = 0;
    }
    
    #region Writing

    public void Write(byte[] input)
    {
        Buffer.AddRange(input);
        _buffUpdated = true;
    }
    
    public void Write<T>(T input)
    {
        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Int16: // short
                Buffer.AddRange(BitConverter.GetBytes((short)(object)input!));
                break;
            case TypeCode.Int32: // integer 
                Buffer.AddRange(BitConverter.GetBytes((int)(object)input!));
                break;
            case TypeCode.Double: // long
                Buffer.AddRange(BitConverter.GetBytes((long)(object)input!));
                break;
            case TypeCode.Single: // float
                Buffer.AddRange(BitConverter.GetBytes((float)(object)input!));
                break;
            case TypeCode.String:
                var chars = (string)(object)input!;
                Buffer.AddRange(BitConverter.GetBytes(chars.Length));
                Buffer.AddRange(Encoding.ASCII.GetBytes(chars));
                break;
        }
        _buffUpdated = true;

    }
    
    #endregion

    #region Read

    
    public byte[] Read(int length, bool peek = true)
    {
        if (_buffUpdated)
        {
            _readBuffer = Buffer.ToArray();
            _buffUpdated = false;
        }

        var ret = Buffer.GetRange(_readPos, length).ToArray();

        if (peek) _readPos += length;

        return ret;

    }

    
    public T? Read<T>(bool peek = true)
    {
        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Int32: // integer 
                if (Buffer.Count > _readPos)
                {
                    if (_buffUpdated)
                    {
                        _readBuffer = Buffer.ToArray();
                        _buffUpdated = false;
                    }

                    var ret = BitConverter.ToInt32(_readBuffer!, _readPos);
                    if (peek & Buffer.Count > _readPos)
                        _readPos += sizeof(int);
                    return (T)(object)ret;
                }
                else
                    throw new Exception("Byte buffer is exceed!");
                
            case TypeCode.Int16: // short 
                if (Buffer.Count > _readPos)
                {
                    if (_buffUpdated)
                    {
                        _readBuffer = Buffer.ToArray();
                        _buffUpdated = false;
                    }

                    var ret = BitConverter.ToInt16(_readBuffer!, _readPos);
                    if (peek & Buffer.Count > _readPos)
                        _readPos += sizeof(short);
                    return (T)(object)ret;
                }
                else
                    throw new Exception("Byte buffer is exceed!");
                
            case TypeCode.Single: // float 
                if (Buffer.Count > _readPos)
                {
                    if (_buffUpdated)
                    {
                        _readBuffer = Buffer.ToArray();
                        _buffUpdated = false;
                    }

                    var ret = BitConverter.ToSingle(_readBuffer!, _readPos);
                    if (peek & Buffer.Count > _readPos)
                        _readPos += sizeof(float);
                    return (T)(object)ret;
                }
                else
                    throw new Exception("Byte buffer is exceed!");
                
            case TypeCode.Int64: // long
                if (Buffer.Count > _readPos)
                {
                    if (_buffUpdated)
                    {
                        _readBuffer = Buffer.ToArray();
                        _buffUpdated = false;
                    }

                    var ret = BitConverter.ToInt64(_readBuffer!, _readPos);
                    if (peek & Buffer.Count > _readPos)
                        _readPos += sizeof(long);
                    return (T)(object)ret;
                }
                else
                    throw new Exception("Byte buffer is exceed!");

            case TypeCode.String:
                var length = Read<int>();
                if (_buffUpdated)
                {
                    _readBuffer = Buffer.ToArray();
                    _buffUpdated = false;
                }
                var retString = Encoding.ASCII.GetString(_readBuffer!, _readPos, length);
                if ((peek & Buffer.Count > _readPos) && retString.Length > 0)
                    _readPos += length;
                
                return (T)(object)retString;
        }

        return default(T);
    }

    #endregion

    #region Dispose

    private bool _disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if(disposing)
                Buffer.Clear();
            _readPos = 0;
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    

    #endregion
    
    
}