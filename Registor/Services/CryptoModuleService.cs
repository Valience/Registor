using System.Reflection;
using System.Text.Json;
using Registor.Model;
using Registor.Helpers;

namespace Registor.Services;
public class CryptoModuleService
{
    private readonly string filePath;

    public CryptoModuleService()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        filePath = Path.Combine(folderPath, "modules.json");
    }
    public async Task<List<CryptoModule>> GetCryptoModulesAsync()
    {
        if (!File.Exists(filePath))
            return new List<CryptoModule>();

        string json = await File.ReadAllTextAsync(filePath);
        var modules = JsonSerializer.Deserialize<List<CryptoModule>>(json) ?? new List<CryptoModule>();

        return CryptoModuleHelper.RemoveNullModules(modules);
    }

    public async Task SaveModulesAsync(List<CryptoModule> modules)
    {
        modules = CryptoModuleHelper.RemoveNullModules(modules);

        string json = JsonSerializer.Serialize(modules);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task AddModuleAsync(CryptoModule module)
    {
        var modules = await GetCryptoModulesAsync();
        modules.Add(module);
        await SaveModulesAsync(modules);
    }

    public async Task UpdateModuleAsync(CryptoModule updatedModule)
    {
        var modules = await GetCryptoModulesAsync();
        var existingModule = modules.FirstOrDefault(m => m.Id == updatedModule.Id);
        if (existingModule != null)
        {
            existingModule.SerialNumber = updatedModule.SerialNumber;
            existingModule.ModuleName = updatedModule.ModuleName;
            existingModule.IPAddress = updatedModule.IPAddress;
            existingModule.Ports = updatedModule.Ports;
            await SaveModulesAsync(modules);
        }
    }

    public async Task DeleteModuleAsync(string id)
    {
        var modules = await GetCryptoModulesAsync();
        var moduleToDelete = modules.FirstOrDefault(m => m.Id == id);
        if (moduleToDelete != null)
        {
            modules.Remove(moduleToDelete);
            await SaveModulesAsync(modules);
        }
    }
}
