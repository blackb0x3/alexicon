namespace Alexicon.Helpers;

public class DatabasePathHelper
{
    private const string DbFileName = "firebotproxy.db";

    public static string DbFilePath => Path.Join(UserHomeFolder, DbFileName);

    public static string GetSqliteConnectionString() => $"Data Source={DbFilePath}";

    private static string UserHomeFolder => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
}