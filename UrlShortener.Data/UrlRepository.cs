using shortid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UrlShortener.Data
{
    public class UrlRepository
    {
        private readonly string _connectionString;

        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string AddUrl(string url)
        {
            Url u = new Url();

            u.OriginalUrl = url;
            u.Hash = ShortId.Generate(10);
            while (HashInUse(u.Hash))
            {
                u.Hash = ShortId.Generate(10);
            }

            using (var context = new UrlContext(_connectionString))
            {
                context.Urls.Add(u);
                context.SaveChanges();
            }

            return u.Hash;
        }

        public string AddUrl(string url, int userId)
        {
            Url u = new Url();

            u.OriginalUrl = url;
            u.UserId = userId;
            u.Hash = ShortId.Generate(10);
            while (HashInUse(u.Hash))
            {
                u.Hash = ShortId.Generate(10);
            }

            using (var context = new UrlContext(_connectionString))
            {
                context.Urls.Add(u);
                context.SaveChanges();
            }

            return u.Hash;
        }

        public bool HashInUse(string hash)
        {
            using (var context = new UrlContext(_connectionString))
            {
                return context.Urls.Any(h => hash == h.Hash);
            }

        }



        public Url GetOriginalUrl(string hash)
        {
            using (var context = new UrlContext(_connectionString))
            {
                return context.Urls.FirstOrDefault(h => h.Hash == hash);
            }
        }

        //public IEnumerable<TaskItem> GetActiveTasks()
        //{
        //    using (var context = new TaskItemsContext(_connectionString))
        //    {
        //        return context.TaskItems.Include(t => t.User)
        //            .Where(t => !t.IsCompleted).ToList();
        //    }
        //}
    }
}
