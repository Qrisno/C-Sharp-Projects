using System;
/*
 * Name: Nino Kristesiashvili,
 * Date: 16.09.2021
 */
namespace hw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = new Address("a", "kristesiashvili", "saakadze", "vashadze", "sgfsdgf", "sgfsdgf", 1020, DateTime.Now, "591150140");
            var i1 = new Address("b", "kristesiashvili", "saakadze", "vashadze", "sgfsdgf", "sgfsdgf", 1020, DateTime.Now, "591150140");
            Address[] arr = { i, i1};
            Array.Sort(arr);
            foreach(Address add in arr)
            {
                Console.WriteLine(add);
            }
        }
    }
}
