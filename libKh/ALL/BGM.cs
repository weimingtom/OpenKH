using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;

namespace LIBKH
{
    namespace ALL
    {
        /// <summary>
        ///     This class with convert bgm files into the midi format
        /// </summary>
        public sealed class BGM
        {
            public void GetBGM(string nme)
            {
                //I really hate jscript .net(GovanifY's Rewriting BGM2MIDI in c# atm)
                //Setting the vars(incomplete b\c I dunno at all the jscript .net language)
                //Concept: Write in a "temp" folder the files like music066.bgm.mid or image1.imgd.bmp, for debug usage.
                byte trackC;
                ushort ppqn;
                var bgmS = File.Open(nme, FileMode.Open, FileAccess.Read);
                var bgm = new BinaryReader(bgmS);
                var midS = File.Open(nme + ".mid", FileMode.Create, FileAccess.Write);
                var mid = new BinaryWriter(midS);

                try
                {
                    //Check for the debug log/console
                    if (bgm.ReadUInt32() != 0x204D4742)
                    {
                        Console.WriteLine("BAD HEADER!(MIDI: {0} )", nme);
                        return;
                    }
                    Console.WriteLine("Seq ID:         {0}", bgm.ReadUInt16());
                    Console.WriteLine("WD  ID:         {0}", bgm.ReadUInt16());
                    Console.WriteLine("# of Tracks:    {0}", trackC = bgm.ReadByte());
                    Console.WriteLine("Unknown:        {0}", bgm.ReadBytes(3).ToString());
                    Console.WriteLine("In-game volume: {0}", bgm.ReadByte());
                    Console.WriteLine("Unknown2:       {0:x2}", bgm.ReadByte());
                    Console.WriteLine("PPQN:           {0}", ppqn = bgm.ReadUInt16());
                    Console.WriteLine("File-Size:      {0}", bgm.ReadUInt32());
                    bgmS.Position += 12; //padding
                    //Now writing the new midi file

                    #region Header

                    mid.Write(0x6468544D); //header
                    mid.Write(0x06000000); //header length
                    mid.Write(0x0100); //track play type
                    mid.Write(trackC); //# tracks
                    mid.Write(ppqn); //PPQN

                    #endregion

                }
                    //for(track<trackC;++track){


                catch
                {
                }

            }
        }

    }
}

