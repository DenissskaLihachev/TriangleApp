using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TriangleApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
        }
        float side_1, side_2, side_3, max, leg1, leg2;

        private void Side1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && !IsPunctuationMark(e.Text[0]))
            {
                e.Handled = true;
            }
            string text = Side1.Text;
            text = text.Replace('.', ',');
            Side1.Text = text;
            Side1.CaretIndex = Side1.Text.Length;
        }
        private void Side2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && !IsPunctuationMark(e.Text[0]))
            {
                e.Handled = true;
            }
            string text = Side2.Text;
            text = text.Replace('.', ',');
            Side2.Text = text;
            Side2.CaretIndex = Side2.Text.Length;
        }
        private void Side3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && !IsPunctuationMark(e.Text[0]))
            {
                e.Handled = true;
            }
            string text = Side3.Text;
            text = text.Replace('.', ',');
            Side3.Text = text;
            Side3.CaretIndex = Side3.Text.Length;
        }
        private bool IsPunctuationMark(char c)
        {
            // Define the allowed punctuation marks
            string punctuationMarks = @"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";
            return punctuationMarks.Contains(c);
        }

        private bool IsValidTriangle(double sideA, double sideB, double sideC)
        {
            return sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA;
        }
        private void DrawTriangle(Window window, double sideA, double sideB, double sideC)
        {
            Canvas canvas = new Canvas();
            canvas.Width = 400;
            canvas.Height = 400;

            Point pointA = new Point(200, 50);
            Point pointB = new Point(200 + sideB, 350);
            Point pointC = new Point(200 - sideA, 350);

            Polygon triangle = new Polygon();
            triangle.Points.Add(pointA);
            triangle.Points.Add(pointB);
            triangle.Points.Add(pointC);
            triangle.Stroke = Brushes.Black;
            triangle.Fill = Brushes.Purple;

            canvas.Children.Add(triangle);

            window.Content = canvas;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Side1.Text) || string.IsNullOrEmpty(Side2.Text) || string.IsNullOrEmpty(Side3.Text))
            {
                MessageBox.Show("Не оставляй пустых полей!");
            }
            else if (float.Parse(Side1.Text) <= 0 || float.Parse(Side2.Text) <= 0 || float.Parse(Side3.Text) <= 0)
            {
                MessageBox.Show("Сторона не может равняться нулю или быть меньше нуля!");
                Side1.Text = "";
                Side2.Text = "";
                Side3.Text = "";
            }
            else
            {
                side_1 = float.Parse(Side1.Text);
                side_2 = float.Parse(Side2.Text);
                side_3 = float.Parse(Side3.Text);

                if (side_1 + side_2 < side_3 || side_1 + side_3 < side_2 || side_2 + side_3 < side_1)
                {
                    MessageBox.Show("Невозможный треугольник!");
                }
                else
                {
                    if (side_1 > side_2)
                    {
                        if (side_1 > side_3)
                        {
                            max = side_1;
                            leg1 = side_2;
                            leg2 = side_3;
                        }
                        else
                        {
                            max = side_3;
                            leg1 = side_2;
                            leg2 = side_1;
                        }
                    }
                    else if (side_2 > side_3)
                    {
                        max = side_2;
                        leg1 = side_3;
                        leg2 = side_1;
                    }
                    else
                    {
                        max = side_3;
                        leg1 = side_2;
                        leg2 = side_1;
                    }

                    if (side_1 == side_2 && side_1 == side_3 && side_2 == side_3)
                    {
                        Result.Text = "Равносторонний";
                        if (side_1 * side_1 < side_2 * side_2 + side_3 * side_3 &&
                            side_2 * side_2 < side_1 * side_1 + side_3 * side_3 &&
                            side_3 * side_3 < side_1 * side_1 + side_2 * side_2)
                        {
                            Result2.Text = "Остроугольный";
                        }

                    }
                    else if (side_1 == side_2 || side_1 == side_3 || side_2 == side_3)
                    {
                        Result.Text = "Равнобедренный";
                        if ((side_1 * side_1 == side_2 * side_2 + side_3 * side_3 ||
                             side_2 * side_2 == side_1 * side_1 + side_3 * side_3 ||
                             side_3 * side_3 == side_1 * side_1 + side_2 * side_2))
                        {
                            Result2.Text = "Прямоугольный";
                        }
                        else if (side_1 * side_1 < side_2 * side_2 + side_3 * side_3 &&
                            side_2 * side_2 < side_1 * side_1 + side_3 * side_3 &&
                            side_3 * side_3 < side_1 * side_1 + side_2 * side_2)
                        {
                            Result2.Text = "Остроугольный";
                        }
                        else
                        {
                            Result2.Text = "Тупоугольный";
                        }
                    }
                    else if (side_1 != side_2 && side_1 != side_3 && side_2 != side_3)
                    {
                        Result.Text = "Разносторонний";
                        if ((side_1 * side_1 == side_2 * side_2 + side_3 * side_3 ||
                             side_2 * side_2 == side_1 * side_1 + side_3 * side_3 ||
                             side_3 * side_3 == side_1 * side_1 + side_2 * side_2))
                        {
                            Result2.Text = "Прямоугольный";
                        }
                        else if (side_1 * side_1 < side_2 * side_2 + side_3 * side_3 &&
                                side_2 * side_2 < side_1 * side_1 + side_3 * side_3 &&
                                side_3 * side_3 < side_1 * side_1 + side_2 * side_2)
                        {
                            Result2.Text = "Остроугольный";
                        }
                        else
                        {
                            Result2.Text = "Тупоугольный";
                        }
                    }
                }
            }
            try
            {
                double sideA = double.Parse(Side1.Text);
                double sideB = double.Parse(Side2.Text);
                double sideC = double.Parse(Side3.Text);

                if (IsValidTriangle(sideA, sideB, sideC))
                {
                    // Создание нового окна
                    Window newWindow = new Window();
                    newWindow.Title = "Треугольник";
                    newWindow.Width = 1185;
                    newWindow.Height = 700;
                    newWindow.Top = 0;
                    newWindow.Left = 344;

                    // Рисование треугольника в новом окне
                    DrawTriangle(newWindow, sideA, sideB, sideC);

                    // Открытие нового окна
                    newWindow.Show();
                }
                else
                {
                }
            }
            catch (FormatException)
            {
            }
        }
    }
}
