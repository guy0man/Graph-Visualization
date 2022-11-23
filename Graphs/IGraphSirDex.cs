namespace Graphs
{
    public interface IGraphSirDex<T>
    {
        /// <summary>
        /// Gets the numbeer of vertices in the graph
        /// </summary>
        int VertexCount { get; }
        /// <summary>
        /// Gets the list of vertices in the graph
        /// </summary>
        IList<T> Vertices { get; }
        /// <summary>
        /// Gets the vertex at the specified index
        /// </summary>
        /// <param name="index">The index of the vertex. Starting index is zero</param>
        /// <returns>Returns the vertex from the specified index</returns>
        T GetVertex(int index);
        /// <summary>
        /// Gets the index of the specified vertex
        /// </summary>
        /// <param name="vertex">The vertex to determine its index of the underlying container</param>
        /// <returns>Returns the index of the vertex. Indexes start at zero. </returns>
        int GetIndex(T vertex);

        IList<T> GetNeighbors(int vertexIndex);
        IList<int> GetNeighborsByIndex(int vertexIndex);
        int GetDegree(int vertexIndex);
        int GetDegree(T vertex);

        /// <summary>
        /// Generate the adjacency matrix of the graph
        /// This property will always recreate the adjacency matrix when called
        /// </summary>
        int[,] AdjacencyMatrix { get; }
        /// <summary>
        /// Gets the adjacency list of the graph. 
        /// Contains a list of the vertices and its neighbors. 
        /// The neighbords are specified using the index of the vertices.
        /// </summary>
        IList<IList<int>> Neighbors { get; }

        /// <summary>
        /// Calculate the shortest path of a weighted graph
        /// from the given starting vertex (using its index)
        /// to all other vertices.
        /// </summary>
        /// <param name="startingVertexIndex">The starting vertex in index form</param>
        /// <returns>Returns a path object which contains the Costs and Predecessors
        /// of the vertices. Also includes the search order.</returns>
        Path GetShortestPath(int startingVertexIndex);
        
    }
}