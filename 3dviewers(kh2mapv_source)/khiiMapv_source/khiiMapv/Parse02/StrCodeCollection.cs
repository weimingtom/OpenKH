namespace khiiMapv.Parse02
{
    using System;
    using System.Collections.Generic;

    public class StrCodeCollection : List<StrCode>
    {
        public StrCodeCollection()
        {
            base..ctor();
            return;
        }

        public override unsafe string ToString()
        {
            string str;
            StrCode code;
            List<StrCode>.Enumerator enumerator;
            str = "";
            enumerator = base.GetEnumerator();
        Label_000D:
            try
            {
                goto Label_0024;
            Label_000F:
                code = &enumerator.Current;
                str = str + code.ToString();
            Label_0024:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_000F;
                }
                goto Label_003D;
            }
            finally
            {
            Label_002F:
                &enumerator.Dispose();
            }
        Label_003D:
            return str;
        }
    }
}

