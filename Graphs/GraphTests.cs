using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace Graphs
{
    public class GraphTests
    {
        [Test]
        public void GetShortestPath_GivenGraph_StartingB_CheckCostsPrevSearchOrder()
        {
            // arrange
            var vertices = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };
            // 0 : A    1 : B   2 : C
            // 3 : D    4 : E   5 : F
            // 6 : G    7 : H
            var edges = new List<Edge> {
	        // neighbors of A
	        new Edge(0,1,2),
            new Edge(0,2,1),
            new Edge(0,3,11),
            new Edge(0,4,13),
            new Edge(0,5,6),

	        // neighbors of B
	        new Edge(1,0,2),
            new Edge(1,2,8),
	        // neighbors of C
	        new Edge(2,0,1),
            new Edge(2,1,8),
            new Edge(2,5,3),
            new Edge(2,7,4),
	        // neighbors of D
	        new Edge(3,0,11),
	        // neighbors of E
	        new Edge(4,0,13),
	        // neighbors of F
	        new Edge(5,0,6),
            new Edge(5,2,3),
            new Edge(5,7,7),
	        // neighbors of G
	        new Edge(6,7,5),
            // neigbors of H
	        new Edge(7,2,4),
            new Edge(7,5,7),
            new Edge(7,6,5)
         };
            var g = new GraphSirDex<string>(vertices, edges);
            // act
            var pathResult = g.GetShortestPath(1);
            var expectedCosts = new List<float> {2,0,3,13,15,6,12,7 };
            var expectedPrev = new List<int> {1,-1,0,0,0,2,7,2};
            var expectedSearchOrder = new List<int> {1,0,2,7};

            // assert
            pathResult.Costs.Should().Equal(expectedCosts);
            pathResult.Prev.Should().Equal(expectedPrev);
            pathResult.SearchOrder.Should().Equal(expectedSearchOrder);
        }
        [Test]
        public void PrintAdjacencyList()
        {
            
            //arrange 
            var vertices = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" }; 
            // 0 : A    1 : B   2 : C
            // 3 : D    4 : E   5 : F
            // 6 : G    7 : H
            var edges = new List<Edge>();

            //neighbors of A
            edges.Add(new Edge(0, 1));
            edges.Add(new Edge(0, 2));
            edges.Add(new Edge(0, 4));
            edges.Add(new Edge(0, 5));
            //neighbors of B
            edges.Add(new Edge(1, 0));
            edges.Add(new Edge(1, 2));
            //neighbors of C
            edges.Add(new Edge(2, 0));
            edges.Add(new Edge(2, 1));
            edges.Add(new Edge(2, 5));
            edges.Add(new Edge(2, 7));
            //neighbors of D
            edges.Add(new Edge(3, 0));
            //neighbors of E
            edges.Add(new Edge(4, 0));
            //neighbors of F
            edges.Add(new Edge(5, 0));
            edges.Add(new Edge(5, 2));
            edges.Add(new Edge(5, 7));
            //neighbors of G
            edges.Add(new Edge(6, 0));
            //neighbors of H
            edges.Add(new Edge(7, 1));
            edges.Add(new Edge(7, 4));
            edges.Add(new Edge(7, 6));
            var g = new GraphSirDex<string>(vertices, edges);
            //act
            g.PrintAdjacencyList();
            //assert
        }
    }
}
