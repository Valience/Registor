using System.Collections.ObjectModel;

namespace Registor.Model;
public class CryptoModule
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? ModuleName { get; set; }
    public string? SerialNumber { get; set; }
    public string? IPAddress { get; set; }
    public ObservableCollection<string>? Ports { get; set; } = new ObservableCollection<string>();
}
