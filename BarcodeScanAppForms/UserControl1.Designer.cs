namespace BarcodeScanAppForms
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_mod_ser_num = new System.Windows.Forms.Label();
            this.txt_mod_ser_num = new System.Windows.Forms.TextBox();
            this.lbl_reader_serial_number = new System.Windows.Forms.Label();
            this.lbl_laminator_number = new System.Windows.Forms.Label();
            this.lbl_date = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.UseScanDate = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_laminator_number = new System.Windows.Forms.TextBox();
            this.txt_reader_serial_num = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_mod_ser_num
            // 
            this.lbl_mod_ser_num.AutoSize = true;
            this.lbl_mod_ser_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mod_ser_num.Location = new System.Drawing.Point(17, 16);
            this.lbl_mod_ser_num.Name = "lbl_mod_ser_num";
            this.lbl_mod_ser_num.Size = new System.Drawing.Size(190, 20);
            this.lbl_mod_ser_num.TabIndex = 0;
            this.lbl_mod_ser_num.Text = "Module Serial Number:";
            // 
            // txt_mod_ser_num
            // 
            this.txt_mod_ser_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mod_ser_num.Location = new System.Drawing.Point(243, 16);
            this.txt_mod_ser_num.Name = "txt_mod_ser_num";
            this.txt_mod_ser_num.Size = new System.Drawing.Size(479, 26);
            this.txt_mod_ser_num.TabIndex = 1;
            // 
            // lbl_reader_serial_number
            // 
            this.lbl_reader_serial_number.AutoSize = true;
            this.lbl_reader_serial_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_reader_serial_number.Location = new System.Drawing.Point(19, 53);
            this.lbl_reader_serial_number.Name = "lbl_reader_serial_number";
            this.lbl_reader_serial_number.Size = new System.Drawing.Size(191, 20);
            this.lbl_reader_serial_number.TabIndex = 2;
            this.lbl_reader_serial_number.Text = "Reader Serial Number:";
            // 
            // lbl_laminator_number
            // 
            this.lbl_laminator_number.AutoSize = true;
            this.lbl_laminator_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_laminator_number.Location = new System.Drawing.Point(19, 85);
            this.lbl_laminator_number.Name = "lbl_laminator_number";
            this.lbl_laminator_number.Size = new System.Drawing.Size(161, 20);
            this.lbl_laminator_number.TabIndex = 4;
            this.lbl_laminator_number.Text = "Laminator Number:";
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_date.Location = new System.Drawing.Point(19, 120);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(53, 20);
            this.lbl_date.TabIndex = 6;
            this.lbl_date.Text = "Date:";
            // 
            // btn_submit
            // 
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.Location = new System.Drawing.Point(0, 3);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(184, 39);
            this.btn_submit.TabIndex = 10;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // DatePicker
            // 
            this.DatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DatePicker.Location = new System.Drawing.Point(243, 114);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(342, 26);
            this.DatePicker.TabIndex = 11;
            // 
            // UseScanDate
            // 
            this.UseScanDate.AutoSize = true;
            this.UseScanDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseScanDate.Location = new System.Drawing.Point(591, 118);
            this.UseScanDate.Name = "UseScanDate";
            this.UseScanDate.Size = new System.Drawing.Size(131, 24);
            this.UseScanDate.TabIndex = 12;
            this.UseScanDate.Text = "Use scan date";
            this.UseScanDate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Close);
            this.panel1.Controls.Add(this.btn_submit);
            this.panel1.Location = new System.Drawing.Point(21, 258);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 44);
            this.panel1.TabIndex = 13;
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(517, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(184, 39);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_laminator_number
            // 
            this.txt_laminator_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_laminator_number.Location = new System.Drawing.Point(243, 82);
            this.txt_laminator_number.Name = "txt_laminator_number";
            this.txt_laminator_number.Size = new System.Drawing.Size(479, 26);
            this.txt_laminator_number.TabIndex = 5;
            // 
            // txt_reader_serial_num
            // 
            this.txt_reader_serial_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_reader_serial_num.Location = new System.Drawing.Point(243, 50);
            this.txt_reader_serial_num.Name = "txt_reader_serial_num";
            this.txt_reader_serial_num.Size = new System.Drawing.Size(479, 26);
            this.txt_reader_serial_num.TabIndex = 3;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UseScanDate);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.txt_laminator_number);
            this.Controls.Add(this.lbl_laminator_number);
            this.Controls.Add(this.txt_reader_serial_num);
            this.Controls.Add(this.lbl_reader_serial_number);
            this.Controls.Add(this.txt_mod_ser_num);
            this.Controls.Add(this.lbl_mod_ser_num);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(745, 305);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_mod_ser_num;
        private System.Windows.Forms.TextBox txt_mod_ser_num;
        private System.Windows.Forms.Label lbl_reader_serial_number;
        private System.Windows.Forms.Label lbl_laminator_number;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.DateTimePicker DatePicker;
        private System.Windows.Forms.CheckBox UseScanDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_laminator_number;
        private System.Windows.Forms.TextBox txt_reader_serial_num;
    }
}
