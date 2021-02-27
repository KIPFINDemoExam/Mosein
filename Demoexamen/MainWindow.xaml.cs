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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demoexamen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private UIElement Generator(Product product)
        {
            var im = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + product.MainImagePath.Trim(), UriKind.Absolute));
            var image = new Image
            {
                Source = im,
                Height = 100,
                Width = 100
            };

            var border = new Border();
            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            panel.Children.Add(image);
            var contentPanel = new StackPanel();
            var buttonPanel = new StackPanel();
            contentPanel.Children.Add(new Label
            {
                FontFamily = new FontFamily("Calibri"),
                Content = product.Title
            });
            contentPanel.Children.Add(new Label
            {
                FontFamily = new FontFamily("Calibri"),
                FontSize = 16,
                Content = product.Cost.ToString() + " руб."
            });

            var contentMode = new Button
            {
                Tag = product,
                Height = 40,
                Content = "Информация"
            };



            contentMode.Click += ContentMode_Click;
            buttonPanel.Children.Add(contentMode);
            contentPanel.Children.Add(buttonPanel);
            panel.Children.Add(contentPanel);
            
            border.BorderBrush = Brushes.Orange;
            border.BorderThickness = new Thickness(2);
            border.Child = panel;

            
            
            return border;
        }

        private void ContentMode_Click(object sender, RoutedEventArgs e)
        {
            var snd = (Button)sender;
            var product = (Product)snd.Tag;
            MessageBox.Show(string.IsNullOrEmpty(product.Description) ? "Описания нет" : product.Description);
        }

        public MainWindow()
        {
            InitializeComponent();

            using (var entities = new DatabaseEntities())
            {
                var products = entities.Product;
                foreach (var item in products)
                {
                    Panel.Children.Add(Generator(item));
                }
            }
        }
    }
}
