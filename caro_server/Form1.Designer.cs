namespace caro_server
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
            this.rtb_Server = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtb_Server
            // 
            this.rtb_Server.Location = new System.Drawing.Point(3, -2);
            this.rtb_Server.Name = "rtb_Server";
            this.rtb_Server.Size = new System.Drawing.Size(326, 230);
            this.rtb_Server.TabIndex = 0;
            this.rtb_Server.Text = "";
            this.rtb_Server.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_Server_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 261);
            this.Controls.Add(this.rtb_Server);
            this.Name = "Form1";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_Server;
    }
}

