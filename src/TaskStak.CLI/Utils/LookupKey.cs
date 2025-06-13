namespace TaskStak.CLI.Utils
{
    public readonly struct LookupKey
    {
        public static string New() => Guid.NewGuid().ToString("N")[..8];
    }
}
