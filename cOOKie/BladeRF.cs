using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using System.Reflection;

namespace cOOKie
{
    public class BladeRF
    {
        private IntPtr _sdr;
        public readonly bool canTx;
        public string           name { get; private set; }
        public string           serialNumber {get; private set;}
        public string           fpgaVersionStr { get; private set; }
        public string           fpgaSize { get; private set; }
        public string           fwVersion {get; private set;}
        //public string[]        _arguments; //key/value pairs, '=' separated

        public Double           rx_sampleRate { get; private set; }
        public UInt32           rx_bandwidth { get; private set; }
        public UInt32           rx_frequency { get; private set; }
        public bladerf_lna_gain rx_lna_gain { get; private set; }
        public int              rx_vgain1 {get; private set;}
        public int              rx_vgain2 {get; private set;}
        public bladerf_format   rx_fileFormat { get; set; }
        public UInt32           rx_nBuffers { get; set; }
        public UInt32           rx_samplesPerBuffer { get; set; }
        public UInt32           rx_nUsbChannels { get; set; }
        public UInt32           rx_streamTimoutuS { get; set; }
        public UInt32           rx_syncTimeoutmS { get; set; }

        public Double           tx_sampleRate { get; private set; }
        public UInt32           tx_bandwidth { get; private set; }
        public UInt32           tx_frequency { get; private set; }
        public int              tx_vgain1 { get; private set; } //(-4) through (-35)
        public int              tx_vgain2 { get; private set; } //0 - 25
        public bladerf_format   tx_fileFormat { get; set; }
        public UInt32           tx_nBuffers { get; set; }
        public UInt32           tx_samplesPerBuffer { get; set; }
        public UInt32           tx_nUsbChannels { get; set; }
        public UInt32           tx_streamTimoutuS { get; set; }
        public UInt32           tx_syncTimeoutmS { get; set; }

        public readonly UInt32 _minFreq;
        public readonly UInt32 _maxFreq;

        public string statusMsg { get; private set; }
        
        public BladeRF(string sdrspec)
        {
            statusMsg = "";

            var status = BrfNativeMethods.bladerf_open(out _sdr, sdrspec);
            if (status != 0)
            {
                statusMsg = BrfNativeMethods.bladerf_strerror(status);
                throw new Exception(statusMsg);
            }
                        
            canTx = true;
            _minFreq = 300000;
            _maxFreq = 3800000000;
            
            //assume default values
            rx_fileFormat = bladerf_format.BLADERF_FORMAT_SC16_Q11;
            rx_syncTimeoutmS = 1000;
            rx_nBuffers = 32;
            rx_nUsbChannels = 16;
            rx_samplesPerBuffer = 32768;

            tx_fileFormat = bladerf_format.BLADERF_FORMAT_SC16_Q11;
            tx_syncTimeoutmS = 1000;
            tx_nBuffers = 32;
            tx_nUsbChannels = 16;
            tx_samplesPerBuffer = 32768;

            bladerf_version fpga_version = BrfNativeMethods.bladerf_fpga_version(_sdr);
            fpgaVersionStr = String.Format("FPGA version: {0}.{1}.{2}", fpga_version.major, fpga_version.minor, fpga_version.patch);

            bladerf_fpga_size fpga_size = BrfNativeMethods.bladerf_get_fpga_size(_sdr);
            fpgaSize = ((int)fpga_size).ToString("N0");

            bladerf_version bladerf_version = BrfNativeMethods.bladerf_version();
            fpgaVersionStr = String.Format("BladeRF version: {0}.{1}.{2}", fpga_version.major, fpga_version.minor, fpga_version.patch);

            StringBuilder serial = new StringBuilder(33);
            status = BrfNativeMethods.bladerf_get_serial(_sdr, serial);
            if (status != 0)
                serialNumber = BrfNativeMethods.bladerf_strerror(status);
            else
                serialNumber = serial.ToString();
            
            //set BladeRF log to output on stdout
            status = BrfNativeMethods.bladerf_get_fw_log(_sdr, IntPtr.Zero);
            if(status !=0)
                statusMsg += BrfNativeMethods.bladerf_strerror(status)+"\n";

            #if DEBUG
	            BrfNativeMethods.bladerf_log_set_verbosity(bladerf_log_level.BLADERF_LOG_LEVEL_VERBOSE);
            #endif

            //query hardware and set properties
            getRxFrequency();
            getRxSamplerate();
            getRxBandwidth();
            getRxLnaGain();
            getRxVGain1();
            getRxVGain2();

            getTxFrequency();
            getTxSamplerate();
            getTxBandwidth();
            getTxVGain1();
            getTxVGain2();

        }

