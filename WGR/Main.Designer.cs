namespace WGR
{
    partial class Main
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
            this.cfwarp = new System.Windows.Forms.CheckBox();
            this.connection = new System.Windows.Forms.Button();
            this.potatogame = new System.Windows.Forms.Label();
            this.freepeer = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.disconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cfwarp
            // 
            this.cfwarp.AutoSize = true;
            this.cfwarp.Location = new System.Drawing.Point(12, 194);
            this.cfwarp.Name = "cfwarp";
            this.cfwarp.Size = new System.Drawing.Size(123, 21);
            this.cfwarp.TabIndex = 1;
            this.cfwarp.Text = "Cloudflare Warp";
            this.cfwarp.UseVisualStyleBackColor = true;
            // 
            // connection
            // 
            this.connection.Location = new System.Drawing.Point(138, 139);
            this.connection.Name = "connection";
            this.connection.Size = new System.Drawing.Size(145, 49);
            this.connection.TabIndex = 2;
            this.connection.Text = "连接";
            this.connection.UseVisualStyleBackColor = true;
            this.connection.Click += new System.EventHandler(this.connection_Click);
            // 
            // potatogame
            // 
            this.potatogame.AutoSize = true;
            this.potatogame.Location = new System.Drawing.Point(24, 9);
            this.potatogame.Name = "potatogame";
            this.potatogame.Size = new System.Drawing.Size(56, 17);
            this.potatogame.TabIndex = 3;
            this.potatogame.Text = "土豆游戏";
            // 
            // freepeer
            // 
            this.freepeer.AutoSize = true;
            this.freepeer.Location = new System.Drawing.Point(24, 171);
            this.freepeer.Name = "freepeer";
            this.freepeer.Size = new System.Drawing.Size(56, 17);
            this.freepeer.TabIndex = 4;
            this.freepeer.Text = "免费节点";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.ColumnWidth = 110;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(24, 29);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(240, 94);
            this.checkedListBox1.TabIndex = 14;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // disconnect
            // 
            this.disconnect.Location = new System.Drawing.Point(138, 194);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(145, 49);
            this.disconnect.TabIndex = 15;
            this.disconnect.Text = "断开";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 254);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.freepeer);
            this.Controls.Add(this.potatogame);
            this.Controls.Add(this.connection);
            this.Controls.Add(this.cfwarp);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CheckBox cfwarp;
        private Button connection;
        private Label potatogame;
        private Label freepeer;
        private CheckedListBox checkedListBox1;
        private Button disconnect;
    }
}