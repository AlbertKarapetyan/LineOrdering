using System;
using System.Collections.Generic;
using System.Text;

class MainClass
{

    public static int LineOrdering(string[] strArr)
    {

        Dictionary<char, List<char>> graph = new Dictionary<char, List<char>>();
        HashSet<char> nodes = new HashSet<char>();
        Dictionary<char, int> inEdges = new Dictionary<char, int>();

        foreach (var rel in strArr)
        {
            var relation = rel.Replace(" ", "");
            char p1 = relation[0];
            char symbol = relation[1];
            char p2 = relation[2];

            graph = CreateGraph(p1, p2, symbol, graph, ref inEdges);

            nodes.Add(p1);
            nodes.Add(p2);
        }

        List<char> retVal = new List<char>();

        return TopologicalSort(graph, inEdges, nodes, retVal);
    }

    static void Main()
    {

        string[] input = { "A>B", "A< C", "C< Z" };
        Console.WriteLine(LineOrdering(input));
    }

    static Dictionary<char, List<char>> CreateGraph(char p1, char p2, char symbol, Dictionary<char, List<char>> graph, ref Dictionary<char, int> inEdges)
    { 
        if (symbol == '>')
        {
            // exm. A>B
            if (!graph.ContainsKey(p1))
                graph[p1] = new List<char>() {
          p2
        };

            if (!inEdges.ContainsKey(p1))
                inEdges[p1] = 0;
            if (!inEdges.ContainsKey(p2))
                inEdges[p2] = 0;
            inEdges[p2]++;

        }
        else if (symbol == '<')
        {
            // exm. A<B
            if (!graph.ContainsKey(p2))
                graph[p2] = new List<char>() {
          p1
        };

            if (!inEdges.ContainsKey(p1))
                inEdges[p1] = 0;
            if (!inEdges.ContainsKey(p2))
                inEdges[p2] = 0;
            inEdges[p1]++;
        }

        return graph;

    }

    static int TopologicalSort(Dictionary<char, List<char>> graph, Dictionary<char, int> inEdges, HashSet<char> nodes, List<char> result)
    {
        int count = 0;
        bool act = false;

        foreach (char node in nodes)
        {
            if (!result.Contains(node) && inEdges[node] == 0)
            {
                act = true;
                result.Add(node);

                var vertexs = graph.ContainsKey(node) ? graph[node] : new List<char>();
                foreach (var vert in vertexs)
                {
                    inEdges[vert]--;
                }

                // Recursively explore further orders
                count += TopologicalSort(graph, inEdges, nodes, result);

                result.RemoveAt(result.Count - 1);

                foreach (var vert in vertexs)
                {
                    inEdges[vert]++;
                }
            }
        }

        if (result.Count == nodes.Count && !act)
        {
            return 1;
        }

        return count;
    }
}