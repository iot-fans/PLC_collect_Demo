using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_test
{
    internal interface IMelsecMaster : IDisposable
    {
        int ReadDeviceBlock(string devType, int firstDevIndex, int deviceCount, out short[] data);
        int WriteDeviceBlock(string devType, int firstDevIndex, int deviceCount, short[] data);
    }
}
