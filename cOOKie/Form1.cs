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
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;

namespace cOOKie
{
    public partial class Form1 : Form
    {
        //Int32 sdr_status;
        //IntPtr _sdr;
        BladeRF brf;
        FirFilter ff;
        Double[,] signal;
        PwmWord pwmWord;
        OokDevice currentDevice;

        StringWriter sdrLog;

        //BladeRF SC16Q11 IQ samples
        Int16[] recordedSignal;
        Int16[] generatedSignal;
        Int16[] transmittedSignal;

        // Create the MATLAB instance 
        MLApp.MLApp matlab;

        //flags to allow parsing scientific notation
        NumberStyles nStyles = NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
        CultureInfo cInfo = CultureInfo.InvariantCulture;

        public Form1()
        {
            InitializeComponent();
            
            // Create the MATLAB instance 
            matlab = new MLApp.MLApp();
            matlab.Execute(@"cd '" + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "'");

            rxSaveFileDialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\signals\";
            txOpenFileDialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\signals\";
            anOpenFileDialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\signals\";
            dvOpenFileDialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\devices\";
            dvSaveFileDialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\devices\";

            cbDvSyncEdge.Items.AddRange(Enum.GetNames(typeof(OokDevice.SyncEdges)));
        }

        [HandleProcessCorruptedStateExceptions]
        private void cbSdr_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string sdr = cb.GetItemText(cb.SelectedItem);
            switch (sdr)
            {
                case "BladeRF":

                    // already connected to bladeRF
                    if (brf != null)
                        return;

                    try
                    {
                        brf = new BladeRF("");
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("BladeRF not found. Exception Details:\n" + ee.Message, "Error");
                        cb.SelectedIndex = -1;
                        return;
                    }

                    //MessageBox.Show("Hardware Version: " + brf.fpgaVersionStr + "\nSerial Number: " + brf.serialNumber, "Blade RF Versions");
                    tbRxSdrStatus.AppendText("Hardware Version: " + brf.fpgaVersionStr);
                    tbRxSdrStatus.AppendText("\r\nSerial Number: " + brf.serialNumber);
                    tbRxSdrStatus.AppendText("\r\nFPGA size: " + brf.fpgaSize);
                    tbRxSdrStatus.AppendText("\r\nFPGA version: " + brf.fpgaVersionStr);

                    tbTxSdrStatus.Text = tbRxSdrStatus.Text;

                    //capture log output from stdout
                    sdrLog = new StringWriter();
                    Console.SetOut(sdrLog);

                    try
                    {
                        getBrfRxConfig();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("BladeRF configuration error. Exception Details:\n" + ee.Message, "Error");
                        brf.close();
                        brf = null;

                        tbRxBandwidth.Text = "";
                        tbRxFrequency.Text = "";
                        tbRxBandwidth.Text = "";
                        tbRxSampleRate.Text = "";
                        cbRxLnaGain.SelectedIndex = -1;
                        tbRxVGain1.Text = "";
                        tbRxVGain2.Text = "";
                        tbRxNumBuffers.Text = "";
                        tbRxNumXfers.Text = "";
                        tbRxBufferSize.Text = "";
                        tbRxSyncTimeout.Text = "";
                        cb.SelectedIndex = -1;

                        tbTxBandwidth.Text = "";
                        tbTxFrequency.Text = "";
                        tbTxBandwidth.Text = "";
                        tbTxSampleRate.Text = "";
                        tbTxVGain1.Text = "";
                        tbTxVGain2.Text = "";
                        tbTxNumBuffers.Text = "";
                        tbTxNumXfers.Text = "";
                        tbTxBufferSize.Text = "";
                        tbTxSyncTimeout.Text = "";
                        cb.SelectedIndex = -1;

                        return;
                    }

                    tbRxBandwidth.Enabled = true;
                    tbRxFrequency.Enabled = true;
                    tbRxBandwidth.Enabled = true;
                    tbRxSampleRate.Enabled = true;
                    cbRxLnaGain.Enabled = true;
                    tbRxVGain1.Enabled = true;
                    tbRxVGain2.Enabled = true;
                    tbRxNumBuffers.Enabled = true;
                    tbRxNumXfers.Enabled = true;
                    tbRxBufferSize.Enabled = true;
                    tbRxSyncTimeout.Enabled = true;

                    tbTxBandwidth.Enabled = true;
                    tbTxFrequency.Enabled = true;
                    tbTxBandwidth.Enabled = true;
                    tbTxSampleRate.Enabled = true;
                    tbTxVGain1.Enabled = true;
                    tbTxVGain2.Enabled = true;
                    tbTxNumBuffers.Enabled = true;
                    tbTxNumXfers.Enabled = true;
                    tbTxBufferSize.Enabled = true;
                    tbTxSyncTimeout.Enabled = true;

                    if (tbRxFileName.Text != "")
                        btnRxReceive.Enabled = true;

                    if (tbTxFileName.Text != "")
                        btnTxTransmit.Enabled = true;

                    if (tbTxFileName.Text != "")
                        btnTxTransmit.Enabled = true;

                    if (brf.statusMsg != "")
                        MessageBox.Show(brf.statusMsg);

                    break;

                case "None":
                    if (brf == null)
                        return;

                    brf.close();
                    sdrLog = null;
                    brf = null;

                    tbRxBandwidth.Text = "";
                    tbRxFrequency.Text = "";
                    tbRxBandwidth.Text = "";
                    tbRxSampleRate.Text = "";
                    cbRxLnaGain.Text = "";
                    cbRxLnaGain.SelectedIndex = -1;
                    tbRxVGain2.Text = "";
                    tbRxNumBuffers.Text = "";
                    tbRxNumXfers.Text = "";
                    tbRxBufferSize.Text = "";
                    tbRxSyncTimeout.Text = "";
                    cb.SelectedIndex = -1;

                    tbTxBandwidth.Text = "";
                    tbTxFrequency.Text = "";
                    tbTxBandwidth.Text = "";
                    tbTxSampleRate.Text = "";
                    tbTxVGain1.Text = "";
                    tbTxVGain2.Text = "";
                    tbTxNumBuffers.Text = "";
                    tbTxNumXfers.Text = "";
                    tbTxBufferSize.Text = "";
                    tbTxSyncTimeout.Text = "";
                    cb.SelectedIndex = -1;

                    tbRxBandwidth.Enabled = false;
                    tbRxFrequency.Enabled = false;
                    tbRxBandwidth.Enabled = false;
                    tbRxSampleRate.Enabled = false;
                    cbRxLnaGain.Enabled = false;
                    tbRxVGain1.Enabled = false;
                    tbRxVGain2.Enabled = false;
                    tbRxNumBuffers.Enabled = false;
                    tbRxNumXfers.Enabled = false;
                    tbRxBufferSize.Enabled = false;
                    tbRxSyncTimeout.Enabled = false;
                    btnRxReceive.Enabled = false;
                    
                    tbTxBandwidth.Enabled = false;
                    tbTxFrequency.Enabled = false;
                    tbTxBandwidth.Enabled = false;
                    tbTxSampleRate.Enabled = false;
                    tbTxVGain1.Enabled = false;
                    tbTxVGain2.Enabled = false;
                    tbTxNumBuffers.Enabled = false;
                    tbTxNumXfers.Enabled = false;
                    tbTxBufferSize.Enabled = false;
                    tbTxSyncTimeout.Enabled = false;
                    btnTxTransmit.Enabled = true;

                    tbTxSdrStatus.Text = tbRxSdrStatus.Text = "";

                    break;

                default:
                    brf = null;
                    break;
            }

