using System;
using System.IO;
using GovanifY.Utility;
using ISO_Tools;
using LIBKH.KH2;
/*           TO READ BEFORE ANYTHING ELSE
 * OpenKH is a huge project which is for now, only supported by 2 guys,
 * GovanifY and Xeeynamo, even if I have sometimes the feeling that Xeeynamo 
 * isn't really working on this project...
 * Anyways, any help should be highly granted and appreciated
 * My work isn't the clearer of the world but he's (at least) working.
 * OpenKH is composed of 3 parts:
 * 
 * LibKH, which is needed for any OpenKH project because it's like a translator:
 * We're giving to him a specific KH file, and he'll give to us a readable file
 * 
 * OpenKH, which is the real core of the project. He will just simply contains
 * all things like 3d motors, etc... He will render any file and will try to imitate
 * the original games, thanks to us who are doing some reversing on their elf, RomFS, etc...
 * 
 * The Editors, which will let us modify the game as we want. For now that's more likely some tests 
 * but later this will be complete editors like a kh2mapv WAY MORE COMPLETE.
 * 
 * You can consider this as an utopia but I think one day this project will work, we just need time
 * and some guys for helping us.
 * 
 * If you want to work on OpenKH by any ways possible, please contact me at govanify@gmail.com
 * 
 * Thanks for reading, Your dear romhacker and programmer, GovanifY*/

namespace OpenKH_Core
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("OpenKH Core / Debug Console");
            Console.WriteLine("Early Version by GovanifY");
            Console.Write("Please enter the name of the game you want to load(KH2 or KH1):");
            string game = Console.ReadLine();
            Console.Write("Please enter the name of the iso you want to load:");
            string isoname = Console.ReadLine();
            Console.Write("Please enter the name of the file you want to load:");
            string inputname = @Console.ReadLine();
            if (game == "KH2")
            {
                Console.Write("Trying to load the iso...");
                FileStream isoStream = File.Open(isoname, FileMode.Open, FileAccess.Read, FileShare.Read);
                var iso = new ISOFileReader(isoStream);
                Console.WriteLine("Done!");
                Console.Write("Searching the IDX and IMG files...");
                Stream dumb = new FileStream(@"libKh.dll", FileMode.Open, FileAccess.Read);
                var IDXStream = new Substream(dumb); //Anti CS0165
                var IMGStream = new Substream(dumb); //Anti CS0165
                var OVLIMGStream = new Substream(dumb); //Anti CS0165
                var OVLIDXStream = new Substream(dumb); //Anti CS0165
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
                string inputname2 = Path.GetFullPath("output/" + inputname);
                Directory.CreateDirectory(Path.GetDirectoryName(inputname2));
                var output = new FileStream(inputname2, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                internalfile.CopyTo(output);
                Console.WriteLine("Done!");
                var br = new BinaryReader(output);
                if (br.ReadUInt32() != 0x01524142)
                {
                    Console.WriteLine("Not a BAR file, continue anyway");
                }
                else
                {
                    Console.WriteLine("BAR file detected! Unbarring him...");
                    var msgSys = new BAR(internalfile);
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