namespace PythonTaskScheduler
{
    partial class EditForm
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
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ProjectName = new TextBox();
            Python_Interpreter = new TextBox();
            Text_Name = new TextBox();
            Text_Python_Interpreter = new TextBox();
            Text_Python_Script = new TextBox();
            Text_Execution_Freq = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            Python_Script = new TextBox();
            LastSuccessfulExecutionTime = new TextBox();
            ExecutionStatus = new TextBox();
            CreatedAt = new TextBox();
            NextExecutionTime = new TextBox();
            Save_Button = new Button();
            Options_Execution_Freq = new ComboBox();
            button_Select_Python_Script = new Button();
            button_Select_Python_Interpreter = new Button();
            Delete_Button = new Button();
            SuspendLayout();
            // 
            // ProjectName
            // 
            ProjectName.Location = new Point(177, 23);
            ProjectName.Name = "ProjectName";
            ProjectName.Size = new Size(232, 23);
            ProjectName.TabIndex = 0;
            // 
            // Python_Interpreter
            // 
            Python_Interpreter.Location = new Point(177, 66);
            Python_Interpreter.Name = "Python_Interpreter";
            Python_Interpreter.Size = new Size(232, 23);
            Python_Interpreter.TabIndex = 1;
            // 
            // Text_Name
            // 
            Text_Name.BorderStyle = BorderStyle.None;
            Text_Name.Location = new Point(59, 26);
            Text_Name.Name = "Text_Name";
            Text_Name.ReadOnly = true;
            Text_Name.Size = new Size(108, 16);
            Text_Name.TabIndex = 2;
            Text_Name.Text = "專案名稱";
            Text_Name.TextAlign = HorizontalAlignment.Center;
            // 
            // Text_Python_Interpreter
            // 
            Text_Python_Interpreter.BorderStyle = BorderStyle.None;
            Text_Python_Interpreter.Location = new Point(59, 69);
            Text_Python_Interpreter.Name = "Text_Python_Interpreter";
            Text_Python_Interpreter.ReadOnly = true;
            Text_Python_Interpreter.Size = new Size(108, 16);
            Text_Python_Interpreter.TabIndex = 3;
            Text_Python_Interpreter.Text = "Python 解譯器路徑";
            Text_Python_Interpreter.TextAlign = HorizontalAlignment.Center;
            // 
            // Text_Python_Script
            // 
            Text_Python_Script.BorderStyle = BorderStyle.None;
            Text_Python_Script.Location = new Point(59, 116);
            Text_Python_Script.Name = "Text_Python_Script";
            Text_Python_Script.ReadOnly = true;
            Text_Python_Script.Size = new Size(108, 16);
            Text_Python_Script.TabIndex = 5;
            Text_Python_Script.Text = "Python 腳本路徑";
            Text_Python_Script.TextAlign = HorizontalAlignment.Center;
            // 
            // Text_Execution_Freq
            // 
            Text_Execution_Freq.BorderStyle = BorderStyle.None;
            Text_Execution_Freq.Location = new Point(59, 158);
            Text_Execution_Freq.Name = "Text_Execution_Freq";
            Text_Execution_Freq.ReadOnly = true;
            Text_Execution_Freq.Size = new Size(108, 16);
            Text_Execution_Freq.TabIndex = 4;
            Text_Execution_Freq.Text = "執行頻率";
            Text_Execution_Freq.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Location = new Point(59, 197);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(108, 16);
            textBox3.TabIndex = 7;
            textBox3.Text = "創造日期";
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Location = new Point(59, 242);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(108, 16);
            textBox4.TabIndex = 6;
            textBox4.Text = "執行狀態";
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Location = new Point(59, 285);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(108, 16);
            textBox5.TabIndex = 9;
            textBox5.Text = "上次成功執行時間";
            textBox5.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Location = new Point(59, 327);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(108, 16);
            textBox6.TabIndex = 8;
            textBox6.Text = "下次執行時間";
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // Python_Script
            // 
            Python_Script.Location = new Point(177, 112);
            Python_Script.Name = "Python_Script";
            Python_Script.Size = new Size(232, 23);
            Python_Script.TabIndex = 10;
            // 
            // LastSuccessfulExecutionTime
            // 
            LastSuccessfulExecutionTime.Location = new Point(177, 282);
            LastSuccessfulExecutionTime.Name = "LastSuccessfulExecutionTime";
            LastSuccessfulExecutionTime.ReadOnly = true;
            LastSuccessfulExecutionTime.Size = new Size(232, 23);
            LastSuccessfulExecutionTime.TabIndex = 14;
            // 
            // ExecutionStatus
            // 
            ExecutionStatus.Location = new Point(177, 239);
            ExecutionStatus.Name = "ExecutionStatus";
            ExecutionStatus.ReadOnly = true;
            ExecutionStatus.Size = new Size(232, 23);
            ExecutionStatus.TabIndex = 13;
            // 
            // CreatedAt
            // 
            CreatedAt.Location = new Point(177, 193);
            CreatedAt.Name = "CreatedAt";
            CreatedAt.ReadOnly = true;
            CreatedAt.Size = new Size(232, 23);
            CreatedAt.TabIndex = 12;
            // 
            // NextExecutionTime
            // 
            NextExecutionTime.Location = new Point(177, 324);
            NextExecutionTime.Name = "NextExecutionTime";
            NextExecutionTime.Size = new Size(232, 23);
            NextExecutionTime.TabIndex = 15;
            // 
            // Save_Button
            // 
            Save_Button.BackColor = Color.FromArgb(0, 192, 0);
            Save_Button.FlatStyle = FlatStyle.Flat;
            Save_Button.Location = new Point(199, 375);
            Save_Button.Name = "Save_Button";
            Save_Button.Size = new Size(75, 23);
            Save_Button.TabIndex = 16;
            Save_Button.Text = "儲存";
            Save_Button.UseVisualStyleBackColor = false;
            Save_Button.Click += btnSave_Click;
            // 
            // Options_Execution_Freq
            // 
            Options_Execution_Freq.FormattingEnabled = true;
            Options_Execution_Freq.Items.AddRange(new object[] { "1分鐘", "3分鐘", "5分鐘", "1小時", "2小時", "6小時", "12小時", "1天", "1週" });
            Options_Execution_Freq.Location = new Point(177, 153);
            Options_Execution_Freq.Name = "Options_Execution_Freq";
            Options_Execution_Freq.Size = new Size(121, 23);
            Options_Execution_Freq.TabIndex = 18;
            Options_Execution_Freq.Text = "1天";
            // 
            // button_Select_Python_Script
            // 
            button_Select_Python_Script.BackColor = Color.Silver;
            button_Select_Python_Script.FlatStyle = FlatStyle.Flat;
            button_Select_Python_Script.Location = new Point(410, 112);
            button_Select_Python_Script.Name = "button_Select_Python_Script";
            button_Select_Python_Script.Size = new Size(75, 23);
            button_Select_Python_Script.TabIndex = 20;
            button_Select_Python_Script.Text = "選擇檔案";
            button_Select_Python_Script.UseVisualStyleBackColor = false;
            button_Select_Python_Script.Click += button_Select_Python_Script_Click;
            // 
            // button_Select_Python_Interpreter
            // 
            button_Select_Python_Interpreter.BackColor = Color.Silver;
            button_Select_Python_Interpreter.FlatStyle = FlatStyle.Flat;
            button_Select_Python_Interpreter.Location = new Point(410, 66);
            button_Select_Python_Interpreter.Name = "button_Select_Python_Interpreter";
            button_Select_Python_Interpreter.Size = new Size(75, 23);
            button_Select_Python_Interpreter.TabIndex = 19;
            button_Select_Python_Interpreter.Text = "選擇檔案";
            button_Select_Python_Interpreter.UseVisualStyleBackColor = false;
            button_Select_Python_Interpreter.Click += button_Select_Python_Interpreter_Click;
            // 
            // Delete_Button
            // 
            Delete_Button.BackColor = Color.Red;
            Delete_Button.FlatStyle = FlatStyle.Flat;
            Delete_Button.Location = new Point(389, 375);
            Delete_Button.Name = "Delete_Button";
            Delete_Button.Size = new Size(75, 23);
            Delete_Button.TabIndex = 21;
            Delete_Button.Text = "刪除";
            Delete_Button.UseVisualStyleBackColor = false;
            Delete_Button.Click += btnDelete_Click;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 450);
            Controls.Add(Delete_Button);
            Controls.Add(button_Select_Python_Script);
            Controls.Add(button_Select_Python_Interpreter);
            Controls.Add(Options_Execution_Freq);
            Controls.Add(Save_Button);
            Controls.Add(NextExecutionTime);
            Controls.Add(LastSuccessfulExecutionTime);
            Controls.Add(ExecutionStatus);
            Controls.Add(CreatedAt);
            Controls.Add(Python_Script);
            Controls.Add(textBox5);
            Controls.Add(textBox6);
            Controls.Add(textBox3);
            Controls.Add(textBox4);
            Controls.Add(Text_Python_Script);
            Controls.Add(Text_Execution_Freq);
            Controls.Add(Text_Python_Interpreter);
            Controls.Add(Text_Name);
            Controls.Add(Python_Interpreter);
            Controls.Add(ProjectName);
            Name = "EditForm";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ProjectName;
        private TextBox Python_Interpreter;
        private TextBox Text_Name;
        private TextBox Text_Python_Interpreter;
        private TextBox Text_Python_Script;
        private TextBox Text_Execution_Freq;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox Python_Script;
        private TextBox LastSuccessfulExecutionTime;
        private TextBox ExecutionStatus;
        private TextBox CreatedAt;
        private TextBox NextExecutionTime;
        private Button Save_Button;
        private ComboBox Options_Execution_Freq;
        private Button button_Select_Python_Script;
        private Button button_Select_Python_Interpreter;
        private Button Delete_Button;
    }
}