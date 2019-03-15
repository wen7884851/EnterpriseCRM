using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common
{
    public class CalculateFormula
    {
       
        public static decimal Calculate(string formulaContent,out bool isSuccess)
        {
            var stack_operator = new Stack<string>();
            var stack_number = new Stack<string>();
            int temp_count = 0;
            string substr = "";
            bool decimalPoint = false;
            formulaContent = formulaContent.Replace("  ", "").Trim();
            for (var i = 0; i < formulaContent.Length; i++)
            {
                substr = formulaContent.Substring(i, 1);
                if (Judgenumber(substr))
                {
                    if (temp_count == 0)
                    {
                        stack_number.Push(substr);
                    }
                    else
                    {
                        temp_count--;
                    }
                    if (Judgenumber(formulaContent.Substring(i + 1, 1)))
                    {
                        string link1 = stack_number.Pop();
                        if(substr==".")
                        {
                            decimalPoint = true;
                        }
                        else
                        {
                            if(decimalPoint)
                            {
                                link1 +="."+formulaContent.Substring(i + 1, 1);
                                decimalPoint = false;
                            }
                            else
                            {
                                link1 += formulaContent.Substring(i + 1, 1);
                            }
                        }
                        stack_number.Push(link1);
                        temp_count++;
                    }
                }
                else if (judgeoperator(substr))
                {
                    if (stack_operator.Count >= 1)
                    {
                        int new1 = judgelevel(substr);
                        int old1 = judgelevel(stack_operator.Peek());
                        if ((old1 < new1) || substr == "(")
                        {
                            stack_operator.Push(substr); 
                        }
                        else
                        {
                            if (substr == ")")
                            {
                                for (; stack_operator.Count > 0; stack_operator.Pop())
                                {
                                    if ((stack_operator.Contains("(")) && stack_operator.Peek() == "(")
                                    {
                                        stack_operator.Pop();
                                        break;
                                    }
                                    else
                                    {
                                        int temp1 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                        int temp2 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                        stack_number.Push(operator_dected(stack_operator.Peek(), temp1.ToString(), temp2.ToString()).ToString());
                                    }
                                }
                            }
                            else
                            {
                                for (; ((stack_operator.Count > 0 )&& stack_number.Count >= 2 )&& stack_operator.Peek() != "("; stack_operator.Pop())
                                {
                                    string temp_a = substr;
                                    int new2 = judgelevel(temp_a);
                                    int old2 = judgelevel(stack_operator.Peek());
                                    if ((old2 < new2) || substr == "(")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        int temp3 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                        int temp4 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                        stack_number.Push(operator_dected(stack_operator.Peek(), temp3.ToString(), temp4.ToString()).ToString());
                                    }
                                }
                                stack_operator.Push(substr);
                            }
                        }
                    }
                    else
                    {
                        stack_operator.Push(substr);
                    }
                }
                else
                {
                    isSuccess = false;
                    return 0;
                }
            }
            isSuccess = true;
            return Convert.ToDecimal(stack_number.Peek());
        }

        readonly static string[] numberString = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0","." };
        readonly static string[] operatorString = { "(", ")", "+", "-", "*", "/" };
        private static bool Judgenumber(string text)
        {
            return numberString.Contains(text);
        }
       
        private static bool judgeoperator(string text)
        {
            return operatorString.Contains(text);
        }
        private static decimal addition(object a, object b)
        {
            decimal d1 = decimal.Parse(a.ToString());
            decimal d2 = decimal.Parse(b.ToString());
            return d2 + d1;
        }
        private static decimal subduction(object a, object b)
        {
            decimal d1 = decimal.Parse(a.ToString());
            decimal d2 = decimal.Parse(b.ToString());
            return d2 - d1;
        }
        private static decimal multiplication(object a, object b)
        {
            decimal d1 = decimal.Parse(a.ToString());
            decimal d2 = decimal.Parse(b.ToString());
            return d2 * d1;
        }
        private static decimal division(object a, object b)
        {
            decimal d1 = decimal.Parse(a.ToString());
            decimal d2 = decimal.Parse(b.ToString());
            return d2 / d1;
        }
        private static int judgelevel(string text)
        {
            if (text.Equals("("))
            {
                return 1;
            }
            else if ((text.Equals(")") )|| text.Equals("("))
            {
                return 1;
            }
            else if ((text.Equals("+")) || text.Equals("-"))
            {
                return 2;
            }
            else if ((text.Equals("*")) || text.Equals("/"))
            {
                return 3;
            }
            else
                return 10;


        }
        private static decimal operator_dected(string types, string a, string b)
        {
            if (types == "+")
            {
                return addition(a, b);
            }
            else if (types == "-")
            {
                return subduction(a, b);
            }
            else if (types == "*")
            {
                return multiplication(a, b);
            }
            else if (types == "/")
            {
                return division(a, b);
            }
            else
                return 999;
        }
    }
}
