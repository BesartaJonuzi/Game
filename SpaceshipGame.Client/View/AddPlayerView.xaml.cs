using SkiaSharp;
using SpaceshipGame.ViewModel;
using SpaceshipGame.Model;
using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SpaceshipGame.View;

public partial class AddPlayerView : ContentPage
{
	public AddPlayerView()
	{
		InitializeComponent();
		BindingContext = new AddPlayerViewModel();
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