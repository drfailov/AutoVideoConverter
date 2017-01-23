namespace FP_Converter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelReports = new System.Windows.Forms.Label();
            this.labelErrors = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDelSelected = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBoxRequest = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(10, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(560, 441);
            this.listBox1.TabIndex = 0;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.labelReports);
            this.panel1.Controls.Add(this.labelErrors);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonDelSelected);
            this.panel1.Controls.Add(this.buttonGo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(570, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 463);
            this.panel1.TabIndex = 1;
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(91, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(95, 39);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "STOP";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelReports
            // 
            this.labelReports.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelReports.Location = new System.Drawing.Point(6, 383);
            this.labelReports.Name = "labelReports";
            this.labelReports.Size = new System.Drawing.Size(159, 33);
            this.labelReports.TabIndex = 5;
            this.labelReports.Text = "No reports";
            this.labelReports.Click += new System.EventHandler(this.labelReports_Click);
            // 
            // labelErrors
            // 
            this.labelErrors.ForeColor = System.Drawing.Color.Maroon;
            this.labelErrors.Location = new System.Drawing.Point(7, 339);
            this.labelErrors.Name = "labelErrors";
            this.labelErrors.Size = new System.Drawing.Size(159, 33);
            this.labelErrors.TabIndex = 4;
            this.labelErrors.Text = "No errors";
            this.labelErrors.Click += new System.EventHandler(this.labelErrors_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 196);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Old files will be owerwriten";
            // 
            // buttonDelSelected
            // 
            this.buttonDelSelected.Location = new System.Drawing.Point(3, 430);
            this.buttonDelSelected.Name = "buttonDelSelected";
            this.buttonDelSelected.Size = new System.Drawing.Size(112, 26);
            this.buttonDelSelected.TabIndex = 1;
            this.buttonDelSelected.Text = "Del selected";
            this.buttonDelSelected.UseVisualStyleBackColor = true;
            this.buttonDelSelected.Click += new System.EventHandler(this.buttonDelSelected_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(6, 3);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 39);
            this.buttonGo.TabIndex = 0;
            this.buttonGo.Text = "GO";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // textBoxRequest
            // 
            this.textBoxRequest.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxRequest.Location = new System.Drawing.Point(10, 10);
            this.textBoxRequest.Name = "textBoxRequest";
            this.textBoxRequest.Size = new System.Drawing.Size(560, 22);
            this.textBoxRequest.TabIndex = 2;
            this.textBoxRequest.Text = "ffmpeg.exe -i \"(INFILE)\" -c:v libx265 -preset slow -crf 30 -c:a aac -b:a 160k \"(O" +
    "UTFILE)\"";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 483);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBoxRequest);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "FP Converter";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonDelSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelErrors;
        private System.Windows.Forms.Label labelReports;
        private System.Windows.Forms.TextBox textBoxRequest;
        private System.Windows.Forms.Button buttonStop;
    }
}

