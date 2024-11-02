using DAL.Concreate;
using DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVVM.Model
{
    public class CurrentUser
    {
        public ObjectId UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; } = null;
        public string? Password { get; set; }
        public BsonArray Interests { get; set; } = new BsonArray();
    }
}
