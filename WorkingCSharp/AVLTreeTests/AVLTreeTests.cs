using FluentAssertions;
using ITI.AVLTree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.AVLTreeTests
{
    public class AVLTreeTests
    {
        [Test]
        public void can_add_node()
        {
            Tree<int> tree = new Tree<int>();
            tree.Add(5);
            tree.Count.Should().Be(1);

            tree.Add(1);
            tree.Add(2);
            tree.Add(8);
            tree.Count.Should().Be(4);

            tree.Add(5);
            tree.Count.Should().Be(5);

            tree.SearchNode(tree.Head, 1).Count.Should().Be(1);
            tree.SearchNode(tree.Head, 2).Count.Should().Be(1);
            tree.SearchNode(tree.Head, 8).Count.Should().Be(1);
            tree.SearchNode(tree.Head, 5).Count.Should().Be(2);
        }
    }
}
