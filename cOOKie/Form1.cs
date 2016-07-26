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
        Double sr;
        PwmWord pwmWord;

        // Create the MATLAB instance 
        MLApp.MLApp matlab;

        public Form1()
        {
            InitializeComponent();
            btnReceive.Enabled = false;  //disable Rx button until SDR is selected
            btnTransmit.Enabled = false; //disable Tx button until tx-capable SDR is selected

            // Create the MATLAB instance 
            matlab = new MLApp.MLApp();
            string temp = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            matlab.Execute(@"cd '" + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"'");
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

                        MessageBox.Show(brf.getHwVersion(), "Blade RF Versions");
                    }
                    catch (Exception ee) {
                        MessageBox.Show("BladeRF not found. Exception Details:\n"+ee.Message, "Error");
                        cb.SelectedIndex = -1;
                    
                    }

                    
                    break;

                default:
                    break;
            }

        }

        
        private void tbSdrFrequency_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if ((!String.IsNullOrEmpty(tb.Text))&&(!brf.setFrequency(tb.Text)))
            {
                MessageBox.Show("Invald frequency: " + tb.Text, "Error");
                tb.Select();
            }

            tb.Text = brf.getFrequency().ToString();
            
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

        private void btbRxSelectFolder_Click(object sender, EventArgs e)
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
            path+=tbRxRecord.Text;
            if (File.Exists(path))
            {
                MessageBox.Show("FileExists", "Warning");
            }
        }


        private void btnRxRecordSelect_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            openRecordFileDialog.Filter = "BladeRF files (*.SC16Q11)|*.SC16Q11|All files (*.*)|*.*";
            openRecordFileDialog.InitialDirectory = tbRxFolder.Text;
            DialogResult result = openRecordFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                tbRxRecord.Text = Path.GetFileName(openRecordFileDialog.FileName);
                tbRxFolder.Text = Path.GetDirectoryName(openRecordFileDialog.FileName);
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
            NumberStyles styles = NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint;
            CultureInfo cInfo = CultureInfo.InvariantCulture;

            string signalFileName = tbAnFolder.Text + (tbAnFolder.Text.EndsWith(@"\")?"":@"\") + tbAnSignalFile.Text;

            if (!File.Exists(signalFileName))
            { 
                MessageBox.Show("Signal File does not exist","Error");
                return;
            }

            // sample rate can be blank if only viewing signal file
            sr = 0;
            if (!tbAnSampleRate.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnSampleRate.Text, styles, cInfo, out sr) || sr < 2e6 || sr > 8e6)
                {
                    MessageBox.Show("Sample Rate has a missing or incorrect value.", "Error");
                    return;
                }
            }

            //filter order can be blank if all filter options are also blank
            Double fo = 0;
            if (!tbAnFirOrder.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnFirOrder.Text, styles, cInfo, out fo) || fo < 2 || fo > 128)
                {
                    MessageBox.Show("Fir Order has an incorrect value.", "Error");
                    return;
                }
            }

            //low pass can be blank if all filter fields are blank
            Double lp=0;
            if (!tbAnLowPass.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnLowPass.Text, styles, cInfo, out lp) || lp < 2 || lp > sr/2)
                {
                    MessageBox.Show("Low Pass (Hz) has an incorrect value.", "Error");
                    return;
                }
            }

            //stop can be blank if all filter fields are blank
            Double st = 0;
            if (!tbAnStop.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnStop.Text, styles, cInfo, out st) || st <= lp || st > sr/2 )
                {
                    MessageBox.Show("Stop (Hx) has an incorrect value.", "Error");
                    return;
                }
            }

            //amplitude clip can be blank for no clipping
            Double ac = 0;
            if (!tbAnClip.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnClip.Text, styles, cInfo, out ac) || ac < 0.0 || ac > 1.0)
                {
                    MessageBox.Show("Amplitude Clip has an incorrect value.", "Error");
                    return;
                }
            }

            //amplitude floor can be blank for no flooring
            Double fl = -1;
            if (!tbAnFloor.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnFloor.Text, styles, cInfo, out fl) || fl < 0.0 || fl >= ac)
                {
                    MessageBox.Show("Amplitude Floor has an incorrect value.", "Error");
                    return;
                }
            }

            //First Sample can be blank for no trimming front of signal
            UInt32 fSam = 0;
            if (!tbAnFirstSample.Text.Equals(""))
            {
                if (!UInt32.TryParse(tbAnFirstSample.Text, styles, cInfo, out fSam) || fSam < 0)
                {
                    MessageBox.Show("First Sample has an incorrect value.", "Error");
                    return;
                }
            }

            //Last Sample can be blank for no trimming front of signal
            UInt32 lSam = 0;
            if (!tbAnLastSample.Text.Equals(""))
            {
                if (!UInt32.TryParse(tbAnLastSample.Text, styles, cInfo, out lSam) || lSam <= fSam)
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
            if (!matlab.Equals(null))
            {
                matlab.Quit();
            }
        }

        private void btnAnAnalyze_Click(object sender, EventArgs e)
        {
            pwmWord = new PwmWord();
            if (!pwmWord.analyzeSignal(signal, sr, 10))
            {
                return;
            }

            tbAnNumBits.Text = pwmWord.dataBits.Count.ToString();
            tbAnAvgNarrowWidth.Text = pwmWord.avgNarrowWidth.ToString();
            tbAnAvgWideWidth.Text = pwmWord.avgWideWidth.ToString();

            //set pwmWord contents
            tbAnWordContents.Text = pwmWord.contents;

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
            lblAnBits.Text = "Bits ("+pwmWord.dataBits.Count.ToString("d2") + ")";
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
            int bitNumber = (int)cb.SelectedItem + pwmWord.nStartBits;
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

        private void rbAnMs_CheckedChanged(object sender, EventArgs e)
        {
            tbAnAvgNarrowWidth.Text = ((1 / sr) * pwmWord.avgNarrowWidth * 1000).ToString() + " ms";
            tbAnAvgWideWidth.Text = ((1 / sr) * pwmWord.avgWideWidth * 1000).ToString() + " ms";
            tbAnAvgRisePeriod.Text = ((1 / sr) * pwmWord.avgRisePeriod * 1000).ToString() + " ms";
            tbAnAvgFallPeriod.Text = ((1 / sr) * pwmWord.avgFallPeriod * 1000).ToString() + " ms";
        }

        private void rbAnSam_CheckedChanged(object sender, EventArgs e)
        {
            tbAnAvgNarrowWidth.Text = pwmWord.avgNarrowWidth.ToString();
            tbAnAvgWideWidth.Text   =   pwmWord.avgWideWidth.ToString();
            tbAnAvgRisePeriod.Text  =  pwmWord.avgRisePeriod.ToString();
            tbAnAvgFallPeriod.Text  =  pwmWord.avgFallPeriod.ToString();

        }

        private void btnReceive_Click(object sender, EventArgs e)
        {

        }
        
    }
}
