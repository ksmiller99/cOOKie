using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Numerics;

using System.IO;
using Newtonsoft.Json;

using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;

namespace cOOKie
{
    public partial class Form1 : Form
    {
        Int32 sdr_status;
        IntPtr _sdr;
        string sdrspec = "";
        BladeRF brf;
        FirFilter ff;
        Double[,] signal;
        PwmWord pwmWord;
        OokDevice currentDevice;

        //BladeRF SC16Q11 IQ samples
        UInt16[] recordedSignal;
        UInt16[] generatedSignal;

        // Create the MATLAB instance 
        MLApp.MLApp matlab;

        //flags to allow parsing scientific notation
        NumberStyles nStyles = NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
        CultureInfo cInfo = CultureInfo.InvariantCulture;
        
        public Form1()
        {
            InitializeComponent();
            btnRxReceive.Enabled = false;  //disable Rx button until SDR is selected and file selected
            btnTransmit.Enabled = false; //disable Tx button until tx-capable SDR is selected
            cbSdrRxLnaGain.SelectedIndex = 0;

            // Create the MATLAB instance 
            matlab = new MLApp.MLApp();
            matlab.Execute(@"cd '" + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"'");

            cbDvSyncEdge.Items.AddRange(Enum.GetNames(typeof( OokDevice.SyncEdges)));
         }

        [HandleProcessCorruptedStateExceptions]
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string sdr = cb.GetItemText(cb.SelectedItem);
            switch (sdr){
                case "BladeRF":
                    try
                    {
                        
                        sdr_status = BrfNativeMethods.bladerf_open(out _sdr, sdrspec);
                        //bladerf_version fpga_version = BrfNativeMethods.bladerf_fpga_version(_sdr);
                        //MessageBox.Show(String.Format("FPGA version: {0}.{1}.{2}", fpga_version.major, fpga_version.minor, fpga_version.patch),"BladeRF Versions");
                        brf = new BladeRF(_sdr,         //handle to SDR
                                      true,         //can transmit
                                      0,            //min freq
                                      4000000000    //max freq
                                      );

                        
                        MessageBox.Show(brf.getHwVersion()+"\nSerial Number: "+brf.getSerialNumber(), "Blade RF Versions");
                        getBrfRxConfig();
                        tbSdrRxBandwidth.Enabled = true;
                        tbSdrRxFrequency.Enabled = true;
                        tbSdrRxBandwidth.Enabled = true;
                        tbSdrRxSampleRate.Enabled = true;
                        cbSdrRxLnaGain.Enabled = true;
                        tbSdrRxVGain1.Enabled = true;
                        tbSdrRxVGain2.Enabled = true;
                        tbSdrRxNumBuffers.Enabled = true;
                        tbSdrRxNumUsbChannels.Enabled = true;
                        tbSdrRxSamPerBuffer.Enabled = true;
                        tbSdrRxSyncTimeout.Enabled = true;
                        
                        

                    }
                    catch (Exception ee) {
                        MessageBox.Show("BladeRF not found. Exception Details:\n"+ee.Message, "Error");
                        cb.SelectedIndex = -1;
                    
                    }

                    if (tbRxFileName.Text != "")
                        btnRxReceive.Enabled = true;
                    
                    break;

                default:
                    brf = null;
                    break;
            }

        }

        
        private void tbSdrFrequency_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            if ((!String.IsNullOrEmpty(tb.Text))&&(!brf.setFrequency(tb.Text, out msg)))
            {
                MessageBox.Show("Invald frequency: " + tb.Text+"\n"+msg, "Error");
                tb.Select();
            }

            msg = "";
            tb.Text = brf.getFrequency(out msg).ToString("N0");
            tb.Text = (msg == "") ? tb.Text : msg;
        }

        private void btnFilterSelect_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            openFilterDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            DialogResult result = openFilterDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                tbRxFilter.Text = Path.GetFileName(openFilterDialog.FileName);
                ff = FirFilter.init(openFilterDialog.FileName, 0);
                if (ff == null)
                {
                    tbRxFilter.Text = "";
                }
            
            }
            
        }

        private void rbRxInputRaw_CheckedChanged(object sender, EventArgs e)
        {
            btnFilterSelect.Enabled = false;
        }

        private void rbRxInputFiltered_CheckedChanged(object sender, EventArgs e)
        {
            btnFilterSelect.Enabled = true;
        }

        private void btnRxSelectFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                tbRxFolder.Text = folderBrowserDialog1.SelectedPath;
                tbAnFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void tbRxFolder_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!Directory.Exists(tb.Text))
            {
                MessageBox.Show("Folder does not exist: " + tb.Text, "Error");
                tb.Text = "";
            }
            else
            {
                tbAnFolder.Text = tbRxFolder.Text;
            }
        }

        private void tbRxRecord_Leave(object sender, EventArgs e)
        {
            string path = tbRxFolder.Text;
            path+=path.EndsWith(@"\")?"":@"\";
            path+=tbRxFileName.Text;
            if (File.Exists(path))
            {
                MessageBox.Show("FileExists", "Warning");
            }
        }


        private void btnRxSelectFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = "BladeRF files (*.SC16Q11)|*.SC16Q11|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = tbRxFolder.Text;
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                tbRxFileName.Text = Path.GetFileName(saveFileDialog1.FileName);
                tbRxFolder.Text = Path.GetDirectoryName(saveFileDialog1.FileName);
                if (cbSdrSDR.Text != "")
                    btnRxReceive.Enabled = true;
            }
        }
 
