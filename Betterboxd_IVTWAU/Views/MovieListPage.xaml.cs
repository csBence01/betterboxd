using Betterboxd_IVTWAU.ViewModels;

namespace Betterboxd_IVTWAU.Views;

public partial class MovieListPage : ContentPage
{
    public MovieListPage(MovieListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((MovieListViewModel)BindingContext).LoadCommand.ExecuteAsync(null);
    }
}