        private void getRxFrequency()
        {
            UInt32 val;
            int status = BrfNativeMethods.bladerf_get_frequency(_sdr, bladerf_module.BLADERF_MODULE_RX, out val);

            if (status != 0)
            {
                throw new Exception(BrfNativeMethods.bladerf_strerror(status));
            }
            rx_frequency = val;
        }

        private void getTxFrequency()
        {
            UInt32 val;
            int status = BrfNativeMethods.bladerf_get_frequency(_sdr, bladerf_module.BLADERF_MODULE_TX, out val);

            if (status != 0)
            {
                throw new Exception(BrfNativeMethods.bladerf_strerror(status));
            }
            tx_frequency = val;
        }

        public bool setFrequency(UInt32 val, string module, out string msg)
        {
            bool ok = true;
            msg = "";
            int status;

            if (module == "RX")
                status = BrfNativeMethods.bladerf_set_frequency(_sdr, bladerf_module.BLADERF_MODULE_RX, val);
            else if (module == "TX")
                status = BrfNativeMethods.bladerf_set_frequency(_sdr, bladerf_module.BLADERF_MODULE_TX, val);
            else
                throw new ArgumentException("Invalid module value: " + module,"module");

            if (status == 0)
            {
                if(module == "RX")
                    this.rx_frequency = val;
                else
                    this.tx_frequency = val;
            }
            else
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }
           
            return ok;
        }

