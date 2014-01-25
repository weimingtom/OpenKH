namespace khiiMapv.Put
{
    using System;

    public interface IPuteffect
    {
        void Apply();
        void Restore();
        void Save();
    }
}

