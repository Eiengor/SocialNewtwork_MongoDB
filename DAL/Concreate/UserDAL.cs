using DAL.Abstract;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Concreate
{
    public class UserDAL : IUserDAL
    {
        private readonly IMongoCollection<User> _users;
        public UserDAL(IMongoCollection<User> users)
        {
            _users = users;
        }
        public void Add(User user)
        {
            if(string.IsNullOrEmpty(user.UserName))
            {
                throw new Exception("Username is empty");
            }
            if (string.IsNullOrEmpty(user.FirstName))
            {
                throw new Exception("First name is empty");
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                throw new Exception("Last name is empty");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new Exception("Email is empty");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Password is empty");
            }
            if (_users.Find<User>(u => u.Email == user.Email).FirstOrDefault() != null)
            {
                throw new Exception("There is already someone with this Email");
            }

            try
            {
                _users.InsertOne(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting user: {ex.Message}");
                throw;
            }
        }

        public void DeleteByID(ObjectId ID)
        {
            var userToDelete = Builders<User>.Filter.Eq(u => u.UserID, ID);
            _users.DeleteOneAsync(userToDelete);
        }

        public List<User> GetAll()
        {
            return _users.Find(Builders<User>.Filter.Empty).ToList();
        }

        public User GetByID(ObjectId ID)
        {
            return _users.Find<User>(u => u.UserID == ID).FirstOrDefault();
        }

        public User GetByUserName(string userName)
        {
            return _users.Find<User>(u => u.UserName == userName).FirstOrDefault();
        }

        public void Update(ObjectId ID, User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserID, ID);
            var update = Builders<User>.Update.
                Set(u => u.UserID, updatedUser.UserID).
                Set(u => u.UserName, updatedUser.UserName).
                Set(u => u.FirstName, updatedUser.FirstName).
                Set(u => u.LastName, updatedUser.LastName).
                Set(u => u.Password, updatedUser.Password).
                Set(u => u.Email, updatedUser.Email).
                Set(u => u.Interests, updatedUser.Interests);
                _users.UpdateOne(filter, update);
        }
        public bool UserExists(string username)
        {
            return _users.AsQueryable().Any(user => user.UserName == username);
        }
        public bool CorrectPassword(string username, string password)
        {
            var user = GetByUserName(username);
            return user != null && user.Password == password;
        }

        public void AddFollower(ObjectId userId, ObjectId followerID)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserID, userId);
            var update = Builders<User>.Update.AddToSet(u => u.FollowerIDs, followerID);
            var result = _users.UpdateOne(filter, update);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("Failed to add follower. User may not exist.");
            }
        }

        public void AddFollowing(ObjectId userId, ObjectId followingID)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserID, userId);
            var update = Builders<User>.Update.AddToSet(u => u.FollowingIDs, followingID);
            var result = _users.UpdateOne(filter, update);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("Failed to add following. User may not exist.");
            }
        }

        public void DeleteFollower(ObjectId userId, ObjectId followerID)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserID, userId);
            var update = Builders<User>.Update.Pull(u => u.FollowerIDs, followerID);
            var result = _users.UpdateOne(filter, update);
        }

        public void DeleteFollowing(ObjectId userId, ObjectId followingID)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserID, userId);
            var update = Builders<User>.Update.Pull(u => u.FollowingIDs, followingID);
            var result = _users.UpdateOne(filter, update);
        }
    }
}
