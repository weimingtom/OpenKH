namespace khkh_xldMii.Mx
{
    using khkh_xldMii.Mc;
    using System;
    using System.IO;

    internal class UtilAxBoneReader
    {
        public UtilAxBoneReader()
        {
            base..ctor();
            return;
        }

        public static AxBone read(BinaryReader br)
        {
            AxBone bone;
            bone = new AxBone();
            bone.cur = br.ReadInt32();
            bone.parent = br.ReadInt32();
            bone.v08 = br.ReadInt32();
            bone.v0c = br.ReadInt32();
            bone.x1 = br.ReadSingle();
            bone.y1 = br.ReadSingle();
            bone.z1 = br.ReadSingle();
            bone.w1 = br.ReadSingle();
            bone.x2 = br.ReadSingle();
            bone.y2 = br.ReadSingle();
            bone.z2 = br.ReadSingle();
            bone.w2 = br.ReadSingle();
            bone.x3 = br.ReadSingle();
            bone.y3 = br.ReadSingle();
            bone.z3 = br.ReadSingle();
            bone.w3 = br.ReadSingle();
            return bone;
        }
    }
}

