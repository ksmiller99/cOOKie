using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cOOKie
{
    class PwmWord
    {
        public List<Bit> allBits;
        public List<Bit> dataBits;
        public List<Bit> startBits;
        public List<Bin> bins;

        //public enum SyncEdge{Rising, Falling, Unknown}

        public UInt32 sampleRate            { get; set; }
        public double avgNarrowWidth        { get; set; }
        public double narrowStDev           { get; set; }
        public double avgWideWidth          { get; set; }
        public double wideStDev             { get; set; }
        public double avgRisePeriod         { get; set; }
        public double riseStDev             { get; set; }
        public double avgFallPeriod         { get; set; }
        public double fallStDev             { get; set; }
        public OokDevice.SyncEdges syncEdge { get; private set; }
        public string dataContents          { get; set; }
        public string sbContents            { get; private set; }
        public int startBitsN               { get; set; }
        public int startBitWidth            { get; set; }
        public int startBitDelay            { get; set; } //# of samples between last start bit and first data bit
        public double interWordDelay        { get; set; } //# of samples between last data bit and start bit of next word

        public PwmWord()
        {
            allBits = new List<Bit>();
            bins = new List<Bin>();
        }

        /// <summary>
        /// Analyzes the signal and populates the PwmWord properties. The signal should start just before start bit, 
        /// and end just after next start bit.
        /// </summary>
        /// <param name="signal">double[,] from MATLAB</param>
        /// <param name="sr">Sample Rate in Hz</param>
        /// <param name="tolerance">% of time difference to be considered valid</param>
        /// <returns></returns>
        public bool analyzeSignal(double[,] signal, UInt32 sr, uint tolerance, out string msg)
        {
            this.sampleRate = sr;
            msg = "";
            //examine signal at middle level
            double threshold = (signal.Cast<double>().ToList().Max() + signal.Cast<double>().ToList().Min())/2;

            double lastLevel = 0; // used to determine direction when threshold is crossed

            //examine all dataBits and place into allBits
            for (int i = 1; i <= signal.Length-1; ++i)
            {
                double val = signal[i, 0];
                if (lastLevel > threshold && val < threshold)
                {
                    //falling - last bit on list should have fall time and fall period blank
                    allBits.Last().fallTime = i;
                    if (allBits.Count > 1)
                    {
                        allBits.Last().fallPeriod = i - allBits[allBits.Count - 2].fallTime;
                    }

                    //calculate pulse width and bin it
                    int pulseWidth = allBits.Last().fallTime - allBits.Last().riseTime;
                    allBits.Last().pulseWidth = pulseWidth;
                    bool found = false;
                    foreach (Bin bin in bins)
                    {
                        //if pulsewidth is within the tolerance for this bin, then add it to bin
                        if (Math.Abs(pulseWidth - bin.width) < (bin.width * (tolerance / 100.0)))
                        {
                            ++bin.count;
                            found = true;
                            allBits.Last().binName = bin.width;
                            break;
                        }
                    }

                    if (!found)
                    {
                        bins.Add(new Bin(pulseWidth));
                        allBits.Last().binName = pulseWidth;
                    }

                }

                if (lastLevel < threshold && val > threshold)
                {
                    //rising - last bit on list should be complete = add new bit
                    allBits.Add(new Bit(allBits.Count+1));
                    allBits.Last().riseTime = i;
                    if (allBits.Count > 1)
                    {
                        allBits.Last().risePeriod = i - allBits[allBits.Count - 2].riseTime;
                    }
  
                }

                lastLevel = val;
            }//end for

            if (allBits.Count == 0) 
            {
                msg = "No bits found. Check your selections";
                return false;
            }

            if (bins.Count != 2)
            {
               msg ="Not binary PWM - " + bins.Count.ToString() + " bit widths found.";
                return false;
            }

            //calculate avg rise period including start dataBits
            avgRisePeriod = (from bit in allBits select bit.risePeriod).Average();

            // find and cout start bits
            if (!findStartBits())
            {
                msg="Error in findStartBits()";
                return false;
            }

            startBits = new List<Bit>();
            startBits.AddRange(allBits.GetRange(0, startBitsN));

            //separate data dataBits from start dataBits at beginning and end of signal
            dataBits = new List<Bit>();
            dataBits.AddRange(allBits.GetRange(startBitsN,allBits.Count-startBitsN-1));
            
            //get # of samples between last start bit and first databit, the remove first databit periods
            dataBits[0].risePeriod = 0;
            dataBits[0].fallPeriod = 0;

            if (!calcBinValues(out msg))
            {
                return false;
            }

            double minBinName = (from bin in bins select bin.width).Min();
            double maxBinName = (from bin in bins select bin.width).Max();

            avgNarrowWidth = (from bit in dataBits where bit.binName == minBinName select bit.pulseWidth).Average();
            avgWideWidth = (from bit in dataBits where bit.binName == maxBinName select bit.pulseWidth).Average();
            
            narrowStDev = stdDeviation(from bit in dataBits where bit.binName == minBinName select bit.pulseWidth);
            wideStDev = stdDeviation(from bit in dataBits where bit.binName == maxBinName select bit.pulseWidth);
            
            avgRisePeriod = (from bit in dataBits where bit.bitPos > (startBitsN + 1) select bit.risePeriod).Average();
            riseStDev = stdDeviation(dataBits
                 .Where(bit => bit.bitPos > startBitsN+1)
                 .Select(bit => bit.risePeriod)
                 .ToList());
            avgFallPeriod = (from bit in dataBits where bit.bitPos > (startBitsN + 1) select bit.fallPeriod).Average();
            fallStDev = stdDeviation(dataBits
                 .Where(bit => bit.bitPos > startBitsN + 1)
                 .Select(bit => bit.fallPeriod)
                 .ToList());    

            // set syncEdge = must be a magnitude difference
            if(riseStDev > 10 * fallStDev)
            {
                syncEdge = OokDevice.SyncEdges.Falling;
                startBitDelay = Math.Max(dataBits[0].fallTime - startBits.Last().fallTime - Convert.ToInt32(avgFallPeriod),0);
                interWordDelay = allBits.Last().fallTime - dataBits.Last().fallTime - avgFallPeriod;
            }
            else if(fallStDev > 10 * riseStDev)
            {
                syncEdge = OokDevice.SyncEdges.Rising;
                startBitDelay = dataBits[0].riseTime - startBits.Last().riseTime - Convert.ToInt32(avgRisePeriod);
                interWordDelay = allBits.Last().riseTime - dataBits.Last().riseTime - avgRisePeriod;
            }
            else
            {
                syncEdge = OokDevice.SyncEdges.Unknown;
                startBitDelay = 0;
            }

           // set pwmWord dataContents
            dataContents = "";
            foreach (Bit bit in dataBits)
            {
                if (bit.binName == minBinName)
                {
                    dataContents += "0";
                }
                else
                {
                    if (bit.binName == maxBinName)
                    {
                        dataContents += "1";
                    }
                    else
                    {
                        dataContents += "x"; //more than 2 bins
                    }
                }
            }

            // set start bit dataContents
            sbContents = "";
            foreach (Bit bit in startBits)
            {
                if (bit.binName == minBinName)
                {
                    sbContents += "0";
                }
                else
                {
                    if (bit.binName == maxBinName)
                    {
                        sbContents += "1";
                    }
                    else
                    {
                        sbContents += "x"; //more than 2 bins
                    }
                }
            }

            //temp write dataBits to csv for excel double-checking
            String csv = string.Join(",", (from bit in allBits select new { bit.binName, bit.riseTime, bit.fallTime }));
            File.WriteAllText(@"c:\temp\ook.csv", csv);

            calcBitDeviations();

            return true;
        }// end fill bins

        protected double stdDeviation(IEnumerable<int> values)
        {
            double avg = values.Average();
            double sum = values.Sum(d => Math.Pow(d-avg,2));
            return(Math.Sqrt((sum) / (values.Count() - 1)));
        }

        /// <summary>
        /// Calculate and store the standard deviation for each bin
        /// </summary>
        protected bool calcBinValues(out string msg)
        {
            msg = "";
            bool ok = true;
            IEnumerable<int> values;
            foreach (Bin bin in bins)
            {
                values = from bit in dataBits
                         where bit.binName == bin.width
                         select bit.pulseWidth;

                if (values.Count() > 0)
                {
                    bin.avgWidth = values.Average();
                    bin.stdDev = stdDeviation(values);
                }
                else
                {
                    msg = "Not Binary PWM - One or more bins are empty";
                    ok = false;
                }
            }

            return ok;
        }

        private void calcBitDeviations()
        {
            foreach (Bit bit in dataBits)
            {
                //pw deviation from bin standard deviation
                double stDevPw = (from bin in bins where bin.width == bit.binName select bin.stdDev).First();
                bit.pwDeviation = Math.Abs(stDevPw - bit.pulseWidth);

            }
        }

        /// <summary>
        /// searches bit list to find one of more dataBits at the beginning of a word
        /// seperated from the rest of the word
        /// </summary>
        private bool findStartBits()
        {
            bool ok = false;
            startBitsN = 0;
            Bit bit;
            //start with second bit because first does not have a period
            for (int i = 1; i < allBits.Count; ++i )
            {
                
                bit = allBits[i];
                if ((bit.risePeriod) > avgRisePeriod * 1.5)
                {
                    startBitsN = i;
                    ok = true;
                    break;
                }
            }

            //if start bit count = number of bits in word, no startbit
            

            //if start(s) bit not found to have larger period, assume first bit is start bit
            if (!ok || startBitsN == (allBits.Count-1))
            {
                startBitsN = 1;
                ok = true;
            }

            return ok;
        }

    }   

    class Bin
    {
        public int width { get; set; }      //pulse width in samples, also serves as name of bin
        public int count { get; set; }      //number of pulses with widths within 10% of width
        public double avgWidth { get; set; }//average width of bin
        public double stdDev { get; set; }  //standard deviation

        public Bin(int pw)
        {
            this.width = pw;
            this.count = 1;
        }
    }

    class Bit
    {
        public int binName { get; set; }        //bin name (bin.width) to which bit belongs
        public int bitPos { get; set; }         //position of bit within word
        public int riseTime { get; set; }       //sample number of rising edge
        public int fallTime { get; set; }       //sample number of falling edge
        public int pulseWidth { get; set; }     //fallTime - riseTime
        public int risePeriod { get; set; }     //number of samples since previous bit's riseTime
        public int fallPeriod { get; set; }     //number of samples since previous bi's fallTime
        public double pwDeviation { get; set; } //pulse width distance from standard deviation for it's bin
        public double rpDeviation { get; set; } //rise period distance from standard deviaiton
        public double fpDeviation { get; set; } //fall period distance from deviation

        public Bit(int pos)
        {
            bitPos = pos;
        }
    }
}
