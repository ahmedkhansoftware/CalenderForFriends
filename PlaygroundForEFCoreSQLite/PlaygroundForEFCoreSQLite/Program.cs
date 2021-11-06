using PracticeLibrary;
using System;

namespace PlaygroundForEFCoreSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}blogging.db";
            Console.WriteLine(DbPath);

            using (var db = new BloggingContext())
            {
                Post p1 = new();
                p1.Content = "John";
                p1.Title = null;

                db.Posts.Add(p1);
                db.SaveChanges();

                var UserOne = new User();
                UserOne.BirthDate = DateTime.Now;
                UserOne.EmailAddress = "FlippyJoe@Gmail.com";
                UserOne.FullName = "Ahmed Khan";
                UserOne.Guid = Guid.NewGuid().ToString();
                UserOne.PhoneNumber = "9119119119";

                db.Users.Add(UserOne);
                db.SaveChanges();
            }

        }
    }
}
