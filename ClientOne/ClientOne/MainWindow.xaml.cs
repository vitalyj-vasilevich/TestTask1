using System.Windows;
using System.Net;
using System.IO;
using System;

namespace ClientOne
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Url = "https://localhost:44352/api/values"; 

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel();
        }

        private void EnableEditButton_CLick(object sender, RoutedEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;
            View.Visibility = Visibility.Hidden;
        }

        private void DragAndDrop_Drop(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Catching image");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {                        
            MessageBox.Show("Removing a book");
            Book book = (Book)ViewList.SelectedItem;
            string reqUrl = Url + "/" + book.Id.ToString();
            WebRequest webRequest = WebRequest.Create(reqUrl);
            webRequest.Method = "DELETE";
            webRequest.ContentType = "text/json";
            var writer = new StreamWriter(webRequest.GetRequestStream());
            writer.Write("");
            writer.Flush();
            writer.Close();
            /*var response = (HttpWebResponse)webRequest.GetResponse();
            response.Close();*/
            MessageBox.Show("Removal Succeed");
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            Add.Visibility = Visibility.Visible;
            View.Visibility = Visibility.Hidden;
        }

        private void AcceptAddButton_Click(object sender, RoutedEventArgs e)
        {
            View.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
        }

        private void AcceptEditButton_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book
            {
                Name = AddName.Text,
                Pages = Convert.ToInt32(AddPages.Text),
                Author = AddAuthor.Text,
                Id = 100
            };
            WebRequest webRequest = WebRequest.Create(Url);
            webRequest.Method = "PUT";
            webRequest.ContentType = "text/json";
            var writer = new StreamWriter(webRequest.GetRequestStream());
            writer.Write(book.GetJson());
            writer.Flush();
            writer.Close();
            Edit.Visibility = Visibility.Hidden;
            View.Visibility = Visibility.Visible;
            MessageBox.Show("Edit succeed");
        }
    }
}
