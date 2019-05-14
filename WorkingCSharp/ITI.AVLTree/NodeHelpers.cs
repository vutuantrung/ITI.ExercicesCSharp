using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.AVLTree
{
    public class NodeHelpers
    {
        public Node CreateNode(int value, Node left = null, Node right = null)
        {
            Node newNode = new Node();
            newNode.Value = value;
            newNode.Left = left;
            newNode.Right = right;
            newNode.Height = 0;

            return newNode;
        }

        public int GetNodeHeight(Node node)
        {
            throw new NotImplementedException();
        }

        public Node SearchMin(Node node)
        {
            if (node == null) return null;
            if (node != null)
            {
                node = node.Left;
            }
            return node;
        }

        public Node SearchMax(Node node)
        {
            if (node == null) return null;
            if ( node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }

        public Node AddNode(int value)
        {
            throw new NotImplementedException();
        }

        public Node DeleteNode(Node node)
        {
            throw new NotImplementedException();
        }

        public Node SearchNodeByValue(int value)
        {
            throw new NotImplementedException();
        }

        public void BalanceTree(Node node)
        {
            throw new NotImplementedException();
        }

        public Node LeftRotation(Node node)
        {
            throw new NotImplementedException();
        }

        public Node RightRotation(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
