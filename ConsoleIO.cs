using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGame
{
    public class ConsoleIO:IUI
    {
        public void Exit()
        {
            System.Environment.Exit(0);
        }

        public string GetInputString()
        {   
            return Console.ReadLine();
           
        }

        public void PutString(string output)
        {
            Console.WriteLine(output);
        }

    }
}
