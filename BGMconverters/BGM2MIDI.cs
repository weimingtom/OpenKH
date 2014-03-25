/* This is a very early BGM to MIDI converter. There is still a lot unknown. */
/*
uint	Signature 0x204D4742 "BGM " 
ushort	Sequence ID (This file #)
ushort	WD ID
byte	Track count
byte*3	Unknown
byte	In-game Volume (If this is like VAG files, 127 is 100% max)
byte	Unknown
ushort	Parts Per Quarter Note
uint	File-size
byte*12	Padding
for each track:
    uint Track size
	byte*? Track commands

Commands:
	Each command consists of:
	  1) Delta time (1-4 bytes; variable length)
	  2) Command code (1 byte)
	  3) Command arguments (varies per command)
	 All timings seem to follow the official MIDI spec.
	00:	End of track
	02:	Loop begin
	03:	Loop end
	08:	Set tempo
		byte:	bpm
	0A
		byte
	0C:	Time signature
		ushort
	0D
		byte
	10:	Note on with previous key and velocity
	11:	Note on
		byte:	Key
		byte:	Velocity
	12:	Note on with previous velocity
		byte:	Key
	13:	Note on with previous key
		byte:	Velocity
	18:	Note off; Previous note
	19
		byte
		byte
	1A:	Note off
		byte:	Key
	20:	Program change
		byte: new program
	()22:	Volume
		byte
	24:	Expression
		byte
	26:	Pan
		byte
	28
		byte
	31
		byte
	34
		byte
	35
		byte
	3C:	Sustain Pedal
		byte
	3E
		byte
	40
		byte
		byte
		byte
	47
		byte
		byte
	48
		byte
		byte
		byte
	50
		byte
		byte
		byte
	58
		byte
	5C
	5D:	Portamento?
		byte
*/
using System;
using System.IO;

