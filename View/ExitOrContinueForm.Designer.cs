namespace View
{
    public partial class ExitOrContinueForm
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
            this.ContinueBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ContinueBtn.Location = new System.Drawing.Point(40, 135);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(126, 47);
            this.ContinueBtn.TabIndex = 0;
            this.ContinueBtn.Text = "Yes";
            this.ContinueBtn.UseVisualStyleBackColor = true;
            this.ContinueBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitBtn.Location = new System.Drawing.Point(222, 135);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(126, 47);
            this.ExitBtn.TabIndex = 1;
            this.ExitBtn.Text = "No";
            this.ExitBtn.UseVisualStyleBackColor = true;
            // 
            // ExitOrContinueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 212);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.ContinueBtn);
            this.Name = "ExitOrContinueForm";
            this.Text = "Othello";
            this.Load += new System.EventHandler(this.ExitOrContinueForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ContinueBtn;
        private System.Windows.Forms.Button ExitBtn;
    }
}