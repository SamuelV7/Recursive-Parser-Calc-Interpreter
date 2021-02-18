using System;
using System.Collections.Generic;
using BinaryTrees;

namespace BinaryTrees
{
    public enum Tree
    {
        Left,
        Right
    }

    public class BinaryTreeOfSyntax
    {
        public Node root = null;

        public bool isChildEmpty(Tree treePos)
        {
            if (root == null)
            {
                return true;
            }
            return treePos switch
            {
                Tree.Left => root.Left != null,
                Tree.Right => root.Right != null,
                _ => false
            };
        }
        public void AddChild(Node child, Tree pos)
        {
            var childNode = child;
            if (root == null)
            {
                root = childNode;
                return;
            }

            switch (pos)
            {
                case Tree.Left:
                    if (root.Left == null)
                    {
                        root.Left = childNode;
                        break;
                    }
                    if (root.Left != null)
                    {
                        root.Right = childNode;
                    }

                    break;
                case Tree.Right:
                    root.Right = childNode;
                    break;
                default:
                    Console.WriteLine("Error in Tree Pos");
                    break;
            }
        }

        public void AddRoot(Node Parent, Tree moveTreeTo)
        {
            var newParentNode = Parent;
            if (root == null)
            {
                root = newParentNode;
                return;
            }

            switch (moveTreeTo)
            {
                case Tree.Left:
                    newParentNode.Left = root;
                    root = newParentNode;
                    break;
                case Tree.Right:
                    newParentNode.Right = root;
                    root = newParentNode;
                    break;
                default:
                    Console.WriteLine("There is Error");
                    break;
            }

        }
    }

    public class ParserForCalculator
    {
        BinaryTreeOfSyntax SyntaxTree;
        LinkedListNode<Token> currentToken;
        
        public ParserForCalculator(LinkedList<Token> tokens)
        {
            var theTokens = tokens;
            currentToken = tokens.First;
            SyntaxTree = new BinaryTreeOfSyntax();
            //theTokens = token;
            //var SyntaxTree = new BinaryTreeOfSyntax();
            if (tokens is null)
            {
                Console.WriteLine("List is empty");
                throw new ArgumentNullException(nameof(tokens));
            }

            var tokLength = theTokens.Count;
            
            //-1 because otherwise when it gets to the last item
            //the next item will be null and the program will crash
            //Need to fix this in a better way
            for (var i = 0; i < theTokens.Count; i++)
            {
                ParseExpression();
                var linkedListNode = this.currentToken;
                if (linkedListNode != null) currentToken = linkedListNode.Next;
            }
        }


        private LinkedListNode<Token> nextToken()
        {
            return currentToken.Next;
        }

        private void ParseExpression()
        {
            if (currentToken.Value.Type == TokenType.Numbers)
            {
                ParseNumber();
            }
            switch (currentToken.Value.Type)
            {
                case TokenType.OpsMult:
                case TokenType.OpsDiv:
                case TokenType.OpsSub:
                case TokenType.OpsAdd:
                    var ops = new Node();
                    ops.tokenType = currentToken.Value.Type;
                    SyntaxTree.AddRoot(ops, Tree.Left);
                    break;
            }
        }

        private void ParseNumber()
        {
            if (currentToken.Value.Type == TokenType.Numbers)
            {
                var node = new Node
                {
                    data = currentToken.Value.value,
                    tokenType = currentToken.Value.Type
                };
                //var nextToken = currentToken.Next;
                SyntaxTree.AddChild(node, Tree.Left);
            }
            else
            {
                throw new Exception();
            }
        }
        //Need a Parse Factor or even more basic that is parse Ops
        //Ops like Div, Multi, Add, Subtract.
    }
}
