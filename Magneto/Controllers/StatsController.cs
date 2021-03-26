using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magneto.Services;
using Magneto.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magneto.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly MagnetoContext _context;
        public StatsController(MagnetoContext context)
        
        {
            _context = context;
        }

        [HttpGet]
        public async Task<StatsViewModel> getStats()
        {
            IHumanStats query = new HumanService(_context);
            var list = await query.GetHumans();

            StatsViewModel stats = new StatsViewModel();
            stats.count_mutant_dna = list.Where(m => m.mutant).Count();
            stats.count_human_dna = list.Where(m => !m.mutant).Count();
            stats.ratio = list.Count() > 0 ? Math.Round(Convert.ToDouble(stats.count_mutant_dna) / Convert.ToDouble(list.Count()), 2) : 0;
            
            return stats;
        }
    }
}
