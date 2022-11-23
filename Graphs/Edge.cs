using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// A weighted edge for a weighted graph. 
    /// For unweighted edges, a default zero weight is used.
    /// </summary>
    public class Edge
    {
        public Edge(int from, int to, float weight = 0)
        {
            From = from;
            To = to;
            Weight = weight;
        }
        /// <summary>
        /// Specified the starting vertex for this edge instance
        /// IMPORTANT: the vertex is represented by an int index
        /// </summary>
        public int From { get; }
        public int To { get; }
        public float Weight { get; }

    }
}
