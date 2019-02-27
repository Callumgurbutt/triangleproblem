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
            this.fileFinder = new System.Windows.Forms.Button();
            this.analyser = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.outputFileNameBox = new System.Windows.Forms.TextBox();
            this.finished = new System.Windows.Forms.TextBox();
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
            // fileFinder
            // 
            this.fileFinder.Location = new System.Drawing.Point(65, 85);
            this.fileFinder.Name = "fileFinder";
            this.fileFinder.Size = new System.Drawing.Size(362, 32);
            this.fileFinder.TabIndex = 3;
            this.fileFinder.Text = "Please select the file you wish you analyse";
            this.fileFinder.UseVisualStyleBackColor = true;
            this.fileFinder.Click += new System.EventHandler(this.fileFinder_Click);
            // 
            // analyser
            // 
            this.analyser.Location = new System.Drawing.Point(347, 222);
            this.analyser.Name = "analyser";
            this.analyser.Size = new System.Drawing.Size(80, 42);
            this.analyser.TabIndex = 4;
            this.analyser.Text = "analyse";
            this.analyser.UseVisualStyleBackColor = true;
            this.analyser.Click += new System.EventHandler(this.analyser_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 125);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(362, 22);
            this.textBox1.TabIndex = 5;
            // 
            // outputFileNameBox
            // 
            this.outputFileNameBox.Location = new System.Drawing.Point(65, 194);
            this.outputFileNameBox.Name = "outputFileNameBox";
            this.outputFileNameBox.Size = new System.Drawing.Size(362, 22);
            this.outputFileNameBox.TabIndex = 6;
            this.outputFileNameBox.TextChanged += new System.EventHandler(this.outputFileNameBox_TextChanged);
            // 
            // finished
            // 
            this.finished.Location = new System.Drawing.Point(347, 270);
            this.finished.Name = "finished";
            this.finished.Size = new System.Drawing.Size(80, 22);
            this.finished.TabIndex = 7;
            this.finished.TextChanged += new System.EventHandler(this.finished_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 314);
            this.Controls.Add(this.finished);
            this.Controls.Add(this.outputFileNameBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.analyser);
            this.Controls.Add(this.fileFinder);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button fileFinder;
        private System.Windows.Forms.Button analyser;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox outputFileNameBox;
        private System.Windows.Forms.TextBox finished;
    }
}

