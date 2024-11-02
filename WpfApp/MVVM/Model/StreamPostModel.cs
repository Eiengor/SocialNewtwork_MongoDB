using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVVM.Model
{
    public class StreamPostModel
    {
        public ObjectId PostID { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime Date { get; set; }
        public string? AuthorName { get; set; }
    }
}
