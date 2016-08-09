using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;

namespace cOOKie
{
    public class BladeRF
    {
        public readonly IntPtr _sdr;
        public readonly bool canTx;
        private string          _name;
        private string          _serialNumber;
        private string          _HwVersion;
        private string          _FwVersion;
        private Double          _sampleRate;
        private Int32           _bandwidth;
        private UInt32          _frequency;
        private string[]        _arguments; //key/value pairs, '=' separated
        //private int           _gain;
        public bladerf_format   _fileFormat { get; set; }
        public UInt32           _nBuffers { get; set; }
        public UInt32           _samplesPerBuffer { get; set; }
        public UInt32           _nUsbChannels { get; set; }
        public UInt32           _streamTimoutuS { get; set; }
        public UInt32           _syncTimeoutmS { get; set; }
                                
        private UInt32          _minFreq;
        private UInt32          _maxFreq;
        
        public BladeRF(IntPtr ip, bool tx, UInt32 minFreq, UInt32 maxFreq)
        {
            _sdr = ip;
            canTx = tx;
            _minFreq = minFreq;
            _maxFreq = maxFreq;
            _serialNumber = "?";

            //assume default values
            _fileFormat = bladerf_format.BLADERF_FORMAT_SC16_Q11;
            _syncTimeoutmS = 1000;
            _nBuffers = 32;
            _nUsbChannels = 16;
            _samplesPerBuffer = 32768;

            bladerf_version fpga_version = BrfNativeMethods.bladerf_fpga_version(_sdr);
            _HwVersion = String.Format("FPGA version: {0}.{1}.{2}", fpga_version.major, fpga_version.minor, fpga_version.patch);
            
            //String str = "";
            //int status = 0;
            //status = BrfNativeMethods.bladerf_get_serial(_sdr, out str);
            //int dummy = status;

        }

        public UInt32 getFrequency(out string msg)
        {
            UInt32 val;
            msg = "";
            int status = BrfNativeMethods.bladerf_get_frequency(_sdr, bladerf_module.BLADERF_MODULE_RX, out val);

            if (status !=0 )
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }
            return val;
        }

        public bool setFrequency(string str, out string msg)
        {
            UInt32 val;
            bool ok;
            msg = "";
            val = Conversions.str2uint_suffix(str, this._minFreq, this._maxFreq, Conversions.hz_suffixes, out ok);
            if (ok)
            {
                int status = BrfNativeMethods.bladerf_set_frequency(_sdr, bladerf_module.BLADERF_MODULE_RX, val);
                if (status == 0)
                {
                    this._frequency = val;
                }
                else
                {
                    msg = BrfNativeMethods.bladerf_strerror(status);
                    ok = false;
                }
            }
            return ok;
        }

        public string getHwVersion() 
        {
            return _HwVersion;
        }

        public string getSerialNumber()
        {
            return _serialNumber;
        }

        public double setSampleRate(double sr, out string msg)
        {
            double actualSampleRate;
            msg = "";
            int status = BrfNativeMethods.bladerf_set_sample_rate(_sdr, bladerf_module.BLADERF_MODULE_RX, sr, out actualSampleRate);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }

