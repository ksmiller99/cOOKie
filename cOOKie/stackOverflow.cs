using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cOOKie
{
    class StackOverflow
    {

        public static void test()
        {
            // Create narrow and wide dataBits to copy into signal
            UInt16[] narrowBit = new UInt16[200];
            UInt16[] wideBit = new UInt16[200];
            
            // Put pulse on front end of bit
            UInt16[] nn = Enumerable.Repeat((UInt16)2047, 50).ToArray();
            UInt16[] ww = Enumerable.Repeat((UInt16)2047, 100).ToArray();
            Array.Copy(Enumerable.Repeat((UInt16)2047, 50).ToArray(), 0, narrowBit, 0, 50);
            //System.Buffer.BlockCopy(nn, 0, narrowBit, 0, 50);
            Array.Copy(Enumerable.Repeat((UInt16)2047, 100).ToArray(), 0, wideBit, 0, 100);
            //System.Buffer.BlockCopy(ww, 0, wideBit, 0, 100);
        }

        public static void testUInt16Array()
        {
            UInt16[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            UInt16[] b = new UInt16[10];
            UInt16[] c = new UInt16[10];
            Buffer.BlockCopy(a, 0, b, 0, 20);
            Array.Copy(a, 0, c, 0, 10);
        }

    }
}
