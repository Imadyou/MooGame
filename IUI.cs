using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGame
{
    public interface IUI
    {
        void PutString(string output);
        string GetInputString();
        void Exit();
    }
}
