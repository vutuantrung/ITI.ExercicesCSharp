using ITI.AVLTree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.AVLTreeTests
{
    public class AVLTreeTests
    {
        NodeHelpers _nodeHelper = new NodeHelpers();

        [Test]
        public void can_create_node()
        {
            Node newNode = _nodeHelper.CreateNode(1);

            Assert.That(newNode.Value == 1);
            Assert.That(newNode.Left == null);
            Assert.That(newNode.Right == null);
        }
    }
}
