using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BrownianMotion.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private double sigma = 20;
    [ObservableProperty]
    private double mean = 2;
    [ObservableProperty]
    private double initialPrice = 100.0;
    [ObservableProperty]
    private int numDays = 252;
    [ObservableProperty]
    private int numSimulations = 5;
    [ObservableProperty]
    private string lineStyle = "Solid";
    [ObservableProperty]
    private bool isSolidChecked = true;
    [ObservableProperty]
    private bool isDashedChecked;
    [ObservableProperty]
    private bool isDottedChecked;

    [ObservableProperty]
    private ObservableCollection<double[]> simulations = new();

    public MainViewModel()
    {
        GenerateSimulationCommand.Execute(null);
    }

    partial void OnIsDottedCheckedChanged(bool value)
    {
        if (value) LineStyle = "Dotted";
    }

    partial void OnIsDashedCheckedChanged(bool value)
    {
        if (value) LineStyle = "Dashed";
    }

    partial void OnIsSolidCheckedChanged(bool value)
    {
        if (value) LineStyle = "Solid";
    }

    [RelayCommand]
    private void GenerateSimulation()
    {
        Simulations.Clear();

        for (int i = 0; i < NumSimulations; i++)
        {
            var brownianMotion = new Models.BrownianMotion(
                Sigma / 100,
                Mean / 100,
                InitialPrice,
                NumDays
            );

            Simulations.Add(brownianMotion.Generate());
        }
    }

    [RelayCommand]
    private void ChangeLineStyle(string style)
    {
        LineStyle = style;
    }

}
