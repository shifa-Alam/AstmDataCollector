using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astm.PortListener
{
    public interface IportListener
    {
        void PortOpen();
        void PortClose();
        //void WriteToFile(string data);
        //void WriteToFile(string exception, bool isException = true);
        //string ReadFromFile();
       
    }
}
