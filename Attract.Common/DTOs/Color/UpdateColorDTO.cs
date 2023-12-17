using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Color
{
    public class UpdateColorDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string ColorHexa { get; set; }
    }
}
