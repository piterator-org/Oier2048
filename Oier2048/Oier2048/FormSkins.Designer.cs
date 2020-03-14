namespace Oier2048
{
    partial class FormSkins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSkins));
            this.pictureBoxView = new System.Windows.Forms.PictureBox();
            this.labelPrev = new System.Windows.Forms.Label();
            this.labelNext = new System.Windows.Forms.Label();
            this.labelCancel = new System.Windows.Forms.Label();
            this.labelOk = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxView
            // 
            this.pictureBoxView.BackgroundImage = global::Oier2048.Properties.Resources._2;
            this.pictureBoxView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxView.Location = new System.Drawing.Point(89, 13);
            this.pictureBoxView.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxView.Name = "pictureBoxView";
            this.pictureBoxView.Size = new System.Drawing.Size(222, 160);
            this.pictureBoxView.TabIndex = 302;
            this.pictureBoxView.TabStop = false;
            this.pictureBoxView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Start_MouseDown);
            // 
            // labelPrev
            // 
            this.labelPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.labelPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelPrev.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelPrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(193)))), ((int)(((byte)(236)))));
            this.labelPrev.Location = new System.Drawing.Point(9, 69);
            this.labelPrev.Margin = new System.Windows.Forms.Padding(0);
            this.labelPrev.Name = "labelPrev";
            this.labelPrev.Size = new System.Drawing.Size(80, 48);
            this.labelPrev.TabIndex = 201;
            this.labelPrev.Text = "Prev";
            this.labelPrev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelPrev_MouseUp);
            // 
            // labelNext
            // 
            this.labelNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.labelNext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelNext.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(193)))), ((int)(((byte)(236)))));
            this.labelNext.Location = new System.Drawing.Point(311, 69);
            this.labelNext.Margin = new System.Windows.Forms.Padding(0);
            this.labelNext.Name = "labelNext";
            this.labelNext.Size = new System.Drawing.Size(80, 48);
            this.labelNext.TabIndex = 202;
            this.labelNext.Text = "Next";
            this.labelNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelNext_MouseUp);
            // 
            // labelCancel
            // 
            this.labelCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.labelCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCancel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(193)))), ((int)(((byte)(236)))));
            this.labelCancel.Location = new System.Drawing.Point(9, 183);
            this.labelCancel.Margin = new System.Windows.Forms.Padding(0);
            this.labelCancel.Name = "labelCancel";
            this.labelCancel.Size = new System.Drawing.Size(188, 48);
            this.labelCancel.TabIndex = 301;
            this.labelCancel.Text = "取消 [ESC]";
            this.labelCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelCancel_MouseUp);
            // 
            // labelOk
            // 
            this.labelOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.labelOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelOk.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(193)))), ((int)(((byte)(236)))));
            this.labelOk.Location = new System.Drawing.Point(203, 183);
            this.labelOk.Margin = new System.Windows.Forms.Padding(0);
            this.labelOk.Name = "labelOk";
            this.labelOk.Size = new System.Drawing.Size(188, 48);
            this.labelOk.TabIndex = 302;
            this.labelOk.Text = "确定 [回车]";
            this.labelOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelOk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelOk_MouseUp);
            // 
            // FormSkins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(400, 240);
            this.ControlBox = false;
            this.Controls.Add(this.labelOk);
            this.Controls.Add(this.labelCancel);
            this.Controls.Add(this.labelNext);
            this.Controls.Add(this.labelPrev);
            this.Controls.Add(this.pictureBoxView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 240);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 240);
            this.Name = "FormSkins";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSkins";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.Load += new System.EventHandler(this.FormSkins_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSkins_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxView;
        private System.Windows.Forms.Label labelPrev;
        private System.Windows.Forms.Label labelNext;
        private System.Windows.Forms.Label labelCancel;
        private System.Windows.Forms.Label labelOk;
    }
}