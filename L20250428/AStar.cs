namespace L20250428
{
    internal class AStar
    {
        const int INF = 987654321;

        public static void Main()
        {
            ConstructMap();
            FindStartAndEnd();
        }


        const int MAX_Y = 10;
        const int MAX_X = 10;
        static char[][] map = new char[MAX_Y][];

        //정점 : 좌표 => X, Y
        static int startX, startY, endX, endY;
        //FindStartAndEnd() : 시작 지점과 도착 지점을 찾는 함수
        static void FindStartAndEnd()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'S')
                    {
                        startX = j;
                        startY = i;
                    }
                    else if (map[i][j] == 'G')
                    {
                        endX = j;
                        endY = i;
                    }
                }
            }

        }
        // 맵을 구성한다.
        static void ConstructMap()
        {
            map[0] = "          ".ToCharArray();
            map[1] = "          ".ToCharArray();
            map[2] = "          ".ToCharArray();
            map[3] = "    #     ".ToCharArray();
            map[4] = " S  #  G  ".ToCharArray();
            map[5] = "    #     ".ToCharArray();
            map[6] = "          ".ToCharArray();
            map[7] = "          ".ToCharArray();
            map[8] = "          ".ToCharArray();
            map[9] = "          ".ToCharArray();
        }

        static void PrintMap()
        {

        }

        class AStarNode
        {
            //좌표값
            public int X;
            public int Y;
            //식에서 f 값을 의미 
            public int F;
            public AStarNode Path; //이전에 어디를 거쳐서 왔는지

        }

        /*//맵에 경로 표시하기
        public static void PrintWay(AStarNode node)
        {
            if (node.X == endX && node.Y == endY) return;
            else
            {
                PrintWay(node.Path);
                Console.WriteLine(node.X + "," + node.Y);
            }
        }*/


        //GetHeuristic : 휴리스틱을 구하는 함수 => 맨해튼 거리
        //입력 : 좌표 2개 
        public static int GetHeuristic(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x1 - x2);
            int dy = Math.Abs(y1 - y2);

            return dx + dy;
        }
        static void SetPath()
        {
            //각 좌표마다 맞는 노드를 생성해야 함
            AStarNode[,] path = new AStarNode[MAX_Y, MAX_X];
            for (int y = 0; y < MAX_Y; y++)
            {
                for (int x = 0; x < MAX_X; x++)
                {
                    path[y, x] = new AStarNode() { X = x, Y = y, F = INF };
                }
            }
            PriorityQueue<AStarNode, int> pq = new();
            pq.Enqueue(path[startY, startX], 0);
            //8방향 탐색
            int[] dy = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] dx = { -1, 0, 1, 1, 1, 0, -1, -1 };
            int[] dg = { 14, 10, 14, 10, 14, 10, 14, 10 }; //비용

            while (pq.Count > 0)
            {
                //경로를 찾을 때까지 반복
                //1. 다음에 방문할 정점을 가져옴
                AStarNode next = pq.Dequeue();

                if(next.X == endX && next.Y == endY)
                {
                    break;
                }

                //2. 8방향으로 탐색 진행
                for (int i = 0; i < 8; i++)
                {
                    int nx = next.X + dx[i];
                    int ny = next.Y + dy[i];

                    if (nx < 0 || ny < 0 || nx >= MAX_X || ny >= MAX_Y) continue;
                    if (map[ny][nx] == '#') continue; //벽이면 패스
                    //2-1. 부분 최단 경로를 찾아 pq에 삽입
                    int f = dg[i] + 10 * GetHeuristic(nx, ny, endX,endY);
                    AStarNode newNode = path[ny, nx];
                    if(newNode.F > f)
                    {
                        newNode.F = f;
                        newNode.Path = next;
                        pq.Enqueue(newNode, newNode.F);
                    }
                }   
            }

        }
       

    }
}
