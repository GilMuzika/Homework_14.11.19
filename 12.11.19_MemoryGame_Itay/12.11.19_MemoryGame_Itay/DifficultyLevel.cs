using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._11._19_MemoryGame_Itay
{

    class DifficultyLevel
    {
        public int ToGo { get; set; }

        public string ToDisplay { get; set; }

        public DifficultyLevel(int num)
        {
            ToGo = num;

            _ = num == -1 ? ToDisplay = "Max" : ToDisplay = Convert.ToString(num);
            

        }

        public override string ToString()
        {
            return ToDisplay;
        }

    }
}
