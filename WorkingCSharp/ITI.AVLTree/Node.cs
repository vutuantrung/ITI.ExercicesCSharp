using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.AVLTree
{
    public class Node<TNode> : IComparable<TNode> where TNode : IComparable
    {
        Node<TNode> _left;
        Node<TNode> _right;
        int _count;

        public int Count => _count;

        public TNode Value { get; set; }

        public Tree<TNode> Tree { get; set; }

        public Node<TNode> Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
                if (_left != null) _left.Parent = this;
            }
        }

        public Node<TNode> Right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
                if (_right != null) _right.Parent = this;
            }
        }

        public Node<TNode> Parent { get; set; }

        public Node(Tree<TNode> tree, TNode value, Node<TNode> parent)
        {
            Tree = tree;
            Value = value;
            Parent = parent;
            _count = 1;
        }

        public int BalanceFactor => Right.Height - Left.Height;

        public int Height => GetNodeHeight();

        public int GetNodeHeight()
        {
            if (_left == null && _right == null) return 1;
            return 1 + Math.Max(_left.Height, _right.Height);
        }

        public Node<TNode> Min() => SearchMin();

        public Node<TNode> SearchMin()
        {
            Node<TNode> iterator = this;
            while (iterator.Left != null)
            {
                iterator = iterator.Left;
            }
            return iterator;
        }

        public Node<TNode> Max() => SearchMax();

        public Node<TNode> SearchMax()
        {
            Node<TNode> iterator = this;
            while (iterator.Right != null)
            {
                iterator = iterator.Right;
            }
            return iterator;
        }

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

        public void Balance()
        {
            throw new NotImplementedException();
        }

        public void LeftRotation()
        {
            throw new NotImplementedException();
        }

        public void RightRotation()
        {
            throw new NotImplementedException();
        }

        public void LeftLeftRotation()
        {
            throw new NotImplementedException();
        }

        public void LeftRightRotation()
        {
            throw new NotImplementedException();
        }

        public void RightLeftRotation()
        {
            throw new NotImplementedException();
        }

        public void RightRightRotation()
        {
            throw new NotImplementedException();
        }

        public void IncrementCount()
        {
            _count++;
        }

        public Node<TNode> Predecessor()
        {
            throw new NotImplementedException();
        }

        public Node<TNode> Successor()
        {
            if (Right != null)
            {
                return Right.Min();
            }
            if (Parent != null)
            {
                Node<TNode> iterator = this;
                Node<TNode> parentNode = Parent;

                while (parentNode != null && iterator == parentNode.Right)
                {
                    iterator = parentNode;
                    parentNode = parentNode.Parent;
                }
                return parentNode;
            }
            return null;
        }

        private TreeState State
        {
            get
            {
                if (BalanceFactor > 1) return TreeState.RightHeavy;
                if (BalanceFactor < -1) return TreeState.LeftHeavy;
                return TreeState.Balanced;
            }
        }

        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy
        }
    }
}
