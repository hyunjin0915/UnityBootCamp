namespace L20250428
{
    internal class Program
    {
         const int INF = 987654321;

        private static int[][] graph;
        static void ConstructGraph()
        {
            graph = new int[7][];

            graph[0] = new int[] { 0, 7, INF, INF, 3, 10, INF };
            graph[1] = new int[] { 7, 0, 4, 10, 2, 6, INF };
            graph[2] = new int[] { INF, 4, 0, 2, INF, INF, INF };
            graph[3] = new int[] { INF, 10, 2, 0, 11, 9, 4 };
            graph[4] = new int[] { 3, 2, INF, 11, 0, INF, 5 };
            graph[5] = new int[] { 10, 6, INF, 9, INF, 0, INF };
            graph[6] = new int[] { INF, INF, INF, 4, 5, INF, 0 };
        }
        static void Main(string[] args)
        {
            ConstructGraph();
            int[] path = null;
            Console.WriteLine(GetDistance(0, 3, out path));

            int current = 3;

            while(true)
            {
                Console.WriteLine(current);
                current = path[current];
                if(current == path[current])
                {
                    Console.WriteLine(current);
                    break;
                }
            }

            //PrintWay(3, path);
        }

        //GetDistance : 최단 거리를 구하는 함수
        //입력 : 시작 정점, 도착 정점
        //출력 : 최단 거리
        

        public static void PrintWay(int index, int[] path)
        {
            if (index == 0) return;
            else
            {
                PrintWay(path[index], path);
                Console.WriteLine(index);
            }
        }
        public static int GetDistance(int start, int end, out int[] path)
        {
            //1. start에서 다른 모든 정점까지의 거리를 저장할 배열을 만든다
            int[] dist = new int[7];

            //2. dist 배열 초기화하기
            for (int i = 0; i < 7; i++)
            {
                dist[i] = INF;
            }
            dist[start] = 0;
            path = new int[7];


            //방문하지 않은 정점 중 dist 가 최소인 정점을 찾기 위한 우선순위 큐를 생성한다
            PriorityQueue<int, int> pq = new();
            pq.Enqueue(start, dist[start]);

            //3. 방문 한ㅇ 정점을 기록할 집합을 만든다
            bool[] isVisited = new bool[7];

            //모든 최단 경로를 찾을 때까지 반복
            while (pq.Count > 0)
            {
                //다음에 방문할 정점을 우선순위 큐에서 가져옴
                int next = pq.Dequeue();

                //dist 갱신
                for (int v = 0; v < graph[next].Length; v++) //연결된 정점만 살펴봄
                {
                    int distViaNext = dist[next] + graph[next][v];
                    //최단 거리 비교
                    //start -> next ->v 가 더 짧으면 dist[v]를 갱신하고 pq에 삽입
                    if (distViaNext < dist[v])
                    {
                        path[v] = next;
                        dist[v] = distViaNext;
                        pq.Enqueue(v, dist[v]);
                    }
                }
            }
            return dist[end];


            //4. 모든 최단 경로를 찾을 때까지 반복
            //4-1. 방문하지 않은 정점 중 dist 가 최소인 정점을 찾는다
            for (int count = 0; count < dist.Length; count++)
            {
                int next = 0;
                int minDist = dist[next];
                for (int i = 0; i < dist.Length; i++)
                {
                    if (isVisited[i] == false && dist[i] < minDist)
                    {
                        next = i;
                        minDist = dist[next];
                    }
                }
                //4-2. 방문
                isVisited[next] = true;

                //4-3. 최단 경로 갱신
                //next 경유해서 i 번째 노드로 가는 게 빠른지?
                for (int v = 0; v < graph[next].Length; v++)
                {
                    dist[v] = Math.Min(dist[v], dist[next] + graph[next][v]);
                }
            }


        }

    }
}
