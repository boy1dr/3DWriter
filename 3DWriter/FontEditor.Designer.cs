namespace _3DWriter
{
    partial class FontEditor
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
            this.FontComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_save_as = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_charmap = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.lv_points = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_x1 = new System.Windows.Forms.TextBox();
            this.tb_y1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_x2 = new System.Windows.Forms.TextBox();
            this.tb_y2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.pb_editor = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_width = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btn_add_char = new System.Windows.Forms.Button();
            this.tb_char_to_add = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_editor)).BeginInit();
            this.SuspendLayout();
            // 
            // FontComboBox
            // 
            this.FontComboBox.FormattingEnabled = true;
            this.FontComboBox.Location = new System.Drawing.Point(46, 6);
            this.FontComboBox.Name = "FontComboBox";
            this.FontComboBox.Size = new System.Drawing.Size(121, 21);
            this.FontComboBox.TabIndex = 0;
            this.FontComboBox.SelectedIndexChanged += new System.EventHandler(this.FontComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Font";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Reload";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_save_as
            // 
            this.btn_save_as.Location = new System.Drawing.Point(254, 4);
            this.btn_save_as.Name = "btn_save_as";
            this.btn_save_as.Size = new System.Drawing.Size(75, 23);
            this.btn_save_as.TabIndex = 3;
            this.btn_save_as.Text = "SaveAs";
            this.btn_save_as.UseVisualStyleBackColor = true;
            this.btn_save_as.Click += new System.EventHandler(this.btn_save_as_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Character map";
            // 
            // lv_charmap
            // 
            this.lv_charmap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lv_charmap.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lv_charmap.Location = new System.Drawing.Point(15, 69);
            this.lv_charmap.MultiSelect = false;
            this.lv_charmap.Name = "lv_charmap";
            this.lv_charmap.ShowGroups = false;
            this.lv_charmap.Size = new System.Drawing.Size(73, 371);
            this.lv_charmap.TabIndex = 5;
            this.lv_charmap.UseCompatibleStateImageBehavior = false;
            this.lv_charmap.View = System.Windows.Forms.View.Details;
            this.lv_charmap.SelectedIndexChanged += new System.EventHandler(this.lv_charmap_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Character";
            // 
            // lv_points
            // 
            this.lv_points.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lv_points.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lv_points.Location = new System.Drawing.Point(516, 114);
            this.lv_points.MultiSelect = false;
            this.lv_points.Name = "lv_points";
            this.lv_points.Size = new System.Drawing.Size(100, 326);
            this.lv_points.TabIndex = 10;
            this.lv_points.UseCompatibleStateImageBehavior = false;
            this.lv_points.View = System.Windows.Forms.View.Details;
            this.lv_points.SelectedIndexChanged += new System.EventHandler(this.lv_points_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 95;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(513, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(626, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Line Start";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(626, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Line End";
            // 
            // tb_x1
            // 
            this.tb_x1.Location = new System.Drawing.Point(695, 134);
            this.tb_x1.Name = "tb_x1";
            this.tb_x1.Size = new System.Drawing.Size(34, 20);
            this.tb_x1.TabIndex = 14;
            // 
            // tb_y1
            // 
            this.tb_y1.Location = new System.Drawing.Point(735, 134);
            this.tb_y1.Name = "tb_y1";
            this.tb_y1.Size = new System.Drawing.Size(34, 20);
            this.tb_y1.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(704, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "X";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(744, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Y";
            // 
            // tb_x2
            // 
            this.tb_x2.Location = new System.Drawing.Point(695, 157);
            this.tb_x2.Name = "tb_x2";
            this.tb_x2.Size = new System.Drawing.Size(34, 20);
            this.tb_x2.TabIndex = 18;
            // 
            // tb_y2
            // 
            this.tb_y2.Location = new System.Drawing.Point(735, 157);
            this.tb_y2.Name = "tb_y2";
            this.tb_y2.Size = new System.Drawing.Size(34, 20);
            this.tb_y2.TabIndex = 19;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(775, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 44);
            this.button3.TabIndex = 23;
            this.button3.Text = "Set";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pb_editor
            // 
            this.pb_editor.Image = global::_3DWriter.Properties.Resources.red_grid;
            this.pb_editor.Location = new System.Drawing.Point(102, 69);
            this.pb_editor.Name = "pb_editor";
            this.pb_editor.Size = new System.Drawing.Size(400, 400);
            this.pb_editor.TabIndex = 25;
            this.pb_editor.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(513, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Points";
            // 
            // tb_width
            // 
            this.tb_width.Location = new System.Drawing.Point(516, 67);
            this.tb_width.Name = "tb_width";
            this.tb_width.Size = new System.Drawing.Size(45, 20);
            this.tb_width.TabIndex = 27;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(629, 64);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(110, 23);
            this.button6.TabIndex = 28;
            this.button6.Text = "Update preview";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(516, 446);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 23);
            this.button5.TabIndex = 29;
            this.button5.Text = "Add";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btn_add_char
            // 
            this.btn_add_char.Location = new System.Drawing.Point(46, 445);
            this.btn_add_char.Name = "btn_add_char";
            this.btn_add_char.Size = new System.Drawing.Size(44, 23);
            this.btn_add_char.TabIndex = 30;
            this.btn_add_char.Text = "Add";
            this.btn_add_char.UseVisualStyleBackColor = true;
            this.btn_add_char.Click += new System.EventHandler(this.btn_add_char_Click);
            // 
            // tb_char_to_add
            // 
            this.tb_char_to_add.Location = new System.Drawing.Point(15, 447);
            this.tb_char_to_add.Name = "tb_char_to_add";
            this.tb_char_to_add.Size = new System.Drawing.Size(25, 20);
            this.tb_char_to_add.TabIndex = 31;
            // 
            // FontEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 479);
            this.Controls.Add(this.tb_char_to_add);
            this.Controls.Add(this.btn_add_char);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.tb_width);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pb_editor);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tb_y2);
            this.Controls.Add(this.tb_x2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_y1);
            this.Controls.Add(this.tb_x1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lv_points);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lv_charmap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_save_as);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FontComboBox);
            this.Name = "FontEditor";
            this.Text = "FontEditor";
            this.Load += new System.EventHandler(this.FontEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_editor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox FontComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_save_as;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lv_charmap;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lv_points;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_x1;
        private System.Windows.Forms.TextBox tb_y1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_x2;
        private System.Windows.Forms.TextBox tb_y2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pb_editor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_width;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btn_add_char;
        private System.Windows.Forms.TextBox tb_char_to_add;
    }
}