            return actualSampleRate;
        }

        public uint getRxSamplerate(out string msg)
        {
            uint sr;
            msg = "";
            int status = BrfNativeMethods.bladerf_get_sample_rate(_sdr, bladerf_module.BLADERF_MODULE_RX, out sr);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }
            return sr;
        }
        
        public uint getRxBandwidth(out string msg)
        {
            uint bw;
            msg = "";
            int status = BrfNativeMethods.bladerf_get_bandwidth(_sdr, bladerf_module.BLADERF_MODULE_RX, out bw);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }

            return bw;
        }

        public int getRxLnaGain(out string msg)
        {
            bladerf_lna_gain lnag;
            int gain = 0;
            msg = "";

            int status = BrfNativeMethods.bladerf_get_lna_gain(_sdr, out lnag);
            if (status == 0)
            {
                switch (lnag)
                {
                    case bladerf_lna_gain.BLADERF_LNA_GAIN_BYPASS:
                        gain = 0;
                        break;

                    case bladerf_lna_gain.BLADERF_LNA_GAIN_MAX:
                        gain = 6;
                        break;

                    case bladerf_lna_gain.BLADERF_LNA_GAIN_MID:
                        gain = 3;
                        break;

                    case bladerf_lna_gain.BLADERF_LNA_GAIN_UNKNOWN:
                        gain = -1;
                        break;

                    default:
                        break;
                }
            }
            else 
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }
            return gain;
        }

        public bool setRxLnaGain(int gain, out string msg)
        {
            bladerf_lna_gain lnag;
            bool ok = true;
            msg = "";
            switch (gain)
            {
                case 0:
                    lnag = bladerf_lna_gain.BLADERF_LNA_GAIN_BYPASS;
                    break;

                case 3:
                    lnag = bladerf_lna_gain.BLADERF_LNA_GAIN_MID;
                    break;

                case 6:
                    lnag = bladerf_lna_gain.BLADERF_LNA_GAIN_MAX;
                    break;

                default:
                    lnag = bladerf_lna_gain.BLADERF_LNA_GAIN_UNKNOWN;
                    ok = false;
                    break;
            }

            if (ok)
            {
                int status = BrfNativeMethods.bladerf_set_lna_gain(_sdr, lnag);
                if (status != 0)
                {
                    msg = BrfNativeMethods.bladerf_strerror(status);
                    ok = false;
                }
    
            }

            return ok;
            
        }

        public uint setRxBandwidth(uint bw, out string msg)
        {
            msg = "";
            uint actualBw = 0;
            int status = BrfNativeMethods.bladerf_set_bandwidth(_sdr, bladerf_module.BLADERF_MODULE_RX, bw, out actualBw);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }

            if (actualBw != bw) 
            {
                msg = String.Format("Actual bandwidth does not match the set bandwidth (Actual/Set): {0}/{1}", actualBw,bw);
            }
            return actualBw;

        }

        public int getRxVGain1(out string msg)
        {
            msg = "";
            int vg1 = -1;
            int status = BrfNativeMethods.bladerf_get_rxvga1(_sdr, out vg1);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }

            return vg1;
        }
        
        public int getRxVGain2(out string msg)
        {
            msg = "";
            int vg2 = -1;
            int status = BrfNativeMethods.bladerf_get_rxvga2(_sdr, out vg2);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
            }

            return vg2;
        }

        public bool setRxVGain1(int vg1, out string msg)
        {
            msg = "";
            bool ok = true;
            int status = BrfNativeMethods.bladerf_set_rxvga1(_sdr, vg1);
            if (!(status == 0))
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }
            return ok;
        }
       
        public bool setRxVGain2(int vg2, out string msg)
        {
            msg = "";
            bool ok = true;
            int status = BrfNativeMethods.bladerf_set_rxvga2(_sdr, vg2);
            if (!(status == 0))
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }
            return ok;
        }

        public unsafe Int16[] bladerf_RX(UInt32 N, out string msg)
        {
            Int16[] samples = new Int16[N * 2];
            Int32 status;

            //const UInt32 timeout_ms = 5000;
            //const UInt32 Num_Buffers = 16;
            //const UInt32 Num_Xfers = 8;
            //UInt32 buf_size = 65536;

            // configure RX module to receive data
            //status = BrfNativeMethods.bladerf_sync_config(_sdr, bladerf_module.BLADERF_MODULE_RX, bladerf_format.BLADERF_FORMAT_SC16_Q11, Num_Buffers, buf_size, Num_Xfers, timeout_ms);
            //if (status == 0)
            
            msg = "";
            if (rxSyncConfig(out msg))
            {
                // enable rx module
                status = BrfNativeMethods.bladerf_enable_module(_sdr, bladerf_module.BLADERF_MODULE_RX, 1);
                if (status != 0)
                {
                    msg = "Cannot enable RX modulue: \n";
                    msg += BrfNativeMethods.bladerf_strerror(status);
                    return samples;
                }

                Thread.Sleep(10);
                fixed (Int16* _samplesPtr = &samples[0])
                {
                    status = BrfNativeMethods.bladerf_sync_rx(_sdr, _samplesPtr, N, IntPtr.Zero, _syncTimeoutmS);
                    if (status != 0)
                    {
                        msg = "Cannot receive samples: \n";
                        msg += BrfNativeMethods.bladerf_strerror(status);
                        return samples;
                    }
                }

                status = BrfNativeMethods.bladerf_enable_module(_sdr, bladerf_module.BLADERF_MODULE_RX, 0);
                if (status != 0)
                {
                    msg = "Cannot disable RX modulue: \n";
                    msg += BrfNativeMethods.bladerf_strerror(status);
                    return samples;
                }

            }
            else
            {
                samples = null;
                Console.WriteLine("Error configuring device: \n"+msg);
            }

            return samples;
        }   // end of bladerf_RX

        public bool rxSyncConfig(out string msg)
        {
            msg = "";
            bool ok = true;
            int status = BrfNativeMethods.bladerf_sync_config(_sdr, bladerf_module.BLADERF_MODULE_RX, _fileFormat, _nBuffers, _samplesPerBuffer, _nUsbChannels, _syncTimeoutmS);
            if (status != 0)
            {
                ok = false;
                msg = BrfNativeMethods.bladerf_strerror(status);
            }
            return ok;
        
        }
    }

   

}
