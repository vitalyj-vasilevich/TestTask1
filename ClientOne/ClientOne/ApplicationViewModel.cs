using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace ClientOne
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Book selectedBook;
        private static int freeId;

        public ObservableCollection<Book> Books { get; set; }
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        public ApplicationViewModel()
        {
            freeId = 0;
            Books = new ObservableCollection<Book>();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44352/api/values");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "Get";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                var books = (Book[])jsonSerializer.Deserialize(sr, typeof(Book[]));
                foreach (var b in books)
                {
                    Books.Add(b);
                    freeId += b.Id;
                }
            }
            selectedBook = Books[0];
        }
            
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
