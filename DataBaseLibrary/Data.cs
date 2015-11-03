using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLibrary
{
    public class Data
    {
        public static void CreateUser(string login, string password)
        {
            using (DataContext context = new DataContext())
            {
                //var user = context.Users
                //    .Where(u => u.Login == login)
                //    .Select(u => u.Id)
                //    .FirstOrDefault();

                //if (user != 0)
                {
                    User newUser = new User();
                    newUser.Login = login;
                    newUser.Password = password;

                    context.Users.Add(newUser);

                    context.SaveChanges();
                }

            }
        }

        public static List<string> PrintAllUser()
        {
            using (var context = new DataContext())
            {
                List<string> result = new List<string>();

                var users = context.Users.Select(u => u);

                foreach(var user in users)
                {
                    string temp = $"Login:\t{user.Login}\nPassword:\t{user.Password}";
                    result.Add(temp);
                }

                return result;
            }
        }
    }
}
