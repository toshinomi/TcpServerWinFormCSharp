namespace TcpServerWinFormCSharp
{
    partial class FormTcpServer
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.Location = new System.Drawing.Point(70, 47);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(104, 23);
            this.lblIpAddress.TabIndex = 0;
            this.lblIpAddress.Text = "IP Address";
            this.lblIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(70, 84);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(104, 23);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Port";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormTcpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIpAddress);
            this.Name = "FormTcpServer";
            this.Text = "TCP Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.Label lblPort;
    }
}

