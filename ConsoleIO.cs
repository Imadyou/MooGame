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
            return Console.ReadLine().Trim();           
        
        }

        public void PutString(string output)
        {
            Console.WriteLine(output);
        }
    }
}
