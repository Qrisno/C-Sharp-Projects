using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Infint : IComparable<Infint>
    {
        public List<int> Digits { get; set; }
        public bool isNegative { get; set; }
        const int FIRST_NUMBER_POS = 4;
        const int FIRST_NUMBER_NEG = -4;
        const int FIRST_NUMBER_GREATER_NEG = 3;
        const int FIRST_NUMBER_LESS_NEG = -3;
        const int FIRST_NUMBER_GREATER = 2;
        const int FIRST_NUMBER_LESS = -2;
        const int FIRST_NUMBER_EQUAL_NEG = 1;
        const int EQUAL = -1;
        public Infint(string num)
        {

            if (num.StartsWith('-'))
            {
                isNegative = true;
            }
            else
            {
                isNegative = false;
            }

            Digits = new List<int>();
            if (isNegative)
            {
                for (int j = 1; j < num.Length; j++)
                {
                    int digit;
                    int.TryParse(num[j].ToString(), out digit);
                    Digits.Add(digit);
                }
            }
            else
            {
                for (int j = 0; j < num.Length; j++)
                {
                    int digit;
                    int.TryParse(num[j].ToString(), out digit);
                    Digits.Add(digit);
                }
            }
            Digits.Reverse();

        }
        // re-reverse the list and output the string
        private string AdjustList(List<int> list)
        {
            string final = "";
            list.Reverse();
            list.ForEach(num => final += num.ToString());
            return final;
        }

        //checks if two numbers are equal in modulos
        public int ModulusChecker(Infint b)
        {
            
                if (Digits.Count > b.Digits.Count)
                {
                    return FIRST_NUMBER_GREATER;
                }
                else if (Digits.Count < b.Digits.Count)
                {

                    return FIRST_NUMBER_LESS;
                }
            
            if (Digits.Count == b.Digits.Count)
            {
                for (int j = Digits.Count - 1; j >= 0; j--)
                {
                    try
                    {
                        if (Digits[j] > b.Digits[j])
                        {

                            return FIRST_NUMBER_GREATER;
                        }
                        else if (Digits[j] < b.Digits[j])
                        {

                            return FIRST_NUMBER_LESS;
                        }
                    }
                    catch
                    {
                        return FIRST_NUMBER_GREATER;
                    }
                }
            }


            return EQUAL;
            
            
        }

        private bool CheckForRemainder(int a, int b, int c = 0)
        {
            if (a + b + c >= 10)
            {
                return true;
            }
            return false;
        }

        private bool CheckForDivision(int a, int b)
        {
            if (a - b < 0)
            {
                return true;
            }
            return false;
        }

        private List<int> PerformSum(ref List<int> results, int num1, int num2, ref int remainder, bool futureRemainder)
        {
            if (futureRemainder)
            {
                int sum = (num1 + num2 + remainder) % 10;
                results.Add(sum);
                remainder = 1;
                return results;
            }
            else
            {

                int sum = num1 + num2 + remainder;
                results.Add(sum);
                remainder = 0;
                return results;

            }
        }
        //minus logic
        public string Minus(Infint obj)
        {
           
            var result = new List<int>();
            
            if (this.CompareTo(obj) == EQUAL)
            {
                result.Add(0);
                return AdjustList(result);
            }
            if (isNegative && obj.isNegative)
            {
                obj.isNegative = false;
                return this.Plus(obj);
            }
            if (this.CompareTo(obj) == FIRST_NUMBER_GREATER)
            {
                var remainder = 0;
                const int NUMBER_2_DNE = 0;
                var remainderExists = false;

                for (int i = 0; i < Digits.Count; i++)
                {
                    try
                    {
                        remainderExists = CheckForDivision(Digits[i], obj.Digits[i]);
                        if (remainderExists)
                        {
                            int sum = (10 + (Digits[i] - remainder)) - obj.Digits[i];
                            result.Add(sum);
                            remainder = 1;
                        }
                        else
                        {
                            int sum = Digits[i] - remainder - obj.Digits[i];
                            result.Add(sum);
                            remainder = 0;
                        }
                    }
                    catch
                    {
                        remainderExists = CheckForDivision(Digits[i], NUMBER_2_DNE);
                        if (remainderExists)
                        {
                            int sum = (10 + (Digits[i] - remainder));
                            result.Add(sum);
                            remainder = 1;
                        }
                        else
                        {

                            int sum = Digits[i] - remainder;
                            result.Add(sum);
                            remainder = 0;
                        }
                    }
                }

                if (remainder == 1)
                {
                    result.Add(remainder);
                }

                return AdjustList(result);
            }
            else if (this.CompareTo(obj) == FIRST_NUMBER_LESS)
            {
                
                for (int i = 0; i < Digits.Count; i++)
                {
                    if (Digits[i] >= 0)
                    {
                        
                        result.Add(obj.Digits[i] - Digits[i]);
                    }
                    else
                    {
                        result.Add(obj.Digits[i]);
                    }
                }

                return AdjustList(result);
            }
            else if (this.CompareTo(obj) == FIRST_NUMBER_NEG)
            {
                for (int i = 0; i < Digits.Count; i++)
                {
                    if (Digits[i] >= 0)
                    {

                        result.Add(obj.Digits[i] - Digits[i]);
                    }
                    else
                    {
                        result.Add(obj.Digits[i]);
                    }
                }

                return AdjustList(result);
            }
            else if (this.CompareTo(obj) == FIRST_NUMBER_POS)
            {
                for (int i = 0; i < Digits.Count; i++)
                {
                    if (Digits[i] >= 0)
                    {
                        try
                        {
                            result.Add(Digits[i] - obj.Digits[i]);
                        }
                        catch
                        {
                            result.Add(Digits[i]);
                        }

                        
                    }
                    else
                    {
                        result.Add(Digits[i]);
                    }
                }

                return AdjustList(result);
            }

            return "";
        }

        //main logic for multiplication
        private string MultiplyLogic(ref int carry, ref int res, Infint obj1, Infint obj2)
        {
            int index = -1;
            List<int> count = new List<int>();
            var results = new List<List<int>>();//2D kinda list
            foreach (var digit1 in obj2.Digits)
            {
                var result = new List<int>();
                if (index >= 0)//only add 0s after the first iteration
                {
                    count.Add(0);
                }

                index++;

                foreach (var digit2 in obj1.Digits)
                {
                    res = ((digit1 * digit2) % 10) + carry;

                    result.Add(res);
                    carry = (digit1 * digit2) / 10;
                }
                if (carry != 0)
                {
                    result.Add(carry);
                }
                result.Reverse();
                if (index >= 0)
                {
                    foreach (var zero in count)
                    {

                        result.Add(zero);
                    }

                    results.Add(result);

                }
            }
            string resul = "";
            for (int i = 0; i < results.Count; i++)
            {

                try
                {
                    if (i == 0)
                    {
                        string number1 = String.Join("", results[i]);
                        string number2 = String.Join("", results[i + 1]);
                        resul = new Infint(number1).Plus(new Infint(number2));

                    }
                    else if (i >= 2)
                    {

                        string num = String.Join("", results[i]);

                        resul = new Infint(resul).Plus(new Infint(num));

                    }
                    else
                    {

                        continue;
                    }


                }
                catch
                {
                    return resul;
                }

            }
            return resul;
        }

        public string Multiply(Infint obj)
        {
            
            int carry = 0;
            int res = 0;
            if (Digits.Count >= obj.Digits.Count)
            {
                return MultiplyLogic(ref carry, ref res, this, obj);

            }
            else
            {
                return MultiplyLogic(ref carry, ref res, obj, this);
            }
            return "";
        }
        public string Plus(Infint obj)
        {
            var result = new List<int>();
            if (this.CompareTo(obj) == FIRST_NUMBER_POS)
            {
                return this.Minus(obj);
            }
            else if (this.CompareTo(obj) == FIRST_NUMBER_NEG)
            {
                //obj.Numbers.Minus(Numbers)
                return obj.Minus(this);
            }
            if (this.CompareTo(obj) == FIRST_NUMBER_GREATER || this.CompareTo(obj) == EQUAL)
            {
                var remainder = 0;
                const int NUMBER_2_DNE = 0;
                var remainderExists = false;

                for (int i = 0; i < Digits.Count; i++)
                {
                    try
                    {
                        remainderExists = CheckForRemainder(Digits[i], obj.Digits[i], remainder);
                        PerformSum(ref result, Digits[i], obj.Digits[i], ref remainder, remainderExists);
                    }
                    catch
                    {
                        remainderExists = CheckForRemainder(Digits[i], NUMBER_2_DNE, remainder);
                        PerformSum(ref result, Digits[i], NUMBER_2_DNE, ref remainder, remainderExists);
                    }
                }

                if (remainder == 1)
                {
                    result.Add(remainder);
                }

                return AdjustList(result);
            }
            else if (this.CompareTo(obj) == FIRST_NUMBER_LESS)
            {
                bool remainderExists;
                int remainder = 0;
                for (int i = 0; i < obj.Digits.Count; i++)
                {
                    if (obj.Digits[i] >= 0)
                    {
                        try
                        {
                            remainderExists = CheckForRemainder(Digits[i], obj.Digits[i], remainder);
                            PerformSum(ref result, Digits[i], obj.Digits[i], ref remainder, remainderExists);
                        }
                        catch
                        {
                            remainderExists = CheckForRemainder(obj.Digits[i], 0, remainder);
                            PerformSum(ref result, obj.Digits[i], 0, ref remainder, remainderExists);
                        }

                    }
                    else
                    {
                        result.Add(Digits[i]);
                    }
                }

                return AdjustList(result);
            }
            
            return "";
        }
        public int CompareTo(Infint obj)
        {
           
            if (isNegative && !obj.isNegative)
            {

                return FIRST_NUMBER_NEG;
            }
            else if (!isNegative && obj.isNegative)
            {

                return FIRST_NUMBER_POS;
            }
            else
            {
                if (Digits.Count > obj.Digits.Count)
                {
                    return FIRST_NUMBER_GREATER;
                }
                else if (Digits.Count < obj.Digits.Count)
                {

                    return FIRST_NUMBER_LESS;
                }

                if (this.isNegative)
                {
                    for (int j = Digits.Count-1; j >= 0; j--)
                    {
                        try
                        {
                            if (Digits[j] > obj.Digits[j])
                            {
                                return FIRST_NUMBER_LESS_NEG;
                            }
                            else if (Digits[j] < obj.Digits[j])
                            {
                                return FIRST_NUMBER_GREATER_NEG;
                            }
                        }
                        catch
                        {
                            return FIRST_NUMBER_GREATER_NEG;
                        }
                    }

                    return FIRST_NUMBER_EQUAL_NEG;
                }
                else
                {
                    if (Digits.Count == obj.Digits.Count)
                    {
                        for (int j = Digits.Count - 1; j >= 0; j--)
                        {
                            try
                            {
                                if (Digits[j] > obj.Digits[j])
                                {

                                    return FIRST_NUMBER_GREATER;
                                }
                                else if (Digits[j] < obj.Digits[j])
                                {

                                    return FIRST_NUMBER_LESS;
                                }
                            }
                            catch
                            {
                                return FIRST_NUMBER_GREATER;
                            }
                        }
                    }
                }
            }
            return EQUAL;
        }

    }
}
