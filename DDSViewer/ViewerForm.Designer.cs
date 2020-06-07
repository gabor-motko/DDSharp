namespace DDSViewer
{
    partial class ViewerForm
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
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.filePathText = new System.Windows.Forms.TextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.loadDdsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox.Location = new System.Drawing.Point(12, 39);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(776, 399);
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            // 
            // filePathText
            // 
            this.filePathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathText.Location = new System.Drawing.Point(12, 12);
            this.filePathText.Name = "filePathText";
            this.filePathText.Size = new System.Drawing.Size(567, 20);
            this.filePathText.TabIndex = 1;
            this.filePathText.Text = "F:\\git\\DDSharp\\DDSViewer\\TestImages\\stonefloor_rgb.dds";
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(688, 10);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(100, 23);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Read header";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // loadDdsButton
            // 
            this.loadDdsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadDdsButton.Location = new System.Drawing.Point(585, 10);
            this.loadDdsButton.Name = "loadDdsButton";
            this.loadDdsButton.Size = new System.Drawing.Size(97, 23);
            this.loadDdsButton.TabIndex = 2;
            this.loadDdsButton.Text = "Load DDS";
            this.loadDdsButton.UseVisualStyleBackColor = true;
            this.loadDdsButton.Click += new System.EventHandler(this.loadDdsButton_Click);
            // 
            // ViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loadDdsButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.filePathText);
            this.Controls.Add(this.imageBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewerForm";
            this.Text = "ViewerForm";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.TextBox filePathText;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button loadDdsButton;
    }
}