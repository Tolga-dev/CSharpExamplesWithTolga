using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DefaultNamespace.TcpClients
{
    public class Packet : IDisposable
    {
        public List<byte> Buffer { get; } = new List<byte>();

        private byte[]? _readBuffer;
        private int _readPos;
        private bool _buffUpdated;


        public int Length() => Buffer.Count - _readPos;

        public void Clear()
        {
            Buffer.Clear();
            _readPos = 0;
        }

        public byte[] ToArray() => Buffer.ToArray();

        public int Count()
        {
            return Buffer.Count;
        }

        #region Writing

        public void WriteByte(byte[] input)
        {
            Buffer.AddRange(input);
            _buffUpdated = true;
        }

        private void WriteShort(short value)
        {
            Console.WriteLine("Short");
            Buffer.AddRange(BitConverter.GetBytes(value));
            _buffUpdated = true;
        }

        private void WriteInt(int value)
        {
            Console.WriteLine("Int");
            Buffer.AddRange(BitConverter.GetBytes(value));
            _buffUpdated = true;
        }

        private void WriteLong(long value)
        {
            Console.WriteLine("Long");
            Buffer.AddRange(BitConverter.GetBytes(value));
            _buffUpdated = true;
        }

        private void WriteFloat(float value)
        {
            Console.WriteLine("Float");
            Buffer.AddRange(BitConverter.GetBytes(value));
            _buffUpdated = true;
        }

        private void WriteString(string value)
        {
            Console.WriteLine("String");
            Buffer.AddRange(BitConverter.GetBytes(value.Length));
            Buffer.AddRange(Encoding.ASCII.GetBytes(value));
            _buffUpdated = true;
        }

        #endregion

        #region Read


        public byte[] ReadByte(int length, bool peek = true)
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

        private short ReadShort(bool peek = true)
        {
            if (Buffer.Count > _readPos)
            {
                if (_buffUpdated)
                {
                    _readBuffer = Buffer.ToArray();
                    _buffUpdated = false;
                }

                var ret = BitConverter.ToInt16(_readBuffer!, _readPos);
                if (peek & Buffer.Count > _readPos)
                    _readPos += 2;
                return ret;
            }
            else
                throw new Exception("Byte buffer is exceed!");
        }

        private int ReadInt(bool peek = true)
        {
            if (Buffer.Count > _readPos)
            {
                if (_buffUpdated)
                {
                    _readBuffer = Buffer.ToArray();
                    _buffUpdated = false;
                }

                var ret = BitConverter.ToInt32(_readBuffer!, _readPos);
                if (peek & Buffer.Count > _readPos)
                    _readPos += 4;
                return ret;
            }
            else
                throw new Exception("Byte buffer is exceed!");
        }

        private long ReadLong(bool peek = true)
        {
            if (Buffer.Count > _readPos)
            {
                if (_buffUpdated)
                {
                    _readBuffer = Buffer.ToArray();
                    _buffUpdated = false;
                }

                var ret = BitConverter.ToInt64(_readBuffer!, _readPos);
                if (peek & Buffer.Count > _readPos)
                    _readPos += 8;
                return ret;
            }
            else
                throw new Exception("Byte buffer is exceed!");
        }

        private float ReadFloat(bool peek = true)
        {
            if (Buffer.Count > _readPos)
            {
                if (_buffUpdated)
                {
                    _readBuffer = Buffer.ToArray();
                    _buffUpdated = false;
                }

                var ret = BitConverter.ToSingle(_readBuffer!, _readPos);
                if (peek & Buffer.Count > _readPos)
                    _readPos += 4;
                return ret;
            }
            else
                throw new Exception("Byte buffer is exceed!");
        }

        private string ReadString(bool peek = true)
        {
            var length = ReadInt(true);
            if (_buffUpdated)
            {
                _readBuffer = Buffer.ToArray();
                _buffUpdated = false;
            }

            var retString = Encoding.ASCII.GetString(_readBuffer!, _readPos, length);
            if ((peek & Buffer.Count > _readPos) && retString.Length > 0)
                _readPos += length;

            return retString;
        }

        #endregion

        #region Dispose

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
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
}