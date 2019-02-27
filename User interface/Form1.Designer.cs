namespace User_interface
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
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.FileFinder = new System.Windows.Forms.Button();
            this.Analyser = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OutputFileNameBox = new System.Windows.Forms.TextBox();
            this.Finished = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(65, 160);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(362, 22);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "Please type the file name for the output in the box below";
            // 
            // FileFinder
            // 
            this.FileFinder.Location = new System.Drawing.Point(65, 85);
            this.FileFinder.Name = "FileFinder";
            this.FileFinder.Size = new System.Drawing.Size(362, 32);
            this.FileFinder.TabIndex = 3;
            this.FileFinder.Text = "Please select the file you wish you analyse";
            this.FileFinder.UseVisualStyleBackColor = true;
            this.FileFinder.Click += new System.EventHandler(this.FileFinder_Click);
            // 
            // Analyser
            // 
            this.Analyser.Location = new System.Drawing.Point(347, 222);
            this.Analyser.Name = "Analyser";
            this.Analyser.Size = new System.Drawing.Size(80, 42);
            this.Analyser.TabIndex = 4;
            this.Analyser.Text = "analyse";
            this.Analyser.UseVisualStyleBackColor = true;
            this.Analyser.Click += new System.EventHandler(this.Analyser_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 125);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(362, 22);
            this.textBox1.TabIndex = 5;
            // 
            // OutputFileNameBox
            // 
            this.OutputFileNameBox.Location = new System.Drawing.Point(65, 194);
            this.OutputFileNameBox.Name = "OutputFileNameBox";
            this.OutputFileNameBox.Size = new System.Drawing.Size(362, 22);
            this.OutputFileNameBox.TabIndex = 6;
            this.OutputFileNameBox.TextChanged += new System.EventHandler(this.OutputFileNameBox_TextChanged);
            // 
            // Finished
            // 
            this.Finished.Location = new System.Drawing.Point(347, 270);
            this.Finished.Name = "Finished";
            this.Finished.Size = new System.Drawing.Size(80, 22);
            this.Finished.TabIndex = 7;
            this.Finished.TextChanged += new System.EventHandler(this.Finished_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 314);
            this.Controls.Add(this.Finished);
            this.Controls.Add(this.OutputFileNameBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Analyser);
            this.Controls.Add(this.FileFinder);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button FileFinder;
        private System.Windows.Forms.Button Analyser;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox OutputFileNameBox;
        private System.Windows.Forms.TextBox Finished;
    }
}

