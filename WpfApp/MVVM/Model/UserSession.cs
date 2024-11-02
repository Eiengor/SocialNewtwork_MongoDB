using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVVM.Model
{
    public static class UserSession
    {
        public static ObjectId LoggedInUserId { get; private set; }

        public static void SetCurrentUser(ObjectId userId)
        {
            LoggedInUserId = userId;
        }
    }
}
