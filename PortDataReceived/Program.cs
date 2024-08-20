using System;
using Astm.PortListener;

namespace Astm
{
    class Program
    {
        
        public static void Main()
        {

            IportListener portListener = new PortListtener();
           
            try
            {
                portListener.PortOpen();
                Console.ReadLine();
                portListener.PortClose();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

    }
}
