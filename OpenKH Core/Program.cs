using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovanifY.Utility;
using ISO_Tools;
using LIBKH.KH2;

namespace OpenKH_Core
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("OpenKH Core / Debug Console");
            Console.WriteLine("Early Version by GovanifY");
            Console.Write("Please enter the name of the game you want to load(KH2 or KH1):");
            var game = Console.ReadLine();
            Console.Write("Please enter the name of the iso you want to load:");
            var isoname = Console.ReadLine();
            Console.Write("Please enter the name of the file you want to load:");
            var inputname = @Console.ReadLine();
            if (game == "KH2")
            {
                Console.Write("Trying to load the iso...");
                FileStream isoStream = File.Open(isoname, FileMode.Open, FileAccess.Read, FileShare.Read);
                var iso = new ISOFileReader(isoStream);
                Console.WriteLine("Done!");
                Console.Write("Searching the IDX and IMG files...");
                Stream dumb = new FileStream(@"libKh.dll", FileMode.Open, FileAccess.Read);
                Substream IDXStream = new Substream(dumb); //Anti CS0165
                Substream IMGStream = new Substream(dumb); //Anti CS0165
                Substream OVLIMGStream = new Substream(dumb); //Anti CS0165
                Substream OVLIDXStream = new Substream(dumb); //Anti CS0165
                foreach (FileDescriptor file in iso)
                {
                    string file2 = file.FullName;
                    if (file2.EndsWith("OVL.IDX"))
                    {
                        OVLIDXStream = iso.GetFileStream(file);
                    }
                    if (file2.EndsWith("OVL.IMG"))
                    {
                        OVLIMGStream = iso.GetFileStream(file);
                    }
                    if (file2.EndsWith("KH2.IDX"))
                    {
                        IDXStream = iso.GetFileStream(file);
                    }
                    if (file2.EndsWith("KH2.IMG"))
                    {
                        IMGStream = iso.GetFileStream(file);
                    }
                }
                if (IDXStream == IMGStream)
                {
                    throw new Exception("IDX or IMG Stream isn't loaded correctly!");
                }
                Console.WriteLine("Done!");
                var LIBKHIDX = new IDX(IDXStream, IMGStream); // TODO add a fucking support for the OVL!
                Console.Write("Opening the internal file...");
                Stream internalfile = LIBKHIDX.OpenFile(inputname);
                Console.WriteLine("Done!");
                Console.Write("Extracting the file...");
                var inputname2 = Path.GetFullPath("output/" + inputname);
                    Directory.CreateDirectory(Path.GetDirectoryName(inputname2));
                    var output = new FileStream(inputname2, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                Console.WriteLine("Done!");
                output.Position = 0;
                var br = new BinaryReader(output);
                
                if (br.ReadUInt32() != 0x01524142)
                {
                    Console.WriteLine("Not a BAR file, continue anyway");
                }
                else
                {
                    Console.WriteLine("BAR file detected! Unbarring him...");
                    LIBKH.KH2.BAR msgSys = new LIBKH.KH2.BAR(internalfile);
                }
                Console.Read();
            }
            else
            {
                throw new Exception("NOT IMPLEMENTED");
            }

        }
        }
    }
