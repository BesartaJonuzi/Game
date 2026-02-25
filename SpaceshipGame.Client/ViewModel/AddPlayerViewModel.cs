using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SpaceshipGame.Model;

namespace SpaceshipGame.ViewModel
{
    public partial class AddPlayerViewModel : INotifyPropertyChanged
    {
        public ICommand AddPlayerAndStartGameCommand { get; }
        public ICommand RotateLeftCommand { get; }
        public ICommand RotateRightCommand { get; }

        public ObservableCollection<Player> Players { get; } = new();

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                if (_username == value) return;
                _username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        // Example state you might want to change from the triangle buttons
        private int _selectedShipIndex;
        public int SelectedShipIndex
        {
            get => _selectedShipIndex;
            set
            {
                if (_selectedShipIndex == value) return;
                _selectedShipIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedShipIndex)));
            }
        }

        public AddPlayerViewModel()
        {
            AddPlayerAndStartGameCommand = new Command(async () => await AddPlayerAndStartGame());
            RotateLeftCommand = new Command(OnRotateLeft);
            RotateRightCommand = new Command(OnRotateRight);
        }

        private void OnRotateLeft()
        {
            // Example behaviour: move selection left (wrap around)
            if (SelectedShipIndex <= 0) SelectedShipIndex = 2; else SelectedShipIndex--;
        }

        private void OnRotateRight()
        {
            // Example behaviour: move selection right (wrap around)
            if (SelectedShipIndex >= 2) SelectedShipIndex = 0; else SelectedShipIndex++;
        }

        

        private async Task AddPlayerAndStartGame()
        {
           
            //lägga till att add player i databasen i samma command?
            await Shell.Current.GoToAsync(nameof(View.GameView), new System.Collections.Generic.Dictionary<string, object>
            {
                { "player", new Player { PlayerName = Username, Score = 0 } }
            });

        }

        //private async Task AddPlayer()
        //{
        // if (string.IsNullOrWhiteSpace(Username)) return;
       // Players.Add(new Player { PlayerName = Username, Score = 0 });
        //    Username = string.Empty; // Clear input after adding
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