public void echo(Object e){if(typeof e!=="string"){e=""+e;}Console.WriteLine(e);}
public class BinaryWriter2 extends BinaryWriter {
	public BinaryWriter2(System.IO.Stream stream){super(stream);}
	public void Write(Int32 i){byte[] b=BitConverter.GetBytes(i).reverse();super.Write(b,0,4);}
	public void Write(UInt32 i){byte[] b=BitConverter.GetBytes(i).reverse();super.Write(b,0,4);}
	public void Write(Int16 i){byte[] b=BitConverter.GetBytes(i).reverse();super.Write(b,0,2);}
	public void Write(UInt16 i){byte[] b=BitConverter.GetBytes(i).reverse();super.Write(b,0,2);}
	public void Write(Int64 i){byte[] b=BitConverter.GetBytes(i).reverse();super.Write(b,0,8);}
	public void Write(UInt64 i){byte[] b=BitConverter.GetBytes(i).reverse();super.Write(b,0,8);}
	public void WriteDelta(Object i){
		if(i>0xFFFFFFF){throw Error("Too large of time!");}
		UInt32 b=i&0x7F;
		while((i>>=7)){
			b<<=8;
			b|=((i&0x7F)|0x80);
		}
		do{
			super.Write(byte(b));
			if((b&0x80)===0){break;}
			b>>=8;
		}while(true);
	}
	public void WriteBytes(byte[] i){super.Write(i);}
	public void WriteDummy(Object i){if(i===0){return;}WriteDelta(i);WriteBytes([0xFF,0x06,0]);}
}
public void main(){
	argv=Environment.GetCommandLineArgs(),argc=argv.Length,nme;
	if(argc>1){
		Console.WriteLine("Using BGM File: {0}",nme=argv[1]);
	}else{
		Console.Write("Enger BGM File: ");
		nme=Console.ReadLine();
	}
	FileStream bgmS=File.Open(nme,FileMode.Open,FileAccess.Read);
		BinaryReader bgm=new BinaryReader(bgmS);
		FileStream midS=File.Open(nme+".mid",FileMode.Create,FileAccess.Write);
		BinaryWriter2 mid=new BinaryWriter2(midS);
		trackC:byte,ppqn:UInt16;
		byte lKey=0,byte lVelocity=64;
		byte track=0,tSzT:UInt32,delta,cmd:byte,t,trackLenOffset;
		/*program2channel={},byte channelL=0,*/byte channel=0;
	try{
		if(bgm.ReadUInt32()!==0x204D4742){Console.WriteLine("BAD HEADER!");return;}
		Console.WriteLine("Seq ID:         {0}",bgm.ReadUInt16());
		Console.WriteLine("WD  ID:         {0}",bgm.ReadUInt16());
		Console.WriteLine("# of Tracks:    {0}",trackC=bgm.ReadByte());
		Console.WriteLine("Unknown:        {0}",bgm.ReadBytes(3).toString());
		Console.WriteLine("In-game volume: {0}",bgm.ReadByte());
		Console.WriteLine("Unknown2:       {0:x2}",bgm.ReadByte());
		Console.WriteLine("PPQN:           {0}",ppqn=bgm.ReadUInt16());
		Console.WriteLine("File-Size:      {0}",bgm.ReadUInt32());
		bgmS.Position+=12;//padding
//trackC=4;
		mid.Write(UInt32(0x4d546864));//header
		mid.Write(UInt32(6));//header length
		mid.Write(UInt16(1));//track play type
		mid.Write(UInt16(trackC));//# tracks
		mid.Write(ppqn);//PPQN
		
		Console.ReadLine();
		
		for(;track<trackC;++track){
			tSzT= bgm.ReadUInt32();
			Console.WriteLine("Track {0}; Length= {1}",track,tSzT);
			
			mid.Write(UInt32(0x4d54726b));//header
			trackLenOffset=midS.Position;
			mid.Write(UInt32(0));//len
			
			tdelta=0;
			channel=track;
			for(tSzT+=bgmS.Position;bgmS.Position<tSzT-1;){
				delta=t=0;
				do{
					delta=(delta<<7)+((t=bgm.ReadByte())&0x7F);
				}while(t&0x80);
				tdelta+=delta;
				cmd=bgm.ReadByte();
				/*if(track>1){//skip track
					cmd=0;
					bgmS.Position=tSzT;
					continue;
				}*/
				//Console.WriteLine("Current command: {0:x2}",cmd);
				switch(cmd){
					case 0x00:
						mid.WriteDelta(delta);
						mid.WriteBytes([0xFF,0x2F,0x00]);
						midS.Position=trackLenOffset;
						mid.Write(UInt32(midS.Length-4-trackLenOffset));//update len
						midS.Seek(0,SeekOrigin.End);
						break;	//End of track
					case 0x02:
						mid.WriteDelta(delta);
						mid.WriteBytes([0xFF,0x06,9,108,111,111,112,83,116,97,114,116]);//loopStart
						break;	//Loop begin
					case 0x03:
						mid.WriteDelta(delta);
						mid.WriteBytes([0xFF,0x06,7,108,111,111,112,69,110,100]);//loopEnd
						break;	//Loop end
					//case 0x04:break;	//End of track?
					case 0x08:
						mid.WriteDelta(delta);
						t=60000000/bgm.ReadByte();//bpm
						mid.WriteBytes([0xFF,0x51,3,byte(t>>16),byte(t>>8),byte(t)]);
						break;			//Set tempo
					case 0x0A:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x0c:
						mid.WriteDelta(delta);
						mid.WriteBytes([0xFF,0x58,4,bgm.ReadByte(),bgm.ReadByte(),byte(ppqn),8]);
						//Not sure if 8 is set or variable
						break;			//Time signature
					case 0x0D:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x10:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0x90)|channel,lKey,lVelocity]);
						break;	//play previous key, no velocity param
					case 0x11:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0x90)|channel,lKey=bgm.ReadByte(),lVelocity=bgm.ReadByte()]);
						//key,velocity
						break;			//key on with velocity
					case 0x12:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0x90)|channel,lKey=bgm.ReadByte(),lVelocity]);
						break;			//key on with prev velocity
					case 0x13:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0x90)|channel,lKey,lVelocity=bgm.ReadByte()]);
						break;			//play previous key with velocity param
					case 0x18:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0x80)|channel,lKey,64]);
						break;	//Note off (prev key)
					case 0x19:
						t=[bgm.ReadByte(),bgm.ReadByte()];
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (2 byte extra)
					case 0x1A:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0x80)|channel,lKey=bgm.ReadByte(),64]);
						break;	//Note off
					case 0x20:
						t=bgm.ReadByte();
						/* Can be more then 16 programs, so cannot rely on channel=program; KH seems to follow it tho
						if(typeof program2channel[t]!=="undefined"){
							channel=program2channel[t];
							Console.WriteLine("  Swapping to channel {0}",channel);
							break;
						}
						if(channelL<16){channel=channelL;++channelL;}else{}*/
						if(t<16){channel=t;}else{Console.WriteLine("  Program number is over 16! Using channel 0!  This is a \"optimization\" done for square games");}
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0xC0)|channel,t]);
						/*program2channel[t]=channel;*/
						Console.WriteLine("  Swapping to NEW channel {0} for {1}",channel,t);
						break;			//assign instrument / program change
					case 0x22:
						mid.WriteDelta(delta);
						t=bgm.ReadByte();
						mid.WriteBytes([byte(0xB0)|channel,7,t]);
						Console.WriteLine("  Set volume for {0} to {1}",channel,t);
						break;			//set volume (I am positive that volume values in this driver do not align with standard MIDI. (see FFXI 213 Ru'Lude Gardens.psf2 for example))
					case 0x24:
						t=bgm.ReadByte();
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0xB0)|channel,11,t]);
						Console.WriteLine("  Set expr-Vol for {0} to {1}",channel,t);
						break;			//expression
					case 0x26:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0xB0)|channel,10,bgm.ReadByte()]);
						break;			//pan
					case 0x28:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x31:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x34:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: {1:x2} 0x{0:x2} {2:x2}",cmd,delta,t);
						mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x35:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x3E:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x3C:
						mid.WriteDelta(delta);
						mid.WriteBytes([byte(0xB0)|channel,64,bgm.ReadByte()>0?0x7F:0]);
						break;			//Sustain Pedal
					case 0x40:
						t=[bgm.ReadByte(),bgm.ReadByte(),bgm.ReadByte()];
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (3 byte extra)
					case 0x47:
						t=[bgm.ReadByte(),bgm.ReadByte()];
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (2 byte extra)
					case 0x48:
						t=[bgm.ReadByte(),bgm.ReadByte(),bgm.ReadByte()];
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (3 byte extra)
					case 0x50:
						t=[bgm.ReadByte(),bgm.ReadByte(),bgm.ReadByte()];
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (3 byte extra)
					case 0x58:
						t=bgm.ReadByte();
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);mid.WriteDummy(delta);
						break;			//Unknown (1 byte extra)
					case 0x5C:
						 t=[bgm.ReadByte(),bgm.ReadByte()];
						 Console.WriteLine("Not implemented: 0x{0:x2}",cmd);
						 mid.WriteDummy(delta);
						//lsb,msb
						break;		//pitch bend		I SHOULD GO BACK AND VERIFY THE RANGE OF THE PITCH BEND
					case 0x5D:
						t=bgm.ReadByte();
						Console.WriteLine("Not implemented: 0x{0:x2}",cmd);
						mid.WriteDummy(delta);
						break;		//Portamento?
					//case 0x60:break;	//Init?
					//case 0x61:break;	//Init?
					//case 0x7F:break;	//Init?
					default:
						Console.WriteLine("Unknown command: 0x{0:x2}",cmd);
						mid.WriteDummy(delta);
				}/**/
			}
			Console.WriteLine("  Total ticks in this track: {0}",tdelta);
			if(bgmS.Position!==tSzT){
				Console.WriteLine("Got a bad auto-offset! ({0} ahead) Attempting to fix...",bgmS.Position-tSzT);
				bgmS.Position=tSzT;
			}
		}
		
	}finally{bgm.Close();mid.Close();};
}main();
Console.ReadLine();