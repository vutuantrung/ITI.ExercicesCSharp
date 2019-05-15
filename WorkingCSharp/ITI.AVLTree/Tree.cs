using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ITI.AVLTree
{
    public class Tree<T> : IEnumerable<T> where T : IComparable
    {
        public Node<T> Head { get; set; }

        int _count;

        public int Count => _count;

        public Tree()
        {
            Head = null;
            _count = 0;
        }

        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new Node<T>(this, value, null);
            }
            else
            {
                AddToTree(Head, value);
            }
            _count++;
        }

        private void AddToTree(Node<T> node, T value)
        {
            if(value.CompareTo(node.Value) == 0)
            {
                node.IncrementCount();
            }

            if (value.CompareTo(node.Value) < 0)
            {
                if(node.Left == null)
                {
                    Node<T> newNode = new Node<T>(this, value, node);
                    node.Left = newNode;
                    newNode.Parent = node;
                }
                else
                {
                    AddToTree(node.Left, value);
                }
            }

            if (value.CompareTo(node.Value) > 0)
            {
                if (node.Right == null)
                {
                    Node<T> newNode = new Node<T>(this, value, node);
                    node.Right = newNode;
                    newNode.Parent = node;
                }
                else
                {
                    AddToTree(node.Right, value);
                }
            }
        }

        public void RemoveNode(T value)
        {
            //throw new NotImplementedException();
            if (Head == null) return;
            Node<T> deleteNode = SearchNode(value);
            if (deleteNode == null) return;

            if(deleteNode.Left == null && deleteNode.Right == null)
            {
                deleteNode.Parent = null;
            }
            else if(deleteNode.Left != null && deleteNode.Right != null)
            {
                Node<T> nodeReplace = deleteNode.Right.Min();
            }
            else
            {

            }
        }

        public Node<T> SearchNode(T value)
        {
            if (Head == null) return null;
            return SearchNode(Head, value);
        }

        public Node<T> SearchNode(Node<T> node, T value)
        {
            if (value.CompareTo(node.Value) == 0) return node;
            if(value.CompareTo(node.Value) < 0 && node.Left != null)
            {
                return SearchNode(node.Left, value);
            }
            if (value.CompareTo(node.Value) > 0 && node.Right != null)
            {
                return SearchNode(node.Right, value);
            }
            return null;
        }

        public bool Contains(T value) => SearchNode(Head, value) != null;

        public void Clear()
        {
            Head = null;
            _count = 0;
        }

        class EE : IEnumerator<T>
        {
            readonly Tree<T> _tree;
            Node<T> _currentNode;

            public EE(Tree<T> tree)
            {
                _tree = tree;
                _currentNode = _tree.Head;
            }

            public T Current
            {
                get
                {
                    if (_currentNode == null) throw new InvalidOperationException();
                    return _currentNode.Value;
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_currentNode.Right != null)
                {
                    _currentNode = _currentNode.Right;
                    return true;
                }
                if(_currentNode.Parent != null)
                {
                    Node<T> parentNode = _currentNode.Parent;
                    while(parentNode != null && _currentNode == parentNode.Right)
                    {
                        _currentNode = parentNode;
                        parentNode = parentNode.Parent;
                    }
                    return true;
                }
                return false;
            }

            public void Dispose()
            {
                throw new NotSupportedException();
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EE(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
