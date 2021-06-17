using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace Lab3_WPF_
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }



        Settings settings = null;
        private static List<Line> lines = new List<Line>();
        public static List<Line> getLines()
        {
            return lines;
        }

        

        private static List<Point> points = new List<Point>();
        public static List<Point> getPoints()
        {
            return points;
        }

        private static int counter = 0;
        public static int getCounter()
        {
            return counter;
        }

        //Point start;
        Point end;


        private void OnClickUndoBtn(object sender, RoutedEventArgs e)
        {
            int checker = counter;
            MyCanvas.Children.Remove(lines[counter - 2]);
            
            lines.RemoveAt(counter - 2);
            points.RemoveAt(counter - 1);
            counter--;
        }


        private void OnClickSaveBtn(object sender, RoutedEventArgs e)
        {
            settings = new Settings();
            settings.Save();
        }


        private void OnClickCleanAll(object sender, RoutedEventArgs e)
        {
            lines.Clear();
            points.Clear();
            MyCanvas.Children.Clear();
            counter = 0;
        }


        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            end = e.GetPosition(this);
            counter++;
            points.Add(end);
        }


        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (counter)
            {
                case 1:
                    break;
                default:
                    drawLine(points[counter - 2], points[counter - 1]);
                    break;
            }

        }


        private void drawLine(Point startPoint,Point endPoint)
        {
            Line newLine = new Line();
            newLine.Stroke = Brushes.Black;
            newLine.X1 = startPoint.X;
            newLine.Y1 = startPoint.Y - 80;
            newLine.X2 = endPoint.X;
            newLine.Y2 = endPoint.Y - 80;
            
            lines.Add(newLine);
            MyCanvas.Children.Add(newLine);
        }


       


        //private void drawPoint(Point p)
        //{
        //    Ellipse point = new Ellipse();
        //    point.Width = 8;
        //    point.Height = 8;
        //    point.Fill = Brushes.Black;
        //    point.StrokeThickness = 10;

        //    Canvas.SetLeft(point, p.X);
        //    Canvas.SetTop(point, p.Y - 80);

        //    MyCanvas.Children.Add(point);
        //}

    }

}
