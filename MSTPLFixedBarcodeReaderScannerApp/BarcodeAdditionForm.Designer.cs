namespace MSTPLFixedBarcodeReaderScannerApp
{
    partial class BarcodeAdditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarcodeAdditionForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_submit = new System.Windows.Forms.Button();
            this.panel_bcdetails = new System.Windows.Forms.Panel();
            this.lbl_bcrserial = new System.Windows.Forms.Label();
            this.dd_bcrserial = new System.Windows.Forms.ComboBox();
            this.txt_bcserial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_addbcdetails = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_bcdetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel_bcdetails);
            this.panel1.Controls.Add(this.lbl_addbcdetails);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 393);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_close);
            this.panel2.Controls.Add(this.btn_submit);
            this.panel2.Location = new System.Drawing.Point(12, 223);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(751, 101);
            this.panel2.TabIndex = 2;
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(520, 29);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(186, 45);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.Location = new System.Drawing.Point(31, 29);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(186, 45);
            this.btn_submit.TabIndex = 0;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // panel_bcdetails
            // 
            this.panel_bcdetails.Controls.Add(this.lbl_bcrserial);
            this.panel_bcdetails.Controls.Add(this.dd_bcrserial);
            this.panel_bcdetails.Controls.Add(this.txt_bcserial);
            this.panel_bcdetails.Controls.Add(this.label1);
            this.panel_bcdetails.Location = new System.Drawing.Point(12, 45);
            this.panel_bcdetails.Name = "panel_bcdetails";
            this.panel_bcdetails.Size = new System.Drawing.Size(751, 172);
            this.panel_bcdetails.TabIndex = 1;
            // 
            // lbl_bcrserial
            // 
            this.lbl_bcrserial.AutoSize = true;
            this.lbl_bcrserial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_bcrserial.Location = new System.Drawing.Point(28, 57);
            this.lbl_bcrserial.Name = "lbl_bcrserial";
            this.lbl_bcrserial.Size = new System.Drawing.Size(191, 20);
            this.lbl_bcrserial.TabIndex = 3;
            this.lbl_bcrserial.Text = "Reader Serial Number:";
            // 
            // dd_bcrserial
            // 
            this.dd_bcrserial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dd_bcrserial.FormattingEnabled = true;
            this.dd_bcrserial.Location = new System.Drawing.Point(233, 57);
            this.dd_bcrserial.Name = "dd_bcrserial";
            this.dd_bcrserial.Size = new System.Drawing.Size(482, 28);
            this.dd_bcrserial.TabIndex = 2;
            // 
            // txt_bcserial
            // 
            this.txt_bcserial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bcserial.Location = new System.Drawing.Point(233, 25);
            this.txt_bcserial.Name = "txt_bcserial";
            this.txt_bcserial.Size = new System.Drawing.Size(482, 26);
            this.txt_bcserial.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Module Serial Number:";
            // 
            // lbl_addbcdetails
            // 
            this.lbl_addbcdetails.AutoSize = true;
            this.lbl_addbcdetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_addbcdetails.Location = new System.Drawing.Point(283, 18);
            this.lbl_addbcdetails.Name = "lbl_addbcdetails";
            this.lbl_addbcdetails.Size = new System.Drawing.Size(200, 24);
            this.lbl_addbcdetails.TabIndex = 0;
            this.lbl_addbcdetails.Text = "Add Barcode Details";
            // 
            // BarcodeAdditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 393);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BarcodeAdditionForm";
            this.Text = "BarcodeAdditionForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BarcodeAdditionForm_FormClosed);
            this.Load += new System.EventHandler(this.BarcodeAdditionForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel_bcdetails.ResumeLayout(false);
            this.panel_bcdetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_addbcdetails;
        private System.Windows.Forms.Panel panel_bcdetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_bcserial;
        private System.Windows.Forms.Label lbl_bcrserial;
        private System.Windows.Forms.ComboBox dd_bcrserial;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_close;
    }
}