using System;
using System.Threading.Tasks;

namespace UserToDoBatchJob
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                _ = UserToDoHelper.GetToDoList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
