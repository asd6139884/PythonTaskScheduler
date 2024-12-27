namespace PythonTaskScheduler
{
    partial class PythonTaskScheduler
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TabControl1 = new TabControl();
            ScheduleManagementPage = new TabPage();
            NowTime_ScheduleManagementPage = new Label();
            dataGridView1 = new DataGridView();
            SettingPage = new TabPage();
            NowTime_SettingPage = new Label();
            ErrorMessage = new TextBox();
            Cancel_Button = new Button();
            Options_Execution_Freq = new ComboBox();
            button_Select_Python_Script = new Button();
            Show_Python_Script = new TextBox();
            button_Select_Python_Interpreter = new Button();
            Show_Python_Interpreter = new TextBox();
            Input_Name = new TextBox();
            Save_Button = new Button();
            Text_Python_Script = new TextBox();
            Text_Python_Interpreter = new TextBox();
            Text_Name = new TextBox();
            Text_Execution_Freq = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            專案名稱 = new DataGridViewTextBoxColumn();
            執行狀態 = new DataGridViewTextBoxColumn();
            上次執行成功時間 = new DataGridViewTextBoxColumn();
            下次執行時間 = new DataGridViewTextBoxColumn();
            立即執行 = new DataGridViewTextBoxColumn();
            TabControl1.SuspendLayout();
            ScheduleManagementPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SettingPage.SuspendLayout();
            SuspendLayout();
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(ScheduleManagementPage);
            TabControl1.Controls.Add(SettingPage);
            TabControl1.Location = new Point(1, 1);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(798, 451);
            TabControl1.TabIndex = 0;
            // 
            // ScheduleManagementPage
            // 
            ScheduleManagementPage.BackColor = SystemColors.Control;
            ScheduleManagementPage.Controls.Add(NowTime_ScheduleManagementPage);
            ScheduleManagementPage.Controls.Add(dataGridView1);
            ScheduleManagementPage.Location = new Point(4, 24);
            ScheduleManagementPage.Name = "ScheduleManagementPage";
            ScheduleManagementPage.Padding = new Padding(3);
            ScheduleManagementPage.Size = new Size(790, 423);
            ScheduleManagementPage.TabIndex = 0;
            ScheduleManagementPage.Text = "排程管理";
            ScheduleManagementPage.Click += ScheduleManagementPage_Click;
            // 
            // NowTime_ScheduleManagementPage
            // 
            NowTime_ScheduleManagementPage.AutoSize = true;
            NowTime_ScheduleManagementPage.Location = new Point(644, 15);
            NowTime_ScheduleManagementPage.Name = "NowTime_ScheduleManagementPage";
            NowTime_ScheduleManagementPage.Size = new Size(42, 15);
            NowTime_ScheduleManagementPage.TabIndex = 1;
            NowTime_ScheduleManagementPage.Text = "label1";
            NowTime_ScheduleManagementPage.Click += label1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 專案名稱, 執行狀態, 上次執行成功時間, 下次執行時間, 立即執行 });
            dataGridView1.Location = new Point(0, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(626, 279);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // SettingPage
            // 
            SettingPage.BackColor = SystemColors.Control;
            SettingPage.Controls.Add(NowTime_SettingPage);
            SettingPage.Controls.Add(ErrorMessage);
            SettingPage.Controls.Add(Cancel_Button);
            SettingPage.Controls.Add(Options_Execution_Freq);
            SettingPage.Controls.Add(button_Select_Python_Script);
            SettingPage.Controls.Add(Show_Python_Script);
            SettingPage.Controls.Add(button_Select_Python_Interpreter);
            SettingPage.Controls.Add(Show_Python_Interpreter);
            SettingPage.Controls.Add(Input_Name);
            SettingPage.Controls.Add(Save_Button);
            SettingPage.Controls.Add(Text_Python_Script);
            SettingPage.Controls.Add(Text_Python_Interpreter);
            SettingPage.Controls.Add(Text_Name);
            SettingPage.Controls.Add(Text_Execution_Freq);
            SettingPage.Location = new Point(4, 24);
            SettingPage.Name = "SettingPage";
            SettingPage.Padding = new Padding(3);
            SettingPage.Size = new Size(790, 423);
            SettingPage.TabIndex = 1;
            SettingPage.Text = "設定";
            SettingPage.Click += SettingPage_Click;
            // 
            // NowTime_SettingPage
            // 
            NowTime_SettingPage.AutoSize = true;
            NowTime_SettingPage.Location = new Point(644, 15);
            NowTime_SettingPage.Name = "NowTime_SettingPage";
            NowTime_SettingPage.Size = new Size(42, 15);
            NowTime_SettingPage.TabIndex = 2;
            NowTime_SettingPage.Text = "label1";
            // 
            // ErrorMessage
            // 
            ErrorMessage.BackColor = SystemColors.Control;
            ErrorMessage.BorderStyle = BorderStyle.None;
            ErrorMessage.ForeColor = Color.Red;
            ErrorMessage.Location = new Point(271, 231);
            ErrorMessage.Name = "ErrorMessage";
            ErrorMessage.Size = new Size(273, 16);
            ErrorMessage.TabIndex = 13;
            ErrorMessage.TextChanged += ErrorMessage_TextChanged;
            // 
            // Cancel_Button
            // 
            Cancel_Button.BackColor = Color.FromArgb(255, 128, 128);
            Cancel_Button.FlatStyle = FlatStyle.Flat;
            Cancel_Button.Location = new Point(379, 324);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(75, 23);
            Cancel_Button.TabIndex = 12;
            Cancel_Button.Text = "取消";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // Options_Execution_Freq
            // 
            Options_Execution_Freq.FormattingEnabled = true;
            Options_Execution_Freq.Items.AddRange(new object[] { "1分鐘", "5分鐘", "1小時", "2小時", "6小時", "12小時", "1天", "1週" });
            Options_Execution_Freq.Location = new Point(233, 181);
            Options_Execution_Freq.Name = "Options_Execution_Freq";
            Options_Execution_Freq.Size = new Size(121, 23);
            Options_Execution_Freq.TabIndex = 11;
            Options_Execution_Freq.Text = "1天";
            // 
            // button_Select_Python_Script
            // 
            button_Select_Python_Script.BackColor = Color.Silver;
            button_Select_Python_Script.FlatStyle = FlatStyle.Flat;
            button_Select_Python_Script.Location = new Point(545, 143);
            button_Select_Python_Script.Name = "button_Select_Python_Script";
            button_Select_Python_Script.Size = new Size(75, 23);
            button_Select_Python_Script.TabIndex = 10;
            button_Select_Python_Script.Text = "選擇檔案";
            button_Select_Python_Script.UseVisualStyleBackColor = false;
            button_Select_Python_Script.Click += button_Select_Python_Script_Click;
            // 
            // Show_Python_Script
            // 
            Show_Python_Script.Location = new Point(233, 143);
            Show_Python_Script.Name = "Show_Python_Script";
            Show_Python_Script.Size = new Size(311, 23);
            Show_Python_Script.TabIndex = 9;
            // 
            // button_Select_Python_Interpreter
            // 
            button_Select_Python_Interpreter.BackColor = Color.Silver;
            button_Select_Python_Interpreter.FlatStyle = FlatStyle.Flat;
            button_Select_Python_Interpreter.Location = new Point(545, 106);
            button_Select_Python_Interpreter.Name = "button_Select_Python_Interpreter";
            button_Select_Python_Interpreter.Size = new Size(75, 23);
            button_Select_Python_Interpreter.TabIndex = 8;
            button_Select_Python_Interpreter.Text = "選擇檔案";
            button_Select_Python_Interpreter.UseVisualStyleBackColor = false;
            button_Select_Python_Interpreter.Click += button_Select_Python_Interpreter_Click;
            // 
            // Show_Python_Interpreter
            // 
            Show_Python_Interpreter.Location = new Point(233, 106);
            Show_Python_Interpreter.Name = "Show_Python_Interpreter";
            Show_Python_Interpreter.Size = new Size(311, 23);
            Show_Python_Interpreter.TabIndex = 7;
            // 
            // Input_Name
            // 
            Input_Name.Location = new Point(233, 68);
            Input_Name.Name = "Input_Name";
            Input_Name.Size = new Size(311, 23);
            Input_Name.TabIndex = 6;
            Input_Name.TextChanged += Input_Name_TextChanged;
            // 
            // Save_Button
            // 
            Save_Button.BackColor = Color.FromArgb(0, 192, 0);
            Save_Button.FlatStyle = FlatStyle.Flat;
            Save_Button.Location = new Point(279, 324);
            Save_Button.Name = "Save_Button";
            Save_Button.Size = new Size(75, 23);
            Save_Button.TabIndex = 4;
            Save_Button.Text = "儲存";
            Save_Button.UseVisualStyleBackColor = false;
            Save_Button.Click += Save_Button_Click;
            // 
            // Text_Python_Script
            // 
            Text_Python_Script.BorderStyle = BorderStyle.None;
            Text_Python_Script.Location = new Point(80, 150);
            Text_Python_Script.Name = "Text_Python_Script";
            Text_Python_Script.ReadOnly = true;
            Text_Python_Script.Size = new Size(108, 16);
            Text_Python_Script.TabIndex = 3;
            Text_Python_Script.Text = "Python 腳本路徑";
            Text_Python_Script.TextAlign = HorizontalAlignment.Center;
            // 
            // Text_Python_Interpreter
            // 
            Text_Python_Interpreter.BorderStyle = BorderStyle.None;
            Text_Python_Interpreter.Location = new Point(80, 113);
            Text_Python_Interpreter.Name = "Text_Python_Interpreter";
            Text_Python_Interpreter.ReadOnly = true;
            Text_Python_Interpreter.Size = new Size(108, 16);
            Text_Python_Interpreter.TabIndex = 2;
            Text_Python_Interpreter.Text = "Python 解譯器路徑";
            Text_Python_Interpreter.TextAlign = HorizontalAlignment.Center;
            // 
            // Text_Name
            // 
            Text_Name.BorderStyle = BorderStyle.None;
            Text_Name.Location = new Point(80, 75);
            Text_Name.Name = "Text_Name";
            Text_Name.ReadOnly = true;
            Text_Name.Size = new Size(108, 16);
            Text_Name.TabIndex = 1;
            Text_Name.Text = "專案名稱";
            Text_Name.TextAlign = HorizontalAlignment.Center;
            Text_Name.TextChanged += textBox1_TextChanged;
            // 
            // Text_Execution_Freq
            // 
            Text_Execution_Freq.BorderStyle = BorderStyle.None;
            Text_Execution_Freq.Location = new Point(80, 186);
            Text_Execution_Freq.Name = "Text_Execution_Freq";
            Text_Execution_Freq.ReadOnly = true;
            Text_Execution_Freq.Size = new Size(108, 16);
            Text_Execution_Freq.TabIndex = 0;
            Text_Execution_Freq.Text = "執行頻率";
            Text_Execution_Freq.TextAlign = HorizontalAlignment.Center;
            Text_Execution_Freq.TextChanged += ExecutionFrequency_TextChanged;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // 專案名稱
            // 
            專案名稱.HeaderText = "專案名稱";
            專案名稱.Name = "專案名稱";
            專案名稱.ReadOnly = true;
            // 
            // 執行狀態
            // 
            執行狀態.HeaderText = "執行狀態";
            執行狀態.Name = "執行狀態";
            執行狀態.ReadOnly = true;
            // 
            // 上次執行成功時間
            // 
            上次執行成功時間.HeaderText = "上次執行成功時間";
            上次執行成功時間.Name = "上次執行成功時間";
            上次執行成功時間.ReadOnly = true;
            上次執行成功時間.Width = 120;
            // 
            // 下次執行時間
            // 
            下次執行時間.HeaderText = "下次執行時間";
            下次執行時間.Name = "下次執行時間";
            下次執行時間.ReadOnly = true;
            下次執行時間.Width = 120;
            // 
            // 立即執行
            // 
            立即執行.HeaderText = "立即執行";
            立即執行.Name = "立即執行";
            立即執行.ReadOnly = true;
            // 
            // PythonTaskScheduler
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(801, 450);
            Controls.Add(TabControl1);
            ForeColor = SystemColors.ControlText;
            Name = "PythonTaskScheduler";
            Text = "Python Task Scheduler";
            Load += Form1_Load;
            TabControl1.ResumeLayout(false);
            ScheduleManagementPage.ResumeLayout(false);
            ScheduleManagementPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            SettingPage.ResumeLayout(false);
            SettingPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl TabControl1;
        private TabPage ScheduleManagementPage;
        private TabPage SettingPage;
        private TextBox Text_Execution_Freq;
        private DataGridView dataGridView1;
        private TextBox Text_Name;
        private Button Save_Button;
        private TextBox Text_Python_Script;
        private TextBox Text_Python_Interpreter;
        private TextBox Input_Name;
        private OpenFileDialog openFileDialog1;
        private Button button_Select_Python_Interpreter;
        private TextBox Show_Python_Interpreter;
        private Button button_Select_Python_Script;
        private TextBox Show_Python_Script;
        private ComboBox Options_Execution_Freq;
        private Button Cancel_Button;
        private TextBox ErrorMessage;
        private ContextMenuStrip contextMenuStrip1;
        private Label NowTime_ScheduleManagementPage;
        private Label NowTime_SettingPage;
        private DataGridViewTextBoxColumn 專案名稱;
        private DataGridViewTextBoxColumn 執行狀態;
        private DataGridViewTextBoxColumn 上次執行成功時間;
        private DataGridViewTextBoxColumn 下次執行時間;
        private DataGridViewTextBoxColumn 立即執行;
    }
}