/*************************************************************************************************************************/

        private void btnAnSelectFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            openRecordFileDialog.Filter = "BladeRF files (*.SC16Q11)|*.SC16Q11|All files (*.*)|*.*";
            openRecordFileDialog.InitialDirectory = tbRxFolder.Text;
            DialogResult result = openRecordFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                tbRxFolder.Text = Path.GetDirectoryName(openRecordFileDialog.FileName);
                tbAnFolder.Text = Path.GetDirectoryName(openRecordFileDialog.FileName);
                tbAnSignalFile.Text = Path.GetFileName(openRecordFileDialog.FileName);
            }
        }

        private void btnAnFilter_Click(object sender, EventArgs e)
        {
            //flags to allow parsing scientific notation
            NumberStyles nStyles = NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint;
            CultureInfo cInfo = CultureInfo.InvariantCulture;

            string signalFileName = tbAnFolder.Text + (tbAnFolder.Text.EndsWith(@"\")?"":@"\") + tbAnSignalFile.Text;

            if (!File.Exists(signalFileName))
            { 
                MessageBox.Show("Signal File does not exist","Error");
                return;
            }

            // sample rate can be blank if only viewing signal file
            double sr = 0;
            if (!tbAnSampleRate.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnSampleRate.Text, nStyles, cInfo, out sr) || sr < 2e6 || sr > 8e6)
                {
                    MessageBox.Show("Sample Rate has a missing or incorrect value.", "Error");
                    return;
                }
            }

            //filter order can be blank if all filter options are also blank
            Double fo = 0;
            if (!tbAnFirOrder.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnFirOrder.Text, nStyles, cInfo, out fo) || fo < 2 || fo > 128)
                {
                    MessageBox.Show("Fir Order has an incorrect value.", "Error");
                    return;
                }
            }

            //low pass can be blank if all filter fields are blank
            Double lp=0;
            if (!tbAnLowPass.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnLowPass.Text, nStyles, cInfo, out lp) || lp < 2 || lp > sr/2)
                {
                    MessageBox.Show("Low Pass (Hz) has an incorrect value.", "Error");
                    return;
                }
            }

            //stop can be blank if all filter fields are blank
            Double st = 0;
            if (!tbAnStop.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnStop.Text, nStyles, cInfo, out st) || st <= lp || st > sr / 2)
                {
                    errorProviderAn.SetError(tbAnStop, "Stop (Hz) has an incorrect value.");
                    return;
                }
                else
                    errorProviderAn.SetError(tbAnStop, "");
            }

            //amplitude clip can be blank for no clipping
            Double ac = 0;
            if (!tbAnClip.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnClip.Text, nStyles, cInfo, out ac) || ac < 0.0 || ac > 1.0)
                {
                    errorProviderAn.SetError(tbAnClip, "Amplitude Clip has an incorrect value.");
                    return;
                }
                else
                    errorProviderAn.SetError(tbAnClip, "");
            }

            //amplitude floor can be blank for no flooring
            Double fl = -1;
            if (!tbAnFloor.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnFloor.Text, nStyles, cInfo, out fl) || fl < 0.0 || fl >= ac)
                {
                    MessageBox.Show("Amplitude Floor has an incorrect value.", "Error");
                    return;
                }
            }

            //First Sample can be blank for no trimming front of signal
            UInt32 fSam = 0;
            if (!tbAnFirstSample.Text.Equals(""))
            {
                if (!UInt32.TryParse(tbAnFirstSample.Text, nStyles, cInfo, out fSam) || fSam < 0)
                {
                    MessageBox.Show("First Sample has an incorrect value.", "Error");
                    return;
                }
            }

            //Last Sample can be blank for no trimming front of signal
            UInt32 lSam = 0;
            if (!tbAnLastSample.Text.Equals(""))
            {
                if (!UInt32.TryParse(tbAnLastSample.Text, nStyles, cInfo, out lSam) || lSam <= fSam)
                {
                    MessageBox.Show("Last Sample has an incorrect value.", "Error");
                    return;
                }
            }

            bool noFilter = (tbAnSampleRate.Text == "" && tbAnFirOrder.Text == "" && tbAnLowPass.Text == "" && tbAnStop.Text == "");
            object result = null;
            try
            {
                matlab.Feval("cOOKieFilter", 4, out result, sr, fo, lp, st, fl, ac, fSam, lSam, signalFileName);
                try
                {
                    object[] res = result as object[];
                    string mlResult = (String)res[0];
                    uint[,] args = (uint[,])res[3];
                    if (mlResult.Equals("OK"))
                        try
                        {
                            signal = (Double[,])res[1];
                            btnAnAnalyze.Enabled = true;
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message, "Error");
                        }
                    else
                    {
                        MessageBox.Show(mlResult, "MATLAB Error");
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Error: Matlab results cannot be parsed");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Error: Unhandled MATLAB Exception");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           if (currentDevice != null && !currentDevice.isSaved)
            { 
                if(!(MessageBox.Show("OOK Device has unsaved changes - do you want to exit and lose those changes?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning)==DialogResult.Yes))
                {
                    e.Cancel = true;
                    return;
                }
            }
            
            if (!matlab.Equals(null))
            {
                matlab.Quit();
            }
        }

        private void btnAnAnalyze_Click(object sender, EventArgs e)
        {
            pwmWord = new PwmWord();
            UInt32 sr;
            if (!UInt32.TryParse(tbAnSampleRate.Text, nStyles, cInfo, out sr))
            {
                errorProviderAn.SetError(tbAnSampleRate, "Invalid sample rate");
                return;
            }

            //signal should start just before start bit, and end just after next start bit
            string msg;
            if (!pwmWord.analyzeSignal(signal, sr, 10, out msg))
            {
                MessageBox.Show(msg, "Error");
                return;
            }

            tbAnSBDelay.Text = pwmWord.startBitDelay.ToString();
            tbAnNumBits.Text = pwmWord.dataBits.Count.ToString();
            tbAnAvgNarrowWidth.Text = pwmWord.avgNarrowWidth.ToString("N3");
            tbAnAvgWideWidth.Text = pwmWord.avgWideWidth.ToString("N3");
            tbAnIWDelay.Text = pwmWord.interWordDelay.ToString();

            //set pwmWord and start bit dataContents
            tbAnWordContents.Text = pwmWord.dataContents;
            tbAnStartBitContents.Text = pwmWord.sbContents;
            
            //rise/fall stats
            tbAnAvgRisePeriod.Text = pwmWord.avgRisePeriod.ToString();
            tbAnRiseStDev.Text = Math.Round(pwmWord.riseStDev,4).ToString();
            tbAnAvgFallPeriod.Text = pwmWord.avgFallPeriod.ToString();
            tbAnFallStDev.Text = Math.Round(pwmWord.fallStDev, 4).ToString();

            //set Bins label and drop-down list
            lblAnBins.Text = "Bins("+pwmWord.bins.Count.ToString()+")";
            var list = from bin in pwmWord.bins select bin.width;
            object[] ar = list.Cast<object>().ToArray();
            cbAnBins.Items.Clear();
            cbAnBins.Items.AddRange(ar);
            cbAnBins.Enabled = true;

            //set Bits label and drop-down list
            lblAnBits.Text = "Data Bits ("+pwmWord.dataBits.Count.ToString("d2") + ")";
            list = Enumerable.Range(1,pwmWord.dataBits.Count);
            ar = list.Cast<object>().ToArray();
            cbAnBits.Items.Clear();
            cbAnBits.Items.AddRange(ar);
            cbAnBits.Enabled = true;
            btnAnCurBitPlus.Enabled = true;


        }

        private void cbAnBins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int binName = (int)cb.SelectedItem;
            Bin bin = pwmWord.bins.First(b => b.width == binName);
            tbAnCurBinWidth.Text = binName.ToString();
            tbAnCurBinStDev.Text = Math.Round(bin.stdDev, 4).ToString();
            tbAnCurBinCount.Text = bin.count.ToString();
            tbAnCurBinBits.Text= "";
            foreach(var item in pwmWord.dataBits.Select((bit,i) => new {i, bit}))
            {
                if (binName == item.bit.binName)
                {
                    tbAnCurBinBits.Text += (item.i+1).ToString()+" ";
                }
            }
        }

        private void cbAnBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int bitNumber = (int)cb.SelectedItem + pwmWord.startBitsN;
            Bit bit = pwmWord.dataBits.First(b => b.bitPos == bitNumber);
            
            tbAnCurBitPos.Text = bit.bitPos.ToString();
            
            tbAnCurBitWidth.Text = bit.pulseWidth.ToString();
            tbAnCurBitPWDev.Text = Math.Round(bit.pwDeviation, 4).ToString();

            tbAnCurBitRTime.Text = bit.riseTime.ToString();
            tbAnCurBitFPeriod.Text = bit.fallPeriod.ToString();
            tbAnCurBitFDev.Text = Math.Round(bit.fpDeviation,4).ToString();

            tbAnCurBitFTime.Text = bit.fallTime.ToString();
            tbAnCurBitRPeriod.Text = bit.risePeriod.ToString();
            tbAnCurBitRDev.Text = Math.Round(bit.rpDeviation, 4).ToString();

            if (cb.SelectedIndex == 0)
            {
                btnAnCurBitMinus.Enabled = false;
            }
            else
            {
                btnAnCurBitMinus.Enabled = true;
            }

            if (cb.SelectedIndex == cb.Items.Count-1)
            {
                btnAnCurBitPlus.Enabled = false;
            }
            else
            {
                btnAnCurBitPlus.Enabled = true;
            }
        }

        private void btnAnCurBitPlus_Click(object sender, EventArgs e)
        {
            if (cbAnBits.SelectedIndex <= cbAnBits.Items.Count) 
            {
                cbAnBits.SelectedIndex += 1;
            }
        }

        private void btnAnCurBitMinus_Click(object sender, EventArgs e)
        {
            if (cbAnBits.SelectedIndex != 0)
            {
                cbAnBits.SelectedIndex -= 1;
            }
        }

        /// <summary>
        /// Change display to milliseconds when Ms radio button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAnMs_CheckedChanged(object sender, EventArgs e)
        {
            UInt32 sr = pwmWord.sampleRate;

            tbAnAvgNarrowWidth.Text = Math.Round(((1.0 / sr) * pwmWord.avgNarrowWidth * 1000), 3).ToString("N3") + " ms";
            tbAnAvgWideWidth.Text   = Math.Round(((1.0 / sr) * pwmWord.avgWideWidth   * 1000), 3).ToString("N3") + " ms";
            tbAnAvgRisePeriod.Text  = Math.Round(((1.0 / sr) * pwmWord.avgRisePeriod  * 1000), 3).ToString("N3") + " ms";
            tbAnAvgFallPeriod.Text  = Math.Round(((1.0 / sr) * pwmWord.avgFallPeriod  * 1000), 3).ToString("N3") + " ms";
            tbAnSBDelay.Text = Math.Round(((1.0 / sr) * pwmWord.startBitDelay * 1000), 3).ToString("N3") + " ms";
            tbAnIWDelay.Text = Math.Round(((1.0 / sr) * pwmWord.interWordDelay * 1000), 3).ToString("N3") + " ms";
        }                             

        /// <summary>
        /// Change display to sample counts when Samples radio button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAnSam_CheckedChanged(object sender, EventArgs e)
        {
            tbAnAvgNarrowWidth.Text = pwmWord.avgNarrowWidth.ToString("N3");
            tbAnAvgWideWidth.Text = pwmWord.avgWideWidth.ToString("N3");
            tbAnAvgRisePeriod.Text = pwmWord.avgRisePeriod.ToString("N3");
            tbAnAvgFallPeriod.Text = pwmWord.avgFallPeriod.ToString("N3");
            tbAnSBDelay.Text = pwmWord.startBitDelay.ToString();
            tbAnIWDelay.Text = pwmWord.interWordDelay.ToString();
        }

        /// <summary>
        /// Change display to hertz when Hz button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAnHz_CheckedChanged(object sender, EventArgs e)
        {
            UInt32 sr = pwmWord.sampleRate;

            tbAnAvgNarrowWidth.Text = Math.Round((sr / pwmWord.avgNarrowWidth)).ToString("N3") + " Hz";
            tbAnAvgWideWidth.Text = Math.Round((sr / pwmWord.avgWideWidth)).ToString("N3") + " Hz";
            tbAnAvgRisePeriod.Text = Math.Round((sr / pwmWord.avgRisePeriod)).ToString("N3") + " Hz";
            tbAnAvgFallPeriod.Text = Math.Round((sr / pwmWord.avgFallPeriod)).ToString("N3") + " Hz";
            tbAnSBDelay.Text = Math.Round(((sr * 1.0) / pwmWord.startBitDelay)).ToString("N3") + " Hz";
            tbAnIWDelay.Text = Math.Round(((sr * 1.0) / pwmWord.interWordDelay)).ToString("N3") + " Hz";
        }

        /// <summary>
        /// Start receiving samples from SDR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceive_Click(object sender, EventArgs e)
        {
            foreach (Control c in errorProviderRx.ContainerControl.Controls)
                if (errorProviderRx.GetError(c) != "")
                {
                    c.Focus();
                    return;
                }

            //# of samples = sample rate * time
            UInt32 N = UInt32.Parse(tbSdrRxSampleRate.Text,nStyles,cInfo) * UInt32.Parse(tbSdrRxRecordTime.Text,nStyles,cInfo);
            Int16[] samples = new Int16[N * 2];   // x2, one for I and one for Q
            string msg;
            samples = brf.bladerf_RX(N, out msg);
            if (msg != "")
            {
                MessageBox.Show(msg, "Error");
                return;
            }
            //// save to a csv file
            //try
            //{
            //    StreamWriter file = new StreamWriter(tbRxFolder.Text+'\\'+tbRxFileName.Text, false);

            //    for (UInt32 idx = 0; idx < N * 2; idx += 2)
            //    {
            //        file.WriteLine("{0},{1}", samples[idx], samples[idx + 1]);
            //    }

            //    file.Close();
            //    Console.WriteLine("File saved.");
            //    MessageBox.Show("File Saved");
            //}
            //catch
            //{
            //    Console.WriteLine("Error Opeing or writing to the file.");
            //}

            //save as SC16Q11 binary file
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(tbRxFolder.Text + '\\' + tbRxFileName.Text, FileMode.Create)))
                {
                    for (int i = 0; i < samples.Length; ++i)
                    {
                        writer.Write(samples[i]);
                    }
                }
                Console.WriteLine("File saved.");
                MessageBox.Show("File Saved");

            }
            catch
            {
                Console.WriteLine("Error Opening or writing to the file.");
                MessageBox.Show("Error Opening or writing to the file.", "Error");
            }

            BrfNativeMethods.bladerf_close(brf._sdr);
                  
        }

        private void cbSdrRxLnaGain_Leave(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string msg = "";
            brf.setRxLnaGain(Int32.Parse(cb.Text), out msg);
        }

        private void tabRx_Enter(object sender, EventArgs e)
        {
            if (cbSdrSDR.SelectedIndex == 0)
            {
                getBrfRxConfig();
            }
        }

        private void getBrfRxConfig()
        {
            string msg;
            msg = "";
            tbSdrRxFrequency.Text = brf.getFrequency(out msg).ToString("N0");
            tbSdrRxFrequency.Text = (msg == "") ? tbSdrRxFrequency.Text : msg;

            msg = "";
            tbSdrRxSampleRate.Text = brf.getRxSamplerate(out msg).ToString("N0");
            tbSdrRxSampleRate.Text = (msg == "") ? tbSdrRxSampleRate.Text : msg;

            msg = "";
            tbSdrRxBandwidth.Text = brf.getRxBandwidth(out msg).ToString("N0");
            tbSdrRxBandwidth.Text = (msg == "") ? tbSdrRxBandwidth.Text : msg;

            msg = "";
            cbSdrRxLnaGain.Text = brf.getRxLnaGain(out msg).ToString("N0");
            cbSdrRxLnaGain.Text = (msg == "") ? cbSdrRxLnaGain.Text : msg;
           
            msg = "";
            tbSdrRxVGain1.Text = brf.getRxVGain1(out msg).ToString("N0");
            tbSdrRxVGain1.Text = (msg == "") ? tbSdrRxVGain1.Text : msg;

            msg = "";
            tbSdrRxVGain2.Text = brf.getRxVGain2(out msg).ToString("N0");
            tbSdrRxVGain2.Text = (msg == "") ? tbSdrRxVGain2.Text : msg;

            tbSdrRxNumBuffers.Text = brf._nBuffers.ToString("N0");
            tbSdrRxSamPerBuffer.Text = brf._samplesPerBuffer.ToString("N0");
            tbSdrRxNumUsbChannels.Text = brf._nUsbChannels.ToString("N0");
            tbSdrRxSyncTimeout.Text = brf._syncTimeoutmS.ToString("N0");

            if (Double.Parse(tbSdrRxBandwidth.Text) <= double.Parse(tbSdrRxSampleRate.Text))
            {
                this.errorProviderRx.SetError(tbSdrRxBandwidth, "Bandwidth <= Samplerate");
                this.errorProviderRx.SetError(tbSdrRxSampleRate, "Bandwidth <= Samplerate");
            }

        }

        private void tbSdrRxVGain1_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg = "";
            int vg1 = -1;
            if (!Int32.TryParse(tb.Text, out vg1) )
            {
                MessageBox.Show("V Gain 1 is not a valid number","Error");
            }
            else
            {
                if (!brf.setRxVGain1(vg1, out msg))
                {
                    MessageBox.Show(msg, "Error");
                }
                else
                {
                    tb.Text = brf.getRxVGain1(out msg).ToString();
                    if (msg != "")
                    {
                        MessageBox.Show(msg, "Error");
                    }
                }
            }
        }
        
        private void tbSdrRxVGain2_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg = "";
            int vg2 = -1;
            if (!Int32.TryParse(tb.Text, out vg2))
            {
                MessageBox.Show("V Gain 2 is not a valid number","Error");
            }
            else
            {
                if (!brf.setRxVGain2(vg2, out msg))
                {
                    MessageBox.Show(msg,"Error");
                }
                else
                {
                    tb.Text = brf.getRxVGain2(out msg).ToString();
                    if (msg != "")
                    {
                        MessageBox.Show(msg, "Error");
                    }
                }
            }
        }

        private void tbSdrRxNumBuffers_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text, out val))
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, "Not a positive integer");
            }
            else
            {
                brf._nBuffers = val;
                if (!brf.rxSyncConfig(out msg))
                {
                    e.Cancel = true;
                    this.errorProviderRx.SetError(tb, msg);
                }
                else
                {
                    this.errorProviderRx.SetError(tb, msg);
                }
            }
        }

        private void tbSdrRxSamPerBuffer_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text,nStyles, cInfo, out val))
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, "Not a positive integer");
            }
            else
            {
                ///round down to multiple of 1024  
                val = ((val / 1024) + 1) * 1024;
                brf._samplesPerBuffer = val;
                if (!brf.rxSyncConfig(out msg))
                {
                    e.Cancel = true;
                    this.errorProviderRx.SetError(tb, msg);
                }
                else
                {
                    this.errorProviderRx.SetError(tb, "");
                    tb.Text = val.ToString("N0");
                }
            }
        }

        private void tbSdrRxNumUsbChannels_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text, out val))
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, "Not a positive integer");
            }
            else
            {
                brf._nUsbChannels = val;
                if (!brf.rxSyncConfig(out msg))
                {
                    e.Cancel = true;
                    this.errorProviderRx.SetError(tb, msg);
                }
                else
                {
                    this.errorProviderRx.SetError(tb, msg);
                }
            }

        }

        private void tbSdrRxSyncTimeout_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text, nStyles, cInfo, out val))
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, "Not a positive integer");
            }
            else
            {
                brf._syncTimeoutmS = val;
                if (!brf.rxSyncConfig(out msg))
                {
                    e.Cancel = true;
                    this.errorProviderRx.SetError(tb, msg);
                }
                else
                {
                    this.errorProviderRx.SetError(tb, msg);
                }
            }

        }

        private void tbSdrRxSampleRate_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double sr = 0.0;
            if (!Double.TryParse(tb.Text.Replace(",", ""), nStyles, cInfo, out sr))
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, "Not a valid number");
                return;
            }

            string msg = "";
            sr = brf.setSampleRate(sr, out msg);
            if (msg != "")
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, msg);
                return;
            }

            msg = "";
            sr = brf.getRxSamplerate(out msg);
            if (msg != "")
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, msg);
                return;
            }
            tb.Text = sr.ToString("N0");

            if (sr <= Double.Parse(tbSdrRxBandwidth.Text))
            {
                this.errorProviderRx.SetError(tb, "Sample rate <= Bandwidth");
                this.errorProviderRx.SetError(tbSdrRxBandwidth, "Sample rate <= Bandwidth");
                return;
            }
            this.errorProviderRx.SetError(tb, "");
        }

        private void tbSdrRxBandwidth_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            uint bw;
            if (!UInt32.TryParse(tb.Text.Replace(",", ""), nStyles, cInfo, out bw))
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb,"Not a valid number.");
                return;
            }

            string msg = "";
            bw = brf.setRxBandwidth(bw, out msg);
            if (msg != "")
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, msg);
                return;
           }

            msg = "";
            bw = brf.getRxBandwidth(out msg);
            if (msg != "")
            {
                e.Cancel = true;
                this.errorProviderRx.SetError(tb, msg);
            }
            tb.Text = bw.ToString("N0");

            if (bw >= Double.Parse(tbSdrRxSampleRate.Text))
            {
                this.errorProviderRx.SetError(tb, "Sample rate <= Bandwidth");
                this.errorProviderRx.SetError(tbSdrRxSampleRate, "Sample rate <= Bandwidth");
                return;
            }
            this.errorProviderRx.SetError(tb, "");

        }

        private void tbSdrRxRecordTime_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            UInt16 val;
            if (!UInt16.TryParse(tb.Text, out val) || val < 1 || val > 10)
            {
                errorProviderRx.SetError(tb, "Must be integer between 1 and 10");
                e.Cancel = true;
            }
        }

        private void btnAnMakeDevice_Click(object sender, EventArgs e)
        {
            //get sample rate
            //double sr = Double.Parse(tbAnSampleRate.Text, nStyles, cInfo);
            UInt32 sr = pwmWord.sampleRate;
            double bitWidth = (pwmWord.syncEdge == OokDevice.SyncEdges.Falling)?pwmWord.avgFallPeriod:pwmWord.avgRisePeriod;

            try
            {
                currentDevice = new OokDevice("",
                                              pwmWord.sbContents,
                                              ((1.0 / sr) * pwmWord.startBitDelay) * 1000,
                                              pwmWord.dataBits.Count,
                                              ((1.0 / sr) * pwmWord.avgNarrowWidth) * 1000,
                                              ((1.0 / sr) * pwmWord.avgWideWidth) * 1000,
                                              ((1.0 / sr) * bitWidth) * 1000,
                                              pwmWord.syncEdge,
                                              ((1.0 / sr) * pwmWord.interWordDelay) * 1000);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Cannot create device with these settings:\n" + ee.Message, "Device Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentDevice = null;
                return;
            }

            tbDvDataBitPeriod.Text = currentDevice.bitPeriod_ms.ToString("N3");
            tbDvNarrowPW.Text = currentDevice.narrowWidth_ms.ToString("N3");
            tbDvNumDataBits.Text = currentDevice.nDataBits.ToString();
            tbDvSbContents.Text = currentDevice.startBits;
            tbDvSbPad.Text = currentDevice.startBitPad_ms.ToString("N3");
            cbDvSyncEdge.Text = (currentDevice.syncEdge == OokDevice.SyncEdges.Rising) ? "Rising" : (currentDevice.syncEdge == OokDevice.SyncEdges.Falling) ? "Falling" : "Unknown";
            tbDvWidePW.Text = currentDevice.wideWidth_ms.ToString("N3");
            tbDvWordPad.Text = currentDevice.wordPad_ms.ToString("N3");

            tbDvDeviceName.Enabled = true;
            tbDvDataBitPeriod.Enabled = true;
            tbDvNarrowPW.Enabled = true;
            tbDvNumDataBits.Enabled = true;
            tbDvSbContents.Enabled = true;
            tbDvSbPad.Enabled = true;
            cbDvSyncEdge.Enabled = true;
            tbDvWidePW.Enabled = true;
            tbDvWordPad.Enabled = true;

            btnDvSaveDevice.Enabled = true;
            btnDvSaveAsDevice.Enabled = true;
            
            //signal creation
            tbDvSampleRate.Text = pwmWord.sampleRate.ToString("N0");
            tbDvWordContents.Text = pwmWord.dataContents;
            tbDvWordContents.Enabled = true;
            btnDvMakeSignal.Enabled = true;

            tabControl1.SelectedTab = tabDevice;
            //tabDevice.Select();
            tbDvDeviceName.Focus();
        }

        private void btnAnSelectFolder_Click(object sender, EventArgs e)
        {

        }

        private void btnDevSaveDevice_Click(object sender, EventArgs e)
        {
            if (tbDvDeviceName.Text == "")
            {
                MessageBox.Show("Device must have a name", "Error");
                return;
            }

            bool ok = true;
            foreach (Control c in gbDvDevice.Controls)
            {
                if (errorProviderDv.GetError(c) != "")
                {
                    ok = false;
                }
            }

            if (!ok)
            {
                MessageBox.Show("Validation Errors", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currentDevice != null && currentDevice.name != tbDvDeviceName.Text && !currentDevice.isSaved)
            {
                if (!(MessageBox.Show("Current device has unsaved changes. Do you want to continue and lose those changes?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    return;
                }
            }

            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\devices\";
            if (!Directory.Exists(path))
            {
                DialogResult dr = MessageBox.Show("Devices folder does not exist - do you want to create it?", "Folder Missing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch
                    {
                        MessageBox.Show("Cannot create directory: \n" + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            SaveFileDialog svd = new SaveFileDialog();
            svd.FileName = currentDevice.name;
            svd.Title = "Save OOK Device File";
            svd.Filter = "JSON File|*.json";
            svd.InitialDirectory = path;
            svd.ShowDialog();

            if (svd.FileName != "")
            {
                File.WriteAllText(svd.FileName, JsonConvert.SerializeObject(currentDevice));
                currentDevice.isSaved = true;
            }

 
        }

        private void SampleRate_Validating(object sender, CancelEventArgs e)
        {
            // sample rate can be blank
            Control ct = (Control)sender; 
            double sr = 0;
            if (!ct.Text.Equals(""))
            {
                if (!Double.TryParse(ct.Text, nStyles, cInfo, out sr) || sr < 2e6 || sr > 8e6)
                {
                    MessageBox.Show("Sample Rate has a missing or incorrect value.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void tbDvDeviceName_Validating(object sender, CancelEventArgs e)
        {
            string msg;
            TextBox tb = (TextBox)sender;
            if (!currentDevice.setName(tb.Text, out msg))
            {
                errorProviderDv.SetError(tbDvDeviceName, msg);
            }
            else
            {
                errorProviderDv.SetError(tbDvDeviceName, "");
            }
        }

        private void btnDvNewDevice_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (currentDevice != null && currentDevice.isSaved == false)
            {
                if (!(MessageBox.Show("Current device is not saved! Do you want to continue and lose changes?", 
                    "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    return;
                }
            }

            try
            {
                currentDevice = new OokDevice("", "", 0, 0, 0, 0, 0, OokDevice.SyncEdges.Unknown, 0);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error creating new device:\n" + ee.Message, "Device Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentDevice = null;
                ok = false;
            }

            tbDvDataBitPeriod.Enabled = ok;
            tbDvDeviceName.Enabled = ok;
            tbDvNarrowPW.Enabled = ok;
            tbDvNumDataBits.Enabled = ok;
            tbDvSbContents.Enabled = ok;
            tbDvSbPad.Enabled = ok;
            cbDvSyncEdge.Enabled = ok;
            tbDvWidePW.Enabled = ok;
            tbDvWordPad.Enabled = ok;

            tbDvDataBitPeriod.Text = "";
            tbDvDeviceName.Text = "";
            tbDvNarrowPW.Text = "";
            tbDvNumDataBits.Text = "";
            tbDvSbContents.Text = "";
            tbDvSbPad.Text = "";
            cbDvSyncEdge.Text = "";
            tbDvWidePW.Text = "";
            cbDvSyncEdge.SelectedIndex = 2;
            tbDvWordPad.Text = "";

            btnDvSaveDevice.Enabled = ok;
            btnDvSaveAsDevice.Enabled = ok;

            //signal settings
            tbDvSampleRate.Enabled = ok;
            tbDvWordContents.Enabled = ok;
            tbDvRepetitions.Enabled = ok;
            btnDvMakeSignal.Enabled = ok;
           

        }

        private void tbDvSbContents_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox) sender;
           
            string msg;
            if (!currentDevice.setStartBits(tb.Text, out msg))
            {
                errorProviderDv.SetError(tbDvSbContents, msg);
            }
            else
            {
                errorProviderDv.SetError(tbDvSbContents, "");
            }
            
        }

        private void tbDvSbPad_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text,nStyles,cInfo,out val)) || val < 0 || val > 1000)
            {
                errorProviderDv.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setStartBitPad(val, out msg))
            {
                errorProviderDv.SetError(tb, msg);
            }
            else
            {
                errorProviderDv.SetError(tb, "");
            }
        }

        private void tbDvNumDataBits_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int val;
            if (!(Int32.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProviderDv.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setNDataBits(val, out msg))
            {
                errorProviderDv.SetError(tb, msg);
            }
            else
            {
                errorProviderDv.SetError(tb, "");
            }
        }

        private void tbDvNarrowPW_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProviderDv.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setNarrowWidth_ms(val, out msg))
            {
                errorProviderDv.SetError(tb, msg);
            }
            else
            {
                errorProviderDv.SetError(tb, "");
            }
        }

        private void tbDvWidePW_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProviderDv.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setWideWidth_ms(val, out msg))
            {
                errorProviderDv.SetError(tb, msg);
            }
            else
            {
                errorProviderDv.SetError(tb, "");
            }
        }

        private void tbDvDataBitPeriod_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProviderDv.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setBitPeriod_ms(val, out msg))
            {
                errorProviderDv.SetError(tb, msg);
            }
            else
            {
                errorProviderDv.SetError(tb, "");
            }
        }

        private void cbDvSyncEdge_Validating(object sender, CancelEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            OokDevice.SyncEdges se = (OokDevice.SyncEdges)Enum.Parse(typeof(OokDevice.SyncEdges), cb.SelectedItem.ToString());
            string msg;
            if (!currentDevice.setSyncEdge(se, out msg))
            {
                errorProviderDv.SetError(cb, msg);
            }
            else
            {
                errorProviderDv.SetError(cb, "");
            }
        }

        private void tbDvWordPad_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProviderDv.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setWordPad_ms(val, out msg))
            {
                errorProviderDv.SetError(tb, msg);
            }
            else
            {
                errorProviderDv.SetError(tb, "");
            }
        }

        private void btnDvMakeSignal_Click(object sender, EventArgs e)
        {
            if (currentDevice == null) 
            {
                MessageBox.Show("The current OOK device is not defined", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string msg;
            if (!currentDevice.isValid) 
            {
                if (!currentDevice.validate(out msg))
                {
                    MessageBox.Show("The current OOK device is not valid:\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //be sure relevant textboxes are validated
            tbDvSampleRate.Focus();
            tbDvWordContents.Focus();
            tbDvRepetitions.Focus();
            tbDvSampleRate.Focus();

            if (errorProviderDv.GetError(tbDvSampleRate) != "" ||
                errorProviderDv.GetError(tbDvWordContents) != "" ||
                errorProviderDv.GetError(tbDvRepetitions) != "")
            {
                MessageBox.Show("One or more signal settings is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // sr = sample rate
            UInt32 sr = UInt32.Parse(tbDvSampleRate.Text, nStyles, cInfo);

            generatedSignal = currentDevice.makeWordSignal(sr, tbDvWordContents.Text, int.Parse(tbDvRepetitions.Text), out msg);
            if (msg != "")
            {
                MessageBox.Show(msg, "Cannot Create Signal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // convert signal from IQ to magnitude for plotting in MATLAB
            int[] mlSignal = new int[generatedSignal.Length / 2];
            for (int i = 0; i < generatedSignal.Length; i += 2)
            {
                // calculate magnitude of complex number 
                mlSignal[i / 2] = (int)Math.Sqrt(Math.Pow(generatedSignal[i], 2) + Math.Pow(generatedSignal[i + 1], 2));
            }

            object result = null;
            try 
            {
                matlab.Feval("PlotSignal", 1, out result, mlSignal);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Error: Unhandled MATLAB Exception");
                return;
            }

            try
            {
                object[] res = result as object[];
                string mlResult = (String)res[0];
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Error: Matlab results cannot be parsed");
                return;
            }

            MessageBox.Show("Signal plot is being displayed in a MATLAB window", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDvSaveSignal.Enabled = true;
        }

        private void btnDvOpenDevice_Click(object sender, EventArgs e)
        {
            if (currentDevice != null && !currentDevice.isSaved)
            {
                if (!(MessageBox.Show("Current device is not saved - do you want to continue and lose changes?",
                    "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    return;
                }
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON Files (*.json)|*.json";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\devices\";
            
            ofd.InitialDirectory = path;
            DialogResult result = ofd.ShowDialog();
            if (!(result == DialogResult.OK)) // Test result.
            {
                return;
            }

            //currentDevice = new OokDevice();
            currentDevice = JsonConvert.DeserializeObject<OokDevice>(File.ReadAllText(ofd.FileName));
            
            tbDvDataBitPeriod.Enabled = true;
            tbDvDeviceName.Enabled = true;
            tbDvNarrowPW.Enabled = true;
            tbDvNumDataBits.Enabled = true;
            tbDvSbContents.Enabled = true;
            tbDvSbPad.Enabled = true;
            cbDvSyncEdge.Enabled = true;
            tbDvWidePW.Enabled = true;
            tbDvWordPad.Enabled = true;

            tbDvDataBitPeriod.Text = currentDevice.bitPeriod_ms.ToString("N3");
            tbDvDeviceName.Text = currentDevice.name;
            tbDvNarrowPW.Text = currentDevice.narrowWidth_ms.ToString("N3");
            tbDvNumDataBits.Text = currentDevice.nDataBits.ToString();
            tbDvSbContents.Text = currentDevice.startBits;
            tbDvSbPad.Text = currentDevice.startBitPad_ms.ToString("N3");
            cbDvSyncEdge.Text = (currentDevice.syncEdge == OokDevice.SyncEdges.Rising) ? "Rising" : (currentDevice.syncEdge == OokDevice.SyncEdges.Falling) ? "Falling" : "Unknown";
            //cbDvSyncEdge.SelectedIndex = currentDevice.syncEdge.
            tbDvWidePW.Text = currentDevice.wideWidth_ms.ToString("N3");
            tbDvWordPad.Text = currentDevice.wordPad_ms.ToString("N3");

            btnDvSaveDevice.Enabled = true;
            btnDvSaveAsDevice.Enabled = true;

            //signal settings
            tbDvSampleRate.Enabled = true;
            tbDvWordContents.Enabled = true;
            tbDvRepetitions.Enabled = true;
            btnDvMakeSignal.Enabled = true;

        }

        private void btnDvSaveSignal_Click(object sender, EventArgs e)
        {
            if (generatedSignal == null)
            {
                MessageBox.Show("No signal has been generated to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check signals folder
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\signals\";
            if (!Directory.Exists(path))
            {
                DialogResult dr = MessageBox.Show("Signals folder does not exist - do you want to create it?", "Folder Missing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    MessageBox.Show("Cannot create directory:\n" + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            SaveFileDialog svd = new SaveFileDialog();
            svd.FileName = "";
            svd.Title = "Save SC16Q11 File";
            svd.Filter = "BladeRF SC16Q11 Sample File|*.sc16q11";
            svd.InitialDirectory = path;
            svd.ShowDialog();

            if (svd.FileName != "")
            {
                //save as SC16Q11 binary file
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(svd.FileName, FileMode.Create)))
                    {
                        for (int i = 0; i < generatedSignal.Length; ++i)
                        {
                            writer.Write(generatedSignal[i]);
                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Error Opening or writing to the file:\n"+ee.Message,
                        "File Save Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Signal saved to:\n" + svd.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

    }
}
