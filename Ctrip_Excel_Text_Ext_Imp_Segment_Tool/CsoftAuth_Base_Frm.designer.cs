namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    partial class CsoftAuth_Base_Frm
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ts_tb_AuthServer = new System.Windows.Forms.ToolStripTextBox();
            this.ts_btn_Auth = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_tb_Version = new System.Windows.Forms.ToolStripTextBox();
            this.ts_btn_CheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_tb_AuthServer,
            this.ts_btn_Auth,
            this.ts_tb_Version,
            this.ts_btn_CheckUpdate});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(451, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ts_tb_AuthServer
            // 
            this.ts_tb_AuthServer.Name = "ts_tb_AuthServer";
            this.ts_tb_AuthServer.Size = new System.Drawing.Size(120, 23);
            this.ts_tb_AuthServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ts_tb_AuthServer_KeyPress);
            // 
            // ts_btn_Auth
            // 
            this.ts_btn_Auth.Name = "ts_btn_Auth";
            this.ts_btn_Auth.Size = new System.Drawing.Size(74, 23);
            this.ts_btn_Auth.Text = "Csoft 验证";
            this.ts_btn_Auth.Click += new System.EventHandler(this.ts_btn_Auth_Click);
            // 
            // ts_tb_Version
            // 
            this.ts_tb_Version.Name = "ts_tb_Version";
            this.ts_tb_Version.Size = new System.Drawing.Size(80, 23);
            // 
            // ts_btn_CheckUpdate
            // 
            this.ts_btn_CheckUpdate.Name = "ts_btn_CheckUpdate";
            this.ts_btn_CheckUpdate.Size = new System.Drawing.Size(67, 23);
            this.ts_btn_CheckUpdate.Text = "检查更新";
            this.ts_btn_CheckUpdate.Click += new System.EventHandler(this.ts_btn_CheckUpdates_Click);
            // 
            // CsoftAuth_Base_Frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 237);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CsoftAuth_Base_Frm";
            this.Text = "CsoftAuth_Base_Frm";
            this.Load += new System.EventHandler(this.CsoftAuth_Base_Frm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripTextBox ts_tb_AuthServer;
        public System.Windows.Forms.ToolStripMenuItem ts_btn_Auth;
        public System.Windows.Forms.ToolStripTextBox ts_tb_Version;
        public System.Windows.Forms.ToolStripMenuItem ts_btn_CheckUpdate;
    }
}
