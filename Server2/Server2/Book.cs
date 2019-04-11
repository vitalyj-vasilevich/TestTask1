using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Server2
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public int Pages { get; set; }
        [DataMember]
        public string CoverLink { get; set; }
        [DataMember]
        public int Id { get; set; }

        public Book(string name = " ", string author = " ", int pages = -1, string coverLink = " ", int id = -1)
        {
            Name = name;
            Author = author;
            Pages = pages;
            CoverLink = coverLink;
            Id = id;
        }

        public int CompareTo(Book book)
        {
            return (Name.CompareTo(book.Name) + Author.CompareTo(book.Author) + Pages.CompareTo(book.Pages)
                + CoverLink.CompareTo(book.CoverLink) + Id.CompareTo(book.Id));
        }
    }
}
