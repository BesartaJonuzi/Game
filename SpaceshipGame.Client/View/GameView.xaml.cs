using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using SkiaSharp.Views.Maui;
using SpaceshipGame.Model;
using SpaceshipGame.ViewModel;

namespace SpaceshipGame.View;

public partial class GameView : ContentPage, IQueryAttributable
{
    private GameViewModel? vm;
    private readonly Action? _invalidateHandler;

    public GameView()
    {
        InitializeComponent();
        // capture invalidate handler so we can unsubscribe properly
        _invalidateHandler = () => MainThread.BeginInvokeOnMainThread(() => canvasView.InvalidateSurface());
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("player", out var obj) && obj is Player player)
        {
            vm = new GameViewModel(player);
            BindingContext = vm;
            if (_invalidateHandler != null)
                vm.RequestInvalidate += _invalidateHandler;
        }
    }

    private void CanvasView_PaintSurface(object? sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        vm?.Render(e.Surface.Canvas, e.Info);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm?.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        vm?.Stop();
        if (vm != null && _invalidateHandler != null)
            vm.RequestInvalidate -= _invalidateHandler;
    }
}