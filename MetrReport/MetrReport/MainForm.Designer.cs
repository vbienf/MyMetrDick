namespace PerformanceLab.Utils.ReportMaker
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openJMeterLogDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAggregateReport = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.toTime = new System.Windows.Forms.MaskedTextBox();
            this.fromTime = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericEffort = new System.Windows.Forms.NumericUpDown();
            this.addMetFilesButton = new System.Windows.Forms.Button();
            this.metricsGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabJmeterReport = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericRump = new System.Windows.Forms.NumericUpDown();
            this.numericEffortJM = new System.Windows.Forms.NumericUpDown();
            this.addJMeterFilesBut = new System.Windows.Forms.Button();
            this.JmeterGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxLimit = new System.Windows.Forms.CheckBox();
            this.ToTimeJM = new System.Windows.Forms.MaskedTextBox();
            this.fromTimeJM = new System.Windows.Forms.MaskedTextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.selectXLSXFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.metrFoldBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelMetrics = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBarThread = new System.Windows.Forms.ProgressBar();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonMakeXLSX = new System.Windows.Forms.Button();
            this.checkBoxTime = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabAggregateReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEffort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metricsGridView)).BeginInit();
            this.tabJmeterReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEffortJM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JmeterGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openJMeterLogDialog
            // 
            this.openJMeterLogDialog.FilterIndex = 0;
            this.openJMeterLogDialog.Multiselect = true;
            this.openJMeterLogDialog.RestoreDirectory = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabAggregateReport);
            this.tabControl.Controls.Add(this.tabJmeterReport);
            this.tabControl.Location = new System.Drawing.Point(12, 5);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(3, 3);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(752, 223);
            this.tabControl.TabIndex = 10;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabAggregateReport
            // 
            this.tabAggregateReport.Controls.Add(this.checkBoxTime);
            this.tabAggregateReport.Controls.Add(this.label2);
            this.tabAggregateReport.Controls.Add(this.toTime);
            this.tabAggregateReport.Controls.Add(this.fromTime);
            this.tabAggregateReport.Controls.Add(this.label3);
            this.tabAggregateReport.Controls.Add(this.numericUpDown1);
            this.tabAggregateReport.Controls.Add(this.label1);
            this.tabAggregateReport.Controls.Add(this.numericEffort);
            this.tabAggregateReport.Controls.Add(this.addMetFilesButton);
            this.tabAggregateReport.Controls.Add(this.metricsGridView);
            this.tabAggregateReport.Location = new System.Drawing.Point(4, 22);
            this.tabAggregateReport.Name = "tabAggregateReport";
            this.tabAggregateReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabAggregateReport.Size = new System.Drawing.Size(744, 197);
            this.tabAggregateReport.TabIndex = 0;
            this.tabAggregateReport.Text = "Lunix/HP-UX metrics";
            this.tabAggregateReport.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(567, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Точное время снимаемых метрик";
            // 
            // toTime
            // 
            this.toTime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.toTime.Location = new System.Drawing.Point(577, 162);
            this.toTime.Mask = "00/00/0000 00:00:00";
            this.toTime.Name = "toTime";
            this.toTime.PromptChar = '@';
            this.toTime.Size = new System.Drawing.Size(154, 20);
            this.toTime.TabIndex = 6;
            // 
            // fromTime
            // 
            this.fromTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.fromTime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.fromTime.Location = new System.Drawing.Point(577, 143);
            this.fromTime.Mask = "00/00/0000 00:00:00";
            this.fromTime.Name = "fromTime";
            this.fromTime.PromptChar = '@';
            this.fromTime.Size = new System.Drawing.Size(154, 20);
            this.fromTime.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(661, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Номер теста";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(573, 49);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(84, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(661, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Профиль";
            // 
            // numericEffort
            // 
            this.numericEffort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericEffort.Location = new System.Drawing.Point(573, 72);
            this.numericEffort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericEffort.Name = "numericEffort";
            this.numericEffort.Size = new System.Drawing.Size(84, 20);
            this.numericEffort.TabIndex = 2;
            // 
            // addMetFilesButton
            // 
            this.addMetFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addMetFilesButton.Location = new System.Drawing.Point(574, 6);
            this.addMetFilesButton.Name = "addMetFilesButton";
            this.addMetFilesButton.Size = new System.Drawing.Size(164, 39);
            this.addMetFilesButton.TabIndex = 0;
            this.addMetFilesButton.Text = "Добавить метрики";
            this.addMetFilesButton.UseVisualStyleBackColor = true;
            this.addMetFilesButton.Click += new System.EventHandler(this.addMetFilesButton_Click);
            // 
            // metricsGridView
            // 
            this.metricsGridView.AllowDrop = true;
            this.metricsGridView.AllowUserToAddRows = false;
            this.metricsGridView.AllowUserToDeleteRows = false;
            this.metricsGridView.AllowUserToResizeRows = false;
            this.metricsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metricsGridView.BackgroundColor = System.Drawing.SystemColors.Info;
            this.metricsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.metricsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.MDelete});
            this.metricsGridView.Location = new System.Drawing.Point(3, 9);
            this.metricsGridView.MultiSelect = false;
            this.metricsGridView.Name = "metricsGridView";
            this.metricsGridView.RowHeadersVisible = false;
            this.metricsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metricsGridView.Size = new System.Drawing.Size(561, 178);
            this.metricsGridView.TabIndex = 9;
            this.metricsGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.metricsGridView_CellMouseClick);
            this.metricsGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.metricsGridView_RowsAdded);
            this.metricsGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.metricsGridView_DragDrop);
            this.metricsGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.metricsGridView_DragEnter);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Path";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Путь к логу";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.HeaderText = "Label";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "Имя соответствующего листа в результирующем отчете";
            this.dataGridViewTextBoxColumn2.Width = 58;
            // 
            // MDelete
            // 
            this.MDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MDelete.HeaderText = "Delete?";
            this.MDelete.Name = "MDelete";
            this.MDelete.ReadOnly = true;
            this.MDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MDelete.Text = "X";
            this.MDelete.ToolTipText = "Удалить из списка";
            this.MDelete.UseColumnTextForButtonValue = true;
            this.MDelete.Width = 50;
            // 
            // tabJmeterReport
            // 
            this.tabJmeterReport.Controls.Add(this.label5);
            this.tabJmeterReport.Controls.Add(this.numericUpDown2);
            this.tabJmeterReport.Controls.Add(this.label6);
            this.tabJmeterReport.Controls.Add(this.label7);
            this.tabJmeterReport.Controls.Add(this.numericRump);
            this.tabJmeterReport.Controls.Add(this.numericEffortJM);
            this.tabJmeterReport.Controls.Add(this.addJMeterFilesBut);
            this.tabJmeterReport.Controls.Add(this.JmeterGridView);
            this.tabJmeterReport.Controls.Add(this.groupBox2);
            this.tabJmeterReport.Location = new System.Drawing.Point(4, 22);
            this.tabJmeterReport.Name = "tabJmeterReport";
            this.tabJmeterReport.Size = new System.Drawing.Size(744, 197);
            this.tabJmeterReport.TabIndex = 1;
            this.tabJmeterReport.Text = "Jmeter metrics";
            this.tabJmeterReport.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(661, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Номер теста";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown2.Location = new System.Drawing.Point(573, 48);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(84, 20);
            this.numericUpDown2.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(660, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Разгон (мин)";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(661, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Профиль";
            // 
            // numericRump
            // 
            this.numericRump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericRump.Enabled = false;
            this.numericRump.Location = new System.Drawing.Point(573, 92);
            this.numericRump.Name = "numericRump";
            this.numericRump.Size = new System.Drawing.Size(84, 20);
            this.numericRump.TabIndex = 19;
            // 
            // numericEffortJM
            // 
            this.numericEffortJM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericEffortJM.Location = new System.Drawing.Point(573, 71);
            this.numericEffortJM.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericEffortJM.Name = "numericEffortJM";
            this.numericEffortJM.Size = new System.Drawing.Size(84, 20);
            this.numericEffortJM.TabIndex = 18;
            // 
            // addJMeterFilesBut
            // 
            this.addJMeterFilesBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addJMeterFilesBut.Location = new System.Drawing.Point(574, 5);
            this.addJMeterFilesBut.Name = "addJMeterFilesBut";
            this.addJMeterFilesBut.Size = new System.Drawing.Size(164, 39);
            this.addJMeterFilesBut.TabIndex = 16;
            this.addJMeterFilesBut.Text = "Добавить метрики";
            this.addJMeterFilesBut.UseVisualStyleBackColor = true;
            this.addJMeterFilesBut.Click += new System.EventHandler(this.addMetFilesButton_Click);
            // 
            // JmeterGridView
            // 
            this.JmeterGridView.AllowDrop = true;
            this.JmeterGridView.AllowUserToAddRows = false;
            this.JmeterGridView.AllowUserToDeleteRows = false;
            this.JmeterGridView.AllowUserToResizeRows = false;
            this.JmeterGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JmeterGridView.BackgroundColor = System.Drawing.SystemColors.Info;
            this.JmeterGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JmeterGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewButtonColumn1});
            this.JmeterGridView.Location = new System.Drawing.Point(3, 9);
            this.JmeterGridView.MultiSelect = false;
            this.JmeterGridView.Name = "JmeterGridView";
            this.JmeterGridView.RowHeadersVisible = false;
            this.JmeterGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.JmeterGridView.Size = new System.Drawing.Size(561, 178);
            this.JmeterGridView.TabIndex = 21;
            this.JmeterGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.metricsGridView_CellMouseClick);
            this.JmeterGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.metricsGridView_RowsAdded);
            this.JmeterGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.metricsGridView_DragDrop);
            this.JmeterGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.metricsGridView_DragEnter);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Path";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.ToolTipText = "Путь к логу";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "Label";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ToolTipText = "Имя соответствующего листа в результирующем отчете";
            this.dataGridViewTextBoxColumn4.Width = 58;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewButtonColumn1.HeaderText = "Delete?";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewButtonColumn1.Text = "X";
            this.dataGridViewButtonColumn1.ToolTipText = "Удалить из списка";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 50;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxLimit);
            this.groupBox2.Controls.Add(this.ToTimeJM);
            this.groupBox2.Controls.Add(this.fromTimeJM);
            this.groupBox2.Location = new System.Drawing.Point(573, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 76);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // checkBoxLimit
            // 
            this.checkBoxLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLimit.AutoSize = true;
            this.checkBoxLimit.Checked = true;
            this.checkBoxLimit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLimit.Location = new System.Drawing.Point(13, 9);
            this.checkBoxLimit.Name = "checkBoxLimit";
            this.checkBoxLimit.Size = new System.Drawing.Size(147, 17);
            this.checkBoxLimit.TabIndex = 4;
            this.checkBoxLimit.Text = "Ограничить по времени";
            this.checkBoxLimit.UseVisualStyleBackColor = true;
            this.checkBoxLimit.CheckedChanged += new System.EventHandler(this.checkBoxLimit_CheckedChanged);
            // 
            // ToTimeJM
            // 
            this.ToTimeJM.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.ToTimeJM.Location = new System.Drawing.Point(6, 46);
            this.ToTimeJM.Mask = "00/00/0000 00:00:00";
            this.ToTimeJM.Name = "ToTimeJM";
            this.ToTimeJM.PromptChar = '@';
            this.ToTimeJM.Size = new System.Drawing.Size(154, 20);
            this.ToTimeJM.TabIndex = 6;
            // 
            // fromTimeJM
            // 
            this.fromTimeJM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.fromTimeJM.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.fromTimeJM.Location = new System.Drawing.Point(6, 27);
            this.fromTimeJM.Mask = "00/00/0000 00:00:00";
            this.fromTimeJM.Name = "fromTimeJM";
            this.fromTimeJM.PromptChar = '@';
            this.fromTimeJM.Size = new System.Drawing.Size(154, 20);
            this.fromTimeJM.TabIndex = 5;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // metrFoldBrowserDialog
            // 
            this.metrFoldBrowserDialog.RootFolder = System.Environment.SpecialFolder.DesktopDirectory;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelMetrics
            // 
            this.labelMetrics.AutoSize = true;
            this.labelMetrics.Location = new System.Drawing.Point(84, 265);
            this.labelMetrics.Name = "labelMetrics";
            this.labelMetrics.Size = new System.Drawing.Size(28, 13);
            this.labelMetrics.TabIndex = 27;
            this.labelMetrics.Text = "Text";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Прогресс:";
            // 
            // progressBarThread
            // 
            this.progressBarThread.Location = new System.Drawing.Point(22, 293);
            this.progressBarThread.Name = "progressBarThread";
            this.progressBarThread.Size = new System.Drawing.Size(558, 23);
            this.progressBarThread.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarThread.TabIndex = 25;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(627, 234);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(84, 42);
            this.buttonClear.TabIndex = 23;
            this.buttonClear.Text = "Очистить все";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonMakeXLSX
            // 
            this.buttonMakeXLSX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMakeXLSX.Location = new System.Drawing.Point(589, 284);
            this.buttonMakeXLSX.Name = "buttonMakeXLSX";
            this.buttonMakeXLSX.Size = new System.Drawing.Size(164, 32);
            this.buttonMakeXLSX.TabIndex = 24;
            this.buttonMakeXLSX.Text = "Хочу отчет!";
            this.buttonMakeXLSX.UseVisualStyleBackColor = true;
            this.buttonMakeXLSX.Click += new System.EventHandler(this.buttonMakeXLSX_Click);
            // 
            // checkBoxTime
            // 
            this.checkBoxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTime.AutoSize = true;
            this.checkBoxTime.Location = new System.Drawing.Point(575, 103);
            this.checkBoxTime.Name = "checkBoxTime";
            this.checkBoxTime.Size = new System.Drawing.Size(174, 17);
            this.checkBoxTime.TabIndex = 17;
            this.checkBoxTime.Text = "Не ограничивать по времени";
            this.checkBoxTime.UseVisualStyleBackColor = true;
            this.checkBoxTime.CheckedChanged += new System.EventHandler(this.checkBoxTime_CheckedChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(767, 357);
            this.Controls.Add(this.labelMetrics);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBarThread);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonMakeXLSX);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(538, 382);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Maker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabAggregateReport.ResumeLayout(false);
            this.tabAggregateReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEffort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metricsGridView)).EndInit();
            this.tabJmeterReport.ResumeLayout(false);
            this.tabJmeterReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRump)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEffortJM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JmeterGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openJMeterLogDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAggregateReport;
        private System.Windows.Forms.MaskedTextBox fromTime;
        private System.Windows.Forms.MaskedTextBox toTime;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.FolderBrowserDialog selectXLSXFolderDialog;
        private System.Windows.Forms.Button addMetFilesButton;
        private System.Windows.Forms.DataGridView metricsGridView;
        private System.Windows.Forms.FolderBrowserDialog metrFoldBrowserDialog;
        private System.Windows.Forms.NumericUpDown numericEffort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn MDelete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabJmeterReport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericRump;
        private System.Windows.Forms.NumericUpDown numericEffortJM;
        private System.Windows.Forms.Button addJMeterFilesBut;
        private System.Windows.Forms.DataGridView JmeterGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxLimit;
        private System.Windows.Forms.MaskedTextBox ToTimeJM;
        private System.Windows.Forms.MaskedTextBox fromTimeJM;
        private System.Windows.Forms.Label labelMetrics;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ProgressBar progressBarThread;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonMakeXLSX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxTime;
    }
}

