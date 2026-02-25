using SkiaSharp;
using SpaceshipGame.Model;
using System;
using System.ComponentModel;
using Microsoft.Maui.Devices.Sensors;
using SpaceshipGame.Views;
using System.Windows.Input;

namespace SpaceshipGame.ViewModel
{
    public partial class GameViewModel : INotifyPropertyChanged
    {
        public ICommand StopGameCommand { get; }
        public string PlayerName { get; set; }
        public int Score { get; set; }

        private const int StarCount = 1000;
        private readonly SKPoint[] stars = new SKPoint[StarCount];
        private SKPoint shipPosition;
        private float cameraRotationX, cameraRotationY;
        private readonly Random random = new Random();
        private float baseSpeed = 20f;

        // Event the View subscribes to when it should invalidate the canvas
        public event Action? RequestInvalidate;

        private bool sensorsRunning;

        public GameViewModel(Player player)
        {
            PlayerName = player.PlayerName;
            Score = player.Score;

                InitializeStars();
            StopGameCommand = new Command(async () => await StopGameAsync());
        }

        public void Start()
        {
            if (sensorsRunning) return;

            if (Accelerometer.Default.IsSupported)
            {
                Accelerometer.Default.ReadingChanged += OnAccelerometerReadingChanged;
                Accelerometer.Default.Start(SensorSpeed.UI);
            }

            if (Gyroscope.Default.IsSupported)
            {
                Gyroscope.Default.ReadingChanged += OnGyroscopeReadingChanged;
                Gyroscope.Default.Start(SensorSpeed.UI);
            }

            sensorsRunning = true;
        }

        public void Stop()
        {
            if (!sensorsRunning) return;

            if (Accelerometer.Default.IsSupported)
            {
                Accelerometer.Default.ReadingChanged -= OnAccelerometerReadingChanged;
                try { Accelerometer.Default.Stop(); } catch { }
            }

            if (Gyroscope.Default.IsSupported)
            {
                Gyroscope.Default.ReadingChanged -= OnGyroscopeReadingChanged;
                try { Gyroscope.Default.Stop(); } catch { }
            }

            sensorsRunning = false;
        }

        public void Render(SKCanvas canvas, SKImageInfo info)
        {
            canvas.Clear(SKColors.Black);

            using (SKPaint starPaint = new SKPaint { Color = SKColors.White })
            {
                for (int i = 0; i < StarCount; i++)
                {
                    float x = stars[i].X - shipPosition.X;
                    float y = stars[i].Y - shipPosition.Y;

                    x += cameraRotationY * 20;
                    y += cameraRotationX * 20;

                    float z = 1000 + (x * x + y * y) / 20000;
                    float sx = x * 1000 / z + info.Width / 2;
                    float sy = y * 1000 / z + info.Height / 2;

                    if (sx < 0 || sx > info.Width || sy < 0 || sy > info.Height)
                    {
                        stars[i] = new SKPoint(
                            shipPosition.X + (float)(random.NextDouble() * 2000 - 1000),
                            shipPosition.Y + (float)(random.NextDouble() * 2000 - 1000)
                        );
                    }
                    else
                    {
                        canvas.DrawCircle(sx, sy, 2 / (z / 500), starPaint);
                    }
                }
            }

            using (var paint = new SKPaint
            {
                Color = SKColors.Cyan,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                TextSize = 40
            })
            {
                float speed = (float)Math.Sqrt(shipPosition.X * shipPosition.X + shipPosition.Y * shipPosition.Y);
                string speedText = $"Speed: {speed:F1}";

                using (var bgPaint = new SKPaint
                {
                    Color = SKColors.Black.WithAlpha(150),
                    Style = SKPaintStyle.Fill
                })
                {
                    float textWidth = paint.MeasureText(speedText);
                    float bgPadding = 20;
                    canvas.DrawRoundRect(
                        new SKRoundRect(new SKRect(
                            info.Width / 2 - textWidth / 2 - bgPadding,
                            info.Height - 60 - bgPadding,
                            info.Width / 2 + textWidth / 2 + bgPadding,
                            info.Height - 20 + bgPadding
                        ), 10, 10),
                        bgPaint
                    );
                }

                canvas.DrawText(speedText, info.Width / 2, info.Height - 40, paint);
            }
        }

        private void InitializeStars()
        {
            for (int i = 0; i < StarCount; i++)
            {
                stars[i] = new SKPoint(
                    (float)(random.NextDouble() * 2000 - 1000),
                    (float)(random.NextDouble() * 2000 - 1000)
                );
            }
        }

        private void OnAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            baseSpeed = Math.Max(5f, Math.Min(50f, 20f + e.Reading.Acceleration.Z * 30f));
            shipPosition.X += e.Reading.Acceleration.X * baseSpeed;
            shipPosition.Y -= e.Reading.Acceleration.Y * baseSpeed;

            RequestInvalidate?.Invoke();
        }

        private void OnGyroscopeReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            cameraRotationY += e.Reading.AngularVelocity.Y * 0.3f;
            cameraRotationX += e.Reading.AngularVelocity.X * 0.3f;

            RequestInvalidate?.Invoke();
        }

        private async Task StopGameAsync()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
