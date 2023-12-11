namespace CR.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public CooperationViewModel CooperationViewModel { get; set; } = new();
}