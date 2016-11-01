using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.IO;

namespace cOOKie
{
    public class OokDevice
    {
        /// <summary>The edge of the PWM dataBits that has a constant period</summary>
        public enum SyncEdges { Rising, Falling, Unknown}

        public string name { get; private set; }

        /// <summary>0's and 1's or ""; Start dataBits may have a unique time delay before the data dataBits</summary>
        public string startBits { get; private set; }

        /// <summary>The amount of time in ms between the last start bit and first databit</summary>
        public double startBitPad_ms { get; private set; }
        
        /// <summary>The number of data dataBits in a PWM word</summary>
        public int nDataBits { get; private set; }

        /// <summary>The pulse width in ms of the high portion of the narrow bit</summary>
        public double narrowWidth_ms { get; private set; }

        /// <summary>The pulse width in ms of the high portion of the wide bit</summary>
        public double wideWidth_ms { get; private set; }

        /// <summary>The period for every data bit from SyncEdges to the next SyncEdges</summary>
        public double bitPeriod_ms { get; private set; }

        /// <summary>Determines if dataBits are synced on rising aor falling edge</summary>
        public SyncEdges syncEdge { get; private set; }

        /// <summary>The amount of time in ms before the first data bit (or start bit if existing)</summary>
        public double wordPad_ms { get; private set; }

        /// <summary>True if saved. This flag is managed by client</summary>
        public bool isSaved { get; set; }

        /// <summary>True if contains valid data. Flag set by this.validate(). Client is responsible for checking before using device</summary>
        public bool isValid { get; private set; }

        /// <summary>Set by this.validate(). Contains list of validtion errors or ""</summary>
        public string errors { get; private set; }

        public OokDevice(string name,
                         string startBits,
                         double startBitPad_ms,
                         int nDataBits,
                         double narrowWidth_ms,
                         double wideWidth_ms,
                         double bitPeriod_ms,
                         SyncEdges syncEdge,
                         double wordPad_ms)
        {
            string msg;
            if (!setName(name, out msg))
            { throw new ArgumentException(msg, "name"); }

            if (!setStartBits(startBits, out msg))
            { throw new ArgumentException(msg, "startBits"); }

            if (!setStartBitPad(startBitPad_ms, out msg))
            { throw new ArgumentException(msg, "startBitPad_ms"); }

            if (!setNDataBits(nDataBits, out msg))
            { throw new ArgumentException(msg, "nDataBits"); }

            if (!setNarrowWidth_ms(narrowWidth_ms, out msg))
            { throw new ArgumentException(msg, "narrowWidth_ms"); }

            if (!setWideWidth_ms(wideWidth_ms, out msg))
            { throw new ArgumentException(msg, "wideWidth_ms"); }

            if (!setBitPeriod_ms(bitPeriod_ms, out msg))
            { throw new ArgumentException(msg, "bitPeriod_ms"); }

            if (!setSyncEdge(syncEdge, out msg))
            { throw new ArgumentException(msg, "syncEdge"); }

            if (!setWordPad_ms(wordPad_ms, out msg))
            
            this.isSaved = false;
            this.isValid = false;
            this.errors = "unvalidated";
        }// end constructor

        //empty constructor
        //public OokDevice() { isValid = isSaved = false; }

        public bool setName(string nm, out string msg)
        {
            bool ok;
            if ((nm.IndexOfAny(Path.GetInvalidFileNameChars()) == -1) && (nm.IndexOf(" ") == -1))
            {
                name = nm;
                ok = true;
                msg = "";
            }
            else
            {
                ok = false;
                msg = "Name contains invalid characters";
            }
            return ok;
        }

        public bool setStartBits(string sb, out string msg)
        {
            bool ok;
            if (sb.Replace("1", "").Replace("0", "").Length == 0)
            {
                startBits = sb;
                msg = "";
                ok = true;
            }
            else
            {
                msg = "Start Bits can only contain 1's and 0's";
                ok = false;
            }
                startBits = sb;
            
            return ok;
        }

        public bool setStartBitPad(double sbp, out string msg)
        {
            bool ok;
            if (sbp >= 0 && sbp <= 1000)
            {
                startBitPad_ms = sbp;
                ok = true;
                msg = "";
            }
            else
            {
                msg = "Start bit pad must be beween 0 and 1000 ms";
                ok = false;
            }
            return ok;
        }

        public bool setNDataBits(int ndb, out string msg)
        {
            bool ok;
            if (ndb > 0 && ndb <= 1000)
            {
                nDataBits = ndb;
                ok = true;
                msg = "";
            }
            else 
            {
                msg = "Number of data bits must be between 1 and 1000";
                ok = false;
            }
            return ok;
        }

        public bool setNarrowWidth_ms(double nw, out string msg)
        {
            bool ok;
            if (nw > 0)
            {
                narrowWidth_ms = nw;
                ok = true;
                msg = "";
            }
            else
            {
                msg = "Narrow width must be > 0 and < wide width and < bit width";
                ok = false;
            }
            return ok;
        }

        public bool setWideWidth_ms(double ww, out string msg)
        {
            bool ok;
            if (ww > 0)
            {
                wideWidth_ms = ww;
                ok = true;
                msg = "";
            }
            else
            {
                msg = "Narrow width must be > 0 and < wide width and < bit width";
                ok = false;
            }
            return ok;
        }

