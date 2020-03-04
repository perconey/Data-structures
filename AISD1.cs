using System;
using System.Collections.Generic;
using System.Linq;

namespace AISD1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Int32> elements = new List<Int32>
            {
                1,2,3,4,5,6,7,81,1,2,2,3,4,5,54,11,22,3,1,2,2,1,0,-2
            };

            Console.WriteLine(Alg.Max(elements));
            Console.WriteLine(Alg.Min(elements));

            ONP o = new ONP();
            o.ConvertToNormal("4 3 1 - 2 3 * ^ / =");
            Console.WriteLine(o.ConvertToONP("( 15 - 3 ) ^ ( 3 + 2 ) * 6 / 3"));

            Console.Read();
        }
    }

    public class Alg
    {
        public static Int32 Max(IEnumerable<Int32> elements)
        {
            Int32 maxElement = elements.First();
            foreach (Int32 e in elements)
                if (e > maxElement)
                    maxElement = e;
            return maxElement;
        }

        public static Int32 Min(IEnumerable<Int32> elements)
        {
            Int32 minElement = elements.First();
            foreach (Int32 e in elements)
                if (e < minElement)
                    minElement = e;
            return minElement;
        }

    }

    public class ONP
    {

        public ONP()
        {
        }

        public String ConvertToONP(String normal)
        {
            String onp = String.Empty;
            Stack<String> stack = new Stack<String>();
            String element = String.Empty;
            for (Int32 i = 0; i < normal.Length; i++)
            {
                element += normal[i];

                if (element == " ")
                {
                    element = String.Empty;
                    continue;
                }

                if (IsNumber(element))
                {
                    onp += element;
                    if ((i + 1) < normal.Length && normal[i + 1].ToString() != " ")
                    {
                        element = String.Empty;
                        continue;
                    }
                    else
                    {
                        onp += " ";
                        i++;
                        element = String.Empty;
                        continue;
                    }
                }
                else
                {
                    if (normal[i].ToString() == "(")
                    {
                        stack.Push(normal[i].ToString());
                        element = String.Empty;
                        continue;
                    }

                    if (normal[i].ToString() == ")")
                    {
                        while (stack.Peek() != "(")
                            onp += stack.Pop() + " ";
                        stack.Pop();
                        element = String.Empty;
                        continue;
                    }

                    if (stack.Count() >= 1)
                        while (OperatorPriority(stack.Peek()) >= OperatorPriority(normal[i].ToString()))
                        {
                            onp += stack.Pop() + " ";
                            if (stack.Count() == 0)
                                break;
                        }

                    stack.Push(normal[i].ToString());
                    element = String.Empty;
                }
            }

            while (true)
                if (stack.Count() > 0)
                {
                    onp += stack.Pop();
                }
                else break;

            return onp;
        }

        public void ConvertToNormal(String onp)
        {
            String element = String.Empty;
            Stack<String> stack = new Stack<String>();

            for (Int32 i = 0; i < onp.Length; i++)
            {
                element += onp[i].ToString();

                if (element == " ")
                {
                    element = String.Empty;
                    continue;
                }

                if ((i + 1) < onp.Length && onp[i + 1] != ' ')
                {
                    continue;
                }

                if (IsNumber(element))
                    stack.Push(element);
                else if (element == "=")
                {
                    Console.WriteLine(stack.Pop());
                    break;
                }
                else
                {
                    String o1 = stack.Pop();
                    String o2 = stack.Pop();
                    String push = $"({o2} {element} {o1})";
                    stack.Push(push);
                }

                element = String.Empty;
            }
        }

        private Boolean IsNumber(String check)
        {
            return check.All(c => Char.IsNumber(c));
        }

        private Int32 OperatorPriority(String oper)
        {
            if (oper == "^")
                return 3;
            else if (oper == "*" || oper == "/")
                return 2;
            else if (oper == "+" || oper == "-")
                return 1;
            else if (oper == "(")
                return 0;
            throw new ArgumentException("Bad operator");
        }

    }
}
