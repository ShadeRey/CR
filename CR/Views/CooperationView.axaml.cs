using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CR.ViewModels;

namespace CR.Views;

public partial class CooperationView : UserControl
{
    public CooperationView()
    {
        InitializeComponent();
    }

    public CooperationViewModel ViewModel => (DataContext as CooperationViewModel)!;

    private void CooperationSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (ViewModel.CooperationPreSearch is null)
        {
            ViewModel.CooperationPreSearch = ViewModel.Cooperation;
        }

        if (CooperationSearch.Text is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(CooperationSearch.Text))
        {
            CooperationDataGrid.ItemsSource = ViewModel.CooperationPreSearch;
            return;
        }

        Filter();
    }

    private void Filter()
    {
        if (CooperationSearch.Text is null)
        {
            return;
        }
        else
        {
            var filtered = ViewModel.CooperationPreSearch
                .Where(it => it.StartDate.ToString("dd.MM.yyyy").Contains(CooperationSearch.Text)).ToList();
            filtered = filtered.OrderBy(startDate => startDate.StartDate).ToList();
            CooperationDataGrid.ItemsSource = filtered;
        }
    }
}