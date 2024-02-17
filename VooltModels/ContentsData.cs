using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VooltModels
{
    public class ContentsData
    {
        public ContentsData(IList<Block_1> blocks_1, IList<Block_2> blocks_2, IList<Block_3> blocks_3)
        {
            Blocks_1 = blocks_1;
            Blocks_2 = blocks_2;
            Blocks_3 = blocks_3;
        }

        public ContentsData() { }

        public IList<Block_1> Blocks_1 { get; set;}
        public IList<Block_2> Blocks_2 { get; set; }
        public IList<Block_3> Blocks_3 { get; set; }
    }
}
