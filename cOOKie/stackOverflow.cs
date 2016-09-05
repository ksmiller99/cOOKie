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

        public static void serNumTest()
        {
            IntPtr _dev;
            string sdrspec = "";
            StringBuilder serialSB = new StringBuilder(33);
            
            
            var rv = BrfNativeMethods.bladerf_open(out _dev, sdrspec);
            if (rv != 0)
                throw new ApplicationException(String.Format("Cannot open BladeRF device. Is the device locked somewhere?. {0}", BrfNativeMethods.bladerf_strerror(rv)));

            if ((rv = BrfNativeMethods.bladerf_get_serial(_dev, serialSB)) != 0)
                throw new ApplicationException(String.Format("bladerf_get_serial() error. {0}", BrfNativeMethods.bladerf_strerror(rv)));

             // above instruction crashes with following output:
             // A first chance exception of type 'System.AccessViolationException' occurred in mscorlib.dll
             // 'cOOKie.vshost.exe' (CLR v4.0.30319: cOOKie.vshost.exe): Loaded 'C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Configuration\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Configuration.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
             //  The program '[8572] cOOKie.vshost.exe' has exited with code -1073741819 (0xc0000005) 'Access violation'.

            string serial = serialSB.ToString();
            BrfNativeMethods.bladerf_close(_dev);
        }

    }
}
