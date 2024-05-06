using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
public class ElectronicsConfig
{
    public ElectronicsAddConfig config;
    public const string filepath = @"ElectronicsAddConfig.json";
    public ElectronicsConfig()
    {
        try
        {
            ReadConfigFile();
        }
        catch (Exception)
        {
            SetDefault();
            WriteNewConfigFile();
        }
    }
    private ElectronicsAddConfig ReadConfigFile()
    {
        string configJsonData = File.ReadAllText(filepath);
        config = JsonSerializer.Deserialize<ElectronicsAddConfig>(configJsonData);
        return config;
    }

    private void SetDefault()
    {
        config = new ElectronicsAddConfig("[REDACTED] ", "[REDACTED] ", "None", 0, false);
    }
    private void WriteNewConfigFile()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(config, options);
        File.WriteAllText(filepath, jsonString);
    }

}

