using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace _3DWriter
{
    // This is an absolute mess, i am experimenting with a graphical font editor. This will change a lot before i enable the menu option in the main form

    public partial class FontEditor : Form
    {
        double h_height;                                        //font character height
        double h_char_count;                                    //font character count
        string h_font_map;                                      //font map - Character index array
        double[][] font_chars = new double[250][];              //the main font array
        int segs = 0;
        int selected_seg = 0;

        public FontEditor()
        {
            InitializeComponent();
        }

        private void FontEditor_Load(object sender, EventArgs e)
        {
            string[] fileEntries = Directory.GetFiles("fonts");
            foreach (string fileName in fileEntries)
            {
                String font_name = fileName.Replace("fonts" + Path.DirectorySeparatorChar, "");
                if (font_name.IndexOf(".h") > 0)
                {
                    FontComboBox.Items.Add(font_name.Replace(".h", ""));
                }
            }
            load_font("futural");
        }

        private void FontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_font(FontComboBox.Text);
            //lv_charmap
        }

        public void load_font(string fname)
        {
            int counter = 0;
            string line;

            int charcount = 0;
            System.IO.StreamReader file =
            new System.IO.StreamReader("fonts" + Path.DirectorySeparatorChar + fname + ".cmf");
            while ((line = file.ReadLine()) != null)                                    // iterate through the font file
            {
                if (counter == 0) { Double.TryParse(line, out h_char_count); }          //first line: Character count
                if (counter == 1) { Double.TryParse(line, out h_height); }              //second line: Character height
                if (counter == 2) { h_font_map = line; }                                //third line: Character map
                if (counter > 2)
                {
                    //each line consists of width, realwidth, arraysize, [x/y pairs]    //arraysize is unused   //??
                    string[] temparray = line.Split(',');
                    font_chars[charcount] = new double[temparray.Length];
                    for (int idx = 0; idx < temparray.Length; idx++)
                    {
                        Double.TryParse(temparray[idx], out font_chars[charcount][idx]);
                    }
                    charcount++;
                }
                counter++;
            }
            file.Close();
            lv_charmap.Items.Clear();
            for (int char_idx=0; char_idx< h_font_map.Length; char_idx++)
            {
                lv_charmap.Items.Add(h_font_map.Substring(char_idx,1));
            }
        }

        private void lv_charmap_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            lv_points.Items.Clear();

            int idx = 0;
            segs = 0;
            if (lv_charmap.SelectedItems.Count > 0)
            {
                idx = lv_charmap.Items.IndexOf(lv_charmap.SelectedItems[0]);
                double[] thisChar = font_chars[idx];

                double char_width = thisChar[1];
                tb_width.Text = char_width.ToString();
                for (int ptr = 0; ptr < (thisChar.Length - 3) / 4; ptr++)
                {
                    double x1 = thisChar[(ptr * 4) + 3];
                    double y1 = thisChar[(ptr * 4) + 4];
                    double x2 = thisChar[(ptr * 4) + 5];
                    double y2 = thisChar[(ptr * 4) + 6];
                    lv_points.Items.Add( x1 + "," + y1 + "," + x2 + "," + y2);
                    segs++;
                }
                update_preview();
            }
            MessageBox.Show(idx.ToString());
        }

        private void lv_points_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx;
            if (lv_points.SelectedItems.Count > 0)
            {
                idx = lv_points.Items.IndexOf(lv_points.SelectedItems[0]);
                selected_seg = idx;
                update_preview();
            }
        }

        private void update_preview()
        {
            int scale = 10;
            Bitmap preview = new Bitmap(pb_editor.Width, pb_editor.Height);       //init the picturebox
            Graphics previewGraphics = Graphics.FromImage(preview);
            Pen semiTransPen = new Pen(Color.FromArgb(25, 255, 0, 0), 2);           //create a transparent red pen for the margins (offset)


            //height
            previewGraphics.DrawLine(semiTransPen, 0 * scale, 0 * scale, Convert.ToSingle(tb_width.Text) * scale, 0 * scale);
            previewGraphics.DrawLine(semiTransPen, 0 * scale, Convert.ToSingle(h_height) * scale, Convert.ToSingle(tb_width.Text) * scale, Convert.ToSingle(h_height) * scale);

            //width
            previewGraphics.DrawLine(semiTransPen, 0 * scale, 0 * scale, 0 * scale, Convert.ToSingle(h_height) * scale);
            previewGraphics.DrawLine(semiTransPen, Convert.ToSingle(tb_width.Text) * scale, 0 * scale, Convert.ToSingle(tb_width.Text) * scale, Convert.ToSingle(h_height) * scale);


            for (int point = 0; point < segs; point++){
                String[] this_seg = (lv_points.Items[point].Text).Split(',');
                previewGraphics.DrawLine( (selected_seg== point?Pens.Red:Pens.Blue), Convert.ToSingle(this_seg[0]) * scale, Convert.ToSingle(this_seg[1]) * scale, Convert.ToSingle(this_seg[2]) * scale, Convert.ToSingle(this_seg[3]) * scale);
                if (selected_seg == point)
                {
                    tb_x1.Text = this_seg[0];
                    tb_y1.Text = this_seg[1];
                    tb_x2.Text = this_seg[2];
                    tb_y2.Text = this_seg[3];

                    //draw starting point
                    previewGraphics.DrawEllipse(Pens.Red, (Convert.ToSingle(this_seg[0]) * scale)-3, (Convert.ToSingle(this_seg[1]) * scale)-3, 6, 6);
                }
            }
            
            pb_editor.Image = preview;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            update_preview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lv_points.Items[selected_seg].Text = tb_x1.Text + "," + tb_y1.Text + "," + tb_x2.Text + "," + tb_y2.Text;
            update_preview();

            //lv_points
            double[] new_seg = new double[250];
            new_seg[0] = Convert.ToSingle(tb_width.Text);   //width;
            new_seg[1] = Convert.ToSingle(tb_width.Text);   //realwidth;
            new_seg[2] = Convert.ToSingle(lv_points.Items.Count);   //size;

            for(int ptr=0; ptr< lv_points.Items.Count; ptr++)
            {
                String[] this_seg = (lv_points.Items[selected_seg].Text).Split(',');
                new_seg[ptr + 3] = Convert.ToSingle(this_seg[0]);
                new_seg[ptr + 4] = Convert.ToSingle(this_seg[1]);
                new_seg[ptr + 5] = Convert.ToSingle(this_seg[2]);
                new_seg[ptr + 6] = Convert.ToSingle(this_seg[3]);
            }
            font_chars[selected_seg] = new_seg;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lv_points.Items.Add("0,0,0,0");
            segs++;
        }

        private void btn_add_char_Click(object sender, EventArgs e)
        {
            if (tb_char_to_add.Text.Length != 1)
            {
                MessageBox.Show("Please enter a single character");
            }
            else
            {
                lv_charmap.Items.Add(tb_char_to_add.Text);
                font_chars[lv_charmap.Items.Count] = new double[7];
                font_chars[lv_charmap.Items.Count][0] = 5;
                font_chars[lv_charmap.Items.Count][1] = 5;
                font_chars[lv_charmap.Items.Count][2] = 4;
                font_chars[lv_charmap.Items.Count][3] = 0;
                font_chars[lv_charmap.Items.Count][4] = 20;
                font_chars[lv_charmap.Items.Count][5] = 5;
                font_chars[lv_charmap.Items.Count][6] = 20;

            }
        }

        private void btn_save_as_Click(object sender, EventArgs e)
        {
            //btn_save_as
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "custom.cmf";
            save.Filter = "cmf File | *.cmf";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string output = "";

                /*
                if (counter == 0) { Double.TryParse(line, out h_char_count); }          //first line: Character count
                if (counter == 1) { Double.TryParse(line, out h_height); }              //second line: Character height
                if (counter == 2) { h_font_map = line; }                                //third line: Character map
                */

                output += lv_charmap.Items.Count + "\n";
                output += h_height.ToString() + "\n";

                for (int ptr = 0; ptr < lv_charmap.Items.Count; ptr++)
                {
                    output += lv_charmap.Items[ptr].Text;
                }
                output += "\n";
                

                for (int ptr = 0; ptr < lv_charmap.Items.Count; ptr++)
                {
                    for(int aa=0; aa< font_chars[ptr].Length; aa++)
                    {
                        output += font_chars[ptr][aa] + ",";
                    }
                    output += "\n";
                }


                StreamWriter writer = new StreamWriter(save.OpenFile());
                writer.WriteLine(output);
                writer.Dispose();
                writer.Close();
            }
        }
    }
}
