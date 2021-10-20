using System;
using System.Collections.Generic;
using System.Numerics;

  /*Student Name: Nino Kristesiashvili
   * Proffesor: Magda Tsintsadze
   * COMPE361
   * Programming Assignment 2
   */
namespace ConsoleApp3
{

    class Program
    {
        static void Main(string[] args)
        {
            string operand = "";
            int index = 1;
            var text = System.IO.File.ReadAllLines(@"..\..\..\infint.txt");
            string[] lines = text;
            var values = new List<Infint>();

            foreach (string line in lines)
            {
                if ((index - 1) % 3 == 0) // if the operation was done during the previous iteration, it will empty the list
                {
                    values = new List<Infint>();
                }
                if (line == "-" || line == "*" || line == "+")
                {
                    operand = line;
                }
                var num = new Infint(line);
                values.Add(num);
                if (index % 3 == 0 && index != 0)
                {
                    DoOperation(values, operand);
                    index++;
                    continue;
                }
                index++;

            }
        }
        //outputs the sign as string
        private static string DetermineSign(Infint number1, Infint number2, string op)
        {
            if (number1.ModulusChecker(number2) == 2)
            {

                if (number1.isNegative)
                {
                    return "-";
                }
                else
                {
                    return "";
                }
            }
            else if (number1.ModulusChecker(number2) == -2)
            {
                if (number2.isNegative)
                {
                    return "-";
                }
                else
                {
                    if (op == "-")
                    {
                        return "-";
                    }
                    return "";

                }
            }
            else
            {
                return "";
            }
        }
        // determines which operation to do
        private static string DoOperation(List<Infint> vals, string op)
        {
            var number1 = vals[0];
            var number2 = vals[1];
            string num1 = String.Join("", number1.Digits);
            string num2 = String.Join("", number2.Digits);

            if (op == "+")
            {
                Console.WriteLine($"{(number1.isNegative ? "-" : "")}{Reverse(num1)} {op} {(number2.isNegative ? "-" : "")}{Reverse(num2)}" +
                    $" = {DetermineSign(number1,number2, op)} {number1.Plus(number2)}");

                return number1.Plus(number2);
            }

            if (op == "-")
            {
                Console.WriteLine($"{(number1.isNegative ? "-" : "")}{Reverse(num1)} {op} {(number2.isNegative ? "-" : "")}{Reverse(num2)} = {DetermineSign(number1, number2,op)}{number1.Minus(number2)}");

                return number1.Minus(number2);
            }

            if (op == "*")
            {
                Console.WriteLine($"{(number1.isNegative ? "-" : "")}{Reverse(num1)} " +
                    $"{op} {(number2.isNegative ? "-" : "")}{Reverse(num2)}" +
                    $" = {MultiplyResultSign(number1,number2)}" +
                    $"{number1.Multiply(number2)}");

                return number1.Multiply(number2);
            }
            return "";
        }
        //outputs the sign of the multiplication result
        private static string MultiplyResultSign(Infint num1, Infint num2)
        {
            if((num1.isNegative && !num2.isNegative) || (num2.isNegative && !num1.isNegative))
            {
                return "-";
            }
            else
            {
                return "";
            }
        }
       //revrses strings
        private static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}

