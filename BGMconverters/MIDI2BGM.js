/* This is a very early MIDI to BGM converter. There is still a lot unknown. */
import System;
import System.IO;

function error(e){Console.ForegroundColor=ConsoleColor.Red;Console.WriteLine(e);Console.ResetColor();}
class BinaryWriter2 extends BinaryWriter {
	public function BinaryWriter2(stream:System.IO.Stream){super(stream);}
	public function WriteDelta(i):void{if((i%1)!==0){i=Math.round(i);}
		while(i>0xFFFFFFF){WriteDummy(0xFFFFFFF);i-=0xFFFFFFF;}
		var b:UInt32=i&0x7F;
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
	public function WriteBytes(i:byte[]):void{super.Write(i);}
	public function WriteDummy(i):void{
		if(i===0){return;}
		WriteDelta(i);
		WriteBytes([0x5D,0]);
	}
}
class BinaryReader2 extends BinaryReader {
	public function BinaryReader2(stream:System.IO.Stream){super(stream);}
	public function ReadInt32():Int32{return BitConverter.ToInt32(super.ReadBytes(4).reverse(),0);}
	public function ReadUInt32():UInt32{return BitConverter.ToUInt32(super.ReadBytes(4).reverse(),0);}
	public function ReadInt16():Int16{return BitConverter.ToInt16(super.ReadBytes(2).reverse(),0);}
	public function ReadUInt16():UInt16{return BitConverter.ToUInt16(super.ReadBytes(2).reverse(),0);}
	public function ReadInt64():Int64{return BitConverter.ToInt64(super.ReadBytes(8).reverse(),0);}
	public function ReadUInt64():UInt64{return BitConverter.ToUInt64(super.ReadBytes(8).reverse(),0);}
}

function main(){
	var argv=Environment.GetCommandLineArgs(),argc=argv.Length,nme;
	if(argc>1){
		Console.WriteLine('Using BGM File: {0}',nme=argv[1]);
	}else{
		Console.Write('Enter BGM File: ');
		nme=Console.ReadLine();
	}
	var midS:FileStream=File.Open(nme,FileMode.Open,FileAccess.Read),
		mid:BinaryReader2=new BinaryReader2(midS),
		bgmS:FileStream=File.Open(nme+'.bgm',FileMode.Create,FileAccess.Write),
		bgm:BinaryWriter2=new BinaryWriter2(bgmS),
		trackC:byte,ppqn:UInt16,
		lKey:byte=255,lVelocity:byte=64,
		track:byte=0,tSzT:UInt32,delta,cmd:byte,t,trackLenOffset,
		deltaMod:decimal=1,
		/*program2channel={},channelL:byte=0,*/channel:byte=0;
	try{
		if(mid.ReadUInt32()!==0x4d546864||mid.ReadUInt32()!==6){error("BAD HEADER!");return;}
		if((t=mid.ReadUInt16())!==1){error(String.Format("WARNING: Play type is not what's expected! (Got {0}; Want 1)\nPress enter to try to convert anyway...",t));Console.ReadLine();}
		Console.WriteLine('# of Tracks: {0}',ppqn=mid.ReadUInt16());
			if(ppqn>0xFF){error("ERROR: This many tracks cannot be stored in a BGM file!");return;}
			trackC=byte(ppqn);
		Console.WriteLine('PPQN:        {0}',ppqn=mid.ReadUInt16());
		if(ppqn!==48){//KH1 forces PPQN=48, even when you save something else
			deltaMod=(ppqn/48)||1;
			Console.ForegroundColor=ConsoleColor.Cyan;
			Console.WriteLine('Modded PPQN: {0} (calc={2})\nDeltaModDiv: {1}',48,deltaMod,ppqn/deltaMod);
			Console.ResetColor();
			ppqn=48;
		}
		
		bgm.Write(UInt32(0x204D4742));//header
		Console.Write('Enter Seq ID: ');
			bgm.Write(UInt16(Console.ReadLine()|0));
		Console.Write('Enter WD ID: ');
			bgm.Write(UInt16(Console.ReadLine()|0));
		bgm.Write(trackC);
		bgm.Write(byte(5));//unknown, often 5
		bgm.Write(byte(0));//unknown
		bgm.Write(byte(0));//unknown
		Console.Write('Enter in-game volume level [0-255]: ');
			t=Console.ReadLine()|0;
			if(t>255){t=255;}else if(t<21){error("WARNING: {0} is awfully quiet, you might not hear it in-game!");}
			bgm.Write(byte(t));//volume
		bgm.Write(byte(0));//unknown
		bgm.Write(ppqn);
		bgm.Write(UInt32(0));//fileSize
		bgm.Write(UInt32(0));bgm.Write(UInt32(0));bgm.Write(UInt32(0));//padding
		bgmS.Flush();
		Console.Write('Press enter to continue...');Console.ReadLine();
		
		for(;track<trackC;++track){
			if(mid.ReadUInt32()!==0x4d54726b){error(String.Format('Bad track {0} header!',track));return;}
			tSzT = mid.ReadUInt32();
			Console.WriteLine('Track {0}; Length = {1}',track,tSzT);
			
			trackLenOffset=bgmS.Position;
			bgm.Write(UInt32(0));//len
			
			delta=t=0;
			channel=track%16;
			for(tSzT+=midS.Position;midS.Position<tSzT-1;){
				do{
					delta=(delta<<7)+((t=mid.ReadByte())&0x7F);
				}while(t&0x80);
				delta/=deltaMod;
				/*if(track>2){//skip track
					midS.Position=tSzT;
							bgm.WriteDelta(delta);delta=0;
							bgm.WriteBytes([0]);
							bgmS.Position=trackLenOffset;
							bgm.Write(UInt32(bgmS.Length-4-trackLenOffset));//update len
							bgmS.Seek(0,SeekOrigin.End);
							Console.WriteLine('end');
					break;
				}*/
				cmd=mid.ReadByte();
				//Console.WriteLine('Current command: {0:x2}',cmd);
				if(cmd===0xFF){
					t=[mid.ReadByte(),mid.ReadByte()];
					//Console.WriteLine('Current command: {0:x2} {1:x2} {2:x2}',cmd,t[0],t[1]);
					switch(t[0]){
						case 0x2F:
							bgm.WriteDelta(delta);delta=0;
							bgm.WriteBytes([0]);
							bgmS.Position=trackLenOffset;
							bgm.Write(UInt32(bgmS.Length-4-trackLenOffset));//update len
							bgmS.Seek(0,SeekOrigin.End);
							Console.WriteLine('end');
							break;
						case 0x51:
							t=(UInt32(mid.ReadByte())<<16)+(UInt32(mid.ReadByte())<<8)+mid.ReadByte();
							bgm.WriteDelta(delta);delta=0;
							//Console.WriteLine('  Tempo={0} ({1}; {2})',byte(60000000/t),60000000/t,t);
							bgm.WriteBytes([8,byte(60000000/t)]);
							break;
						case 0x58:
							t=[mid.ReadByte(),mid.ReadByte(),mid.ReadByte(),mid.ReadByte()];
							bgm.WriteDelta(delta);delta=0;
							bgm.WriteBytes([0x0c,t[0],t[1]]);
							break;
						case 6:
							t=mid.ReadBytes(t[1]);
							if(t.Length===9&&t[0]===108&&t[1]===111&&t[2]===111&&t[3]===112&&t[4]===83&&t[5]===116&&t[6]===97&&t[7]===114&&t[8]===116){
								bgm.WriteDelta(delta);delta=0;
								bgm.WriteBytes([2]);
							}else if(t.Length===7&&t[0]===108&&t[1]===111&&t[2]===111&&t[3]===112&&t[4]===69&&t[5]===110&&t[6]===100){
								bgm.WriteDelta(delta);delta=0;
								bgm.WriteBytes([3]);
							}
							break;
						case 0:case 1:case 2:case 3:case 4:case 5:case 7:case 8:case 9:case 10:case 11:case 12:case 13:case 14:case 15:case 16:
							//Text events, just ignore
						case 0x7f:
							//Sequencer-specific meta event
							midS.Position+=t[1];break;
						default:midS.Position+=t[1];
							Console.WriteLine('Unknown command1: 0x{0:x2} 0x{1:x2}',cmd,t[0]);
					}
				}else if((cmd&0xB0)===0xB0){
					cmd=mid.ReadByte();t=mid.ReadByte();
					switch(cmd){
						case 7:
							bgm.WriteDelta(delta);delta=0;
							bgm.WriteBytes([0x22,t]);
							break;
						case 11:
							bgm.WriteDelta(delta);delta=0;
							bgm.WriteBytes([0x24,t]);
							break;
						case 10:
							bgm.WriteDelta(delta);delta=0;
							bgm.WriteBytes([0x26,t]);
							break;
						case 64:
							bgm.WriteDelta(delta);delta=0;
							bgm.WriteBytes([0x3C,t]);
							break;
						default:
							Console.WriteLine('Unknown command2: 0xBx 0x{0:x2} (value={1})',cmd,t);
					}
				}else if((cmd&0xC0)===0xC0){
					bgm.WriteDelta(delta);delta=0;
					bgm.WriteBytes([0x20,mid.ReadByte()]);
				}else if((cmd&0x90)===0x90){
					t=[mid.ReadByte(),mid.ReadByte()];
					bgm.WriteDelta(delta);delta=0;
					if(t[0]===lKey){
						if(t[1]===lVelocity){
							bgm.WriteBytes([0x10]);
						}else{
							bgm.WriteBytes([0x13,lVelocity=t[1]]);
						}
					}else{
						if(t[1]===lVelocity){
							bgm.WriteBytes([0x12,lKey=t[0]]);
						}else{
							bgm.WriteBytes([0x11,lKey=t[0],lVelocity=t[1]]);
						}
					}
				}else if((cmd&0x80)===0x80){
					t=[mid.ReadByte(),mid.ReadByte()];
					bgm.WriteDelta(delta);delta=0;
					if(t[0]===lKey){
						bgm.WriteBytes([0x18]);
					}else{
						bgm.WriteBytes([0x1A,lKey=t[0]]);
					}
				}else{
					Console.WriteLine('Unknown command3: 0x{0:x2}',cmd);
				}
			}
			if(midS.Position!==tSzT){
				error(String.Format('Got a bad auto-offset! (pos={0}) Attempting to fix...',midS.Position));
				midS.Position=tSzT;
			}
		}
		bgmS.Position=16;
		bgm.Write(UInt32(bgmS.Length));
	}finally{bgm.Close();mid.Close();};
}main();
Console.ReadLine();