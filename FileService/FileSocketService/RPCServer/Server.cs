using RRQMSocket;
using RRQMSocket.RPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /*
    若汝棋茗
    */
   public class Server: ServerProvider
    {
        int a = 0;
        [RRQMRPCMethod]
        public string TestOne()
       {
            a++;
            if (a % 1000 == 0)
            {
                Console.WriteLine(a);
            }
            return "若汝棋茗";
        }
        
        [RRQMRPCMethod]
        public string TestOne(string mes)
        {
            return mes;
        }
    }
}
