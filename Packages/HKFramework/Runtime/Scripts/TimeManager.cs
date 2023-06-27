namespace HK.Framework.TimeSystems
{
    /// <summary>
    /// <see cref="Time"/>を管理するクラス
    /// </summary>
    public static class TimeManager
    {
        public static readonly Time System = new ();

        public static readonly Time Game = new (System);
    }
}
