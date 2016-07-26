using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cOOKie
{
    public class BladeRF
    {
        public readonly IntPtr _sdr;
        public readonly bool canTx;
        private string _name;
        private string _serialNumber;
        private string _HwVersion;
        private string _FwVersion;
        private Double _sampleRate;
        private Int32 _bandwidth;
        private UInt32 _frequency;
        private string[] _arguments; //key/value pairs, '=' separated
        private int _gain;
        private int _nBuffers;
        private int _samplesPerBuffer;
        private int _nUsbChannels;
        private int _streamTimoutuS;
        private int _syncTimeoutmS;
        
        private UInt32 _minFreq;
        private UInt32 _maxFreq;
        
        public BladeRF(IntPtr ip, bool tx, UInt32 minFreq, UInt32 maxFreq)
        {
            _sdr = ip;
            canTx = tx;
            _minFreq = minFreq;
            _maxFreq = maxFreq;

            bladerf_version fpga_version = BrfNativeMethods.bladerf_fpga_version(_sdr);
            _HwVersion = String.Format("FPGA version: {0}.{1}.{2}", fpga_version.major, fpga_version.minor, fpga_version.patch);
        }

        public UInt32 getFrequency()
        {
            UInt32 val;
            bool ok;
            ok = (BrfNativeMethods.bladerf_get_frequency(_sdr, bladerf_module.BLADERF_MODULE_RX, out val) == 0);

            if (!ok)
            {
                val = 0;
                this._frequency = 0;
            }
            return val;
        }

        public bool setFrequency(string str)
        {
            UInt32 val;
            bool ok;
            val = Conversions.str2uint_suffix(str, this._minFreq, this._maxFreq, Conversions.hz_suffixes, out ok);
            if (ok)
            {
                ok = (BrfNativeMethods.bladerf_set_frequency(_sdr, bladerf_module.BLADERF_MODULE_RX, val) == 0);
                if (ok)
                {
                    this._frequency = val;
                }
            }
            return ok;
        }

        public string getHwVersion() {
            return _HwVersion;
        }
    }

   

}
