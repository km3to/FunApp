using System.Collections.Generic;

namespace FunApp.Models.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexJokeViewModel> Jokes { get; set; }
    }
}