        //todo: handle blank wideWidth
        public bool setBitPeriod_ms(double bp, out string msg) 
        {
            bool ok;
            if (bp > 0)
            {
                bitPeriod_ms = bp;
                ok = true;
                msg = "";
            }
            else
            {
                ok = false;
                msg = "Bit period must be > 0 and > wideWidth_ms";
            }
            return ok;
        }

        //todo: are there invalid selections?
        public bool setSyncEdge(SyncEdges se, out string msg)
        {
            msg = "";
            syncEdge = se;
            return true;
        }

        //todo:  define max wordpad 
        public bool setWordPad_ms(double wp, out string msg)
        {
            bool ok;
            if (wp > 0)
            {
                wordPad_ms = wp;
                ok = true;
                msg = "";
            }
            else
            {
                msg = "Word pad must be >= 0 ms";
                ok = false;
            }
            return ok;
        }

        public Int16[] makeWordSignal(UInt32 samplerate, string dataBits, int repeat, out string msg)
        {
            msg = "";
            if(dataBits.Length != nDataBits){
                msg = "Wrong number of bits";
                return null;
            }

            if (dataBits.Count(f => f == '0') + dataBits.Count(f => f == '1') != dataBits.Length)
            {
                msg = "Only '1' and '0' allowed in word bits";
                return null;
            }

            // srp = samplerate period
            double srp = 1.0 / samplerate;

            // spb = samples per bit
            int spb = Convert.ToInt32((bitPeriod_ms / 1000) * samplerate);

            // spw = samples per wide pulse
            int spw =  Convert.ToInt32((wideWidth_ms / 1000) * samplerate);

            // spn = samples per narrow pulse
            int spn =  Convert.ToInt32((narrowWidth_ms / 1000) * samplerate);

            // padN = padding of narrow pulse to get bit width
            //int padN = spb - spn;

            // padW = padding of wide pulse to get bit width
            //int padW = spb - spw;



            // Create narrow and wide dataBits to copy into signal
            Int16[] narrowBit = new Int16[spb * 2];
            Int16[] wideBit = new Int16[spb * 2];
            
            // maximum modulation set to 80%
            double maxMod = 2047 * .8;
            
            if (syncEdge == SyncEdges.Falling)
            {
                // Put pulse on back end of bit
                Array.Copy(Enumerable.Repeat((Int16)maxMod, spn * 2).ToArray(), 0, narrowBit, ((spb - spn) * 2), (spn * 2));
                Array.Copy(Enumerable.Repeat((Int16)maxMod, spw * 2).ToArray(), 0, wideBit, ((spb - spw) * 2), (spw * 2));
            }
            else
            {
                // Put pulse on front end of bit
                Array.Copy(Enumerable.Repeat((Int16)maxMod, spn * 2).ToArray(), 0, narrowBit, 0, (spn * 2));
                Array.Copy(Enumerable.Repeat((Int16)maxMod, spw * 2).ToArray(), 0, wideBit, 0, (spw * 2));
            }

            // Calculate total number of samples:
            // wordpad + (number of start dataBits * samples per bit) + startbit delay + 
            // (number of data dataBits * samples per bit) + wordpad
            
            //length of one word incuding wordpad and start bits
            Int32 nSamples = Convert.ToInt32(
                             ((wordPad_ms / 1000) * samplerate) +
                             (startBits.Length * spb) +
                             ((startBitPad_ms / 1000) * samplerate) +
                             (nDataBits * spb));

            //multiply by the number of times word is repeated
            nSamples *= repeat;

            //add another wordpad at end of signal
            nSamples += (Int32)((wordPad_ms / 1000) * samplerate);

            // create array to hold signal - interleaved I and Q values: 2 array cell per sample 
            Int16[] signal = Enumerable.Repeat((Int16)0,nSamples * 2).ToArray();
            
            // idx is pointer to current location in signal
            // each sample is 2 interleaved 16-bit words (I and Q) 
            int idx = Convert.ToInt32(((wordPad_ms / 1000) * samplerate) * 2);
            
            // ensure idx is on an even number
            idx = (idx % 2 == 0) ? idx : idx + 1;

            for (int r = 0; r < repeat; ++r)
            {
                //add start bits
                foreach (char c in startBits)
                {
                    // narrow bit
                    if (c == '0')
                    {
                        Array.Copy(narrowBit, 0, signal, idx, narrowBit.Length);
                        idx += narrowBit.Length;
                    }
                    // wide bit
                    else
                    {
                        Array.Copy(narrowBit, 0, signal, idx, wideBit.Length);
                        idx += wideBit.Length;
                    }
                }

                // Add space between start bits and first data bit
                idx += Convert.ToInt32(((startBitPad_ms / 1000) * samplerate) * 2);

                //add data dataBits
                foreach (char c in dataBits)
                {
                    // narrow bit
                    if (c == '0')
                    {
                        Array.Copy(narrowBit, 0, signal, idx, narrowBit.Length);
                        idx += narrowBit.Length;
                    }
                    // wide bit
                    else
                    {
                        Array.Copy(wideBit, 0, signal, idx, wideBit.Length);
                        idx += wideBit.Length;
                    }
                }

                idx += Convert.ToInt32(((wordPad_ms / 1000) * samplerate) * 2);
            }// end repeat

            return signal;
   
        }// makeWordSignal

        public bool validate(out string msg)
        {
            msg = "";
            
            if(String.IsNullOrEmpty(name) || (name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1) || (name.IndexOf(" ") != -1))
                {msg+="name has invalid characters\n";}

            return (msg == "");
        }

    }// end of class
}
