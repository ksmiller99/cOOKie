namespace cOOKie
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the dataContents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ttSdr = new System.Windows.Forms.ToolTip(this.components);
            this.tbRxFrequency = new System.Windows.Forms.TextBox();
            this.tbRxSampleRate = new System.Windows.Forms.TextBox();
            this.tbRxBandwidth = new System.Windows.Forms.TextBox();
            this.cbRxLnaGain = new System.Windows.Forms.ComboBox();
            this.tbRxNumBuffers = new System.Windows.Forms.TextBox();
            this.tbRxBufferSize = new System.Windows.Forms.TextBox();
            this.tbRxNumXfers = new System.Windows.Forms.TextBox();
            this.tbRxSyncTimeout = new System.Windows.Forms.TextBox();
            this.cbRxSDR = new System.Windows.Forms.ComboBox();
            this.tbTxBandwidth = new System.Windows.Forms.TextBox();
            this.tbTxSampleRate = new System.Windows.Forms.TextBox();
            this.tbTxFrequency = new System.Windows.Forms.TextBox();
            this.cbTxSDR = new System.Windows.Forms.ComboBox();
            this.tbTxSyncTimeout = new System.Windows.Forms.TextBox();
            this.tbTxNumXfers = new System.Windows.Forms.TextBox();
            this.tbTxBufferSize = new System.Windows.Forms.TextBox();
            this.tbTxNumBuffers = new System.Windows.Forms.TextBox();
            this.ttTxDelay = new System.Windows.Forms.ToolTip(this.components);
            this.tbTxDelay = new System.Windows.Forms.TextBox();
            this.ttTxCount = new System.Windows.Forms.ToolTip(this.components);
            this.tbTxCount = new System.Windows.Forms.TextBox();
            this.ttTxParameters = new System.Windows.Forms.ToolTip(this.components);
            this.ttRxTab = new System.Windows.Forms.ToolTip(this.components);
            this.btnAnSelectFile = new System.Windows.Forms.Button();
            this.tbRxFileName = new System.Windows.Forms.TextBox();
            this.rbRxInputRaw = new System.Windows.Forms.RadioButton();
            this.rbRxInputFiltered = new System.Windows.Forms.RadioButton();
            this.cbRxFormat = new System.Windows.Forms.ComboBox();
            this.tbRxFilter = new System.Windows.Forms.TextBox();
            this.btnFilterSelect = new System.Windows.Forms.Button();
            this.btnRxSelectFile = new System.Windows.Forms.Button();
            this.openFilterDialog = new System.Windows.Forms.OpenFileDialog();
            this.ttRxSelectFilter = new System.Windows.Forms.ToolTip(this.components);
            this.ttAnalyzeTab = new System.Windows.Forms.ToolTip(this.components);
            this.tbAnSignalFile = new System.Windows.Forms.TextBox();
            this.tbAnClip = new System.Windows.Forms.TextBox();
            this.tbAnFloor = new System.Windows.Forms.TextBox();
            this.tbAnFirstSample = new System.Windows.Forms.TextBox();
            this.tbAnLastSample = new System.Windows.Forms.TextBox();
            this.tbAnStop = new System.Windows.Forms.TextBox();
            this.tbAnLowPass = new System.Windows.Forms.TextBox();
            this.tbAnSampleRate = new System.Windows.Forms.TextBox();
            this.tbAnFirOrder = new System.Windows.Forms.TextBox();
            this.tbAnNumBits = new System.Windows.Forms.TextBox();
            this.tbAnAvgNarrowWidth = new System.Windows.Forms.TextBox();
            this.tbAnAvgWideWidth = new System.Windows.Forms.TextBox();
            this.tbAnWordContents = new System.Windows.Forms.TextBox();
            this.tbAnAvgRisePeriod = new System.Windows.Forms.TextBox();
            this.tbAnAvgFallPeriod = new System.Windows.Forms.TextBox();
            this.anOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabAnalyze = new System.Windows.Forms.TabPage();
            this.gbAnWordSummary = new System.Windows.Forms.GroupBox();
            this.btnAnMakeDevice = new System.Windows.Forms.Button();
            this.tbAnIWDelay = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.gbAnStartBits = new System.Windows.Forms.GroupBox();
            this.tbAnSBDelay = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.tbAnStartBitContents = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.gbAnDisplayAs = new System.Windows.Forms.GroupBox();
            this.rbAnSam = new System.Windows.Forms.RadioButton();
            this.rbAnMs = new System.Windows.Forms.RadioButton();
            this.rbAnHz = new System.Windows.Forms.RadioButton();
            this.tbAnFallStDev = new System.Windows.Forms.TextBox();
            this.tbAnRiseStDev = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.gbAnBitDetails = new System.Windows.Forms.GroupBox();
            this.btnAnCurBitMinus = new System.Windows.Forms.Button();
            this.btnAnCurBitPlus = new System.Windows.Forms.Button();
            this.tbAnCurBitFDev = new System.Windows.Forms.TextBox();
            this.tbAnCurBitRDev = new System.Windows.Forms.TextBox();
            this.tbAnCurBitFPeriod = new System.Windows.Forms.TextBox();
            this.tbAnCurBitRPeriod = new System.Windows.Forms.TextBox();
            this.tbAnCurBitPWDev = new System.Windows.Forms.TextBox();
            this.tbAnCurBitFTime = new System.Windows.Forms.TextBox();
            this.tbAnCurBitRTime = new System.Windows.Forms.TextBox();
            this.tbAnCurBitWidth = new System.Windows.Forms.TextBox();
            this.tbAnCurBitPos = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.cbAnBits = new System.Windows.Forms.ComboBox();
            this.lblAnBits = new System.Windows.Forms.Label();
            this.gbAnCurBin = new System.Windows.Forms.GroupBox();
            this.tbAnCurBinStDev = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.tbAnCurBinBits = new System.Windows.Forms.TextBox();
            this.tbAnCurBinCount = new System.Windows.Forms.TextBox();
            this.tbAnCurBinWidth = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.cbAnBins = new System.Windows.Forms.ComboBox();
            this.lblAnBins = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.btnAnAnalyze = new System.Windows.Forms.Button();
            this.gbAnFilter = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnAnFilter = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabDevice = new System.Windows.Forms.TabPage();
            this.tbDvRepetitions = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.gbDvDevice = new System.Windows.Forms.GroupBox();
            this.btnDvOpenDevice = new System.Windows.Forms.Button();
            this.btnDvNewDevice = new System.Windows.Forms.Button();
            this.btnDvSaveDevice = new System.Windows.Forms.Button();
            this.tbDvWordPad = new System.Windows.Forms.TextBox();
            this.cbDvSyncEdge = new System.Windows.Forms.ComboBox();
            this.tbDvDataBitPeriod = new System.Windows.Forms.TextBox();
            this.tbDvWidePW = new System.Windows.Forms.TextBox();
            this.tbDvNarrowPW = new System.Windows.Forms.TextBox();
            this.tbDvNumDataBits = new System.Windows.Forms.TextBox();
            this.tbDvSbPad = new System.Windows.Forms.TextBox();
            this.tbDvSbContents = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDvDeviceName = new System.Windows.Forms.TextBox();
            this.tbDvSampleRate = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.btnDvSaveSignal = new System.Windows.Forms.Button();
            this.btnDvMakeSignal = new System.Windows.Forms.Button();
            this.tbDvWordContents = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.tabTransmit = new System.Windows.Forms.TabPage();
            this.label76 = new System.Windows.Forms.Label();
            this.tbTxSdrStatus = new System.Windows.Forms.TextBox();
            this.gbTxOptions = new System.Windows.Forms.GroupBox();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.tbTxVGain2 = new System.Windows.Forms.TextBox();
            this.tbTxVGain1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.gbTxSignalOptions = new System.Windows.Forms.GroupBox();
            this.btnTxViewSignal = new System.Windows.Forms.Button();
            this.btnTxOpenFile = new System.Windows.Forms.Button();
            this.tbTxFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTxTransmit = new System.Windows.Forms.Button();
            this.tabReceive = new System.Windows.Forms.TabPage();
            this.rbRxOptions = new System.Windows.Forms.GroupBox();
            this.tbRxRecordTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.tbRxVGain2 = new System.Windows.Forms.TextBox();
            this.tbRxVGain1 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.tbRxSdrStatus = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRxReceive = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.rxSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.txOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dvOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dvSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabAnalyze.SuspendLayout();
            this.gbAnWordSummary.SuspendLayout();
            this.gbAnStartBits.SuspendLayout();
            this.gbAnDisplayAs.SuspendLayout();
            this.gbAnBitDetails.SuspendLayout();
            this.gbAnCurBin.SuspendLayout();
            this.gbAnFilter.SuspendLayout();
            this.tabDevice.SuspendLayout();
            this.gbDvDevice.SuspendLayout();
            this.tabTransmit.SuspendLayout();
            this.gbTxOptions.SuspendLayout();
            this.gbTxSignalOptions.SuspendLayout();
            this.tabReceive.SuspendLayout();
            this.rbRxOptions.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbRxFrequency
            // 
            this.tbRxFrequency.Enabled = false;
            this.tbRxFrequency.Location = new System.Drawing.Point(175, 51);
            this.tbRxFrequency.Name = "tbRxFrequency";
            this.tbRxFrequency.Size = new System.Drawing.Size(100, 20);
            this.tbRxFrequency.TabIndex = 2;
            this.ttSdr.SetToolTip(this.tbRxFrequency, "Set the SDR to the specified frequency");
            this.tbRxFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.tbFrequency_Validating);
            // 
            // tbRxSampleRate
            // 
            this.tbRxSampleRate.Enabled = false;
            this.tbRxSampleRate.Location = new System.Drawing.Point(175, 81);
            this.tbRxSampleRate.Name = "tbRxSampleRate";
            this.tbRxSampleRate.Size = new System.Drawing.Size(100, 20);
            this.tbRxSampleRate.TabIndex = 3;
            this.ttSdr.SetToolTip(this.tbRxSampleRate, "Set the SDR to the specified sample rate");
            this.tbRxSampleRate.Validating += new System.ComponentModel.CancelEventHandler(this.tbSampleRate_Validating);
            // 
            // tbRxBandwidth
            // 
            this.tbRxBandwidth.Enabled = false;
            this.tbRxBandwidth.Location = new System.Drawing.Point(175, 111);
            this.tbRxBandwidth.Name = "tbRxBandwidth";
            this.tbRxBandwidth.Size = new System.Drawing.Size(100, 20);
            this.tbRxBandwidth.TabIndex = 4;
            this.ttSdr.SetToolTip(this.tbRxBandwidth, "Set the SDR to the specified bandwidth");
            this.tbRxBandwidth.Validating += new System.ComponentModel.CancelEventHandler(this.tbBandwidth_Validating);
            // 
            // cbRxLnaGain
            // 
            this.cbRxLnaGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRxLnaGain.Enabled = false;
            this.cbRxLnaGain.Items.AddRange(new object[] {
            "Unknown",
            "0 dB - Bypass",
            "3 dB - Mid",
            "6 dB - Max"});
            this.cbRxLnaGain.Location = new System.Drawing.Point(175, 141);
            this.cbRxLnaGain.Name = "cbRxLnaGain";
            this.cbRxLnaGain.Size = new System.Drawing.Size(100, 21);
            this.cbRxLnaGain.TabIndex = 5;
            this.ttSdr.SetToolTip(this.cbRxLnaGain, "Set the SDR to the specified gain");
            this.cbRxLnaGain.Validating += new System.ComponentModel.CancelEventHandler(this.cbRxLnaGain_Validating);
            // 
            // tbRxNumBuffers
            // 
            this.tbRxNumBuffers.Enabled = false;
            this.tbRxNumBuffers.Location = new System.Drawing.Point(175, 231);
            this.tbRxNumBuffers.Name = "tbRxNumBuffers";
            this.tbRxNumBuffers.Size = new System.Drawing.Size(100, 20);
            this.tbRxNumBuffers.TabIndex = 8;
            this.ttSdr.SetToolTip(this.tbRxNumBuffers, "Allocate <n> sample buffers");
            this.tbRxNumBuffers.Validating += new System.ComponentModel.CancelEventHandler(this.tbNumBuffers_Validating);
            // 
            // tbRxBufferSize
            // 
            this.tbRxBufferSize.Enabled = false;
            this.tbRxBufferSize.Location = new System.Drawing.Point(175, 261);
            this.tbRxBufferSize.Name = "tbRxBufferSize";
            this.tbRxBufferSize.Size = new System.Drawing.Size(100, 20);
            this.tbRxBufferSize.TabIndex = 9;
            this.ttSdr.SetToolTip(this.tbRxBufferSize, "Allocate <n> samples in each sample buffer");
            this.tbRxBufferSize.Validating += new System.ComponentModel.CancelEventHandler(this.tbBufferSize_Validating);
            // 
            // tbRxNumXfers
            // 
            this.tbRxNumXfers.Enabled = false;
            this.tbRxNumXfers.Location = new System.Drawing.Point(175, 291);
            this.tbRxNumXfers.Name = "tbRxNumXfers";
            this.tbRxNumXfers.Size = new System.Drawing.Size(100, 20);
            this.tbRxNumXfers.TabIndex = 10;
            this.ttSdr.SetToolTip(this.tbRxNumXfers, "Utilize up to <n> simultaneous USB transfers");
            this.tbRxNumXfers.Validating += new System.ComponentModel.CancelEventHandler(this.tbNumXfers_Validating);
            // 
            // tbRxSyncTimeout
            // 
            this.tbRxSyncTimeout.Enabled = false;
            this.tbRxSyncTimeout.Location = new System.Drawing.Point(175, 321);
            this.tbRxSyncTimeout.Name = "tbRxSyncTimeout";
            this.tbRxSyncTimeout.Size = new System.Drawing.Size(100, 20);
            this.tbRxSyncTimeout.TabIndex = 12;
            this.ttSdr.SetToolTip(this.tbRxSyncTimeout, "Set sync function timeout to <n> milliseconds");
            this.tbRxSyncTimeout.Validating += new System.ComponentModel.CancelEventHandler(this.tbSyncTimeout_Validating);
            // 
            // cbRxSDR
            // 
            this.cbRxSDR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRxSDR.FormattingEnabled = true;
            this.cbRxSDR.Items.AddRange(new object[] {
            "BladeRF",
            "None"});
            this.cbRxSDR.Location = new System.Drawing.Point(175, 21);
            this.cbRxSDR.Name = "cbRxSDR";
            this.cbRxSDR.Size = new System.Drawing.Size(121, 21);
            this.cbRxSDR.TabIndex = 0;
            this.ttSdr.SetToolTip(this.cbRxSDR, "Select SDR to use");
            this.cbRxSDR.SelectedIndexChanged += new System.EventHandler(this.cbSdr_SelectedIndexChanged);
            // 
            // tbTxBandwidth
            // 
            this.tbTxBandwidth.Enabled = false;
            this.tbTxBandwidth.Location = new System.Drawing.Point(175, 111);
            this.tbTxBandwidth.Name = "tbTxBandwidth";
            this.tbTxBandwidth.Size = new System.Drawing.Size(100, 20);
            this.tbTxBandwidth.TabIndex = 30;
            this.ttSdr.SetToolTip(this.tbTxBandwidth, "Set the SDR to the specified bandwidth");
            this.tbTxBandwidth.Validating += new System.ComponentModel.CancelEventHandler(this.tbBandwidth_Validating);
            // 
            // tbTxSampleRate
            // 
            this.tbTxSampleRate.Enabled = false;
            this.tbTxSampleRate.Location = new System.Drawing.Point(175, 81);
            this.tbTxSampleRate.Name = "tbTxSampleRate";
            this.tbTxSampleRate.Size = new System.Drawing.Size(100, 20);
            this.tbTxSampleRate.TabIndex = 28;
            this.ttSdr.SetToolTip(this.tbTxSampleRate, "Set the SDR to the specified sample rate");
            this.tbTxSampleRate.Validating += new System.ComponentModel.CancelEventHandler(this.tbSampleRate_Validating);
            // 
            // tbTxFrequency
            // 
            this.tbTxFrequency.Enabled = false;
            this.tbTxFrequency.Location = new System.Drawing.Point(175, 51);
            this.tbTxFrequency.Name = "tbTxFrequency";
            this.tbTxFrequency.Size = new System.Drawing.Size(100, 20);
            this.tbTxFrequency.TabIndex = 26;
            this.ttSdr.SetToolTip(this.tbTxFrequency, "Set the SDR to the specified frequency");
            this.tbTxFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.tbFrequency_Validating);
            // 
            // cbTxSDR
            // 
            this.cbTxSDR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTxSDR.FormattingEnabled = true;
            this.cbTxSDR.Items.AddRange(new object[] {
            "BladeRF",
            "None"});
            this.cbTxSDR.Location = new System.Drawing.Point(175, 21);
            this.cbTxSDR.Name = "cbTxSDR";
            this.cbTxSDR.Size = new System.Drawing.Size(121, 21);
            this.cbTxSDR.TabIndex = 24;
            this.ttSdr.SetToolTip(this.cbTxSDR, "Select SDR to use");
            this.cbTxSDR.SelectedIndexChanged += new System.EventHandler(this.cbSdr_SelectedIndexChanged);
            // 
            // tbTxSyncTimeout
            // 
            this.tbTxSyncTimeout.Enabled = false;
            this.tbTxSyncTimeout.Location = new System.Drawing.Point(175, 291);
            this.tbTxSyncTimeout.Name = "tbTxSyncTimeout";
            this.tbTxSyncTimeout.Size = new System.Drawing.Size(100, 20);
            this.tbTxSyncTimeout.TabIndex = 58;
            this.ttSdr.SetToolTip(this.tbTxSyncTimeout, "Set sync function timeout to <n> milliseconds");
            // 
            // tbTxNumXfers
            // 
            this.tbTxNumXfers.Enabled = false;
            this.tbTxNumXfers.Location = new System.Drawing.Point(175, 261);
            this.tbTxNumXfers.Name = "tbTxNumXfers";
            this.tbTxNumXfers.Size = new System.Drawing.Size(100, 20);
            this.tbTxNumXfers.TabIndex = 57;
            this.ttSdr.SetToolTip(this.tbTxNumXfers, "Utilize up to <n> simultaneous USB transfers");
            // 
            // tbTxBufferSize
            // 
            this.tbTxBufferSize.Enabled = false;
            this.tbTxBufferSize.Location = new System.Drawing.Point(175, 231);
            this.tbTxBufferSize.Name = "tbTxBufferSize";
            this.tbTxBufferSize.Size = new System.Drawing.Size(100, 20);
            this.tbTxBufferSize.TabIndex = 55;
            this.ttSdr.SetToolTip(this.tbTxBufferSize, "Allocate <n> samples in each sample buffer");
            // 
            // tbTxNumBuffers
            // 
            this.tbTxNumBuffers.Enabled = false;
            this.tbTxNumBuffers.Location = new System.Drawing.Point(175, 201);
            this.tbTxNumBuffers.Name = "tbTxNumBuffers";
            this.tbTxNumBuffers.Size = new System.Drawing.Size(100, 20);
            this.tbTxNumBuffers.TabIndex = 54;
            this.ttSdr.SetToolTip(this.tbTxNumBuffers, "Allocate <n> sample buffers");
            // 
            // tbTxDelay
            // 
            this.tbTxDelay.Location = new System.Drawing.Point(134, 51);
            this.tbTxDelay.Name = "tbTxDelay";
            this.tbTxDelay.Size = new System.Drawing.Size(100, 20);
            this.tbTxDelay.TabIndex = 1;
            this.tbTxDelay.Text = "0";
            this.ttTxDelay.SetToolTip(this.tbTxDelay, "Microseconds to delay before transmissions");
            this.tbTxDelay.Validating += new System.ComponentModel.CancelEventHandler(this.tbTxDelay_Validating);
            // 
            // tbTxCount
            // 
            this.tbTxCount.Location = new System.Drawing.Point(134, 78);
            this.tbTxCount.Name = "tbTxCount";
            this.tbTxCount.Size = new System.Drawing.Size(100, 20);
            this.tbTxCount.TabIndex = 2;
            this.tbTxCount.Text = "1";
            this.ttTxCount.SetToolTip(this.tbTxCount, "Number of times to send transmission");
            this.tbTxCount.Validating += new System.ComponentModel.CancelEventHandler(this.tbTxCount_Validating);
            // 
            // btnAnSelectFile
            // 
            this.btnAnSelectFile.Location = new System.Drawing.Point(314, 32);
            this.btnAnSelectFile.Name = "btnAnSelectFile";
            this.btnAnSelectFile.Size = new System.Drawing.Size(85, 23);
            this.btnAnSelectFile.TabIndex = 23;
            this.btnAnSelectFile.Text = "Select File";
            this.ttRxTab.SetToolTip(this.btnAnSelectFile, "Select existing file to overwrite.");
            this.btnAnSelectFile.UseVisualStyleBackColor = true;
            this.btnAnSelectFile.Click += new System.EventHandler(this.btnAnSelectFile_Click);
            // 
            // tbRxFileName
            // 
            this.tbRxFileName.Location = new System.Drawing.Point(472, 61);
            this.tbRxFileName.Name = "tbRxFileName";
            this.tbRxFileName.ReadOnly = true;
            this.tbRxFileName.Size = new System.Drawing.Size(100, 20);
            this.tbRxFileName.TabIndex = 14;
            this.ttRxTab.SetToolTip(this.tbRxFileName, resources.GetString("tbRxFileName.ToolTip"));
            // 
            // rbRxInputRaw
            // 
            this.rbRxInputRaw.AutoSize = true;
            this.rbRxInputRaw.Checked = true;
            this.rbRxInputRaw.Enabled = false;
            this.rbRxInputRaw.Location = new System.Drawing.Point(472, 122);
            this.rbRxInputRaw.Name = "rbRxInputRaw";
            this.rbRxInputRaw.Size = new System.Drawing.Size(47, 17);
            this.rbRxInputRaw.TabIndex = 15;
            this.rbRxInputRaw.TabStop = true;
            this.rbRxInputRaw.Text = "Raw";
            this.ttRxTab.SetToolTip(this.rbRxInputRaw, "Specifies that should record raw (unfiltered) input should be recorded");
            this.rbRxInputRaw.UseVisualStyleBackColor = true;
            // 
            // rbRxInputFiltered
            // 
            this.rbRxInputFiltered.AutoSize = true;
            this.rbRxInputFiltered.Enabled = false;
            this.rbRxInputFiltered.Location = new System.Drawing.Point(546, 122);
            this.rbRxInputFiltered.Name = "rbRxInputFiltered";
            this.rbRxInputFiltered.Size = new System.Drawing.Size(59, 17);
            this.rbRxInputFiltered.TabIndex = 16;
            this.rbRxInputFiltered.Text = "Filtered";
            this.ttRxTab.SetToolTip(this.rbRxInputFiltered, "Specifies that should record filtered (not raw) input should be recorded");
            this.rbRxInputFiltered.UseVisualStyleBackColor = true;
            // 
            // cbRxFormat
            // 
            this.cbRxFormat.Enabled = false;
            this.cbRxFormat.FormattingEnabled = true;
            this.cbRxFormat.Items.AddRange(new object[] {
            "Binary",
            "CSV"});
            this.cbRxFormat.Location = new System.Drawing.Point(472, 91);
            this.cbRxFormat.Name = "cbRxFormat";
            this.cbRxFormat.Size = new System.Drawing.Size(121, 21);
            this.cbRxFormat.TabIndex = 18;
            this.cbRxFormat.Text = "Binary";
            this.ttRxTab.SetToolTip(this.cbRxFormat, "Configures how RX\'d messages are formatted");
            // 
            // tbRxFilter
            // 
            this.tbRxFilter.Location = new System.Drawing.Point(472, 151);
            this.tbRxFilter.Name = "tbRxFilter";
            this.tbRxFilter.ReadOnly = true;
            this.tbRxFilter.Size = new System.Drawing.Size(121, 20);
            this.tbRxFilter.TabIndex = 17;
            this.ttRxTab.SetToolTip(this.tbRxFilter, "Use the specified filter for recording samples");
            // 
            // btnFilterSelect
            // 
            this.btnFilterSelect.Enabled = false;
            this.btnFilterSelect.Location = new System.Drawing.Point(626, 149);
            this.btnFilterSelect.Name = "btnFilterSelect";
            this.btnFilterSelect.Size = new System.Drawing.Size(85, 23);
            this.btnFilterSelect.TabIndex = 34;
            this.btnFilterSelect.Text = "Select Filter";
            this.ttRxTab.SetToolTip(this.btnFilterSelect, "Select JSON filter file to filter signal with.");
            this.btnFilterSelect.UseVisualStyleBackColor = true;
            // 
            // btnRxSelectFile
            // 
            this.btnRxSelectFile.Location = new System.Drawing.Point(626, 59);
            this.btnRxSelectFile.Name = "btnRxSelectFile";
            this.btnRxSelectFile.Size = new System.Drawing.Size(85, 23);
            this.btnRxSelectFile.TabIndex = 41;
            this.btnRxSelectFile.Text = "Select File";
            this.ttRxTab.SetToolTip(this.btnRxSelectFile, "Select existing file to overwrite.");
            this.btnRxSelectFile.UseVisualStyleBackColor = true;
            this.btnRxSelectFile.Click += new System.EventHandler(this.btnRxSelectFile_Click);
            // 
            // tbAnSignalFile
            // 
            this.tbAnSignalFile.Location = new System.Drawing.Point(168, 35);
            this.tbAnSignalFile.Name = "tbAnSignalFile";
            this.tbAnSignalFile.ReadOnly = true;
            this.tbAnSignalFile.Size = new System.Drawing.Size(100, 20);
            this.tbAnSignalFile.TabIndex = 1;
            this.ttAnalyzeTab.SetToolTip(this.tbAnSignalFile, "File that contains RAW signal data to be analyzed");
            // 
            // tbAnClip
            // 
            this.tbAnClip.Location = new System.Drawing.Point(169, 233);
            this.tbAnClip.Name = "tbAnClip";
            this.tbAnClip.Size = new System.Drawing.Size(100, 20);
            this.tbAnClip.TabIndex = 3;
            this.ttAnalyzeTab.SetToolTip(this.tbAnClip, "Use this value to clip the top of the signal if it is noisy.");
            // 
            // tbAnFloor
            // 
            this.tbAnFloor.Location = new System.Drawing.Point(169, 263);
            this.tbAnFloor.Name = "tbAnFloor";
            this.tbAnFloor.Size = new System.Drawing.Size(100, 20);
            this.tbAnFloor.TabIndex = 4;
            this.ttAnalyzeTab.SetToolTip(this.tbAnFloor, "Use this to cut the bottom off the signal if it is noisy");
            // 
            // tbAnFirstSample
            // 
            this.tbAnFirstSample.Location = new System.Drawing.Point(169, 293);
            this.tbAnFirstSample.Name = "tbAnFirstSample";
            this.tbAnFirstSample.Size = new System.Drawing.Size(100, 20);
            this.tbAnFirstSample.TabIndex = 5;
            this.ttAnalyzeTab.SetToolTip(this.tbAnFirstSample, "First sample in the region you want to analyze");
            // 
            // tbAnLastSample
            // 
            this.tbAnLastSample.Location = new System.Drawing.Point(169, 323);
            this.tbAnLastSample.Name = "tbAnLastSample";
            this.tbAnLastSample.Size = new System.Drawing.Size(100, 20);
            this.tbAnLastSample.TabIndex = 6;
            this.ttAnalyzeTab.SetToolTip(this.tbAnLastSample, "Last sample in the region you want to analyze");
            // 
            // tbAnStop
            // 
            this.tbAnStop.Location = new System.Drawing.Point(128, 116);
            this.tbAnStop.Name = "tbAnStop";
            this.tbAnStop.Size = new System.Drawing.Size(100, 20);
            this.tbAnStop.TabIndex = 3;
            this.ttAnalyzeTab.SetToolTip(this.tbAnStop, "Highest frequency to pass through filter");
            // 
            // tbAnLowPass
            // 
            this.tbAnLowPass.Location = new System.Drawing.Point(128, 86);
            this.tbAnLowPass.Name = "tbAnLowPass";
            this.tbAnLowPass.Size = new System.Drawing.Size(100, 20);
            this.tbAnLowPass.TabIndex = 2;
            this.ttAnalyzeTab.SetToolTip(this.tbAnLowPass, "Lowest frequency to pass filter unattenuated ");
            // 
            // tbAnSampleRate
            // 
            this.tbAnSampleRate.Location = new System.Drawing.Point(128, 26);
            this.tbAnSampleRate.Name = "tbAnSampleRate";
            this.tbAnSampleRate.Size = new System.Drawing.Size(100, 20);
            this.tbAnSampleRate.TabIndex = 0;
            this.ttAnalyzeTab.SetToolTip(this.tbAnSampleRate, "Sample Rate that signal was recorded with");
            this.tbAnSampleRate.Validating += new System.ComponentModel.CancelEventHandler(this.SampleRate_Validating);
            // 
            // tbAnFirOrder
            // 
            this.tbAnFirOrder.Location = new System.Drawing.Point(128, 56);
            this.tbAnFirOrder.Name = "tbAnFirOrder";
            this.tbAnFirOrder.Size = new System.Drawing.Size(100, 20);
            this.tbAnFirOrder.TabIndex = 1;
            this.ttAnalyzeTab.SetToolTip(this.tbAnFirOrder, "Number of taps on FIR filter");
            // 
            // tbAnNumBits
            // 
            this.tbAnNumBits.Location = new System.Drawing.Point(150, 55);
            this.tbAnNumBits.Name = "tbAnNumBits";
            this.tbAnNumBits.ReadOnly = true;
            this.tbAnNumBits.Size = new System.Drawing.Size(52, 20);
            this.tbAnNumBits.TabIndex = 37;
            this.ttAnalyzeTab.SetToolTip(this.tbAnNumBits, "Number of bits per word");
            // 
            // tbAnAvgNarrowWidth
            // 
            this.tbAnAvgNarrowWidth.Location = new System.Drawing.Point(150, 84);
            this.tbAnAvgNarrowWidth.Name = "tbAnAvgNarrowWidth";
            this.tbAnAvgNarrowWidth.ReadOnly = true;
            this.tbAnAvgNarrowWidth.Size = new System.Drawing.Size(52, 20);
            this.tbAnAvgNarrowWidth.TabIndex = 38;
            this.ttAnalyzeTab.SetToolTip(this.tbAnAvgNarrowWidth, "Average width of narrow pulses");
            // 
            // tbAnAvgWideWidth
            // 
            this.tbAnAvgWideWidth.Location = new System.Drawing.Point(150, 115);
            this.tbAnAvgWideWidth.Name = "tbAnAvgWideWidth";
            this.tbAnAvgWideWidth.ReadOnly = true;
            this.tbAnAvgWideWidth.Size = new System.Drawing.Size(52, 20);
            this.tbAnAvgWideWidth.TabIndex = 39;
            this.ttAnalyzeTab.SetToolTip(this.tbAnAvgWideWidth, "Average width of wide pulses");
            // 
            // tbAnWordContents
            // 
            this.tbAnWordContents.Location = new System.Drawing.Point(150, 145);
            this.tbAnWordContents.Name = "tbAnWordContents";
            this.tbAnWordContents.ReadOnly = true;
            this.tbAnWordContents.Size = new System.Drawing.Size(191, 20);
            this.tbAnWordContents.TabIndex = 40;
            this.ttAnalyzeTab.SetToolTip(this.tbAnWordContents, "Binary contents of word");
            // 
            // tbAnAvgRisePeriod
            // 
            this.tbAnAvgRisePeriod.Location = new System.Drawing.Point(150, 175);
            this.tbAnAvgRisePeriod.Name = "tbAnAvgRisePeriod";
            this.tbAnAvgRisePeriod.ReadOnly = true;
            this.tbAnAvgRisePeriod.Size = new System.Drawing.Size(52, 20);
            this.tbAnAvgRisePeriod.TabIndex = 41;
            this.ttAnalyzeTab.SetToolTip(this.tbAnAvgRisePeriod, "Average period between rising edge of bits");
            // 
            // tbAnAvgFallPeriod
            // 
            this.tbAnAvgFallPeriod.Location = new System.Drawing.Point(150, 205);
            this.tbAnAvgFallPeriod.Name = "tbAnAvgFallPeriod";
            this.tbAnAvgFallPeriod.ReadOnly = true;
            this.tbAnAvgFallPeriod.Size = new System.Drawing.Size(52, 20);
            this.tbAnAvgFallPeriod.TabIndex = 42;
            this.ttAnalyzeTab.SetToolTip(this.tbAnAvgFallPeriod, "Period between falling edges of bits");
            // 
            // tabAnalyze
            // 
            this.tabAnalyze.Controls.Add(this.gbAnWordSummary);
            this.tabAnalyze.Controls.Add(this.gbAnBitDetails);
            this.tabAnalyze.Controls.Add(this.gbAnCurBin);
            this.tabAnalyze.Controls.Add(this.btnAnAnalyze);
            this.tabAnalyze.Controls.Add(this.gbAnFilter);
            this.tabAnalyze.Controls.Add(this.btnAnFilter);
            this.tabAnalyze.Controls.Add(this.tbAnLastSample);
            this.tabAnalyze.Controls.Add(this.tbAnFirstSample);
            this.tabAnalyze.Controls.Add(this.tbAnFloor);
            this.tabAnalyze.Controls.Add(this.tbAnClip);
            this.tabAnalyze.Controls.Add(this.tbAnSignalFile);
            this.tabAnalyze.Controls.Add(this.btnAnSelectFile);
            this.tabAnalyze.Controls.Add(this.label29);
            this.tabAnalyze.Controls.Add(this.label28);
            this.tabAnalyze.Controls.Add(this.label27);
            this.tabAnalyze.Controls.Add(this.label26);
            this.tabAnalyze.Controls.Add(this.label20);
            this.tabAnalyze.Location = new System.Drawing.Point(4, 22);
            this.tabAnalyze.Name = "tabAnalyze";
            this.tabAnalyze.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnalyze.Size = new System.Drawing.Size(970, 469);
            this.tabAnalyze.TabIndex = 5;
            this.tabAnalyze.Text = "Analyze";
            this.tabAnalyze.UseVisualStyleBackColor = true;
            // 
            // gbAnWordSummary
            // 
            this.gbAnWordSummary.Controls.Add(this.btnAnMakeDevice);
            this.gbAnWordSummary.Controls.Add(this.tbAnIWDelay);
            this.gbAnWordSummary.Controls.Add(this.label57);
            this.gbAnWordSummary.Controls.Add(this.gbAnStartBits);
            this.gbAnWordSummary.Controls.Add(this.gbAnDisplayAs);
            this.gbAnWordSummary.Controls.Add(this.tbAnFallStDev);
            this.gbAnWordSummary.Controls.Add(this.tbAnRiseStDev);
            this.gbAnWordSummary.Controls.Add(this.label39);
            this.gbAnWordSummary.Controls.Add(this.label38);
            this.gbAnWordSummary.Controls.Add(this.tbAnAvgFallPeriod);
            this.gbAnWordSummary.Controls.Add(this.tbAnAvgRisePeriod);
            this.gbAnWordSummary.Controls.Add(this.tbAnWordContents);
            this.gbAnWordSummary.Controls.Add(this.tbAnAvgWideWidth);
            this.gbAnWordSummary.Controls.Add(this.tbAnAvgNarrowWidth);
            this.gbAnWordSummary.Controls.Add(this.tbAnNumBits);
            this.gbAnWordSummary.Controls.Add(this.label37);
            this.gbAnWordSummary.Controls.Add(this.label36);
            this.gbAnWordSummary.Controls.Add(this.label35);
            this.gbAnWordSummary.Controls.Add(this.label34);
            this.gbAnWordSummary.Controls.Add(this.label33);
            this.gbAnWordSummary.Controls.Add(this.label32);
            this.gbAnWordSummary.Location = new System.Drawing.Point(472, 6);
            this.gbAnWordSummary.Name = "gbAnWordSummary";
            this.gbAnWordSummary.Size = new System.Drawing.Size(490, 245);
            this.gbAnWordSummary.TabIndex = 77;
            this.gbAnWordSummary.TabStop = false;
            this.gbAnWordSummary.Text = "Word Summary";
            // 
            // btnAnMakeDevice
            // 
            this.btnAnMakeDevice.Enabled = false;
            this.btnAnMakeDevice.Location = new System.Drawing.Point(373, 203);
            this.btnAnMakeDevice.Name = "btnAnMakeDevice";
            this.btnAnMakeDevice.Size = new System.Drawing.Size(105, 23);
            this.btnAnMakeDevice.TabIndex = 64;
            this.btnAnMakeDevice.Text = "Make OOK Device";
            this.btnAnMakeDevice.UseVisualStyleBackColor = true;
            this.btnAnMakeDevice.Click += new System.EventHandler(this.btnAnMakeDevice_Click);
            // 
            // tbAnIWDelay
            // 
            this.tbAnIWDelay.Location = new System.Drawing.Point(325, 115);
            this.tbAnIWDelay.Name = "tbAnIWDelay";
            this.tbAnIWDelay.ReadOnly = true;
            this.tbAnIWDelay.Size = new System.Drawing.Size(51, 20);
            this.tbAnIWDelay.TabIndex = 63;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(227, 118);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(84, 13);
            this.label57.TabIndex = 62;
            this.label57.Text = "Inter-word Delay";
            // 
            // gbAnStartBits
            // 
            this.gbAnStartBits.Controls.Add(this.tbAnSBDelay);
            this.gbAnStartBits.Controls.Add(this.label56);
            this.gbAnStartBits.Controls.Add(this.tbAnStartBitContents);
            this.gbAnStartBits.Controls.Add(this.label18);
            this.gbAnStartBits.Location = new System.Drawing.Point(32, 15);
            this.gbAnStartBits.Name = "gbAnStartBits";
            this.gbAnStartBits.Size = new System.Drawing.Size(257, 40);
            this.gbAnStartBits.TabIndex = 61;
            this.gbAnStartBits.TabStop = false;
            this.gbAnStartBits.Text = "Start Bits";
            // 
            // tbAnSBDelay
            // 
            this.tbAnSBDelay.Location = new System.Drawing.Point(180, 14);
            this.tbAnSBDelay.Name = "tbAnSBDelay";
            this.tbAnSBDelay.ReadOnly = true;
            this.tbAnSBDelay.Size = new System.Drawing.Size(59, 20);
            this.tbAnSBDelay.TabIndex = 60;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(140, 17);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(34, 13);
            this.label56.TabIndex = 59;
            this.label56.Text = "Delay";
            // 
            // tbAnStartBitContents
            // 
            this.tbAnStartBitContents.Location = new System.Drawing.Point(55, 14);
            this.tbAnStartBitContents.Name = "tbAnStartBitContents";
            this.tbAnStartBitContents.ReadOnly = true;
            this.tbAnStartBitContents.Size = new System.Drawing.Size(55, 20);
            this.tbAnStartBitContents.TabIndex = 56;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(0, 17);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 13);
            this.label18.TabIndex = 55;
            this.label18.Text = "Contents";
            // 
            // gbAnDisplayAs
            // 
            this.gbAnDisplayAs.Controls.Add(this.rbAnSam);
            this.gbAnDisplayAs.Controls.Add(this.rbAnMs);
            this.gbAnDisplayAs.Controls.Add(this.rbAnHz);
            this.gbAnDisplayAs.Location = new System.Drawing.Point(305, 15);
            this.gbAnDisplayAs.Name = "gbAnDisplayAs";
            this.gbAnDisplayAs.Size = new System.Drawing.Size(173, 40);
            this.gbAnDisplayAs.TabIndex = 54;
            this.gbAnDisplayAs.TabStop = false;
            this.gbAnDisplayAs.Text = "Display As:";
            // 
            // rbAnSam
            // 
            this.rbAnSam.AutoSize = true;
            this.rbAnSam.Checked = true;
            this.rbAnSam.Location = new System.Drawing.Point(6, 17);
            this.rbAnSam.Name = "rbAnSam";
            this.rbAnSam.Size = new System.Drawing.Size(65, 17);
            this.rbAnSam.TabIndex = 51;
            this.rbAnSam.TabStop = true;
            this.rbAnSam.Text = "Samples";
            this.rbAnSam.UseVisualStyleBackColor = true;
            this.rbAnSam.CheckedChanged += new System.EventHandler(this.rbAnSam_CheckedChanged);
            // 
            // rbAnMs
            // 
            this.rbAnMs.AutoSize = true;
            this.rbAnMs.Location = new System.Drawing.Point(125, 17);
            this.rbAnMs.Name = "rbAnMs";
            this.rbAnMs.Size = new System.Drawing.Size(38, 17);
            this.rbAnMs.TabIndex = 53;
            this.rbAnMs.Text = "ms";
            this.rbAnMs.UseVisualStyleBackColor = true;
            this.rbAnMs.CheckedChanged += new System.EventHandler(this.rbAnMs_CheckedChanged);
            // 
            // rbAnHz
            // 
            this.rbAnHz.AutoSize = true;
            this.rbAnHz.Location = new System.Drawing.Point(79, 17);
            this.rbAnHz.Name = "rbAnHz";
            this.rbAnHz.Size = new System.Drawing.Size(38, 17);
            this.rbAnHz.TabIndex = 52;
            this.rbAnHz.Text = "Hz";
            this.rbAnHz.UseVisualStyleBackColor = true;
            this.rbAnHz.CheckedChanged += new System.EventHandler(this.rbAnHz_CheckedChanged);
            // 
            // tbAnFallStDev
            // 
            this.tbAnFallStDev.Location = new System.Drawing.Point(304, 205);
            this.tbAnFallStDev.Name = "tbAnFallStDev";
            this.tbAnFallStDev.ReadOnly = true;
            this.tbAnFallStDev.Size = new System.Drawing.Size(38, 20);
            this.tbAnFallStDev.TabIndex = 50;
            // 
            // tbAnRiseStDev
            // 
            this.tbAnRiseStDev.Location = new System.Drawing.Point(303, 175);
            this.tbAnRiseStDev.Name = "tbAnRiseStDev";
            this.tbAnRiseStDev.ReadOnly = true;
            this.tbAnRiseStDev.Size = new System.Drawing.Size(38, 20);
            this.tbAnRiseStDev.TabIndex = 49;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(227, 208);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(65, 13);
            this.label39.TabIndex = 48;
            this.label39.Text = "Fall Std Dev";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(227, 178);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(70, 13);
            this.label38.TabIndex = 47;
            this.label38.Text = "Rise Std Dev";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(32, 208);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(78, 13);
            this.label37.TabIndex = 35;
            this.label37.Text = "Fall Avg Period";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(32, 178);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(83, 13);
            this.label36.TabIndex = 34;
            this.label36.Text = "Rise Avg Period";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(32, 148);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(78, 13);
            this.label35.TabIndex = 33;
            this.label35.Text = "Word Contents";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(32, 118);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(103, 13);
            this.label34.TabIndex = 32;
            this.label34.Text = "Avg. Wide Width (1)";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(32, 88);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(112, 13);
            this.label33.TabIndex = 31;
            this.label33.Text = "Avg. Narrow Width (0)";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(32, 58);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(102, 13);
            this.label32.TabIndex = 30;
            this.label32.Text = "Number of Data Bits";
            // 
            // gbAnBitDetails
            // 
            this.gbAnBitDetails.Controls.Add(this.btnAnCurBitMinus);
            this.gbAnBitDetails.Controls.Add(this.btnAnCurBitPlus);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitFDev);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitRDev);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitFPeriod);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitRPeriod);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitPWDev);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitFTime);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitRTime);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitWidth);
            this.gbAnBitDetails.Controls.Add(this.tbAnCurBitPos);
            this.gbAnBitDetails.Controls.Add(this.label52);
            this.gbAnBitDetails.Controls.Add(this.label50);
            this.gbAnBitDetails.Controls.Add(this.label49);
            this.gbAnBitDetails.Controls.Add(this.label48);
            this.gbAnBitDetails.Controls.Add(this.label47);
            this.gbAnBitDetails.Controls.Add(this.label46);
            this.gbAnBitDetails.Controls.Add(this.label45);
            this.gbAnBitDetails.Controls.Add(this.label44);
            this.gbAnBitDetails.Controls.Add(this.label43);
            this.gbAnBitDetails.Controls.Add(this.cbAnBits);
            this.gbAnBitDetails.Controls.Add(this.lblAnBits);
            this.gbAnBitDetails.Location = new System.Drawing.Point(571, 260);
            this.gbAnBitDetails.Name = "gbAnBitDetails";
            this.gbAnBitDetails.Size = new System.Drawing.Size(391, 190);
            this.gbAnBitDetails.TabIndex = 76;
            this.gbAnBitDetails.TabStop = false;
            this.gbAnBitDetails.Text = "Bit Details";
            // 
            // btnAnCurBitMinus
            // 
            this.btnAnCurBitMinus.Enabled = false;
            this.btnAnCurBitMinus.Location = new System.Drawing.Point(227, 25);
            this.btnAnCurBitMinus.Name = "btnAnCurBitMinus";
            this.btnAnCurBitMinus.Size = new System.Drawing.Size(20, 25);
            this.btnAnCurBitMinus.TabIndex = 77;
            this.btnAnCurBitMinus.Text = "-";
            this.btnAnCurBitMinus.UseVisualStyleBackColor = true;
            this.btnAnCurBitMinus.Click += new System.EventHandler(this.btnAnCurBitMinus_Click);
            // 
            // btnAnCurBitPlus
            // 
            this.btnAnCurBitPlus.Enabled = false;
            this.btnAnCurBitPlus.Location = new System.Drawing.Point(192, 25);
            this.btnAnCurBitPlus.Name = "btnAnCurBitPlus";
            this.btnAnCurBitPlus.Size = new System.Drawing.Size(20, 25);
            this.btnAnCurBitPlus.TabIndex = 76;
            this.btnAnCurBitPlus.Text = "+";
            this.btnAnCurBitPlus.UseVisualStyleBackColor = true;
            this.btnAnCurBitPlus.Click += new System.EventHandler(this.btnAnCurBitPlus_Click);
            // 
            // tbAnCurBitFDev
            // 
            this.tbAnCurBitFDev.Location = new System.Drawing.Point(316, 150);
            this.tbAnCurBitFDev.Name = "tbAnCurBitFDev";
            this.tbAnCurBitFDev.ReadOnly = true;
            this.tbAnCurBitFDev.Size = new System.Drawing.Size(48, 20);
            this.tbAnCurBitFDev.TabIndex = 75;
            // 
            // tbAnCurBitRDev
            // 
            this.tbAnCurBitRDev.Location = new System.Drawing.Point(316, 120);
            this.tbAnCurBitRDev.Name = "tbAnCurBitRDev";
            this.tbAnCurBitRDev.ReadOnly = true;
            this.tbAnCurBitRDev.Size = new System.Drawing.Size(48, 20);
            this.tbAnCurBitRDev.TabIndex = 74;
            // 
            // tbAnCurBitFPeriod
            // 
            this.tbAnCurBitFPeriod.Location = new System.Drawing.Point(206, 150);
            this.tbAnCurBitFPeriod.Name = "tbAnCurBitFPeriod";
            this.tbAnCurBitFPeriod.ReadOnly = true;
            this.tbAnCurBitFPeriod.Size = new System.Drawing.Size(34, 20);
            this.tbAnCurBitFPeriod.TabIndex = 73;
            // 
            // tbAnCurBitRPeriod
            // 
            this.tbAnCurBitRPeriod.Location = new System.Drawing.Point(206, 120);
            this.tbAnCurBitRPeriod.Name = "tbAnCurBitRPeriod";
            this.tbAnCurBitRPeriod.ReadOnly = true;
            this.tbAnCurBitRPeriod.Size = new System.Drawing.Size(34, 20);
            this.tbAnCurBitRPeriod.TabIndex = 72;
            // 
            // tbAnCurBitPWDev
            // 
            this.tbAnCurBitPWDev.Location = new System.Drawing.Point(316, 87);
            this.tbAnCurBitPWDev.Name = "tbAnCurBitPWDev";
            this.tbAnCurBitPWDev.ReadOnly = true;
            this.tbAnCurBitPWDev.Size = new System.Drawing.Size(48, 20);
            this.tbAnCurBitPWDev.TabIndex = 71;
            // 
            // tbAnCurBitFTime
            // 
            this.tbAnCurBitFTime.Location = new System.Drawing.Point(87, 150);
            this.tbAnCurBitFTime.Name = "tbAnCurBitFTime";
            this.tbAnCurBitFTime.ReadOnly = true;
            this.tbAnCurBitFTime.Size = new System.Drawing.Size(45, 20);
            this.tbAnCurBitFTime.TabIndex = 70;
            // 
            // tbAnCurBitRTime
            // 
            this.tbAnCurBitRTime.Location = new System.Drawing.Point(89, 120);
            this.tbAnCurBitRTime.Name = "tbAnCurBitRTime";
            this.tbAnCurBitRTime.ReadOnly = true;
            this.tbAnCurBitRTime.Size = new System.Drawing.Size(43, 20);
            this.tbAnCurBitRTime.TabIndex = 69;
            // 
            // tbAnCurBitWidth
            // 
            this.tbAnCurBitWidth.Location = new System.Drawing.Point(89, 89);
            this.tbAnCurBitWidth.Name = "tbAnCurBitWidth";
            this.tbAnCurBitWidth.ReadOnly = true;
            this.tbAnCurBitWidth.Size = new System.Drawing.Size(43, 20);
            this.tbAnCurBitWidth.TabIndex = 68;
            // 
            // tbAnCurBitPos
            // 
            this.tbAnCurBitPos.Location = new System.Drawing.Point(89, 63);
            this.tbAnCurBitPos.Name = "tbAnCurBitPos";
            this.tbAnCurBitPos.ReadOnly = true;
            this.tbAnCurBitPos.Size = new System.Drawing.Size(32, 20);
            this.tbAnCurBitPos.TabIndex = 67;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(245, 93);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(51, 13);
            this.label52.TabIndex = 66;
            this.label52.Text = "PW Dev.";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(245, 153);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(49, 13);
            this.label50.TabIndex = 65;
            this.label50.Text = "Fall Dev.";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(245, 123);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(54, 13);
            this.label49.TabIndex = 64;
            this.label49.Text = "Rise Dev.";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(138, 153);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(56, 13);
            this.label48.TabIndex = 63;
            this.label48.Text = "Fall Period";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(138, 123);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(61, 13);
            this.label47.TabIndex = 62;
            this.label47.Text = "Rise Period";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 153);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(49, 13);
            this.label46.TabIndex = 61;
            this.label46.Text = "Fall Time";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 123);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(54, 13);
            this.label45.TabIndex = 60;
            this.label45.Text = "Rise Time";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 93);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(35, 13);
            this.label44.TabIndex = 59;
            this.label44.Text = "Width";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 63);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(44, 13);
            this.label43.TabIndex = 58;
            this.label43.Text = "Position";
            // 
            // cbAnBits
            // 
            this.cbAnBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnBits.Enabled = false;
            this.cbAnBits.FormattingEnabled = true;
            this.cbAnBits.Location = new System.Drawing.Point(89, 29);
            this.cbAnBits.Name = "cbAnBits";
            this.cbAnBits.Size = new System.Drawing.Size(83, 21);
            this.cbAnBits.TabIndex = 21;
            this.cbAnBits.SelectedIndexChanged += new System.EventHandler(this.cbAnBits_SelectedIndexChanged);
            // 
            // lblAnBits
            // 
            this.lblAnBits.AutoSize = true;
            this.lblAnBits.Location = new System.Drawing.Point(6, 32);
            this.lblAnBits.Name = "lblAnBits";
            this.lblAnBits.Size = new System.Drawing.Size(65, 13);
            this.lblAnBits.TabIndex = 45;
            this.lblAnBits.Text = "Data Bits (0)";
            // 
            // gbAnCurBin
            // 
            this.gbAnCurBin.Controls.Add(this.tbAnCurBinStDev);
            this.gbAnCurBin.Controls.Add(this.label51);
            this.gbAnCurBin.Controls.Add(this.tbAnCurBinBits);
            this.gbAnCurBin.Controls.Add(this.tbAnCurBinCount);
            this.gbAnCurBin.Controls.Add(this.tbAnCurBinWidth);
            this.gbAnCurBin.Controls.Add(this.label42);
            this.gbAnCurBin.Controls.Add(this.cbAnBins);
            this.gbAnCurBin.Controls.Add(this.lblAnBins);
            this.gbAnCurBin.Controls.Add(this.label41);
            this.gbAnCurBin.Controls.Add(this.label40);
            this.gbAnCurBin.Location = new System.Drawing.Point(351, 260);
            this.gbAnCurBin.Name = "gbAnCurBin";
            this.gbAnCurBin.Size = new System.Drawing.Size(204, 191);
            this.gbAnCurBin.TabIndex = 57;
            this.gbAnCurBin.TabStop = false;
            this.gbAnCurBin.Text = "Bin Details";
            // 
            // tbAnCurBinStDev
            // 
            this.tbAnCurBinStDev.Location = new System.Drawing.Point(89, 89);
            this.tbAnCurBinStDev.Name = "tbAnCurBinStDev";
            this.tbAnCurBinStDev.ReadOnly = true;
            this.tbAnCurBinStDev.Size = new System.Drawing.Size(100, 20);
            this.tbAnCurBinStDev.TabIndex = 58;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(7, 92);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(52, 13);
            this.label51.TabIndex = 57;
            this.label51.Text = "Std. Dev.";
            // 
            // tbAnCurBinBits
            // 
            this.tbAnCurBinBits.Location = new System.Drawing.Point(89, 149);
            this.tbAnCurBinBits.Name = "tbAnCurBinBits";
            this.tbAnCurBinBits.ReadOnly = true;
            this.tbAnCurBinBits.Size = new System.Drawing.Size(100, 20);
            this.tbAnCurBinBits.TabIndex = 56;
            // 
            // tbAnCurBinCount
            // 
            this.tbAnCurBinCount.Location = new System.Drawing.Point(89, 119);
            this.tbAnCurBinCount.Name = "tbAnCurBinCount";
            this.tbAnCurBinCount.ReadOnly = true;
            this.tbAnCurBinCount.Size = new System.Drawing.Size(100, 20);
            this.tbAnCurBinCount.TabIndex = 55;
            // 
            // tbAnCurBinWidth
            // 
            this.tbAnCurBinWidth.Location = new System.Drawing.Point(89, 59);
            this.tbAnCurBinWidth.Name = "tbAnCurBinWidth";
            this.tbAnCurBinWidth.ReadOnly = true;
            this.tbAnCurBinWidth.Size = new System.Drawing.Size(100, 20);
            this.tbAnCurBinWidth.TabIndex = 54;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(7, 152);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(24, 13);
            this.label42.TabIndex = 53;
            this.label42.Text = "Bits";
            // 
            // cbAnBins
            // 
            this.cbAnBins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnBins.Enabled = false;
            this.cbAnBins.FormattingEnabled = true;
            this.cbAnBins.Location = new System.Drawing.Point(91, 28);
            this.cbAnBins.Name = "cbAnBins";
            this.cbAnBins.Size = new System.Drawing.Size(83, 21);
            this.cbAnBins.TabIndex = 20;
            this.cbAnBins.SelectedIndexChanged += new System.EventHandler(this.cbAnBins_SelectedIndexChanged);
            // 
            // lblAnBins
            // 
            this.lblAnBins.AutoSize = true;
            this.lblAnBins.Location = new System.Drawing.Point(7, 32);
            this.lblAnBins.Name = "lblAnBins";
            this.lblAnBins.Size = new System.Drawing.Size(39, 13);
            this.lblAnBins.TabIndex = 43;
            this.lblAnBins.Text = "Bins(0)";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(7, 122);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 13);
            this.label41.TabIndex = 52;
            this.label41.Text = "Count";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(7, 62);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(78, 13);
            this.label40.TabIndex = 51;
            this.label40.Text = "Average Width";
            // 
            // btnAnAnalyze
            // 
            this.btnAnAnalyze.Enabled = false;
            this.btnAnAnalyze.Location = new System.Drawing.Point(189, 360);
            this.btnAnAnalyze.Name = "btnAnAnalyze";
            this.btnAnAnalyze.Size = new System.Drawing.Size(88, 23);
            this.btnAnAnalyze.TabIndex = 19;
            this.btnAnAnalyze.Text = "Analyze Signal";
            this.btnAnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnAnalyze.Click += new System.EventHandler(this.btnAnAnalyze_Click);
            // 
            // gbAnFilter
            // 
            this.gbAnFilter.Controls.Add(this.tbAnFirOrder);
            this.gbAnFilter.Controls.Add(this.label21);
            this.gbAnFilter.Controls.Add(this.label23);
            this.gbAnFilter.Controls.Add(this.label24);
            this.gbAnFilter.Controls.Add(this.label25);
            this.gbAnFilter.Controls.Add(this.tbAnSampleRate);
            this.gbAnFilter.Controls.Add(this.tbAnLowPass);
            this.gbAnFilter.Controls.Add(this.tbAnStop);
            this.gbAnFilter.Location = new System.Drawing.Point(40, 64);
            this.gbAnFilter.Name = "gbAnFilter";
            this.gbAnFilter.Size = new System.Drawing.Size(237, 143);
            this.gbAnFilter.TabIndex = 2;
            this.gbAnFilter.TabStop = false;
            this.gbAnFilter.Text = "Filter Paramaters";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(21, 29);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(84, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Sample Rate Hz";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(21, 59);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 13);
            this.label23.TabIndex = 2;
            this.label23.Text = "FIR Order";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(21, 89);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(69, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "Low Pass Hz";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(21, 119);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(45, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "Stop Hz";
            // 
            // btnAnFilter
            // 
            this.btnAnFilter.Location = new System.Drawing.Point(67, 360);
            this.btnAnFilter.Name = "btnAnFilter";
            this.btnAnFilter.Size = new System.Drawing.Size(113, 23);
            this.btnAnFilter.TabIndex = 18;
            this.btnAnFilter.Text = "Filter in MATLAB";
            this.btnAnFilter.UseVisualStyleBackColor = true;
            this.btnAnFilter.Click += new System.EventHandler(this.btnAnFilter_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(64, 326);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 13);
            this.label29.TabIndex = 8;
            this.label29.Text = "Last Sample";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(64, 296);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(64, 13);
            this.label28.TabIndex = 7;
            this.label28.Text = "First Sample";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(64, 266);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(79, 13);
            this.label27.TabIndex = 6;
            this.label27.Text = "Amplitude Floor";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(64, 236);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(73, 13);
            this.label26.TabIndex = 5;
            this.label26.Text = "Amplitude Clip";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(64, 38);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Signal File";
            // 
            // tabDevice
            // 
            this.tabDevice.Controls.Add(this.tbDvRepetitions);
            this.tabDevice.Controls.Add(this.label67);
            this.tabDevice.Controls.Add(this.gbDvDevice);
            this.tabDevice.Controls.Add(this.tbDvSampleRate);
            this.tabDevice.Controls.Add(this.label66);
            this.tabDevice.Controls.Add(this.btnDvSaveSignal);
            this.tabDevice.Controls.Add(this.btnDvMakeSignal);
            this.tabDevice.Controls.Add(this.tbDvWordContents);
            this.tabDevice.Controls.Add(this.label65);
            this.tabDevice.Location = new System.Drawing.Point(4, 22);
            this.tabDevice.Name = "tabDevice";
            this.tabDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tabDevice.Size = new System.Drawing.Size(970, 469);
            this.tabDevice.TabIndex = 2;
            this.tabDevice.Text = "Device";
            this.tabDevice.UseVisualStyleBackColor = true;
            // 
            // tbDvRepetitions
            // 
            this.tbDvRepetitions.Location = new System.Drawing.Point(518, 121);
            this.tbDvRepetitions.Name = "tbDvRepetitions";
            this.tbDvRepetitions.Size = new System.Drawing.Size(100, 20);
            this.tbDvRepetitions.TabIndex = 31;
            this.tbDvRepetitions.Text = "5";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(418, 124);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(60, 13);
            this.label67.TabIndex = 30;
            this.label67.Text = "Repetitions";
            // 
            // gbDvDevice
            // 
            this.gbDvDevice.Controls.Add(this.btnDvOpenDevice);
            this.gbDvDevice.Controls.Add(this.btnDvNewDevice);
            this.gbDvDevice.Controls.Add(this.btnDvSaveDevice);
            this.gbDvDevice.Controls.Add(this.tbDvWordPad);
            this.gbDvDevice.Controls.Add(this.cbDvSyncEdge);
            this.gbDvDevice.Controls.Add(this.tbDvDataBitPeriod);
            this.gbDvDevice.Controls.Add(this.tbDvWidePW);
            this.gbDvDevice.Controls.Add(this.tbDvNarrowPW);
            this.gbDvDevice.Controls.Add(this.tbDvNumDataBits);
            this.gbDvDevice.Controls.Add(this.tbDvSbPad);
            this.gbDvDevice.Controls.Add(this.tbDvSbContents);
            this.gbDvDevice.Controls.Add(this.label64);
            this.gbDvDevice.Controls.Add(this.label63);
            this.gbDvDevice.Controls.Add(this.label62);
            this.gbDvDevice.Controls.Add(this.label61);
            this.gbDvDevice.Controls.Add(this.label60);
            this.gbDvDevice.Controls.Add(this.label59);
            this.gbDvDevice.Controls.Add(this.label58);
            this.gbDvDevice.Controls.Add(this.label55);
            this.gbDvDevice.Controls.Add(this.label1);
            this.gbDvDevice.Controls.Add(this.tbDvDeviceName);
            this.gbDvDevice.Location = new System.Drawing.Point(38, 36);
            this.gbDvDevice.Name = "gbDvDevice";
            this.gbDvDevice.Size = new System.Drawing.Size(285, 333);
            this.gbDvDevice.TabIndex = 29;
            this.gbDvDevice.TabStop = false;
            this.gbDvDevice.Text = "OOK Device Settings";
            // 
            // btnDvOpenDevice
            // 
            this.btnDvOpenDevice.Location = new System.Drawing.Point(94, 294);
            this.btnDvOpenDevice.Name = "btnDvOpenDevice";
            this.btnDvOpenDevice.Size = new System.Drawing.Size(55, 23);
            this.btnDvOpenDevice.TabIndex = 28;
            this.btnDvOpenDevice.Text = "Open";
            this.btnDvOpenDevice.UseVisualStyleBackColor = true;
            this.btnDvOpenDevice.Click += new System.EventHandler(this.btnDvOpenDevice_Click);
            // 
            // btnDvNewDevice
            // 
            this.btnDvNewDevice.Location = new System.Drawing.Point(29, 294);
            this.btnDvNewDevice.Name = "btnDvNewDevice";
            this.btnDvNewDevice.Size = new System.Drawing.Size(55, 23);
            this.btnDvNewDevice.TabIndex = 27;
            this.btnDvNewDevice.Text = "New";
            this.btnDvNewDevice.UseVisualStyleBackColor = true;
            this.btnDvNewDevice.Click += new System.EventHandler(this.btnDvNewDevice_Click);
            // 
            // btnDvSaveDevice
            // 
            this.btnDvSaveDevice.Enabled = false;
            this.btnDvSaveDevice.Location = new System.Drawing.Point(159, 294);
            this.btnDvSaveDevice.Name = "btnDvSaveDevice";
            this.btnDvSaveDevice.Size = new System.Drawing.Size(55, 23);
            this.btnDvSaveDevice.TabIndex = 18;
            this.btnDvSaveDevice.Text = "Save";
            this.btnDvSaveDevice.UseVisualStyleBackColor = true;
            this.btnDvSaveDevice.Click += new System.EventHandler(this.btnDevSaveDevice_Click);
            // 
            // tbDvWordPad
            // 
            this.tbDvWordPad.Enabled = false;
            this.tbDvWordPad.Location = new System.Drawing.Point(159, 265);
            this.tbDvWordPad.Name = "tbDvWordPad";
            this.tbDvWordPad.Size = new System.Drawing.Size(100, 20);
            this.tbDvWordPad.TabIndex = 17;
            this.tbDvWordPad.Validating += new System.ComponentModel.CancelEventHandler(this.tbDvWordPad_Validating);
            // 
            // cbDvSyncEdge
            // 
            this.cbDvSyncEdge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDvSyncEdge.Enabled = false;
            this.cbDvSyncEdge.Location = new System.Drawing.Point(159, 235);
            this.cbDvSyncEdge.Name = "cbDvSyncEdge";
            this.cbDvSyncEdge.Size = new System.Drawing.Size(100, 21);
            this.cbDvSyncEdge.TabIndex = 16;
            this.cbDvSyncEdge.Validating += new System.ComponentModel.CancelEventHandler(this.cbDvSyncEdge_Validating);
            // 
            // tbDvDataBitPeriod
            // 
            this.tbDvDataBitPeriod.Enabled = false;
            this.tbDvDataBitPeriod.Location = new System.Drawing.Point(159, 205);
            this.tbDvDataBitPeriod.Name = "tbDvDataBitPeriod";
            this.tbDvDataBitPeriod.Size = new System.Drawing.Size(100, 20);
            this.tbDvDataBitPeriod.TabIndex = 15;
            this.tbDvDataBitPeriod.Validated += new System.EventHandler(this.tbDvDataBitPeriod_Validated);
            // 
            // tbDvWidePW
            // 
            this.tbDvWidePW.Enabled = false;
            this.tbDvWidePW.Location = new System.Drawing.Point(159, 175);
            this.tbDvWidePW.Name = "tbDvWidePW";
            this.tbDvWidePW.Size = new System.Drawing.Size(100, 20);
            this.tbDvWidePW.TabIndex = 14;
            this.tbDvWidePW.Validating += new System.ComponentModel.CancelEventHandler(this.tbDvWidePW_Validating);
            // 
            // tbDvNarrowPW
            // 
            this.tbDvNarrowPW.Enabled = false;
            this.tbDvNarrowPW.Location = new System.Drawing.Point(159, 145);
            this.tbDvNarrowPW.Name = "tbDvNarrowPW";
            this.tbDvNarrowPW.Size = new System.Drawing.Size(100, 20);
            this.tbDvNarrowPW.TabIndex = 13;
            this.tbDvNarrowPW.Validating += new System.ComponentModel.CancelEventHandler(this.tbDvNarrowPW_Validating);
            // 
            // tbDvNumDataBits
            // 
            this.tbDvNumDataBits.Enabled = false;
            this.tbDvNumDataBits.Location = new System.Drawing.Point(159, 115);
            this.tbDvNumDataBits.Name = "tbDvNumDataBits";
            this.tbDvNumDataBits.Size = new System.Drawing.Size(100, 20);
            this.tbDvNumDataBits.TabIndex = 12;
            this.tbDvNumDataBits.Validating += new System.ComponentModel.CancelEventHandler(this.tbDvNumDataBits_Validating);
            // 
            // tbDvSbPad
            // 
            this.tbDvSbPad.Enabled = false;
            this.tbDvSbPad.Location = new System.Drawing.Point(159, 85);
            this.tbDvSbPad.Name = "tbDvSbPad";
            this.tbDvSbPad.Size = new System.Drawing.Size(100, 20);
            this.tbDvSbPad.TabIndex = 11;
            this.tbDvSbPad.Validating += new System.ComponentModel.CancelEventHandler(this.tbDvSbPad_Validating);
            // 
            // tbDvSbContents
            // 
            this.tbDvSbContents.Enabled = false;
            this.tbDvSbContents.Location = new System.Drawing.Point(159, 55);
            this.tbDvSbContents.Name = "tbDvSbContents";
            this.tbDvSbContents.Size = new System.Drawing.Size(100, 20);
            this.tbDvSbContents.TabIndex = 10;
            this.tbDvSbContents.Validating += new System.ComponentModel.CancelEventHandler(this.tbDvSbContents_Validating);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(26, 268);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(77, 13);
            this.label64.TabIndex = 9;
            this.label64.Text = "Word Pad (ms)";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(26, 238);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(59, 13);
            this.label63.TabIndex = 8;
            this.label63.Text = "Sync Edge";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(26, 208);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(100, 13);
            this.label62.TabIndex = 7;
            this.label62.Text = "Data Bit Period (ms)";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(26, 178);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(114, 13);
            this.label61.TabIndex = 6;
            this.label61.Text = "Wide Pulse Width (ms)";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(26, 148);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(123, 13);
            this.label60.TabIndex = 5;
            this.label60.Text = "Narrow Pulse Width (ms)";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(26, 118);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(102, 13);
            this.label59.TabIndex = 4;
            this.label59.Text = "Number of Data Bits";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(26, 88);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(93, 13);
            this.label58.TabIndex = 3;
            this.label58.Text = "Start Bits Pad (ms)";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(26, 58);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(94, 13);
            this.label55.TabIndex = 2;
            this.label55.Text = "Start Bits Contents";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "OOK Device Name";
            // 
            // tbDvDeviceName
            // 
            this.tbDvDeviceName.Enabled = false;
            this.tbDvDeviceName.Location = new System.Drawing.Point(159, 25);
            this.tbDvDeviceName.Name = "tbDvDeviceName";
            this.tbDvDeviceName.Size = new System.Drawing.Size(100, 20);
            this.tbDvDeviceName.TabIndex = 0;
            this.tbDvDeviceName.Validating += new System.ComponentModel.CancelEventHandler(this.tbDvDeviceName_Validating);
            // 
            // tbDvSampleRate
            // 
            this.tbDvSampleRate.Location = new System.Drawing.Point(518, 61);
            this.tbDvSampleRate.Name = "tbDvSampleRate";
            this.tbDvSampleRate.Size = new System.Drawing.Size(100, 20);
            this.tbDvSampleRate.TabIndex = 24;
            this.tbDvSampleRate.Validating += new System.ComponentModel.CancelEventHandler(this.SampleRate_Validating);
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(418, 64);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(68, 13);
            this.label66.TabIndex = 23;
            this.label66.Text = "Sample Rate";
            // 
            // btnDvSaveSignal
            // 
            this.btnDvSaveSignal.Enabled = false;
            this.btnDvSaveSignal.Location = new System.Drawing.Point(518, 149);
            this.btnDvSaveSignal.Name = "btnDvSaveSignal";
            this.btnDvSaveSignal.Size = new System.Drawing.Size(75, 23);
            this.btnDvSaveSignal.TabIndex = 22;
            this.btnDvSaveSignal.Text = "Save Signal";
            this.btnDvSaveSignal.UseVisualStyleBackColor = true;
            this.btnDvSaveSignal.Click += new System.EventHandler(this.btnDvSaveSignal_Click);
            // 
            // btnDvMakeSignal
            // 
            this.btnDvMakeSignal.Enabled = false;
            this.btnDvMakeSignal.Location = new System.Drawing.Point(411, 149);
            this.btnDvMakeSignal.Name = "btnDvMakeSignal";
            this.btnDvMakeSignal.Size = new System.Drawing.Size(75, 23);
            this.btnDvMakeSignal.TabIndex = 21;
            this.btnDvMakeSignal.Text = "Make Signal";
            this.btnDvMakeSignal.UseVisualStyleBackColor = true;
            this.btnDvMakeSignal.Click += new System.EventHandler(this.btnDvMakeSignal_Click);
            // 
            // tbDvWordContents
            // 
            this.tbDvWordContents.Enabled = false;
            this.tbDvWordContents.Location = new System.Drawing.Point(518, 91);
            this.tbDvWordContents.Name = "tbDvWordContents";
            this.tbDvWordContents.Size = new System.Drawing.Size(100, 20);
            this.tbDvWordContents.TabIndex = 20;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(418, 94);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(78, 13);
            this.label65.TabIndex = 19;
            this.label65.Text = "Word Contents";
            // 
            // tabTransmit
            // 
            this.tabTransmit.Controls.Add(this.label76);
            this.tabTransmit.Controls.Add(this.tbTxSdrStatus);
            this.tabTransmit.Controls.Add(this.gbTxOptions);
            this.tabTransmit.Controls.Add(this.gbTxSignalOptions);
            this.tabTransmit.Location = new System.Drawing.Point(4, 22);
            this.tabTransmit.Name = "tabTransmit";
            this.tabTransmit.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransmit.Size = new System.Drawing.Size(970, 469);
            this.tabTransmit.TabIndex = 1;
            this.tabTransmit.Text = "Transmit";
            this.tabTransmit.UseVisualStyleBackColor = true;
            this.tabTransmit.Enter += new System.EventHandler(this.tabRxTx_Enter);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(384, 214);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(63, 13);
            this.label76.TabIndex = 62;
            this.label76.Text = "SDR Status";
            // 
            // tbTxSdrStatus
            // 
            this.tbTxSdrStatus.Location = new System.Drawing.Point(384, 244);
            this.tbTxSdrStatus.Multiline = true;
            this.tbTxSdrStatus.Name = "tbTxSdrStatus";
            this.tbTxSdrStatus.ReadOnly = true;
            this.tbTxSdrStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTxSdrStatus.Size = new System.Drawing.Size(519, 170);
            this.tbTxSdrStatus.TabIndex = 61;
            // 
            // gbTxOptions
            // 
            this.gbTxOptions.Controls.Add(this.tbTxSyncTimeout);
            this.gbTxOptions.Controls.Add(this.tbTxNumXfers);
            this.gbTxOptions.Controls.Add(this.tbTxBufferSize);
            this.gbTxOptions.Controls.Add(this.tbTxNumBuffers);
            this.gbTxOptions.Controls.Add(this.label71);
            this.gbTxOptions.Controls.Add(this.label72);
            this.gbTxOptions.Controls.Add(this.label73);
            this.gbTxOptions.Controls.Add(this.label74);
            this.gbTxOptions.Controls.Add(this.label69);
            this.gbTxOptions.Controls.Add(this.label70);
            this.gbTxOptions.Controls.Add(this.tbTxVGain2);
            this.gbTxOptions.Controls.Add(this.tbTxVGain1);
            this.gbTxOptions.Controls.Add(this.tbTxBandwidth);
            this.gbTxOptions.Controls.Add(this.tbTxSampleRate);
            this.gbTxOptions.Controls.Add(this.tbTxFrequency);
            this.gbTxOptions.Controls.Add(this.cbTxSDR);
            this.gbTxOptions.Controls.Add(this.label10);
            this.gbTxOptions.Controls.Add(this.label30);
            this.gbTxOptions.Controls.Add(this.label31);
            this.gbTxOptions.Controls.Add(this.label68);
            this.gbTxOptions.Location = new System.Drawing.Point(40, 40);
            this.gbTxOptions.Name = "gbTxOptions";
            this.gbTxOptions.Size = new System.Drawing.Size(302, 335);
            this.gbTxOptions.TabIndex = 60;
            this.gbTxOptions.TabStop = false;
            this.gbTxOptions.Text = "Transmitter Options";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(24, 294);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(88, 13);
            this.label71.TabIndex = 56;
            this.label71.Text = "Sync Timout (ms)";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(24, 264);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(103, 13);
            this.label72.TabIndex = 53;
            this.label72.Text = "Number of Transfers";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(24, 234);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(58, 13);
            this.label73.TabIndex = 52;
            this.label73.Text = "Buffer Size";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(24, 204);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(92, 13);
            this.label74.TabIndex = 51;
            this.label74.Text = "Number of Buffers";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(24, 174);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(57, 13);
            this.label69.TabIndex = 50;
            this.label69.Text = "Tx VGain2";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(24, 144);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(57, 13);
            this.label70.TabIndex = 49;
            this.label70.Text = "Tx VGain1";
            // 
            // tbTxVGain2
            // 
            this.tbTxVGain2.Enabled = false;
            this.tbTxVGain2.Location = new System.Drawing.Point(175, 171);
            this.tbTxVGain2.Name = "tbTxVGain2";
            this.tbTxVGain2.Size = new System.Drawing.Size(100, 20);
            this.tbTxVGain2.TabIndex = 48;
            // 
            // tbTxVGain1
            // 
            this.tbTxVGain1.Enabled = false;
            this.tbTxVGain1.Location = new System.Drawing.Point(175, 141);
            this.tbTxVGain1.Name = "tbTxVGain1";
            this.tbTxVGain1.Size = new System.Drawing.Size(100, 20);
            this.tbTxVGain1.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "SDR";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(24, 114);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(57, 13);
            this.label30.TabIndex = 29;
            this.label30.Text = "Bandwidth";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(24, 84);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(68, 13);
            this.label31.TabIndex = 27;
            this.label31.Text = "Sample Rate";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(24, 54);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(57, 13);
            this.label68.TabIndex = 25;
            this.label68.Text = "Frequency";
            // 
            // gbTxSignalOptions
            // 
            this.gbTxSignalOptions.Controls.Add(this.btnTxViewSignal);
            this.gbTxSignalOptions.Controls.Add(this.btnTxOpenFile);
            this.gbTxSignalOptions.Controls.Add(this.tbTxFileName);
            this.gbTxSignalOptions.Controls.Add(this.label4);
            this.gbTxSignalOptions.Controls.Add(this.tbTxCount);
            this.gbTxSignalOptions.Controls.Add(this.tbTxDelay);
            this.gbTxSignalOptions.Controls.Add(this.label3);
            this.gbTxSignalOptions.Controls.Add(this.label2);
            this.gbTxSignalOptions.Controls.Add(this.btnTxTransmit);
            this.gbTxSignalOptions.Location = new System.Drawing.Point(387, 40);
            this.gbTxSignalOptions.Name = "gbTxSignalOptions";
            this.gbTxSignalOptions.Size = new System.Drawing.Size(386, 154);
            this.gbTxSignalOptions.TabIndex = 59;
            this.gbTxSignalOptions.TabStop = false;
            this.gbTxSignalOptions.Text = "Transmit Signal Options";
            // 
            // btnTxViewSignal
            // 
            this.btnTxViewSignal.Enabled = false;
            this.btnTxViewSignal.Location = new System.Drawing.Point(134, 109);
            this.btnTxViewSignal.Name = "btnTxViewSignal";
            this.btnTxViewSignal.Size = new System.Drawing.Size(75, 23);
            this.btnTxViewSignal.TabIndex = 8;
            this.btnTxViewSignal.Text = "View Signal";
            this.btnTxViewSignal.UseVisualStyleBackColor = true;
            this.btnTxViewSignal.Click += new System.EventHandler(this.btnTxViewSignal_Click);
            // 
            // btnTxOpenFile
            // 
            this.btnTxOpenFile.Location = new System.Drawing.Point(294, 16);
            this.btnTxOpenFile.Name = "btnTxOpenFile";
            this.btnTxOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnTxOpenFile.TabIndex = 7;
            this.btnTxOpenFile.Text = "Open File";
            this.btnTxOpenFile.UseVisualStyleBackColor = true;
            this.btnTxOpenFile.Click += new System.EventHandler(this.btnTxOpenFile_Click);
            // 
            // tbTxFileName
            // 
            this.tbTxFileName.Location = new System.Drawing.Point(134, 18);
            this.tbTxFileName.Name = "tbTxFileName";
            this.tbTxFileName.Size = new System.Drawing.Size(100, 20);
            this.tbTxFileName.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Signal File";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Delay";
            // 
            // btnTxTransmit
            // 
            this.btnTxTransmit.Enabled = false;
            this.btnTxTransmit.Location = new System.Drawing.Point(27, 109);
            this.btnTxTransmit.Name = "btnTxTransmit";
            this.btnTxTransmit.Size = new System.Drawing.Size(75, 23);
            this.btnTxTransmit.TabIndex = 4;
            this.btnTxTransmit.Text = "Transmit";
            this.btnTxTransmit.UseVisualStyleBackColor = true;
            this.btnTxTransmit.Click += new System.EventHandler(this.btnTxTransmit_Click);
            // 
            // tabReceive
            // 
            this.tabReceive.Controls.Add(this.rbRxOptions);
            this.tabReceive.Controls.Add(this.label75);
            this.tabReceive.Controls.Add(this.tbRxSdrStatus);
            this.tabReceive.Controls.Add(this.btnRxSelectFile);
            this.tabReceive.Controls.Add(this.tbRxFilter);
            this.tabReceive.Controls.Add(this.tbRxFileName);
            this.tabReceive.Controls.Add(this.btnFilterSelect);
            this.tabReceive.Controls.Add(this.cbRxFormat);
            this.tabReceive.Controls.Add(this.rbRxInputFiltered);
            this.tabReceive.Controls.Add(this.rbRxInputRaw);
            this.tabReceive.Controls.Add(this.label9);
            this.tabReceive.Controls.Add(this.label8);
            this.tabReceive.Controls.Add(this.label7);
            this.tabReceive.Controls.Add(this.label6);
            this.tabReceive.Controls.Add(this.btnRxReceive);
            this.tabReceive.Location = new System.Drawing.Point(4, 22);
            this.tabReceive.Name = "tabReceive";
            this.tabReceive.Padding = new System.Windows.Forms.Padding(3);
            this.tabReceive.Size = new System.Drawing.Size(970, 469);
            this.tabReceive.TabIndex = 3;
            this.tabReceive.Text = "Receive";
            this.tabReceive.UseVisualStyleBackColor = true;
            this.tabReceive.Enter += new System.EventHandler(this.tabRxTx_Enter);
            // 
            // rbRxOptions
            // 
            this.rbRxOptions.Controls.Add(this.tbRxRecordTime);
            this.rbRxOptions.Controls.Add(this.label5);
            this.rbRxOptions.Controls.Add(this.label54);
            this.rbRxOptions.Controls.Add(this.label53);
            this.rbRxOptions.Controls.Add(this.tbRxVGain2);
            this.rbRxOptions.Controls.Add(this.tbRxVGain1);
            this.rbRxOptions.Controls.Add(this.tbRxSyncTimeout);
            this.rbRxOptions.Controls.Add(this.tbRxNumXfers);
            this.rbRxOptions.Controls.Add(this.tbRxBufferSize);
            this.rbRxOptions.Controls.Add(this.tbRxNumBuffers);
            this.rbRxOptions.Controls.Add(this.cbRxLnaGain);
            this.rbRxOptions.Controls.Add(this.tbRxBandwidth);
            this.rbRxOptions.Controls.Add(this.tbRxSampleRate);
            this.rbRxOptions.Controls.Add(this.tbRxFrequency);
            this.rbRxOptions.Controls.Add(this.cbRxSDR);
            this.rbRxOptions.Controls.Add(this.label22);
            this.rbRxOptions.Controls.Add(this.label19);
            this.rbRxOptions.Controls.Add(this.label17);
            this.rbRxOptions.Controls.Add(this.label16);
            this.rbRxOptions.Controls.Add(this.label15);
            this.rbRxOptions.Controls.Add(this.label14);
            this.rbRxOptions.Controls.Add(this.label13);
            this.rbRxOptions.Controls.Add(this.label12);
            this.rbRxOptions.Controls.Add(this.label11);
            this.rbRxOptions.Location = new System.Drawing.Point(40, 40);
            this.rbRxOptions.Name = "rbRxOptions";
            this.rbRxOptions.Size = new System.Drawing.Size(308, 395);
            this.rbRxOptions.TabIndex = 51;
            this.rbRxOptions.TabStop = false;
            this.rbRxOptions.Text = "Receiver Options";
            // 
            // tbRxRecordTime
            // 
            this.tbRxRecordTime.Location = new System.Drawing.Point(175, 351);
            this.tbRxRecordTime.Name = "tbRxRecordTime";
            this.tbRxRecordTime.Size = new System.Drawing.Size(100, 20);
            this.tbRxRecordTime.TabIndex = 48;
            this.tbRxRecordTime.Text = "5";
            this.tbRxRecordTime.Validating += new System.ComponentModel.CancelEventHandler(this.tbRxRecordTime_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Recording time (seconds)";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(27, 204);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(58, 13);
            this.label54.TabIndex = 46;
            this.label54.Text = "Rx VGain2";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(27, 174);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(58, 13);
            this.label53.TabIndex = 45;
            this.label53.Text = "Rx VGain1";
            // 
            // tbRxVGain2
            // 
            this.tbRxVGain2.Enabled = false;
            this.tbRxVGain2.Location = new System.Drawing.Point(175, 201);
            this.tbRxVGain2.Name = "tbRxVGain2";
            this.tbRxVGain2.Size = new System.Drawing.Size(100, 20);
            this.tbRxVGain2.TabIndex = 7;
            this.tbRxVGain2.Validating += new System.ComponentModel.CancelEventHandler(this.tbRxVGain2_Validating);
            // 
            // tbRxVGain1
            // 
            this.tbRxVGain1.Enabled = false;
            this.tbRxVGain1.Location = new System.Drawing.Point(175, 171);
            this.tbRxVGain1.Name = "tbRxVGain1";
            this.tbRxVGain1.Size = new System.Drawing.Size(100, 20);
            this.tbRxVGain1.TabIndex = 6;
            this.tbRxVGain1.Validating += new System.ComponentModel.CancelEventHandler(this.tbRxVGain1_Validating);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(24, 24);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(30, 13);
            this.label22.TabIndex = 23;
            this.label22.Text = "SDR";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 324);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 9;
            this.label19.Text = "Sync Timout (ms)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(24, 294);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Number of Transfers";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 264);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Buffer Size";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 234);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Number of Buffers";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 144);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "LNA Gain";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Bandwidth";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Sample Rate";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Frequency";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(405, 244);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(63, 13);
            this.label75.TabIndex = 50;
            this.label75.Text = "SDR Status";
            // 
            // tbRxSdrStatus
            // 
            this.tbRxSdrStatus.Location = new System.Drawing.Point(405, 274);
            this.tbRxSdrStatus.Multiline = true;
            this.tbRxSdrStatus.Name = "tbRxSdrStatus";
            this.tbRxSdrStatus.ReadOnly = true;
            this.tbRxSdrStatus.Size = new System.Drawing.Size(519, 170);
            this.tbRxSdrStatus.TabIndex = 49;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(405, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Format";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(405, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Filter";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Input";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(405, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "File Name";
            // 
            // btnRxReceive
            // 
            this.btnRxReceive.Enabled = false;
            this.btnRxReceive.Location = new System.Drawing.Point(405, 179);
            this.btnRxReceive.Name = "btnRxReceive";
            this.btnRxReceive.Size = new System.Drawing.Size(75, 23);
            this.btnRxReceive.TabIndex = 25;
            this.btnRxReceive.Text = "Receive";
            this.btnRxReceive.UseVisualStyleBackColor = true;
            this.btnRxReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabReceive);
            this.tabControl1.Controls.Add(this.tabTransmit);
            this.tabControl1.Controls.Add(this.tabAnalyze);
            this.tabControl1.Controls.Add(this.tabDevice);
            this.tabControl1.Location = new System.Drawing.Point(1, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 495);
            this.tabControl1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txOpenFileDialog
            // 
            this.txOpenFileDialog.FileName = "openFileDialog1";
            // 
            // dvOpenFileDialog
            // 
            this.dvOpenFileDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnAnFilter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 527);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "cOOKie#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabAnalyze.ResumeLayout(false);
            this.tabAnalyze.PerformLayout();
            this.gbAnWordSummary.ResumeLayout(false);
            this.gbAnWordSummary.PerformLayout();
            this.gbAnStartBits.ResumeLayout(false);
            this.gbAnStartBits.PerformLayout();
            this.gbAnDisplayAs.ResumeLayout(false);
            this.gbAnDisplayAs.PerformLayout();
            this.gbAnBitDetails.ResumeLayout(false);
            this.gbAnBitDetails.PerformLayout();
            this.gbAnCurBin.ResumeLayout(false);
            this.gbAnCurBin.PerformLayout();
            this.gbAnFilter.ResumeLayout(false);
            this.gbAnFilter.PerformLayout();
            this.tabDevice.ResumeLayout(false);
            this.tabDevice.PerformLayout();
            this.gbDvDevice.ResumeLayout(false);
            this.gbDvDevice.PerformLayout();
            this.tabTransmit.ResumeLayout(false);
            this.tabTransmit.PerformLayout();
            this.gbTxOptions.ResumeLayout(false);
            this.gbTxOptions.PerformLayout();
            this.gbTxSignalOptions.ResumeLayout(false);
            this.gbTxSignalOptions.PerformLayout();
            this.tabReceive.ResumeLayout(false);
            this.tabReceive.PerformLayout();
            this.rbRxOptions.ResumeLayout(false);
            this.rbRxOptions.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttSdr;
        private System.Windows.Forms.ToolTip ttTxDelay;
        private System.Windows.Forms.ToolTip ttTxCount;
        private System.Windows.Forms.ToolTip ttTxParameters;
        private System.Windows.Forms.ToolTip ttRxTab;
        private System.Windows.Forms.OpenFileDialog openFilterDialog;
        private System.Windows.Forms.ToolTip ttRxSelectFilter;
        private System.Windows.Forms.ToolTip ttAnalyzeTab;
        private System.Windows.Forms.OpenFileDialog anOpenFileDialog;
        private System.Windows.Forms.TabPage tabAnalyze;
        private System.Windows.Forms.GroupBox gbAnWordSummary;
        private System.Windows.Forms.GroupBox gbAnDisplayAs;
        private System.Windows.Forms.RadioButton rbAnSam;
        private System.Windows.Forms.RadioButton rbAnMs;
        private System.Windows.Forms.RadioButton rbAnHz;
        private System.Windows.Forms.TextBox tbAnFallStDev;
        private System.Windows.Forms.TextBox tbAnRiseStDev;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox tbAnAvgFallPeriod;
        private System.Windows.Forms.TextBox tbAnAvgRisePeriod;
        private System.Windows.Forms.TextBox tbAnWordContents;
        private System.Windows.Forms.TextBox tbAnAvgWideWidth;
        private System.Windows.Forms.TextBox tbAnAvgNarrowWidth;
        private System.Windows.Forms.TextBox tbAnNumBits;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.GroupBox gbAnBitDetails;
        private System.Windows.Forms.Button btnAnCurBitMinus;
        private System.Windows.Forms.Button btnAnCurBitPlus;
        private System.Windows.Forms.TextBox tbAnCurBitFDev;
        private System.Windows.Forms.TextBox tbAnCurBitRDev;
        private System.Windows.Forms.TextBox tbAnCurBitFPeriod;
        private System.Windows.Forms.TextBox tbAnCurBitRPeriod;
        private System.Windows.Forms.TextBox tbAnCurBitPWDev;
        private System.Windows.Forms.TextBox tbAnCurBitFTime;
        private System.Windows.Forms.TextBox tbAnCurBitRTime;
        private System.Windows.Forms.TextBox tbAnCurBitWidth;
        private System.Windows.Forms.TextBox tbAnCurBitPos;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ComboBox cbAnBits;
        private System.Windows.Forms.Label lblAnBits;
        private System.Windows.Forms.GroupBox gbAnCurBin;
        private System.Windows.Forms.TextBox tbAnCurBinStDev;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox tbAnCurBinBits;
        private System.Windows.Forms.TextBox tbAnCurBinCount;
        private System.Windows.Forms.TextBox tbAnCurBinWidth;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox cbAnBins;
        private System.Windows.Forms.Label lblAnBins;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button btnAnAnalyze;
        private System.Windows.Forms.GroupBox gbAnFilter;
        private System.Windows.Forms.TextBox tbAnFirOrder;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbAnSampleRate;
        private System.Windows.Forms.TextBox tbAnLowPass;
        private System.Windows.Forms.TextBox tbAnStop;
        private System.Windows.Forms.Button btnAnFilter;
        private System.Windows.Forms.TextBox tbAnLastSample;
        private System.Windows.Forms.TextBox tbAnFirstSample;
        private System.Windows.Forms.TextBox tbAnFloor;
        private System.Windows.Forms.TextBox tbAnClip;
        private System.Windows.Forms.TextBox tbAnSignalFile;
        private System.Windows.Forms.Button btnAnSelectFile;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabPage tabDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDvDeviceName;
        private System.Windows.Forms.TabPage tabTransmit;
        private System.Windows.Forms.TextBox tbTxCount;
        private System.Windows.Forms.TextBox tbTxDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTxTransmit;
        private System.Windows.Forms.TabPage tabReceive;
        private System.Windows.Forms.Button btnRxSelectFile;
        private System.Windows.Forms.TextBox tbRxFilter;
        private System.Windows.Forms.TextBox tbRxFileName;
        private System.Windows.Forms.TextBox tbRxSyncTimeout;
        private System.Windows.Forms.TextBox tbRxNumXfers;
        private System.Windows.Forms.TextBox tbRxBufferSize;
        private System.Windows.Forms.TextBox tbRxNumBuffers;
        private System.Windows.Forms.ComboBox cbRxLnaGain;
        private System.Windows.Forms.TextBox tbRxBandwidth;
        private System.Windows.Forms.TextBox tbRxSampleRate;
        private System.Windows.Forms.TextBox tbRxFrequency;
        private System.Windows.Forms.Button btnFilterSelect;
        private System.Windows.Forms.ComboBox cbRxFormat;
        private System.Windows.Forms.RadioButton rbRxInputFiltered;
        private System.Windows.Forms.RadioButton rbRxInputRaw;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRxReceive;
        private System.Windows.Forms.ComboBox cbRxSDR;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox tbRxVGain2;
        private System.Windows.Forms.TextBox tbRxVGain1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbRxRecordTime;
        private System.Windows.Forms.SaveFileDialog rxSaveFileDialog;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox tbAnIWDelay;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.GroupBox gbAnStartBits;
        private System.Windows.Forms.TextBox tbAnSBDelay;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox tbAnStartBitContents;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnAnMakeDevice;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox tbDvWordPad;
        private System.Windows.Forms.ComboBox cbDvSyncEdge;
        private System.Windows.Forms.TextBox tbDvDataBitPeriod;
        private System.Windows.Forms.TextBox tbDvWidePW;
        private System.Windows.Forms.TextBox tbDvNarrowPW;
        private System.Windows.Forms.TextBox tbDvNumDataBits;
        private System.Windows.Forms.TextBox tbDvSbPad;
        private System.Windows.Forms.TextBox tbDvSbContents;
        private System.Windows.Forms.Button btnDvSaveDevice;
        private System.Windows.Forms.TextBox tbDvSampleRate;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Button btnDvSaveSignal;
        private System.Windows.Forms.Button btnDvMakeSignal;
        private System.Windows.Forms.TextBox tbDvWordContents;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Button btnDvOpenDevice;
        private System.Windows.Forms.Button btnDvNewDevice;
        private System.Windows.Forms.GroupBox gbDvDevice;
        private System.Windows.Forms.TextBox tbDvRepetitions;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Button btnTxOpenFile;
        private System.Windows.Forms.TextBox tbTxFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog txOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog dvOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog dvSaveFileDialog;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.TextBox tbTxVGain2;
        private System.Windows.Forms.TextBox tbTxVGain1;
        private System.Windows.Forms.TextBox tbTxBandwidth;
        private System.Windows.Forms.TextBox tbTxSampleRate;
        private System.Windows.Forms.TextBox tbTxFrequency;
        private System.Windows.Forms.ComboBox cbTxSDR;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox tbTxSyncTimeout;
        private System.Windows.Forms.TextBox tbTxNumXfers;
        private System.Windows.Forms.TextBox tbTxBufferSize;
        private System.Windows.Forms.TextBox tbTxNumBuffers;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.GroupBox gbTxOptions;
        private System.Windows.Forms.GroupBox gbTxSignalOptions;
        private System.Windows.Forms.Button btnTxViewSignal;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TextBox tbTxSdrStatus;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox tbRxSdrStatus;
        private System.Windows.Forms.GroupBox rbRxOptions;
    }
}

