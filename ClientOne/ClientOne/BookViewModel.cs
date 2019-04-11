using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientOne
{
    public class BookViewModel :  INotifyPropertyChanged
    {
        private Book book;

        public BookViewModel(Book b)
        {
            book = b;
        }

        private int Id
        {
            get { return book.Id; }
        }

        public string Name
        {
            get { return book.Name; }
            set
            {
                book.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string CoverLink
        {
            get { return CoverLink; }
            set
            {
                CoverLink = value;
                OnPropertyChanged("CoverLink");
            }
        }

        public string Author
        {
            get { return Author; }
            set
            {
                Author = value;
                OnPropertyChanged("Author");
            }
        }

        public int Pages
        {
            get { return Pages; }
            set
            {
                Pages = value;
                OnPropertyChanged("Pages");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
