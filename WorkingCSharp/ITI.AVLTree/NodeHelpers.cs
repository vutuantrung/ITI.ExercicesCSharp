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
            if (node == null) return 0;
            if (node.Left == null && node.Right == null) return 1;
            return 1 + Math.Max(GetNodeHeight(node.Left), GetNodeHeight(node.Right));
        }

        public Node SearchMin(Node node)
        {
            if (node == null) return null;
            while(node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public Node SearchMax(Node node)
        {
            if (node == null) return null;
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }

        public Node AddNode(Node node, int value)
        {
            //throw new NotImplementedException();
            Node root = node;
            Node iterator = node;
            Node parentNode = null;

            if (iterator == null) return CreateNode(value);

            while(iterator != null)
            {
                parentNode = iterator;
                if (value > iterator.Value) iterator = iterator.Right;
                else iterator = iterator.Left;
            }

            Node newNode = CreateNode(value);
            if (value > parentNode.Value) parentNode.Right = newNode;
            else parentNode.Left = newNode;

            // Balance tree

            return root;
        }

        public Node DeleteNode(Node node)
        {
            throw new NotImplementedException();
        }

        public Node SearchNodeByValue(Node node, int value)
        {
            Node iterator = node;
            if (node == null) return null;

            if (value == iterator.Value) return iterator;

            if (value > iterator.Value)
            {
                if (iterator.Right == null) return null;
                return SearchNodeByValue(node.Right, value);
            }

            if (value < iterator.Value)
            {
                if (iterator.Left == null) return null;
                return SearchNodeByValue(node.Left, value);
            }
            return null;
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
