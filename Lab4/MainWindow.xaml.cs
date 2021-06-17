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

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private Point pos = new Point();
        private Image img = new Image();


        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pos = e.GetPosition(this);
            drawPicture();
            MoveTo(img, 100, 120);
        }




        private void drawPicture()
        {
            img.Height = 50;
            img.Width = 50;
            

            Canvas.SetLeft(img, pos.X);
            Canvas.SetTop(img, pos.Y);

            img.Source = new BitmapImage(new Uri(@"images\pig.png",UriKind.Relative) );

            myCanvas.Children.Add(img);
        }




        private static void MoveTo(Image target, double newX, double newY)
        {
            var top = Canvas.GetTop(target);
            var left = Canvas.GetLeft(target);
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(top, newY - top, TimeSpan.FromSeconds(10));
            DoubleAnimation anim2 = new DoubleAnimation(left, newX - left, TimeSpan.FromSeconds(10));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);
        }





    }
}
