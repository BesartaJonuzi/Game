using Microsoft.Maui.Graphics.Platform;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System.Reflection;
using SpaceshipGame.View;
using SpaceshipGame.ViewModel;

namespace SpaceshipGame.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        
        BindingContext = new MainPageViewModel();

        //canvasView.PaintSurface += CanvasView_PaintSurface;
    }

    public void CanvasView_PaintSurface(object? sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        SKCanvas canvas = e.Surface.Canvas;
        SKImageInfo info = e.Info;
        canvas.Clear(SKColors.Black);
        using (SKPaint starPaint = new SKPaint { Color = SKColors.White })
        {
            for (int i = 0; i < 1000; i++)
            {
                float x = (float)(new Random().NextDouble() * info.Width);
                float y = (float)(new Random().NextDouble() * info.Height);
                canvas.DrawCircle(x, y, 2, starPaint);
            }
        }

    }


}
