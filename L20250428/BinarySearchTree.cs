namespace L20250428
{
    internal class BinarySearchTree
    {

        static void Main()
        {
            BSTree tree = new();

            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(1);

            //tree.InorderSearch(tree._root);

            //tree.LevelOrderSearch();

            Console.WriteLine(tree.Contains(7));
        }

        

        class BSTree
        {
            public BSTreeNode _root;

            //Insert : 새로운 데이터를 트리에 추가
            //입력 : 새로운 정수 데이터 
            public void Insert(int data)
            {
                //root 가 null인지 먼저 생각해야 함
                if (this._root == null)
                {
                    BSTreeNode insertNode = new BSTreeNode(data, null, this);
                    this._root = insertNode;
                }
                else
                {
                    _root.Insert(data);
                }
               

            }

            //InorderSearch : 중위 순회 
            public void InorderSearch(BSTreeNode node)
            {
                if (node == null) return;
                InorderSearch(node._leftNode);
                Console.WriteLine(node._data);
                InorderSearch(node._rightNode);

            }
            /* public void InOrderSearch()
             {
                 if(_root == null)
                 {
                     return;
                 }
                 _root.InOrderSearch();
             }*/

            public void LevelOrderSearch()
            {
                //bool[] visited = new bool[100];
                Queue<BSTreeNode> q = new();
                q.Enqueue(_root);
                
                while(q.Count>0)
                {
                    BSTreeNode node = q.Dequeue();
                    Console.WriteLine(node._data);
                    if (node._leftNode != null)  q.Enqueue(node._leftNode);
                    if (node._rightNode != null)  q.Enqueue(node._rightNode);
                }
            }

            //Contains() : 주어진 데이터가 트리에 존재하는지 검사
            // 입력 : 검사할 데이터 int
            // 출력 : 있는지 존재 여부 (bool)
            /*public bool Contains(int data)
            {
                Queue<BSTreeNode> q = new();
                q.Enqueue(_root);

                while (q.Count > 0)
                {
                    BSTreeNode node = q.Dequeue();
                    if(data == node._data) return true;
                    if (node._leftNode != null) q.Enqueue(node._leftNode);
                    if (node._rightNode != null) q.Enqueue(node._rightNode);
                }
                return false;
            }*/

            public bool Contains(int data)
            {
                if(_root == null) return false;
                else return _root.Contains(data);
            }
        }

        class BSTreeNode
        {
            //public int Data { get; init; }
            public int? _data;

            public BSTreeNode? _leftNode;
            public BSTreeNode?_rightNode;
            public BSTreeNode? _parentNode;

            private BSTree _tree; //트리정보

            public BSTreeNode (int data, BSTreeNode parent, BSTree tree)
            {
                _data = data;
                _parentNode = parent;
                _tree = tree;
            }

            public void Insert(int data)
            {
                if(data > _data)
                {
                    if(_rightNode == null)
                    {
                        _rightNode = new BSTreeNode(data, this, _tree);
                    }
                    else
                    {
                        _rightNode.Insert(data);
                    }
                    
                }
                else
                {
                    if (_leftNode == null)
                    {
                        _leftNode = new BSTreeNode(data, this, _tree);
                    }
                    else
                    {
                        _leftNode.Insert(data);
                    }
                    
                }
            }

            /*public void InOrderSearch()
            {
                if (_leftNode != null)
                {
                    _leftNode.InOrderSearch();
                }

            }*/

            public bool Contains(int data)
            {
                if(_data == data) return true;
                else if(data<_data)
                {
                    if(_leftNode == null)
                    {
                        return false;
                    }
                    _leftNode.Contains(data);
                }
                else if (data > _data)
                {
                    if (_rightNode == null)
                    {
                        return false;
                    }
                    _rightNode.Contains(data);
                }
                return false;
            }
        }
    }
}
