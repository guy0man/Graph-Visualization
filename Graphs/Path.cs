using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Path
    {
        public IList<float> Costs { get; }
        public IList<int> Prev { get; }
        public IList<int> SearchOrder { get; set; }

        public Path(IList<float> costs, IList<int> prev, IList<int> searchOrder)
        {
            Costs = costs;
            Prev = prev;
            SearchOrder = searchOrder;
        }   
    }
}
