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

			public void GetBGM(string nme)
			{
				#region vars

				//I really hate jscript .net(GovanifY's Rewriting BGM2MIDI in c# atm)
				//Setting the vars(incomplete b\c I dunno at all the jscript .net language)
				//Concept: Write in a "temp" folder the files like music066.bgm.mid or image1.imgd.bmp, for debug usage.
				//If this concept should be adopted, we'll need to do only a return of a byte and then write this byte on a new file in the temp folder, in the openkh binaries, not in libkh
				int t = 0;
				byte cmd;
				byte trackC;
				var track = new byte();
				ushort ppqn;
				long tSzT;
				int delta = 0;
				FileStream bgmS = File.Open(nme, FileMode.Open, FileAccess.Read);
				var bgm = new BinaryReader(bgmS);
				FileStream midS = File.Open(nme + ".mid", FileMode.Create, FileAccess.Write);
				var mid = new BinaryWriter(midS);
                byte lKey = 0;
			    byte lVelocity = 64;

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
							delta = t = 0;
                            do
                            {
                                byte num14;
                                t = num14 = bgm.ReadByte();
                                delta = (delta << 7) +  (num14 & 0x7f);
                            }
                            while ((t & 0x80) != 0);
							tdelta += delta;
							cmd = bgm.ReadByte();

							#region cases commands
//This part need to be rewroted for c#, should take some time. Already changed some little things
                            //If you have the time to(Xeeynamo)can you refactor this part please?^^"
							switch (cmd)
							{
								case 0x00:
									mid.WriteDelta(delta);
									mid.WriteBytes([0xFF, 0x2F, 0x00])
									;
									midS.Position = trackLenOffset;
									mid.Write(UInt32(midS.Length - 4 - trackLenOffset)); //update len
									midS.Seek(0, SeekOrigin.End);
									break; //End of track
								case 0x02:
									mid.WriteDelta(delta);
									mid.WriteBytes([0xFF, 0x06, 9, 108, 111, 111, 112, 83, 116, 97, 114, 116])
									; //loopStart
									break; //Loop begin
								case 0x03:
									mid.WriteDelta(delta);
									mid.WriteBytes([0xFF, 0x06, 7, 108, 111, 111, 112, 69, 110, 100])
									; //loopEnd
									break; //Loop end
									//case 0x04:break;	//End of track?
								case 0x08:
									mid.WriteDelta(delta);
									t = 60000000/bgm.ReadByte(); //bpm
									mid.WriteBytes([0xFF, 0x51, 3, byte(t >> 16),
									byte 
									(t >> 8),
									byte 
									(t)])
									;
									break; //Set tempo
								case 0x0A:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x0c:
									mid.WriteDelta(delta);
									mid.WriteBytes([0xFF, 0x58, 4, bgm.ReadByte(), bgm.ReadByte(), byte(ppqn),
									8])
									;
									//Not sure if 8 is set or variable
									break; //Time signature
								case 0x0D:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x10:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0x90) | channel,
									lKey,
									lVelocity])
									;
									break; //play previous key, no velocity param
								case 0x11:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0x90) | channel,
									lKey = bgm.ReadByte(),
									lVelocity = bgm.ReadByte()])
									;
									//key,velocity
									break; //key on with velocity
								case 0x12:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0x90) | channel,
									lKey = bgm.ReadByte(),
									lVelocity])
									;
									break; //key on with prev velocity
								case 0x13:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0x90) | channel,
									lKey,
									lVelocity = bgm.ReadByte()])
									;
									break; //play previous key with velocity param
								case 0x18:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0x80) | channel,
									lKey,
									64])
									;
									break; //Note off (prev key)
								case 0x19:
									t =[
									bgm.ReadByte(),
									bgm.ReadByte()]
									;
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (2 byte extra)
								case 0x1A:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0x80) | channel,
									lKey = bgm.ReadByte(),
									64])
									;
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
										channel = t;
									}
									else
									{
										Console.WriteLine(
											"  Program number is over 16! Using channel 0!\n  This is a "optimization" done for square games");
									}
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0xC0) | channel,
									t])
									;
									/*program2channel[t]=channel;*/
									Console.WriteLine("  Swapping to NEW channel {0} for {1}", channel, t);
									break; //assign instrument / program change
								case 0x22:
									mid.WriteDelta(delta);
									t = bgm.ReadByte();
									mid.WriteBytes([byte(0xB0) | channel,
									7,
									t])
									;
									Console.WriteLine("  Set volume for {0} to {1}", channel, t);
									break;
										//set volume (I am positive that volume values in this driver do not align with standard MIDI. (see FFXI 213 Ru'Lude Gardens.psf2 for example))
								case 0x24:
									t = bgm.ReadByte();
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0xB0) | channel,
									11,
									t])
									;
									Console.WriteLine("  Set expr-Vol for {0} to {1}", channel, t);
									break; //expression
								case 0x26:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0xB0) | channel,
									10,
									bgm.ReadByte()])
									;
									break; //pan
								case 0x28:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x31:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x34:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: {1:x2} 0x{0:x2} {2:x2}", cmd, delta, t);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x35:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x3E:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x3C:
									mid.WriteDelta(delta);
									mid.WriteBytes([byte(0xB0) | channel,
									64,
									bgm.ReadByte() > 0 ? 0x7F : 0])
									;
									break; //Sustain Pedal
								case 0x40:
									t =[
									bgm.ReadByte(),
									bgm.ReadByte(),
									bgm.ReadByte()]
									;
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (3 byte extra)
								case 0x47:
									t =[
									bgm.ReadByte(),
									bgm.ReadByte()]
									;
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (2 byte extra)
								case 0x48:
									t =[
									bgm.ReadByte(),
									bgm.ReadByte(),
									bgm.ReadByte()]
									;
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (3 byte extra)
								case 0x50:
									t =[
									bgm.ReadByte(),
									bgm.ReadByte(),
									bgm.ReadByte()]
									;
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (3 byte extra)
								case 0x58:
									t = bgm.ReadByte();
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Unknown (1 byte extra)
								case 0x5C:
									t =[
									bgm.ReadByte(),
									bgm.ReadByte()]
									;
									Console.WriteLine("Not implemented: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									//lsb,msb
									break; //pitch bend		I SHOULD GO BACK AND VERIFY THE RANGE OF THE PITCH BEND
								case 0x5D:
									t = bgm.ReadByte();
									Console.WriteLine("Not implemented: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
									break; //Portamento?
									//case 0x60:break;	//Init?
									//case 0x61:break;	//Init?
									//case 0x7F:break;	//Init?
								default:
									Console.WriteLine("Unknown command: 0x{0:x2}", cmd);
									mid.WriteDummy(delta);
							}

							#endregion
						}
					}
				}
				catch
				{
				}
			}
		}
	}
}