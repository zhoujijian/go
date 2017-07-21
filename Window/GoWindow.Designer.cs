namespace GoWin {
    partial class GoWindow {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.topmenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newKifu = new System.Windows.Forms.ToolStripMenuItem();
            this.openKifu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveKifu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLayer = new System.Windows.Forms.Panel();
            this.textComment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butResearch = new System.Windows.Forms.Button();
            this.butNext = new System.Windows.Forms.Button();
            this.butSaveResearch = new System.Windows.Forms.Button();
            this.butPrev = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.butSetNote = new System.Windows.Forms.Button();
            this.butRemVariety = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelVariety = new System.Windows.Forms.Panel();
            this.lstVariety = new System.Windows.Forms.ListBox();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.topmenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelVariety.SuspendLayout();
            this.panelRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // topmenu
            // 
            this.topmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.topmenu.Location = new System.Drawing.Point(0, 0);
            this.topmenu.Name = "topmenu";
            this.topmenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.topmenu.Size = new System.Drawing.Size(1096, 24);
            this.topmenu.TabIndex = 0;
            this.topmenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newKifu,
            this.openKifu,
            this.saveKifu});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.fileToolStripMenuItem.Text = "Kifu";
            // 
            // newKifu
            // 
            this.newKifu.Name = "newKifu";
            this.newKifu.Size = new System.Drawing.Size(152, 22);
            this.newKifu.Text = "New";
            // 
            // openKifu
            // 
            this.openKifu.Name = "openKifu";
            this.openKifu.Size = new System.Drawing.Size(152, 22);
            this.openKifu.Text = "Open";
            // 
            // saveKifu
            // 
            this.saveKifu.Name = "saveKifu";
            this.saveKifu.Size = new System.Drawing.Size(152, 22);
            this.saveKifu.Text = "Save";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // panelLayer
            // 
            this.panelLayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLayer.Location = new System.Drawing.Point(6, 3);
            this.panelLayer.Name = "panelLayer";
            this.panelLayer.Size = new System.Drawing.Size(574, 574);
            this.panelLayer.TabIndex = 1;
            // 
            // textComment
            // 
            this.textComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textComment.Location = new System.Drawing.Point(3, 29);
            this.textComment.Multiline = true;
            this.textComment.Name = "textComment";
            this.textComment.Size = new System.Drawing.Size(506, 249);
            this.textComment.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(97, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Black:";
            // 
            // butResearch
            // 
            this.butResearch.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butResearch.Location = new System.Drawing.Point(291, 4);
            this.butResearch.Name = "butResearch";
            this.butResearch.Size = new System.Drawing.Size(124, 35);
            this.butResearch.TabIndex = 9;
            this.butResearch.Text = "Research";
            this.butResearch.UseVisualStyleBackColor = true;
            // 
            // butNext
            // 
            this.butNext.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butNext.Location = new System.Drawing.Point(152, 3);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(119, 35);
            this.butNext.TabIndex = 2;
            this.butNext.Text = "Next";
            this.butNext.UseVisualStyleBackColor = true;
            // 
            // butSaveResearch
            // 
            this.butSaveResearch.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSaveResearch.Location = new System.Drawing.Point(434, 3);
            this.butSaveResearch.Name = "butSaveResearch";
            this.butSaveResearch.Size = new System.Drawing.Size(133, 35);
            this.butSaveResearch.TabIndex = 1;
            this.butSaveResearch.Text = "Save";
            this.butSaveResearch.UseVisualStyleBackColor = true;
            // 
            // butPrev
            // 
            this.butPrev.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butPrev.Location = new System.Drawing.Point(5, 3);
            this.butPrev.Name = "butPrev";
            this.butPrev.Size = new System.Drawing.Size(125, 35);
            this.butPrev.TabIndex = 0;
            this.butPrev.Text = "Prev";
            this.butPrev.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(310, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "White:";
            // 
            // butSetNote
            // 
            this.butSetNote.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSetNote.Location = new System.Drawing.Point(3, 280);
            this.butSetNote.Name = "butSetNote";
            this.butSetNote.Size = new System.Drawing.Size(506, 35);
            this.butSetNote.TabIndex = 7;
            this.butSetNote.Text = "Save Note";
            this.butSetNote.UseVisualStyleBackColor = true;
            // 
            // butRemVariety
            // 
            this.butRemVariety.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butRemVariety.Location = new System.Drawing.Point(3, 582);
            this.butRemVariety.Name = "butRemVariety";
            this.butRemVariety.Size = new System.Drawing.Size(506, 34);
            this.butRemVariety.TabIndex = 8;
            this.butRemVariety.Text = "Remove Variety";
            this.butRemVariety.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butResearch);
            this.panel1.Controls.Add(this.butNext);
            this.panel1.Controls.Add(this.butPrev);
            this.panel1.Controls.Add(this.butSaveResearch);
            this.panel1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(6, 582);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 41);
            this.panel1.TabIndex = 0;
            // 
            // panelVariety
            // 
            this.panelVariety.Controls.Add(this.lstVariety);
            this.panelVariety.Controls.Add(this.label1);
            this.panelVariety.Controls.Add(this.butRemVariety);
            this.panelVariety.Controls.Add(this.textComment);
            this.panelVariety.Controls.Add(this.butSetNote);
            this.panelVariety.Controls.Add(this.label2);
            this.panelVariety.Location = new System.Drawing.Point(583, 3);
            this.panelVariety.Name = "panelVariety";
            this.panelVariety.Size = new System.Drawing.Size(513, 620);
            this.panelVariety.TabIndex = 9;
            // 
            // lstVariety
            // 
            this.lstVariety.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstVariety.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVariety.FormattingEnabled = true;
            this.lstVariety.ItemHeight = 14;
            this.lstVariety.Location = new System.Drawing.Point(3, 322);
            this.lstVariety.Name = "lstVariety";
            this.lstVariety.Size = new System.Drawing.Size(506, 254);
            this.lstVariety.TabIndex = 9;
            // 
            // panelRoot
            // 
            this.panelRoot.Controls.Add(this.panelLayer);
            this.panelRoot.Controls.Add(this.panelVariety);
            this.panelRoot.Controls.Add(this.panel1);
            this.panelRoot.Enabled = false;
            this.panelRoot.Location = new System.Drawing.Point(-1, 33);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(1099, 624);
            this.panelRoot.TabIndex = 10;
            // 
            // GoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 658);
            this.Controls.Add(this.panelRoot);
            this.Controls.Add(this.topmenu);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.topmenu;
            this.MaximizeBox = false;
            this.Name = "GoWindow";
            this.Text = "Go-LY";
            this.topmenu.ResumeLayout(false);
            this.topmenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelVariety.ResumeLayout(false);
            this.panelVariety.PerformLayout();
            this.panelRoot.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topmenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button butNext;
        public System.Windows.Forms.Button butSaveResearch;
        public System.Windows.Forms.Button butPrev;
        public System.Windows.Forms.ToolStripMenuItem openKifu;
        public System.Windows.Forms.ToolStripMenuItem saveKifu;
        public System.Windows.Forms.ToolStripMenuItem newKifu;
        private System.Windows.Forms.Panel panelLayer;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button butSetNote;
        public System.Windows.Forms.Button butRemVariety;
        public System.Windows.Forms.Button butResearch;
        public System.Windows.Forms.TextBox textComment;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panelVariety;
        public System.Windows.Forms.Panel panelRoot;
        public System.Windows.Forms.ListBox lstVariety;
    }
}

