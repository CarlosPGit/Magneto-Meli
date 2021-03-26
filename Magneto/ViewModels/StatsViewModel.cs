using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magneto.ViewModels
{
    public class StatsViewModel
    {
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public double ratio { get; set; }
    }
}
