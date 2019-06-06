namespace View
{
    partial class SettingsForm
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
            this.BoardSizeBtn = new System.Windows.Forms.Button();
            this.TwoPlayersBtn = new System.Windows.Forms.Button();
            this.OnePlayerBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeBtn
            // 
            this.BoardSizeBtn.Location = new System.Drawing.Point(58, 27);
            this.BoardSizeBtn.Name = "BoardSizeBtn";
            this.BoardSizeBtn.Size = new System.Drawing.Size(469, 66);
            this.BoardSizeBtn.TabIndex = 0;
            this.BoardSizeBtn.Text = "Board Size: 6x6 (click to increase)";
            this.BoardSizeBtn.UseVisualStyleBackColor = true;
            this.BoardSizeBtn.Click += new System.EventHandler(this.BoardSizeBtn_Click);
            // 
            // TwoPlayersBtn
            // 
            this.TwoPlayersBtn.Location = new System.Drawing.Point(371, 129);
            this.TwoPlayersBtn.Name = "TwoPlayersBtn";
            this.TwoPlayersBtn.Size = new System.Drawing.Size(156, 65);
            this.TwoPlayersBtn.TabIndex = 1;
            this.TwoPlayersBtn.Text = "Play Against Your Friend";
            this.TwoPlayersBtn.UseVisualStyleBackColor = true;
            this.TwoPlayersBtn.Click += new System.EventHandler(this.twoPlayersBtn_Click);
            // 
            // OnePlayerBtn
            // 
            this.OnePlayerBtn.Location = new System.Drawing.Point(58, 129);
            this.OnePlayerBtn.Name = "OnePlayerBtn";
            this.OnePlayerBtn.Size = new System.Drawing.Size(156, 65);
            this.OnePlayerBtn.TabIndex = 2;
            this.OnePlayerBtn.Text = "Play Against The Computer";
            this.OnePlayerBtn.UseVisualStyleBackColor = true;
            this.OnePlayerBtn.Click += new System.EventHandler(this.onePlayerBtn_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 239);
            this.Controls.Add(this.OnePlayerBtn);
            this.Controls.Add(this.TwoPlayersBtn);
            this.Controls.Add(this.BoardSizeBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button BoardSizeBtn;
        public System.Windows.Forms.Button TwoPlayersBtn;
        public System.Windows.Forms.Button OnePlayerBtn;
    }
}