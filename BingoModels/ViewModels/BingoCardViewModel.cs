using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoModels.ViewModels
{
    public class BingoCardViewModel
    {
        public int[,] CardGrid { get; set; } = null!;
        public int Size { get; set; }
        public int MaxValue { get; set; }
        public int Id { get; set; }
        public List<int> SelectedNumbers { get; set; } = null!;
    }
}

