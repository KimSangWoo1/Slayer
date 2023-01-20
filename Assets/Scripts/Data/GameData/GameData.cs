using System;

public abstract class GameData : IGameData, IDisposable
{
    public virtual void Init() {}

    public virtual void Release()
    {
        Dispose();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
