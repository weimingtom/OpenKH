using System;
using System.IO;

namespace LIBKH
{
    namespace ALL
    {
        /// <summary>
        ///     This class with convert bgm files into the midi format
        /// </summary>
        public class BGM
        {
            public void WriteBytes(byte[] i, BinaryWriter writer)
            {
                writer.Write(i);
            }

            public void WriteDelta(int i, BinaryWriter writer)
            {
                if (i > 0xFFFFFFF)
                {
                    throw new Exception("Delta is too long!");

                }
                UInt32 b = ((UInt32) i & 0x7F);
                while (Convert.ToBoolean(i >>= 7)) 
                {
                    b <<= 8;
                    b |= ((uint)(i & 0x7F) | 0x80);
                }
                do
                {
                    writer.Write((byte) b);
                    if ((b & 0x80) == 0)
                    {
                        break;
                    }
                    b >>= 8;
                } while (true);
            }

            public virtual void WriteDummy(int i, BinaryWriter writer)
            {
                if (i == 0)
                {
                    WriteDelta(i, writer);
                    byte[] buffer1 = {0xff, 6, 0};
                    WriteBytes(buffer1, writer);
                }
            }


            public void GetMIDI(string nme)
            {
                #region vars

                //Concept: Write in a "temp" folder the files like music066.bgm.mid or image1.imgd.bmp, for debug usage.
                //If this concept should be adopted, we'll need to do only a return of a byte and then write this byte on a new file in the temp folder, in the openkh binaries, not in libkh
                int t;
                byte cmd;
                byte trackC;
                var track = new byte();
                ushort ppqn;
                long tSzT;
                int delta;
                FileStream bgmS = File.Open(nme, FileMode.Open, FileAccess.Read);
                var bgm = new BinaryReader(bgmS);
                FileStream midS = File.Open(nme + ".mid", FileMode.Create, FileAccess.Write);
                var mid = new BinaryWriter(midS);
                byte lKey = 0;
                byte lVelocity = 0x40;

                #endregion

                try
                {
                    #region checks&info

                    //Check for the debug log/console
                    if (bgm.ReadUInt32() != 0x204D4742)
                    {
                        Console.WriteLine("BAD HEADER!(MIDI: {0} )", nme);
                        return;
                    }
                    Console.WriteLine("Seq ID:         {0}", bgm.ReadUInt16());
                    Console.WriteLine("WD  ID:         {0}", bgm.ReadUInt16());
                    Console.WriteLine("# of Tracks:    {0}", trackC = bgm.ReadByte());
                    Console.WriteLine("Unknown:        {0}", bgm.ReadBytes(3));
                    Console.WriteLine("In-game volume: {0}", bgm.ReadByte());
                    Console.WriteLine("Unknown2:       {0:x2}", bgm.ReadByte());
                    Console.WriteLine("PPQN:           {0}", ppqn = bgm.ReadUInt16());
                    Console.WriteLine("File-Size:      {0}", bgm.ReadUInt32());

                    #endregion

                    bgmS.Position += 12; //padding
                    //Now writing the new midi file

                    #region Header

                    mid.Write(0x6468544D); //header
                    mid.Write(0x06000000); //header length
                    mid.Write(0x0100); //track play type
                    mid.Write(trackC); //# tracks
                    mid.Write(ppqn); //PPQN

                    #endregion

                    for (byte i = track; track < trackC; ++i)
                    {
                        tSzT = bgm.ReadUInt32();
                        Console.WriteLine("Track {0}; Length = {1}", track, tSzT);
                        mid.Write(0x6b72544d); //header
                        long trackLenOffset = midS.Position;
                        mid.Write(0x00000000); //len

                        int tdelta = 0;
                        byte channel = track;
                        for (tSzT += bgmS.Position; bgmS.Position < tSzT - 1;)
                        {
                            delta = 0;
                            do
                            {
                                byte num14;
                                t = num14 = bgm.ReadByte();
                                delta = (delta << 7) + (num14 & 0x7f);
                            } while ((t & 0x80) != 0);
                            tdelta += delta;
                            cmd = bgm.ReadByte();

                            #region cases commands

                            switch (cmd)
                            {
                                case 0x00:
                                    WriteDelta(delta, mid);
                                    var tempa = new byte[] {0xFF, 0x2F, 0x00};
                                    WriteBytes(tempa, mid);
                                    midS.Position = trackLenOffset;
                                    UInt32 abc = ((uint) midS.Length - 4 - (uint) trackLenOffset);
                                    mid.Write(abc); //update len
                                    midS.Seek(0, SeekOrigin.End);
                                    break; //End of track
                                case 0x02:
                                    WriteDelta(delta, mid);
                                    var tempb = new byte[] {0xFF, 0x06, 9, 108, 111, 111, 112, 83, 116, 97, 114, 116};
                                    WriteBytes(tempb, mid)
                                        ; //loopStart
                                    break; //Loop begin
                                case 0x03:
                                    WriteDelta(delta, mid);
                                    var tempc = new byte[] {0xFF, 0x06, 7, 108, 111, 111, 112, 69, 110, 100};
                                    WriteBytes(tempc, mid)
                                        ; //loopEnd
                                    break; //Loop end
                                    //case 0x04:break;	//End of track?
                                case 0x08:
                                    WriteDelta(delta, mid);
                                    t = 60000000/bgm.ReadByte(); //bpm
                                    var tempd = new byte[]
                                    {0xFF, 0x51, 3, (byte) (t >> 16), (byte) (t >> 8), (byte) (t)};
                                    WriteBytes(tempd, mid);
                                    break; //Set tempo
                                case 0x0A:
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x0c:
                                    WriteDelta(delta, mid);
                                    var tempe = new byte[]
                                    {0xFF, 0x58, 4, bgm.ReadByte(), bgm.ReadByte(), (byte) ppqn, 8};
                                    WriteBytes(tempe, mid)
                                        ;
                                    //Not sure if 8 is set or variable
                                    break; //Time signature
                                case 0x0D:
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x10:
                                    WriteDelta(delta, mid);
                                    var tempf = new[] {(byte) (0x90 | channel), lKey, lVelocity};
                                    WriteBytes(tempf, mid);
                                    break; //play previous key, no velocity param
                                case 0x11:
                                    WriteDelta(delta, mid);
                                    var tempg = new[]
                                    {(byte) (0x90 | channel), lKey = bgm.ReadByte(), lVelocity = bgm.ReadByte()};
                                    WriteBytes(tempg, mid);
                                    //key,velocity
                                    break; //key on with velocity
                                case 0x12:
                                    WriteDelta(delta, mid);
                                    var temph = new[] {(byte) (0x90 | channel), lKey = bgm.ReadByte(), lVelocity};
                                    WriteBytes(temph, mid);
                                    break; //key on with prev velocity
                                case 0x13:
                                    WriteDelta(delta, mid);
                                    var tempi = new[] {(byte) (0x90 | channel), lKey, lVelocity = bgm.ReadByte()};
                                    WriteBytes(tempi, mid);
                                    break; //play previous key with velocity param
                                case 0x18:
                                    WriteDelta(delta, mid);
                                    var tempj = new byte[] {(byte) (0x80 | channel), lKey, 64};
                                    WriteBytes(tempj, mid);
                                    break; //Note off (prev key)
                                case 0x19:
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (2 byte extra)
                                case 0x1A:
                                    WriteDelta(delta, mid);
                                    var tempk = new byte[] {(byte) (0x80 | channel), lKey = bgm.ReadByte(), 64};
                                    WriteBytes(tempk, mid);
                                    break; //Note off
                                case 0x20:
                                    t = bgm.ReadByte();
                                    /* Can be more then 16 programs, so cannot rely on channel=program; KH seems to follow it tho
						if(typeof program2channel[t]!=='undefined'){
							channel=program2channel[t];
							Console.WriteLine('  Swapping to channel {0}',channel);
							break;
						}
						if(channelL<16){channel=channelL;++channelL;}else{}*/
                                    if (t < 16)
                                    {
                                        channel = (byte)t;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Program number is over 16! Using channel 0!\n  This is a \"optimization\" done for square games");
                                    }
                                    WriteDelta(delta, mid);
                                    var tempo = new[] {(byte) (0xC0 | channel), (byte) t};
                                    WriteBytes(tempo, mid);
                                    /*program2channel[t]=channel;*/
                                    Console.WriteLine("  Swapping to NEW channel {0} for {1}", channel, t);
                                    break; //assign instrument / program change
                                case 0x22:
                                    WriteDelta(delta, mid);
                                    t = bgm.ReadByte();
                                    var tempp = new byte[] {(byte) (0xB0 | channel), 7, (byte) t};
                                    WriteBytes(tempp, mid);
                                    Console.WriteLine("  Set volume for {0} to {1}", channel, t);
                                    break;
                                    //set volume (I am positive that volume values in this driver do not align with standard MIDI. (see FFXI 213 Ru'Lude Gardens.psf2 for example))
                                case 0x24:
                                    t = bgm.ReadByte();
                                    WriteDelta(delta, mid);
                                    var tempq = new byte[] {(byte) (0xB0 | channel), 11, (byte) t};
                                    WriteBytes(tempq, mid);
                                    Console.WriteLine("  Set expr-Vol for {0} to {1}", channel, t);
                                    break; //expression
                                case 0x26:
                                    WriteDelta(delta, mid);
                                    var tempr = new byte[] {(byte) (0xB0 | channel), 10, bgm.ReadByte()};
                                    WriteBytes(tempr, mid);
                                    break; //pan
                                case 0x28:
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x31:
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x34:
                                    t = bgm.ReadByte();
                                    Console.WriteLine("Unknown command: {1:x2} 0x{0:x2} {2:x2}", cmd, delta, t);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x35:
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x3E:
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x3C:
                                    WriteDelta(delta, mid);
                                    var temps = new byte[]
                                    {(byte) (0xB0 | channel), 64, (byte) (bgm.ReadByte() > 0 ? 0x7F : 0)};
                                    WriteBytes(temps, mid);
                                    break; //Sustain Pedal
                                case 0x40:
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (3 byte extra)
                                case 0x47:
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (2 byte extra)
                                case 0x48:
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (3 byte extra)
                                case 0x50:
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (3 byte extra)
                                case 0x58:
                                    bgm.ReadByte();
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Unknown (1 byte extra)
                                case 0x5C:
                                    bgm.ReadByte();
                                    bgm.ReadByte();
                                    Console.WriteLine("Not implemented: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    //lsb,msb
                                    break; //pitch bend		//TODO: I SHOULD GO BACK AND VERIFY THE RANGE OF THE PITCH BEND
                                case 0x5D:
                                    bgm.ReadByte();
                                    Console.WriteLine("Not implemented: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break; //Portamento?
                                    //case 0x60:break;	//Init?
                                    //case 0x61:break;	//Init?
                                    //case 0x7F:break;	//Init?
                                default:
                                    Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
                                    WriteDummy(delta, mid);
                                    break;
                            }
                                                        #endregion
                            			Console.WriteLine("  Total ticks in this track: {0}",tdelta);
                            if (bgmS.Position == tSzT){}
                            else
                            {
                                Console.WriteLine("Got a bad auto-offset! ({0} ahead) Attempting to fix...", bgmS.Position - tSzT);
                                bgmS.Position = tSzT; 
                            }
                        }
                    }
                }
                finally{bgm.Close();mid.Close();}
            }
        }
    }
}