using Magneto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magneto.Services
{
    public interface IHuman
    {
        Task PostHuman(string dna, bool mutant);
    }
}
