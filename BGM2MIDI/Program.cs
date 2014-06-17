using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using LIBKH;

namespace BGM2MIDI
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Please enter the name of the BGM file:");
            var nme = Console.ReadLine();
            try
            {
                var a = new LIBKH.ALL.BGM();
                a.GetMIDI(nme);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error as occured! Press enter to see the exception");
                Console.Read();
                Console.WriteLine(e);
                Console.ReadLine();
            }
            Console.ReadLine();
        }

        }
    }
