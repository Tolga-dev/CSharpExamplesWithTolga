using System;
using static Tensorflow.Binding;

namespace TensorFlowNET.Examples
{
    /// <summary>
    /// Simple hello world using TensorFlow
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var hello = tf.constant("Hello, TensorFlow!");
            Console.WriteLine(hello);
        }
    }
}