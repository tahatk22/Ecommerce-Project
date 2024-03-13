using AttractDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.Helpers
{
    public class PagingParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int? Color { get; set; }
        public int? SubCategory { get; set; }
        public int? Category { get; set; }
        public ProductTypeOption? ProductOption { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        private int pageSize = 1;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
