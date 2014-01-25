namespace khiiMapv
{
    using System;

    public class DC
    {
        public Guid dcId;
        public string name;
        public M4 o4Map;
        public Parse4Mdlx o4Mdlx;
        public MTex o7;

        public DC()
        {
            this.dcId = Guid.NewGuid();
            base..ctor();
            return;
        }
    }
}

