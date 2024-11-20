using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Registor.Model;
using Registor.Services;
using Registor.View;
using System.Collections.ObjectModel;

namespace Registor.ViewModel;

public partial class CryptoModuleViewModel : BaseViewModel
{
    public ObservableCollection<CryptoModule> CryptoModules { get; } = new();
    CryptoModuleService cryptoModuleService { get; set; }
    [ObservableProperty]
    private CryptoModule? selectedModule;

    [ObservableProperty]
    private bool isModuleSelected;

    [ObservableProperty]
    private bool isNoModuleRegistered;

    [ObservableProperty]
    private bool isRefreshing;
    [ObservableProperty]
    private bool isModuleRegistered;
    [ObservableProperty]
    public string formatTcpPorts;

    public CryptoModuleViewModel(CryptoModuleService cryptoModuleService)
    {
        Title = "CryptoModule";
        this.cryptoModuleService = cryptoModuleService;
    }


    partial void OnSelectedModuleChanged(CryptoModule? value)
    {
        IsModuleSelected = value != null;
        UpdateUI();
    }

    [RelayCommand]
    public void SelectModule(CryptoModule module)
    {
        SelectedModule = module;
        IsModuleSelected = module != null;
        FormatTcpPorts = SelectedModule?.Ports != null && SelectedModule.Ports.Any()
            ? string.Join(",", SelectedModule.Ports)
            : "Не зареєстровані";
        UpdateUI();
    }

    private void UpdateUI()
    {
        IsNoModuleRegistered = CryptoModules.Count == 0;
        IsModuleRegistered = !IsNoModuleRegistered && !IsModuleSelected;
    }

    [RelayCommand]
    async Task GetCryptoModules()
    {
        if(IsBusy) 
          return;
        try
        {
            IsBusy = true;
            IsRefreshing = true;
            var modules = await cryptoModuleService.GetCryptoModulesAsync();

            if (CryptoModules.Count != 0)
                CryptoModules.Clear();

            foreach (var module in modules)
            {
                if (module != null)
                    CryptoModules.Add(module);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
            ClearSelectedModule();
        }
    }

    [RelayCommand]
    public void ClearSelectedModule()
    {
        SelectedModule = null;
        UpdateUI();
    }

    [RelayCommand]
    async Task DeleteModuleAsync()
    {
        if (SelectedModule is null)
            return;

        try
        {
            await cryptoModuleService.DeleteModuleAsync(SelectedModule.Id);
            CryptoModules.Remove(SelectedModule);
            ClearSelectedModule();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task GoToForm(CryptoModule? selectedModule)
    {

        if (selectedModule == null)
        {
            SelectedModule = null;
            await Shell.Current.GoToAsync(nameof(FormPage));
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(FormPage), new Dictionary<string, object>
            {
                { "CryptoModule", selectedModule }
            });
        }
    }
}
