using System.Collections.Generic;

namespace FunApp.Models.DbModels
{
    public class Category
    {
        public Category()
        {
            this.Jokes = new HashSet<Joke>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Joke> Jokes { get; set; }
    }
}