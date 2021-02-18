using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BinaryTrees
{
    //Basic Noode for the tree
    // It contains Left, Right Node as pointer
    //Also has datatype and Tokentype as attributes 
    public class Node
    {
        public string data;
        public TokenType tokenType;
        public Node Left  = null;
        public Node Right = null;
    }

    //Types of Token There are 
    public enum TokenType
    {
        Numbers,
        OpsAdd,
        OpsSub,
        OpsMult,
        OpsDiv,
        None
    }

    public class Token
    {
        public TokenType Type;
        public String value;
    }

    public class Tokeinzer
    {
        string text;

        public LinkedList<Token> tokenList = new LinkedList<Token>();

        //Tokenizer - Takes in in Input converts them to tokens
        //token Object
        public Tokeinzer(string text)
        {
            this.text = text;
            Tokenize();
        }


        private static TokenType Type(char c)
        {
            if (!Char.IsDigit(c))
            {
                return c switch
                {
                    '+' => TokenType.OpsAdd,
                    '-' => TokenType.OpsSub,
                    '*' => TokenType.OpsMult,
                    '/' => TokenType.OpsDiv,
                    _ => TokenType.None
                };
            }
            return TokenType.Numbers;
        }

        private void Tokenize()
        {
            var toTokenize = text;
            var buffer = "";
            for (var i = 0; i < toTokenize.Length; i++)
            {
                buffer += toTokenize[i];
                if (i < toTokenize.Length - 1)
                {
                    if (Type(toTokenize[i]) == Type(toTokenize[i + 1])) continue;
                    var token = new Token { Type = Type(toTokenize[i]), value = buffer };
                    buffer = "";
                    //tokenList.Append(token);
                    tokenList.AddLast(token);
                }
                else
                {
                    var token = new Token { Type = Type(toTokenize[i]), value = buffer };
                    //tokenList.Append(token);
                    tokenList.AddLast(token);
                }
            }
            Console.WriteLine();
        }

        public void Tokenize2()
        {
            var inputToTokenize = text;
            var tokenTypeTemp = Type(inputToTokenize[0]);
            var buffer = "";

            for (int i = 0; i < inputToTokenize.Length; i++)
            {
                buffer += inputToTokenize[i];

                if (Type(inputToTokenize[i]) != tokenTypeTemp)
                {
                    //Create new token for the previous buffer
                    var token = new Token();
                    token.Type = tokenTypeTemp;
                    token.value = buffer;

                    //Add to the List
                    tokenList.Append(token);

                    //Clean buffer
                    buffer = Convert.ToString(buffer[buffer.Length - 1]);

                    //Set the tempTokenType to the current token type
                    tokenTypeTemp = Type(inputToTokenize[i]);
                }
            }
        }
    }

    class Program
    {
        //Function that can create an add Expression
        private static string AddRandomString(int min, int max)
        {
            var rand = new Random();
            var one = Convert.ToString(rand.Next(min, max));
            var two = Convert.ToString(rand.Next(min, max));
            
            //Create a random for the ops 
            var randValue = rand.Next(0, 4);
            var stringOfRand = randValue switch
            {
                0 => "+",
                1 => "-",
                2 => "*",
                3 => "/",
                _ => ""
            };
            var strExpression = one + stringOfRand + two;
            return strExpression;
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("Enter Something");
            //var userInput = "324235+34536";
            //Console.ReadLine();
            var testExpression = AddRandomString(1, 10000);
            var token = new Tokeinzer(testExpression);
            var tokenList = token.tokenList;
            var parser = new ParserForCalculator(tokenList);
            Console.WriteLine("Hopefully This Works");
        }
    }
}
