using SpaceshipGame.View;
using SpaceshipGame.Views;

namespace SpaceshipGame;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AddPlayerView), typeof(AddPlayerView));
        Routing.RegisterRoute(nameof(GameView), typeof(GameView));
        Routing.RegisterRoute(nameof(HighscoreView), typeof(HighscoreView));

    }
}
