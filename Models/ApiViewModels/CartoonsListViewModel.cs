using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cartoonaloge.Models.ApiViewModels
{
    public class CartoonsListViewModel
    {
        public string Name { get; set; }
        public DateTime WhenDebuted { get; set; }
        public int Seasons { get; set; }
        public string Studio { get; set; }
        public string Network { get; set; }
        public string Trivia { get; set; }
    }
}
