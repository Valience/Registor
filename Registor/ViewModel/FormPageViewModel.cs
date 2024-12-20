﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Registor.Model;
using Registor.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Registor.ViewModel;

[QueryProperty(nameof(CryptoModule), "CryptoModule")]
public partial class FormPageViewModel : BaseViewModel
{
    private readonly CryptoModuleService cryptoModuleService;

    [ObservableProperty]
    private CryptoModule cryptoModule;

    [ObservableProperty]
    private bool isEditMode;
    [ObservableProperty]
    private bool isDefaultPortsChecked;
    [ObservableProperty]
    private string validationMessage;

    public FormPageViewModel(CryptoModuleService cryptoModuleService)
    {
        this.cryptoModuleService = cryptoModuleService;
        CryptoModule = new CryptoModule();
        IsEditMode = false;
    }
    partial void OnCryptoModuleChanged(CryptoModule value)
    {
        if (value == null)
        {
            CryptoModule = new CryptoModule();
            IsEditMode = false;
            Title = "Register Module";
            IsDefaultPortsChecked = false;
            return;
        }
        if (value.Ports.Count > 0)
            IsDefaultPortsChecked = true;

        IsEditMode = !string.IsNullOrEmpty(value.ModuleName);
        Title = IsEditMode ? "Edit Module" : "Register Module";
    }

    partial void OnIsDefaultPortsCheckedChanged(bool value)
    {
        if (CryptoModule?.Ports == null) return;

        if (value && CryptoModule.Ports.Count < 1)
        {
            CryptoModule.Ports.Clear();
            foreach (var port in new[] { "3011", "3012", "3013", "3014", "3015", "3016" })
            {
                CryptoModule.Ports.Add(port);
            }
        }
        else if (!value && CryptoModule.Ports.Count > 0)
        {
            CryptoModule.Ports.Clear();
        }

    }
 

    [RelayCommand]
    private static async Task CancelModuleAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task SaveModuleAsync()
    {
        if (!IsValidModule())
        {
            await Shell.Current.DisplayAlert("Error", ValidationMessage, "OK");
            return;
        }
        try
        {
            if (IsEditMode)
            {
                await cryptoModuleService.UpdateModuleAsync(CryptoModule);
            }
            else
            {
                await cryptoModuleService.AddModuleAsync(CryptoModule);
            }

            CryptoModule = new CryptoModule();
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
    private bool IsValidModule()
    {
        if (string.IsNullOrWhiteSpace(CryptoModule.ModuleName) || CryptoModule.ModuleName.Length > 20)
        {
            ValidationMessage = "Module name is required and should be 1-20 characters long.";
            return false;
        }

        if (!int.TryParse(CryptoModule.SerialNumber, out int serialNumber) || serialNumber < 1 || serialNumber > 999)
        {
            ValidationMessage = "Serial number is required and must be between 1 and 999.";
            return false;
        }


        if (!IsValidIPAddress(CryptoModule.IPAddress))
        {
            ValidationMessage = "IP address is not valid. Please enter a valid IP address.";
            return false;
        }

        foreach (var port in CryptoModule.Ports)
        {
            if (!int.TryParse(port, out int portNumber) || portNumber < 1 || portNumber > 65535)
            {
                ValidationMessage = $"Port '{port}' is not valid. Each port must be a number between 1 and 65535.";
                return false;
            }
        }

        return true;
    }

    private bool IsValidIPAddress(string ipAddress)
    {
        string pattern = @"^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])$";

        return Regex.IsMatch(ipAddress, pattern);
    }
}