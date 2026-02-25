using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SpaceshipGame.View;

namespace SpaceshipGame.ViewModel
{
    public class MainPageViewModel
    {
        public ICommand StartGameCommand { get; }
        public ICommand HighScoreCommand { get; }

        public MainPageViewModel()
        {
            StartGameCommand = new Command(async () => await StartGameAsync());
            HighScoreCommand = new Command(async () => await HighScoreAsync());
        }

        private async Task StartGameAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddPlayerView));
        }

        private async Task HighScoreAsync()
        {
            await Shell.Current.GoToAsync(nameof(HighscoreView));
        }
    }
}