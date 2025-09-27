namespace DirectoryManager;

public static class CurrentDirectory
{
    public static void Init()
    {
        // Example:
        // /home/micodiy/RiderProjects/UnifiedToolchain/UnifiedToolchain/bin/Debug/net8.0/UnifiedToolchain
        // -> /home/micodiy/RiderProjects/UnifiedToolchain

        var dir = Directory.GetCurrentDirectory();
        var dirToBin = dir[..dir.IndexOf("/bin", StringComparison.Ordinal)];
        var dirToSlash = dirToBin[..dirToBin.LastIndexOf('/')];
        Directory.SetCurrentDirectory(dirToSlash);
    }
}