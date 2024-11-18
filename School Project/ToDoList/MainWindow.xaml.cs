using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded; // Trigger animation when the window loads
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a storyboard to combine multiple animations
            Storyboard storyboard = new Storyboard();

            // Fade-out animation
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(2),
                BeginTime = TimeSpan.FromSeconds(1), // Optional delay
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTarget(fadeOutAnimation, TodoText);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity"));

            // Scale animation (growing the text)
            DoubleAnimation scaleAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 3.0, // Final size of the text
                Duration = TimeSpan.FromSeconds(2),
                BeginTime = TimeSpan.FromSeconds(1),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(scaleAnimation, TodoText);
            Storyboard.SetTargetProperty(scaleAnimation, new PropertyPath("(TextBlock.RenderTransform).(ScaleTransform.ScaleX)"));

            DoubleAnimation scaleYAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 3.0, // Final size of the text
                Duration = TimeSpan.FromSeconds(2),
                BeginTime = TimeSpan.FromSeconds(1),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(scaleYAnimation, TodoText);
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("(TextBlock.RenderTransform).(ScaleTransform.ScaleY)"));

            // Combine animations in the storyboard
            storyboard.Children.Add(fadeOutAnimation);
            storyboard.Children.Add(scaleAnimation);
            storyboard.Children.Add(scaleYAnimation);

            // Trigger an event when the animation completes
            storyboard.Completed += Storyboard_Completed;

            // Start the storyboard
            storyboard.Begin();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            // Open the next window
            var nextWindow = new SecondWindow();
            nextWindow.Show();

            // Close the current window
            this.Close();
        }
    }
}
