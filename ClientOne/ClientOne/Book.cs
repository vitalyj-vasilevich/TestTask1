using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ClientOne
{
    public class Book : INotifyPropertyChanged
    {
        private string name;
        private string coverLink;
        private string author;
        private int pages;
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string CoverLink
        {
            get { return coverLink; }
            set
            {
                coverLink = value;
                OnPropertyChanged("CoverLink");
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        public int Pages
        {
            get { return pages; }
            set
            {
                pages = value;
                OnPropertyChanged("Pages");
            }        
        }

        public string GetJson()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