        public bool setSampleRate(double sr, string module, out string msg)
        {
            double actualSampleRate;
            bool ok = true;
            msg = "";
            int status;

            if (module == "RX")
                status = BrfNativeMethods.bladerf_set_sample_rate(_sdr, bladerf_module.BLADERF_MODULE_RX, sr, out actualSampleRate);
            else if (module == "TX")
                status = BrfNativeMethods.bladerf_set_sample_rate(_sdr, bladerf_module.BLADERF_MODULE_TX, sr, out actualSampleRate);
            else
                throw new ArgumentException("Invalid module value: " + module, "module");

            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }
            else
            {
                if(module == "RX")
                    this.rx_sampleRate = actualSampleRate;
                else
                    this.tx_sampleRate = actualSampleRate;
            }
            return ok;
        }

        public void getRxSamplerate()
        {
            uint sr;
            string msg;
            int status = BrfNativeMethods.bladerf_get_sample_rate(_sdr, bladerf_module.BLADERF_MODULE_RX, out sr);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                throw new Exception(msg);
            }
            rx_sampleRate = sr;
            return;
        }

        public void getTxSamplerate()
        {
            uint sr;
            string msg;
            int status = BrfNativeMethods.bladerf_get_sample_rate(_sdr, bladerf_module.BLADERF_MODULE_TX, out sr);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                throw new Exception(msg);
            }
            tx_sampleRate = sr;
            return;
        }

        public void getRxBandwidth()
        {
            uint bw;
            string msg = "";
            int status = BrfNativeMethods.bladerf_get_bandwidth(_sdr, bladerf_module.BLADERF_MODULE_RX, out bw);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                throw new Exception(msg);
            }
            else
            {
                rx_bandwidth = bw;
            }

        }

        public void getTxBandwidth()
        {
            uint bw;
            string msg = "";
            int status = BrfNativeMethods.bladerf_get_bandwidth(_sdr, bladerf_module.BLADERF_MODULE_TX, out bw);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                throw new Exception(msg);
            }
            else
            {
                tx_bandwidth = bw;
            }

        }

        public void getRxLnaGain()
        {
            bladerf_lna_gain lnag;
            //int gain = 0;
            string msg = "";

            int status = BrfNativeMethods.bladerf_get_lna_gain(_sdr, out lnag);
            if (status == 0)
            {
                rx_lna_gain = lnag;
            }
            else 
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                throw new Exception(msg);
            }
        }

        public bool setRxLnaGain(int idx, out string msg)
        {
            bool ok = true;
            msg = "";
            int status = BrfNativeMethods.bladerf_set_lna_gain(_sdr, (bladerf_lna_gain) idx);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }
                
            getRxLnaGain();
            
            return ok;
            
        }

        public bool setBandwidth(uint bw, string module, out string msg)
        {
            msg = "";
            bool ok = true;
            uint actualBw = 0;
            int status;

            if (module == "RX")
                status = BrfNativeMethods.bladerf_set_bandwidth(_sdr, bladerf_module.BLADERF_MODULE_RX, bw, out actualBw);
            else if (module == "TX")
                status = BrfNativeMethods.bladerf_set_bandwidth(_sdr, bladerf_module.BLADERF_MODULE_TX, bw, out actualBw);
            else
                throw new ArgumentException("Invalid module: " + module, "module");
            
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }
            else 
            {
                if (module == "RX")
                    this.rx_bandwidth = actualBw;
                else
                    this.tx_bandwidth = actualBw;
            }

            if (actualBw != bw) 
            {
                msg = String.Format("Actual bandwidth does not match the set bandwidth (Actual/Set): {0}/{1}", actualBw,bw);
            }
            return ok;

        }

        public void getRxVGain1()
        {
            int vg1;
            int status = BrfNativeMethods.bladerf_get_rxvga1(_sdr, out vg1);
            if (status != 0)
            {
                throw new Exception(BrfNativeMethods.bladerf_strerror(status));
            }

            rx_vgain1 = vg1;
        }

        public void getTxVGain1()
        {
            int vg1;
            int status = BrfNativeMethods.bladerf_get_txvga1(_sdr, out vg1);
            if (status != 0)
            {
                throw new Exception(BrfNativeMethods.bladerf_strerror(status));
            }

            tx_vgain1 = vg1;
        }

        public void getRxVGain2()
        {
            int vg2;
            int status = BrfNativeMethods.bladerf_get_rxvga2(_sdr, out vg2);
            if (status != 0)
            {
                throw new Exception(BrfNativeMethods.bladerf_strerror(status));
            }

            rx_vgain2 = vg2;
        }

        public void getTxVGain2()
        {
            int vg2;
            int status = BrfNativeMethods.bladerf_get_txvga2(_sdr, out vg2);
            if (status != 0)
            {
                throw new Exception(BrfNativeMethods.bladerf_strerror(status));
            }

            tx_vgain2 = vg2;
        }

        public bool setRxVGain1(int vg1, out string msg)
        {
            msg = "";
            bool ok = true;
            int status = BrfNativeMethods.bladerf_set_rxvga1(_sdr, vg1);

            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }

            getRxVGain1();

            return ok;
        }

        public bool setTxVGain1(int vg1, out string msg)
        {
            msg = "";
            bool ok = true;
            int status = BrfNativeMethods.bladerf_set_txvga1(_sdr, vg1);

            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }

            getTxVGain1();

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

            getRxVGain2();

            return ok;
        }

        public bool setTxVGain2(int vg2, out string msg)
        {
            msg = "";
            bool ok = true;
            int status = BrfNativeMethods.bladerf_set_txvga2(_sdr, vg2);
            if (!(status == 0))
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }

            getTxVGain2();

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
            msg = "";
            status = BrfNativeMethods.bladerf_sync_config(_sdr, bladerf_module.BLADERF_MODULE_RX, rx_fileFormat, rx_nBuffers, rx_samplesPerBuffer, rx_nUsbChannels, rx_syncTimeoutmS);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                return null;
            }

            //ensable RX module
            status = BrfNativeMethods.bladerf_enable_module(_sdr, bladerf_module.BLADERF_MODULE_RX, 1);
            if (status != 0)
            {
                msg = "Cannot ensable RX modulue: \n";
                msg += BrfNativeMethods.bladerf_strerror(status);
                return samples;
            }

            
            Thread.Sleep(10);
            fixed (Int16* _samplesPtr = &samples[0])
            {
                status = BrfNativeMethods.bladerf_sync_rx(_sdr, _samplesPtr, N, IntPtr.Zero, rx_syncTimeoutmS);
                if (status != 0)
                {
                    msg = "Cannot receive samples: \n";
                    msg += BrfNativeMethods.bladerf_strerror(status);
                    return samples;
                }
            }

            //disable RX module
            status = BrfNativeMethods.bladerf_enable_module(_sdr, bladerf_module.BLADERF_MODULE_RX, 0);
            if (status != 0)
            {
                msg = "Cannot disable RX modulue: \n";
                msg += BrfNativeMethods.bladerf_strerror(status);
                return samples;
            }

            return samples;

        }   // end of bladerf_RX

        //called from multiple locations
        public bool syncConfig(string module, out string msg)
        {
            msg = "";
            bool ok = true;
            int status;

            if (module =="RX")
                status = BrfNativeMethods.bladerf_sync_config(_sdr, 
                    bladerf_module.BLADERF_MODULE_RX,
                    rx_fileFormat, 
                    rx_nBuffers, 
                    rx_samplesPerBuffer, 
                    rx_nUsbChannels, 
                    rx_syncTimeoutmS);
            else if (module == "TX")
                status = BrfNativeMethods.bladerf_sync_config(_sdr,
                    bladerf_module.BLADERF_MODULE_TX,
                    tx_fileFormat,
                    tx_nBuffers,
                    tx_samplesPerBuffer,
                    tx_nUsbChannels,
                    tx_syncTimeoutmS);
            else
                throw new ArgumentException("Invalid value: " + module, "module");
            
            if (status != 0)
            {
                ok = false;
                msg = BrfNativeMethods.bladerf_strerror(status);
            }
            return ok;
        }

        public unsafe bool bladerf_TX( Int16[] samples, int repeat, out string msg)
        {
            UInt32 N = (UInt32)(samples.Length / 2);
            Int32 status;

            //// test to reduce sample to 80%
            //for (int i = 0; i < samples.Length; ++i)
            //    samples[i] = (Int16)(samples[i] * .8);

                // configure TX module to receive data
                msg = "";
            status = BrfNativeMethods.bladerf_sync_config(_sdr, bladerf_module.BLADERF_MODULE_TX, tx_fileFormat, tx_nBuffers, tx_samplesPerBuffer, tx_nUsbChannels, tx_syncTimeoutmS);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                return false;
            }

            msg = "";
            // enable tx module
            status = BrfNativeMethods.bladerf_enable_module(_sdr, bladerf_module.BLADERF_MODULE_TX, 1);
            if (status != 0)
            {
                msg = "Cannot enable TX modulue: \n";
                msg += BrfNativeMethods.bladerf_strerror(status);
                return false;
            }

            Thread.Sleep(10);
            fixed (Int16* _samplesPtr = &samples[0])
            {
                for (int i = 0; i < repeat; ++i)
                {
                    status = BrfNativeMethods.bladerf_sync_tx(_sdr, _samplesPtr, N, IntPtr.Zero, tx_syncTimeoutmS);
                    if (status != 0)
                    {
                        msg = "Cannot transmit samples: \n";
                        msg += BrfNativeMethods.bladerf_strerror(status);
                        return false;
                    }
                }
            }

            //disable TX module
            status = BrfNativeMethods.bladerf_enable_module(_sdr, bladerf_module.BLADERF_MODULE_TX, 0);
            if (status != 0)
            {
                msg = "Cannot disable TX modulue: \n";
                msg += BrfNativeMethods.bladerf_strerror(status);
                return false;
            }

            return true;

        }   // end of bladerf_TX

        public void close()
        {
            if (_sdr != null)
            {
                BrfNativeMethods.bladerf_close(_sdr);
            }
        }

        public bool getFwLog(out string msg)
        {
            bool ok = true;
            msg = "";

            //set BladeRF log to output on stdout
            int status = BrfNativeMethods.bladerf_get_fw_log(_sdr, IntPtr.Zero);
            if (status != 0)
            {
                msg = BrfNativeMethods.bladerf_strerror(status);
                ok = false;
            }
            return ok;

        }

     }

   

}
