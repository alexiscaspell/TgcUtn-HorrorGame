using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    public class Dijkstra

    {
        #region ejemplo de como lo usa el chabon
        /*double[,] G = new double[4, 4];

            G[0, 1] = 3;

            G[0, 3] = 6;

            G[1, 3] = 1;

            G[1, 2] = 3;

            G[3, 2] = 1;

            dijstra dist = new dijstra(G, 4);

            var item = dist.dist;

            for (int i = 0; i < item.Length; i++)

            {

                listBox.Items.Add("Node " + i + " Path Distance = " + item[i]);

            }*/
        #endregion

        //Le meti a ciegas una lista para obtener un camino, xq este codigo solo retornaba distancia minima
        //la lista se llama caminos

        public Dijkstra(double[,] G, int s)

        {

            initial(0, s);

            while (queue.Count > 0)

            {

                int u = getNextVertex();

                for (int i = 0; i < s; i++)

                {

                    if (G[u, i] > 0)

                    {

                        if (dist[i] > dist[u] + G[u, i])

                        {

                            dist[i] = dist[u] + G[u, i];

                            caminos[i].Add(u);

                        }

                    }

                }

            }

        }



        public double[] dist { get; set; }

        int getNextVertex()

        {

            var min = double.PositiveInfinity;

            int vertex = -1;

            foreach (int val in queue)

            {

                if (dist[val] <= min)

                {

                    min = dist[val];

                    vertex = val;

                }

            }

            queue.Remove(vertex);

            return vertex;

        }

        List<int> queue = new List<int>();

        public List<List<double>> caminos { get; set; }

        public void initial(int s, int len)

        {

            dist = new double[len];

            caminos = new List<List<double>>();



            for (int i = 0; i < len; i++)

            {

                dist[i] = double.PositiveInfinity;

                queue.Add(i);

                caminos.Add(new List<double>());

            }

            dist[0] = 0;

        }
    }
}
