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
        PictureBox[] segments = new PictureBox[50];
        int segs = 0;

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
                    font_chars[charcount] = new double[temparray.Length + 4];
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
            //attempt to generate UI objects that represent the character
            double startx = 102;
            double starty = 69;
            
            int scale = 10;

            Bitmap preview;

            lv_points.Items.Clear();
            segments_clear();

            int idx = 0;
            if (lv_charmap.SelectedItems.Count > 0)
            {
                idx = lv_charmap.Items.IndexOf(lv_charmap.SelectedItems[0]);

                double[] thisChar = font_chars[idx];
                for (int ptr = 0; ptr < (thisChar.Length-3) / 4; ptr++)
                {
                    double x1 = thisChar[(ptr * 4) + 3];
                    double y1 = thisChar[(ptr * 4) + 4];
                    double x2 = thisChar[(ptr * 4) + 5];
                    double y2 = thisChar[(ptr * 4) + 6];
                    lv_points.Items.Add("[" + x1 + "," + y1 + "][" + x2 + "," + y2 + "]");

                    segments[segs] = new PictureBox();
                    segments[segs].Name = "seg" + segs;

                    if (x2 > x1 && y2 > y1) {   //this probably wont work out.
                        segments[segs].Location = new Point(Convert.ToInt32(x1 * scale), Convert.ToInt32(y1 * scale));
                        segments[segs].Size = new Size(Convert.ToInt32((x2 - x1) * scale)+2, Convert.ToInt32((y2 - y1) * scale)+2);

                        preview = new Bitmap(segments[segs].Width, segments[segs].Height);       //init the picturebox
                        Graphics previewGraphics = Graphics.FromImage(preview);
                        previewGraphics.DrawLine(Pens.Blue, 0, 0, segments[segs].Width, segments[segs].Height);    //draw a stroke to the picturebox

                    }
                    else
                    {
                        segments[segs].Location = new Point(Convert.ToInt32(x2 * scale), Convert.ToInt32(y2 * scale));
                        segments[segs].Size = new Size(Math.Abs(Convert.ToInt32((x1 - x2) * scale)+2), Math.Abs(Convert.ToInt32((y1 - y2) * scale)+2));

                        preview = new Bitmap(Math.Abs(segments[segs].Width)+1, Math.Abs( segments[segs].Height)+1);       //init the picturebox
                        Graphics previewGraphics = Graphics.FromImage(preview);
                        previewGraphics.DrawLine(Pens.Blue,  segments[segs].Width+1, 0, 0, segments[segs].Height+1);    //draw a stroke to the picturebox
                    }
                    segments[segs].BackColor = System.Drawing.Color.White;
                    segments[segs].MouseClick += new MouseEventHandler(segment_Click);
                    segments[segs].Image = preview;

                    panel1.Controls.Add(segments[segs]);        //add the crude line object to the panel container
                    segs++;
                }
            }
        }
        void segment_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            segment_highlight(pb);
            lv_points.Items[5].Selected = true;

        }

        void segments_clear()
        {
            for (int aa = 0; aa < segs; aa++)
            {
                panel1.Controls.Remove(segments[aa]);
            }
            segs = 0;
        }

        void segment_highlight(PictureBox this_pb)
        {
            for(int aa=0; aa< segs; aa++)
            {
                segments[aa].BackColor = System.Drawing.Color.Red;
            }
            this_pb.BackColor = System.Drawing.Color.Green;
        }

    }
}
