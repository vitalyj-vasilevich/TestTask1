using System.IO;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Server2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public Book[] books;
        //public DataContractJsonSerializer jsonFormatter;

        /*ValuesController()
        {
            jsonFormatter = new DataContractJsonSerializer(typeof(LinkedList<Book>));

            using (FileStream fs = new FileStream("book.js", FileMode.OpenOrCreate))
            {
                books = (LinkedList<Book>)jsonFormatter.ReadObject(fs);
            }
        }*/

        public void ReadBooks()
        {
            /*jsonFormatter = new DataContractJsonSerializer(typeof(Book[]));

            using (FileStream fs = new FileStream("book.json", FileMode.Open))
            {
                books = (Book[])jsonFormatter.ReadObject(fs);
            }*/
            using (StreamReader sr = new StreamReader("book.json"))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                books = (Book[])jsonSerializer.Deserialize(sr, typeof(Book[]));
            }                            
        }

        public void UpdateFile()
        {
            using (StreamWriter sw = new StreamWriter("book.json"))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(sw, typeof(Book[]));
            }
        }

        public Book GetBook(string json)
        {
            Book book = JsonConvert.DeserializeObject<Book>(json);
            return book;
        }

        public string GetJson()
        {
            ReadBooks();
            string json = JsonConvert.SerializeObject(books);
            return json;
        }

        public string GetBookToJson(Book book)
        {
            string json = JsonConvert.SerializeObject(book);
            return json;
        }

        public Book GetBookById(int id)
        {
            Book book = new Book();
            foreach (var b in books)
            {
                if (b.Id == id)
                {
                    book = b;
                    break;  
                }
            }
            return book;
        }

        public int GetBookIndexById(int index)
        {
            for (int i = 0; i < books.Length; ++i)
            {
                if (books[i].Id == index)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetBookIndex(Book book)
        {
            for (int i = 0; i < books.Length; ++i)
            {
                if (books[i].Id == book.Id)
                {
                    return i;
                }
            }
            return -1;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return GetJson();            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return GetBookToJson(GetBookById(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values
        [HttpPut]
        public void Put([FromBody] string value)
        {
            Book book = GetBook(value);
            int index = GetBookIndex(book);
            if (index == -1)
            {
                Array.Resize(ref books, books.Length + 1);
                books[books.Length - 1] = book;
            }
            else
            {
                books[index] = book;
            }
            UpdateFile();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            int index = GetBookIndexById(id);
            if (index != -1)
            {
                if (index != books.Length - 1)
                {
                    Book tmp = books[index];
                    books[index] = books[books.Length - 1];
                    books[books.Length - 1] = tmp;
                }
                Array.Resize(ref books, books.Length - 1);
            }
            UpdateFile();
        }
    }
}
