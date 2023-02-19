namespace NerdStore.Pagamentos.AntiCorruption.Configuration;

public class ConfigManager
{
    public string GetValue(string node)
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)])
            .ToArray());
    }
}