            //synchronize both combo boxes
            cbRxSDR.SelectedIndex = cbTxSDR.SelectedIndex = cb.SelectedIndex;


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

        private void btnRxSelectFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            rxSaveFileDialog.OverwritePrompt = true;
            rxSaveFileDialog.Filter = "BladeRF files (*.SC16Q11)|*.SC16Q11|All files (*.*)|*.*";
            DialogResult result = rxSaveFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                tbRxFileName.Text = Path.GetFileName(rxSaveFileDialog.FileName);
                if (cbRxSDR.Text != "")
                    btnRxReceive.Enabled = true;
            }
        }

        /*************************************************************************************************************************/

        private void btnAnSelectFile_Click(object sender, EventArgs e)
        {
            string previousFileName = anOpenFileDialog.FileName;
            // Show the dialog and get result.
            anOpenFileDialog.Filter = "BladeRF files (*.SC16Q11)|*.SC16Q11|All files (*.*)|*.*";
            DialogResult result = anOpenFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                if (previousFileName != anOpenFileDialog.FileName)
                    clearAnalysis();
                tbAnSignalFile.Text = Path.GetFileName(anOpenFileDialog.FileName);
            }
        }

        private void clearAnalysis()
        {
            pwmWord = null;

            tbAnClip.Text = "";
            tbAnFloor.Text = "";
            tbAnFirstSample.Text = "";
            tbAnLastSample.Text = "";

            tbAnSBDelay.Text = "";
            tbAnNumBits.Text = "";
            tbAnAvgNarrowWidth.Text = "";
            tbAnAvgWideWidth.Text = "";
            tbAnIWDelay.Text = "";

            //set pwmWord and start bit dataContents
            tbAnWordContents.Text = "";
            tbAnStartBitContents.Text = "";

            //rise/fall stats
            tbAnAvgRisePeriod.Text = "";
            tbAnRiseStDev.Text = "";
            tbAnAvgFallPeriod.Text = "";
            tbAnFallStDev.Text = "";

            //clear Bins label and drop-down list
            lblAnBins.Text = "Bins(0)";
            cbAnBins.Items.Clear();
            cbAnBins.Enabled = false;
            tbAnCurBinWidth.Text = "";
            tbAnCurBinStDev.Text = "";
            tbAnCurBinCount.Text = "";
            tbAnCurBinBits.Text = "";

            //clear Bits label and drop-down list
            lblAnBits.Text = "Data Bits (0)";
            cbAnBits.Items.Clear();
            cbAnBits.Enabled = false;
            tbAnCurBitPos.Text = "";
            tbAnCurBitWidth.Text = "";
            tbAnCurBitPWDev.Text = "";
            tbAnCurBitRTime.Text = "";
            tbAnCurBitFPeriod.Text = "";
            tbAnCurBitFDev.Text = "";
            tbAnCurBitFTime.Text = "";
            tbAnCurBitRPeriod.Text = "";
            tbAnCurBitRDev.Text = "";
            btnAnCurBitMinus.Enabled = false;
            btnAnCurBitPlus.Enabled = false;

            btnAnCurBitPlus.Enabled = false;
            btnAnMakeDevice.Enabled = false;
        }

        private void btnAnFilter_Click(object sender, EventArgs e)
        {
            //flags to allow parsing scientific notation
            NumberStyles nStyles = NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint;
            CultureInfo cInfo = CultureInfo.InvariantCulture;

            if (!File.Exists(anOpenFileDialog.FileName))
            {
                MessageBox.Show("Signal File does not exist", "Error");
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
            Double lp = 0;
            if (!tbAnLowPass.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnLowPass.Text, nStyles, cInfo, out lp) || lp < 2 || lp > sr / 2)
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
                    errorProvider1.SetError(tbAnStop, "Stop (Hz) has an incorrect value.");
                    return;
                }
                else
                    errorProvider1.SetError(tbAnStop, "");
            }

            //amplitude clip can be blank for no clipping
            Double ac = 0;
            if (!tbAnClip.Text.Equals(""))
            {
                if (!Double.TryParse(tbAnClip.Text, nStyles, cInfo, out ac) || ac < 0.0 || ac > 1.0)
                {
                    errorProvider1.SetError(tbAnClip, "Amplitude Clip has an incorrect value.");
                    return;
                }
                else
                    errorProvider1.SetError(tbAnClip, "");
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
                matlab.Feval("cOOKieFilter", 4, out result, tbAnSignalFile.Text, sr, fo, lp, st, fl, ac, fSam, lSam, anOpenFileDialog.FileName);
                try
                {
                    object[] res = result as object[];
                    string mlResult = (String)res[0];
                    //uint[,] args = (uint[,])res[3];
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
                if (!(MessageBox.Show("OOK Device has unsaved changes - do you want to exit and lose those changes?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (!matlab.Equals(null))
            {
                matlab.Quit();
            }

            if (brf != null)
            {
                brf.close();
            }
        }

        private void btnAnAnalyze_Click(object sender, EventArgs e)
        {
            pwmWord = new PwmWord();
            UInt32 sr;
            if (!UInt32.TryParse(tbAnSampleRate.Text, nStyles, cInfo, out sr))
            {
                errorProvider1.SetError(tbAnSampleRate, "Invalid sample rate");
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
            tbAnRiseStDev.Text = Math.Round(pwmWord.riseStDev, 4).ToString();
            tbAnAvgFallPeriod.Text = pwmWord.avgFallPeriod.ToString();
            tbAnFallStDev.Text = Math.Round(pwmWord.fallStDev, 4).ToString();

            //set Bins label and drop-down list
            lblAnBins.Text = "Bins(" + pwmWord.bins.Count.ToString() + ")";
            var list = from bin in pwmWord.bins select bin.width;
            object[] ar = list.Cast<object>().ToArray();
            cbAnBins.Items.Clear();
            cbAnBins.Items.AddRange(ar);
            cbAnBins.Enabled = true;

            //set Bits label and drop-down list
            lblAnBits.Text = "Data Bits (" + pwmWord.dataBits.Count.ToString("d2") + ")";
            list = Enumerable.Range(1, pwmWord.dataBits.Count);
            ar = list.Cast<object>().ToArray();
            cbAnBits.Items.Clear();
            cbAnBits.Items.AddRange(ar);
            cbAnBits.Enabled = true;
            btnAnCurBitPlus.Enabled = true;
            btnAnMakeDevice.Enabled = true;


        }

        private void cbAnBins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int binName = (int)cb.SelectedItem;
            Bin bin = pwmWord.bins.First(b => b.width == binName);
            tbAnCurBinWidth.Text = binName.ToString();
            tbAnCurBinStDev.Text = Math.Round(bin.stdDev, 4).ToString();
            tbAnCurBinCount.Text = bin.count.ToString();
            tbAnCurBinBits.Text = "";
            foreach (var item in pwmWord.dataBits.Select((bit, i) => new { i, bit }))
            {
                if (binName == item.bit.binName)
                {
                    tbAnCurBinBits.Text += (item.i + 1).ToString() + " ";
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
            tbAnCurBitFDev.Text = Math.Round(bit.fpDeviation, 4).ToString();

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

            if (cb.SelectedIndex == cb.Items.Count - 1)
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
            tbAnAvgWideWidth.Text = Math.Round(((1.0 / sr) * pwmWord.avgWideWidth * 1000), 3).ToString("N3") + " ms";
            tbAnAvgRisePeriod.Text = Math.Round(((1.0 / sr) * pwmWord.avgRisePeriod * 1000), 3).ToString("N3") + " ms";
            tbAnAvgFallPeriod.Text = Math.Round(((1.0 / sr) * pwmWord.avgFallPeriod * 1000), 3).ToString("N3") + " ms";
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
            foreach (Control c in errorProvider1.ContainerControl.Controls)
                if (errorProvider1.GetError(c) != "")
                {
                    c.Focus();
                    return;
                }

            tbRxSdrStatus.AppendText("\r\nReceiving started ...");
            tbTxSdrStatus.AppendText("\r\nReceiving started ...");

            //# of samples = sample rate * time
            UInt32 N = UInt32.Parse(tbRxSampleRate.Text, nStyles, cInfo) * UInt32.Parse(tbRxRecordTime.Text, nStyles, cInfo);
            Int16[] samples = new Int16[N * 2];   // x2, one for I and one for Q
            string msg;
            samples = brf.bladerf_RX(N, out msg);
            if (msg != "")
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbRxSdrStatus.AppendText("\r\n\r\n****\r\nERROR: "+msg+"\r\n****\r\n");
                tbTxSdrStatus.AppendText("\r\n\r\n****\r\nERROR: " + msg + "\r\n****\r\n");
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
            //    Console.WriteLine("Error Opening or writing to the file.");
            //}

            //save as SC16Q11 binary file
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(rxSaveFileDialog.FileName, FileMode.Create)))
                {
                    for (int i = 0; i < samples.Length; ++i)
                    {
                        writer.Write(samples[i]);
                    }
                }
                //Console.WriteLine("File saved.");
                //MessageBox.Show("File Saved");
                tbRxSdrStatus.AppendText("\r\nFile Saved");
                tbTxSdrStatus.AppendText("\r\nFile Saved");
            }
            catch
            {
                Console.WriteLine("Error Opening or writing to the file.");
                MessageBox.Show("Error Opening or writing to the file.", "Error");
            }

        }

        private void tabRxTx_Enter(object sender, EventArgs e)
        {
            if (cbRxSDR.SelectedIndex == 0)
            {
                getBrfRxConfig();
            }
        }

        private void getBrfRxConfig()
        {
            tbRxFrequency.Text = brf.rx_frequency.ToString("N0");
            tbRxSampleRate.Text = brf.rx_sampleRate.ToString("N0");
            tbRxBandwidth.Text = brf.rx_bandwidth.ToString("N0");
            cbRxLnaGain.SelectedIndex = (int)brf.rx_lna_gain;
            tbRxVGain1.Text = brf.rx_vgain1.ToString("N0");
            tbRxVGain2.Text = brf.rx_vgain2.ToString("N0");
            tbRxNumBuffers.Text = brf.rx_nBuffers.ToString("N0");
            tbRxBufferSize.Text = brf.rx_samplesPerBuffer.ToString("N0");
            tbRxNumXfers.Text = brf.rx_nUsbChannels.ToString("N0");
            tbRxSyncTimeout.Text = brf.rx_syncTimeoutmS.ToString("N0");

            tbTxFrequency.Text = brf.tx_frequency.ToString("N0");
            tbTxSampleRate.Text = brf.tx_sampleRate.ToString("N0");
            tbTxBandwidth.Text = brf.tx_bandwidth.ToString("N0");
            tbTxVGain1.Text = brf.tx_vgain1.ToString("N0");
            tbTxVGain2.Text = brf.tx_vgain2.ToString("N0");
            tbTxNumBuffers.Text = brf.tx_nBuffers.ToString("N0");
            tbTxBufferSize.Text = brf.tx_samplesPerBuffer.ToString("N0");
            tbTxNumXfers.Text = brf.tx_nUsbChannels.ToString("N0");
            tbTxSyncTimeout.Text = brf.tx_syncTimeoutmS.ToString("N0");


        }

        private void tbNumBuffers_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text, out val))
            {
                e.Cancel = true;
                this.errorProvider1.SetError(tb, "Not a positive integer");
            }
            else
            {
                brf.rx_nBuffers = val;
                string module;

                if (tb.Name.Contains("Rx"))
                    module = "RX";
                else if (tb.Name.Contains("Tx"))
                    module = "TX";
                else
                    throw new ApplicationException("Invalid textbox name: " + tb.Name);

                if (!brf.syncConfig(module, out msg))
                {
                    e.Cancel = true;
                    this.errorProvider1.SetError(tb, msg);
                }
                else
                    this.errorProvider1.SetError(tb, msg);

            }
        }

        private void tbBufferSize_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text, nStyles, cInfo, out val))
            {
                e.Cancel = true;
                this.errorProvider1.SetError(tb, "Not a positive integer");
            }
            else
            {
                ///round down to multiple of 1024  
                val = ((val / 1024) + 1) * 1024;
                brf.rx_samplesPerBuffer = val;

                string module;
                if (tb.Name.Contains("Rx"))
                    module = "RX";
                else if (tb.Name.Contains("Tx"))
                    module = "TX";
                else
                    throw new ApplicationException("Invalid textbox name: " + tb.Name);

                if (!brf.syncConfig(module, out msg))
                {
                    e.Cancel = true;
                    this.errorProvider1.SetError(tb, msg);
                }
                else
                {
                    this.errorProvider1.SetError(tb, "");
                    tb.Text = val.ToString("N0");
                }
            }
        }

        private void tbNumXfers_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text, out val))
            {
                e.Cancel = true;
                this.errorProvider1.SetError(tb, "Not a positive integer");
            }
            else
            {
                brf.rx_nUsbChannels = val;
                string module;
                if (tb.Name.Contains("Rx"))
                    module = "RX";
                else if (tb.Name.Contains("Tx"))
                    module = "TX";
                else
                    throw new ApplicationException("Invalid textbox name: " + tb.Name);

                if (!brf.syncConfig(module, out msg))
                {
                    e.Cancel = true;
                    this.errorProvider1.SetError(tb, msg);
                }
                else
                {
                    this.errorProvider1.SetError(tb, msg);
                    tb.Text = val.ToString("N0");
                }
            }

        }

        private void tbSyncTimeout_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg;
            UInt32 val;
            if (!UInt32.TryParse(tb.Text, nStyles, cInfo, out val))
            {
                e.Cancel = true;
                this.errorProvider1.SetError(tb, "Not a positive integer");
            }
            else
            {
                string module;
                if (tb.Name.Contains("Rx"))
                    module = "RX";
                else if (tb.Name.Contains("Tx"))
                    module = "TX";
                else
                    throw new ApplicationException("Invalid textbox name: " + tb.Name);
                brf.rx_syncTimeoutmS = val;

                if (!brf.syncConfig(module, out msg))
                {
                    e.Cancel = true;
                    this.errorProvider1.SetError(tb, msg);
                }
                else
                {
                    this.errorProvider1.SetError(tb, msg);
                    tb.Text = val.ToString("N0");
                }
            }

        }

        private void tbSampleRate_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            string msg = "";
            string module;
            PropertyInfo prop;

            //configure for RX or TX module and property
            if (tb.Name.Contains("Rx"))
            {
                module = "RX";
                prop = brf.GetType().GetProperty("rx_sampleRate");
            }
            else if (tb.Name.Contains("Tx"))
            {
                module = "TX";
                prop = brf.GetType().GetProperty("tx_sampleRate");
            }
            else
                throw new ApplicationException("Invalid textbox name: " + tb.Name);

            if (!Double.TryParse(tb.Text, nStyles, cInfo, out val) || !brf.setSampleRate(val, module, out msg))
            {
                MessageBox.Show("Invald sample rate: " + tb.Text + "\n" + msg + "\nResetting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            tb.Text = ((Double)prop.GetValue(brf)).ToString("N0");

        }

        private void tbBandwidth_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            uint bw;
            string msg = "";
            string module;
            PropertyInfo prop;

            if (tb.Name.Contains("Rx"))
            {
                module = "RX";
                prop = brf.GetType().GetProperty("rx_bandwidth");
            }
            else if (tb.Name.Contains("Tx"))
            {
                module = "TX";
                prop = brf.GetType().GetProperty("tx_bandwidth");
            }
            else
                throw new ApplicationException("Invalid textbox name: " + tb.Name);


            if (!UInt32.TryParse(tb.Text, nStyles, cInfo, out bw) || !brf.setBandwidth(bw, module, out msg))
            {
                MessageBox.Show("Invald bandwidth: " + tb.Text + "\n" + msg + "\nResetting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
            tb.Text = ((UInt32)prop.GetValue(brf)).ToString("N0");
        }

        private void tbRxRecordTime_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            UInt16 val;
            if (!UInt16.TryParse(tb.Text, out val) || val < 1 || val > 10)
            {
                errorProvider1.SetError(tb, "Must be integer between 1 and 10");
                e.Cancel = true;
            }
        }

        private void btnAnMakeDevice_Click(object sender, EventArgs e)
        {
            if (pwmWord == null)
                return;

            UInt32 sr = pwmWord.sampleRate;
            double bitWidth = (pwmWord.syncEdge == OokDevice.SyncEdges.Falling) ? pwmWord.avgFallPeriod : pwmWord.avgRisePeriod;

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

            //signal creation
            tbDvSampleRate.Text = pwmWord.sampleRate.ToString("N0");
            tbDvWordContents.Text = pwmWord.dataContents;
            tbDvWordContents.Enabled = true;
            btnDvMakeSignal.Enabled = true;

            tabControl1.SelectedTab = tabDevice;
            //tabDevice.Select();
            tbDvDeviceName.Focus();
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
                if (errorProvider1.GetError(c) != "")
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
            //if (!Directory.Exists(path))
            //{
            //    DialogResult dr = MessageBox.Show("Devices folder does not exist - do you want to create it?", "Folder Missing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    if (dr == DialogResult.Cancel)
            //    {
            //        return;
            //    }

            //    if (dr == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            Directory.CreateDirectory(path);
            //        }
            //        catch
            //        {
            //            MessageBox.Show("Cannot create directory: \n" + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //}

            dvSaveFileDialog.FileName = currentDevice.name;
            dvSaveFileDialog.Title = "Save OOK Device File";
            dvSaveFileDialog.Filter = "JSON File|*.json";
            dvSaveFileDialog.ShowDialog();

            if (dvSaveFileDialog.FileName != "")
            {
                File.WriteAllText(dvSaveFileDialog.FileName, JsonConvert.SerializeObject(currentDevice));
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
                    MessageBox.Show("Sample Rate has a missing or incorrect value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                errorProvider1.SetError(tbDvDeviceName, msg);
            }
            else
            {
                errorProvider1.SetError(tbDvDeviceName, "");
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

            //signal settings
            tbDvSampleRate.Enabled = ok;
            tbDvWordContents.Enabled = ok;
            tbDvRepetitions.Enabled = ok;
            btnDvMakeSignal.Enabled = ok;


        }

        private void tbDvSbContents_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            string msg;
            if (!currentDevice.setStartBits(tb.Text, out msg))
            {
                errorProvider1.SetError(tbDvSbContents, msg);
            }
            else
            {
                errorProvider1.SetError(tbDvSbContents, "");
            }

        }

        private void tbDvSbPad_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProvider1.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setStartBitPad(val, out msg))
            {
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                errorProvider1.SetError(tb, "");
            }
        }

        private void tbDvNumDataBits_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int val;
            if (!(Int32.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProvider1.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setNDataBits(val, out msg))
            {
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                errorProvider1.SetError(tb, "");
            }
        }

        private void tbDvNarrowPW_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProvider1.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setNarrowWidth_ms(val, out msg))
            {
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                errorProvider1.SetError(tb, "");
            }
        }

        private void tbDvWidePW_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProvider1.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setWideWidth_ms(val, out msg))
            {
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                errorProvider1.SetError(tb, "");
            }
        }

        private void tbDvDataBitPeriod_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProvider1.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setBitPeriod_ms(val, out msg))
            {
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                errorProvider1.SetError(tb, "");
            }
        }

        private void cbDvSyncEdge_Validating(object sender, CancelEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            OokDevice.SyncEdges se = (OokDevice.SyncEdges)Enum.Parse(typeof(OokDevice.SyncEdges), cb.SelectedItem.ToString());
            string msg;
            if (!currentDevice.setSyncEdge(se, out msg))
            {
                errorProvider1.SetError(cb, msg);
            }
            else
            {
                errorProvider1.SetError(cb, "");
            }
        }

        private void tbDvWordPad_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double val;
            if (!(Double.TryParse(tb.Text, nStyles, cInfo, out val)) || val < 0 || val > 1000)
            {
                errorProvider1.SetError(tb, "Not a valid number");
                return;
            }

            string msg;
            if (!currentDevice.setWordPad_ms(val, out msg))
            {
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                errorProvider1.SetError(tb, "");
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

            if (errorProvider1.GetError(tbDvSampleRate) != "" ||
                errorProvider1.GetError(tbDvWordContents) != "" ||
                errorProvider1.GetError(tbDvRepetitions) != "")
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

            dvOpenFileDialog.Filter = "JSON Files (*.json)|*.json";
            DialogResult result = dvOpenFileDialog.ShowDialog();
            if (!(result == DialogResult.OK)) // Test result.
            {
                return;
            }

            //currentDevice = new OokDevice();
            currentDevice = JsonConvert.DeserializeObject<OokDevice>(File.ReadAllText(dvOpenFileDialog.FileName));

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
                    MessageBox.Show("Error Opening or writing to the file:\n" + ee.Message,
                        "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Signal saved to:\n" + svd.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btnTxOpenFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            txOpenFileDialog.Filter = "BladeRF files (*.SC16Q11)|*.SC16Q11|All files (*.*)|*.*";
            DialogResult result = txOpenFileDialog.ShowDialog();
            if (!(result == DialogResult.OK)) // Test result.
            {
                return;
            }

            tbTxFileName.Text = Path.GetFileName(txOpenFileDialog.FileName);
            btnTxTransmit.Enabled = (brf != null);
            btnTxViewSignal.Enabled = true;

            //load signal from file
            var tempArray = File.ReadAllBytes(txOpenFileDialog.FileName);
            transmittedSignal = new Int16[tempArray.Length / 2];

            for (int i = 0; i < tempArray.Length; i += 2)
            {
                transmittedSignal[i / 2] = (Int16)(tempArray[i] + tempArray[i + 1] * 256);
            }


        }

        private void tbFrequency_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg = "";
            UInt32 val;
            string module;
            PropertyInfo prop;

            if (tb.Name.Contains("Rx"))
            {
                module = "RX";
                prop = brf.GetType().GetProperty("rx_frequency");
            }
            else if (tb.Name.Contains("Tx"))
            {
                module = "TX";
                prop = brf.GetType().GetProperty("tx_frequency");
            }
            else
            {
                throw new ApplicationException("Invalid textbox name: " + tb.Name);
            }

            if (!UInt32.TryParse(tb.Text, nStyles, cInfo, out val) || !brf.setFrequency(val, module, out msg))
            {
                MessageBox.Show("Invald frequency: " + tb.Text + "\n" + msg + "\nResetting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            tb.Text = ((UInt32)prop.GetValue(brf)).ToString("N0");
        }

        private void cbRxLnaGain_Validating(object sender, CancelEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string msg = "";
            brf.setRxLnaGain(cb.SelectedIndex, out msg);
            cb.SelectedIndex = (int)brf.rx_lna_gain;
        }

        private void tbRxVGain1_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg = "";
            int vg1;
            if (!Int32.TryParse(tb.Text, out vg1) || !brf.setRxVGain1(vg1, out msg))
            {
                MessageBox.Show("Invald Vgain1: " + tb.Text + "\n" + msg + "\nResetting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            tb.Text = brf.rx_vgain1.ToString("N0");

        }

        private void tbRxVGain2_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string msg = "";
            int vg2;
            if (!Int32.TryParse(tb.Text, out vg2) || !brf.setRxVGain2(vg2, out msg))
            {
                MessageBox.Show("Invald Vgain2: " + tb.Text + "\n" + msg + "\nResetting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            tb.Text = brf.rx_vgain2.ToString("N0");

        }

        private void tbTxDelay_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            uint val;
            if (!UInt32.TryParse(tb.Text, nStyles, cInfo, out val) || val > 10000)
            {
                errorProvider1.SetError(tb, "Not a integer between 0 and 10000");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tb, "");
        }

        private void tbTxCount_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            uint val;
            if (!UInt32.TryParse(tb.Text, nStyles, cInfo, out val) || val > 100 || val < 1)
            {
                errorProvider1.SetError(tb, "Not a integer between 1 and 100");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tb, "");

        }

        private void btnTxTransmit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            foreach (Control c in gbTxSignalOptions.Controls)
            {
                if(errorProvider1.GetError(c) != "")
                    return;
            }

            tbRxSdrStatus.AppendText("\r\nTransmission started...");
            tbTxSdrStatus.AppendText("\r\nTransmission started...");

            int repeat = int.Parse(tbTxCount.Text);
            string msg;
            if (!brf.bladerf_TX(transmittedSignal, repeat, out msg))
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbRxSdrStatus.AppendText("\r\n\r\n****\r\nERROR: " + msg + "\r\n****\r\n");
                tbTxSdrStatus.AppendText("\r\n\r\n****\r\nERROR: " + msg + "\r\n****\r\n");
                return;
            }

            tbRxSdrStatus.AppendText("\r\nTransmission completed");
            tbTxSdrStatus.AppendText("\r\nTransmission completed");

        }

        private void btnTxViewSignal_Click(object sender, EventArgs e)
        {
            if (transmittedSignal == null)
                return;

            // convert signal from IQ to magnitude for plotting in MATLAB
            int[] mlSignal = new int[transmittedSignal.Length / 2];
            for (int i = 0; i < transmittedSignal.Length; i += 2)
            {
                // calculate magnitude of complex number 
                mlSignal[i / 2] = (int)Math.Sqrt(Math.Pow(transmittedSignal[i], 2) + Math.Pow(transmittedSignal[i + 1], 2));
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

        }
    }
}
