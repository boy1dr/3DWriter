/*
 * 3DWriter - Chris Mitchell 2017 - Apache 2.0 License
 */
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
using System.Collections;
using System.Diagnostics;

namespace _3DWriter
{
    public partial class Form1 : Form
    {
        double h_height;                                        //font character height
        double h_char_count;                                    //font character count
        double[][] font_chars = new double[250][];              //the main font array
        string last_filename = "";
        bool appfault = false;
        int preview_mag = 2;

        public void load_font(string fname)
        {
            //reads the font file as text. these are written in c++, i'm just reading them in to an array to use in the render function.
            //if you think this is ugly, wait till you see the render function :P
            int counter = 0;
            string line;
            bool comment = false;
            bool array_active = false;
            string array_text = "";
            bool commit = false;
            bool stop_read = false;

            double c_width = 0;                                 //temporary storage of character width. line array index #0             ///not used - i used realwidth instead
            double c_realwidth = 0;                             //temporary storage of character real width. line array index #1
            double c_size = 0;                                  //temporary storage of character size. line array index #2

            int charcount = 0;
            toolStripStatusLabel1.Text = "Loading font - "+fname;
            System.IO.StreamReader file =
            new System.IO.StreamReader("fonts/"+fname+".h");
            while ((line = file.ReadLine()) != null)            // iterate through the font file
            {
                if (!stop_read)
                {
                    line = line.Trim();
                    if (line != "" && line.Substring(0, 2) != "//" && (" " + line).IndexOf("/*") > 0) { comment = true; }
                    if (line != "" && line.Substring(0, 2) != "//" && (" " + line).IndexOf("*/") > 0) { comment = false; }
                    if (line != "" && line.Substring(0, 2) != "//" && !comment)
                    {
                        //This loop plucks out relevant chunks of data from each line
                        if ((" " + line).IndexOf("{") > 0) { array_active = true; }                 //start curly brace - start of stroke x/y pair array
                        if ((" " + line).IndexOf("}") > 0) { array_active = false; commit = true; } //end curly brace - end of stroke x/y pair array
                        if (commit)
                        {
                            //we have all the data for 1 character, time to commit it to the main font character array 
                            string[] temparray = array_text.Split(',');
                            font_chars[charcount] = new double[Convert.ToInt32(c_size + 3)];
                            font_chars[charcount][0] = c_width;
                            font_chars[charcount][1] = c_realwidth;
                            font_chars[charcount][2] = c_size;
                            for (int aa = 0; aa < temparray.Length; aa++)
                            {
                                font_chars[charcount][aa + 3] = Convert.ToSingle(temparray[aa]);
                            }
                            commit = false;
                            array_text = "";
                            charcount++;
                        }
                        if (array_active)
                        {
                            if (array_text == "")
                            {
                                line = line.Substring(line.IndexOf("{") + 1);                       //start of array - start store the data
                            }
                            array_text += line.Replace(" ", "");                                    //clean it up
                        }

                        if (!commit && !array_active)
                        {
                            if ((" " + line).IndexOf("_width") > 0) { c_width = Convert.ToSingle((line.Substring(line.IndexOf("_width") + 8)).Replace(";", "")); }                      //nasty
                            if ((" " + line).IndexOf("_realwidth") > 0) { c_realwidth = Convert.ToSingle((line.Substring(line.IndexOf("_realwidth") + 12)).Replace(";", "")); }         //nasty
                            if ((" " + line).IndexOf("_size") > 0) { c_size = Convert.ToSingle((line.Substring(line.IndexOf("_size") + 7)).Replace(";", "")); }                         //nasty
                            if ((" " + line).IndexOf("_count") > 0) { h_char_count = Convert.ToSingle((line.Substring(line.IndexOf("_count") + 8)).Replace(";", "")); }                 //nasty
                            if ((" " + line).IndexOf("_height") > 0)
                            {
                                h_height = Convert.ToSingle((line.Substring(line.IndexOf("_height") + 9)).Replace(";", ""));                                                            //nasty
                                stop_read = true;
                            }
                        }
                    }
                }
                counter++;
            }
            file.Close();
            update_font_size();
        }

        private void update_font_size()
        {
            toolStripStatusLabel1.Text = "Font: " + FontComboBox.Text;
            toolStripStatusLabel2.Text = "Font Height: " + h_height + " Units";
            toolStripStatusLabel5.Text = "Real line height: " + (h_height * double.Parse(fontscale_value.Text)).ToString() + "mm" ;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("fonts/scriptc.h"))        //check for fonts folder
            {
                MessageBox.Show("Unable to find the fonts folder in the application folder.");
                appfault = true;
                Application.Exit();
            }
            else
            {
                LoadSettings(); //reads the application settings and fills in all the UI components
                LoadFonts(toolStripButton2.Checked);    //fills in the font selection combobox
                load_font(Properties.Settings.Default.default_font);    //reads the selected font file in to memory
            }
        }

        private void LoadFonts(bool simple_fonts)
        {
            //fills in the font selection combobox
            FontComboBox.Items.Clear();
            if (simple_fonts)
            {
                FontComboBox.Items.Add("cursive");
                FontComboBox.Items.Add("futural");
                FontComboBox.Items.Add("scriptc");
                FontComboBox.Items.Add("scripts");
            }
            else
            {
                string[] fileEntries = Directory.GetFiles("fonts/");
                foreach (string fileName in fileEntries)
                {
                    String font_name = fileName.Replace("fonts/", "");
                    if (font_name.IndexOf(".h") > 0)
                    {
                        FontComboBox.Items.Add(font_name.Replace(".h", ""));
                    }
                }
            }
            FontComboBox.Text = Properties.Settings.Default.default_font;
        }

        private void LoadSettings()
        {
            //reads the application settings and fills in all the UI components
            bedwidth.Text = Properties.Settings.Default.bedwidth;
            beddepth.Text = Properties.Settings.Default.beddepth;
            penup.Text = Properties.Settings.Default.penup;
            pendown.Text = Properties.Settings.Default.pendown;
            tspeed.Text = Properties.Settings.Default.tspeed;
            dspeed.Text = Properties.Settings.Default.dspeed;
            homex.Checked = Properties.Settings.Default.homex;
            homey.Checked = Properties.Settings.Default.homey;
            homez.Checked = Properties.Settings.Default.homez;
            offsetx.Text = Properties.Settings.Default.offsetx;
            offsety.Text = Properties.Settings.Default.offsety;
            fontscale_value.Text = Properties.Settings.Default.font_scale.ToString();
            trackBar1.Value = Convert.ToInt32( Properties.Settings.Default.font_scale*100.0  );
            tb_input.Text = Properties.Settings.Default.default_text;
            lspacing.Text = (Properties.Settings.Default.linespace).ToString();
            letspacing.Text = Properties.Settings.Default.letter_spacing;
            toolStripButton2.Checked = Properties.Settings.Default.fonts_all;

            if (Properties.Settings.Default.preview_multiplier == 1) { preview_mag1.Checked = true; }
            if (Properties.Settings.Default.preview_multiplier == 2) { preview_mag2.Checked = true; }
            if (Properties.Settings.Default.preview_multiplier == 4) { preview_mag4.Checked = true; }

            update_bed_size();
        }
       
        private void SaveSettings()
        {
            //saves the application settings from the data in the UI components
            if (!appfault)  //if the app fails to load, wer don't want a save to occur on unload. i.e. if fonts folder is missing
            {
                Properties.Settings.Default.bedwidth = bedwidth.Text;
                Properties.Settings.Default.beddepth = beddepth.Text;
                Properties.Settings.Default.penup = penup.Text;
                Properties.Settings.Default.pendown = pendown.Text;
                Properties.Settings.Default.tspeed = tspeed.Text;
                Properties.Settings.Default.dspeed = dspeed.Text;
                Properties.Settings.Default.homex = homex.Checked;
                Properties.Settings.Default.homey = homey.Checked;
                Properties.Settings.Default.homez = homez.Checked;
                Properties.Settings.Default.offsetx = offsetx.Text;
                Properties.Settings.Default.offsety = offsety.Text;
                Properties.Settings.Default.font_scale = double.Parse(fontscale_value.Text);
                Properties.Settings.Default.default_text = tb_input.Text;
                Properties.Settings.Default.linespace = Convert.ToSingle(lspacing.Text);
                Properties.Settings.Default.default_font = FontComboBox.Text;
                Properties.Settings.Default.letter_spacing = letspacing.Text;
                Properties.Settings.Default.fonts_all = toolStripButton2.Checked;

                if (preview_mag1.Checked) { Properties.Settings.Default.preview_multiplier = 1; }
                if (preview_mag2.Checked) { Properties.Settings.Default.preview_multiplier = 2; }
                if (preview_mag4.Checked) { Properties.Settings.Default.preview_multiplier = 4; }
                
                Properties.Settings.Default.Save();
            }
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //font preview
            String pic_file = (Application.StartupPath) + "\\fonts\\" + FontComboBox.Text + ".png";
            Process.Start(@pic_file);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            fontscale_value.Text = (trackBar1.Value/100.0).ToString();
            update_font_size();
        }

        private void fontscale_value_DoubleClick(object sender, EventArgs e)
        {
            fontscale_value.Text = "0.2";
            trackBar1.Value = 20;
            update_font_size();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save settings on close
            SaveSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //runs the render routine. 'False' argument means it won't offer to save the GCode file.
            render_stuff(false);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //runs the render routine. 'True' argument means it will offer to save the GCode file.
            render_stuff(true);
        }

        private void render_stuff(bool saving)
        {
            //To my future self and anyone else, Sorry

            button1.Enabled = false;                                                //disable the render and preview buttons
            button2.Enabled = false;
            toolStripStatusLabel3.Text = "Rendering...Please wait";
            Application.DoEvents();                                                 //take a breath

            double GX = 0;
            double GY = 0;
            double accum_x = 0;
            double accum_y = 0;

            double scale = double.Parse(fontscale_value.Text);                      //get the font scale
            double char_height = h_height * scale;                                  //scale up the character height
            double line_spacing = Convert.ToSingle(lspacing.Text) * scale;          //scale up the line spacing  
            double letter_spacing = Convert.ToSingle(letspacing.Text) * scale;      //scale up the letter spacing

            double offx = Convert.ToInt32(offsetx.Text);                            //get the X Offset from the UI
            double offy = Convert.ToInt32(offsety.Text);                            //get the Y Offset from the UI

            bool out_of_bounds = false;                                             //init a boolean for general plotting fault
            double max_x = Convert.ToInt32(bedwidth.Text);                          //get the Bed X setting from the UI
            double max_y = Convert.ToInt32(beddepth.Text);                          //get the Bed Y setting from the UI

            int F_draw = Convert.ToInt32(dspeed.Text) * 60;                         //get the Draw speed setting from the UI
            int F_travel = Convert.ToInt32(tspeed.Text) * 60;                       //get the Travel speed setting from the UI

            double lastx = 0;                                                       //keep track of where we were for pen up/pen down test
            double lasty = 0;

            Bitmap preview = new Bitmap(pb_preview.Width, pb_preview.Height);       //init the picturebox
            Graphics previewGraphics = Graphics.FromImage(preview);
            Pen semiTransPen = new Pen(Color.FromArgb(25, 255, 0, 0), 2);           //create a transparent red pen for the margins (offset)

            //draw red offset lines
            previewGraphics.DrawLine(semiTransPen, 0, Convert.ToSingle((offy* preview_mag)), pb_preview.Width, Convert.ToSingle((offy* preview_mag)));  //horizontal line X
            previewGraphics.DrawLine(semiTransPen, Convert.ToSingle(offx * preview_mag), 0, Convert.ToSingle(offx * preview_mag), pb_preview.Height);   //vertical line Y
            
            string output = "";                                                     //init the GCode output string

            if ( !(!homex.Checked && !homey.Checked && !homez.Checked) ) {          //Do we need to home the printer?
                output+= "G28 " + (homex.Checked?"X":"") + " " + (homey.Checked ? "Y" : "") + " " + (homez.Checked ? "Z" : "") + " F" + F_travel + "\r\n";
            }
            output+= "G0 Z" + penup.Text + " F" + F_travel + "\r\n";                //Pen up before any moves
            
            string[] lines = (tb_input.Text).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None); //split the text input up in to lines
            for (int ptr = 0; ptr < lines.Length; ptr++)                            //interate through the lines
            {
                string thisline = lines[ptr];                                       //gets the line
                for (int a = 0; a < thisline.Length; a++)                           //interate through each character of the line
                {
                    //init cnum - this string is a map of the font. the index of the character aligns with the font_chars array for the character data.
                    //Language other than english won't map correctly here - perhaps this can be stored in the font files?
                    int cnum = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~".IndexOf(thisline.Substring(a, 1));
                    double thewidth = Convert.ToInt32(font_chars[cnum][1]);         //gets the character width
                    
                    if (cnum != 0)                                                  //if the index is 0, this is a space
                    {
                        for (int b = 0; b < font_chars[cnum].Length / 4; b++)       //loop through the stroke x/y pairs
                        {
                            double x1 = Convert.ToDouble(font_chars[cnum][(b * 4) + 3] * scale);        //the +3 is because there are 3 array elements prior to x/y pair data in the array.
                            double y1 = Convert.ToDouble(font_chars[cnum][(b * 4) + 3 + 1] * scale);
                            double x2 = Convert.ToDouble(font_chars[cnum][(b * 4) + 3 + 2] * scale);
                            double y2 = Convert.ToDouble(font_chars[cnum][(b * 4) + 3 + 3] * scale);

                            double draw_x1 = accum_x + x1 + offx;                   //calculate the scaled points for the picutrebox
                            double draw_y1 = ( offy + accum_y) + y1;
                            double draw_x2 = accum_x + x2 + offx;
                            double draw_y2 = ( offy + accum_y) + y2;

                            previewGraphics.DrawLine(Pens.Blue, Convert.ToInt32(draw_x1 * preview_mag), Convert.ToInt32(draw_y1 * preview_mag), Convert.ToInt32(draw_x2 * preview_mag), Convert.ToInt32(draw_y2 * preview_mag));    //draw a stroke to the picturebox

                            //start a pen stroke
                            GX = accum_x + x1 + offx;                               //calculate the GCode X value
                            GY = (char_height - y1) + (max_y - offy) - accum_y;     //calculate the GCode Y value

                            if (lastx == accum_x + x1 + offx && lasty == (char_height - y1) + (max_y - offy))           //test if the pen needs to raise for a travel
                            {
                                output += "G0 X" + GX.ToString() + " Y" + (GY).ToString() + " F" + F_draw + "\r\n";     //write the move to the output string
                            }
                            else
                            {
                                output += "G0 Z" + penup.Text + " F" + F_travel + "\r\n";                               //raise the pen - Pen up
                                output += "G0 X" + GX.ToString() + " Y" + GY.ToString() + " F" + F_draw + "\r\n";       //move the pen      
                                output += "G0 Z" + (dryrun.Checked ? penup.Text : pendown.Text) + " F" + F_travel + "\r\n";     //put the pen down (unless dry run is on)
                               
                            }
                            if (Convert.ToInt32(GX) > max_x || Convert.ToInt32(GX) < 0) out_of_bounds = true;       //check if we went out of bounds
                            if (Convert.ToInt32(GY) > max_y || Convert.ToInt32(GY) < 0) out_of_bounds = true;

                            //end the pen stroke
                            GX = (accum_x + x2 + offx);                             //calculate the GCode X value
                            GY = ((char_height - y2) + (max_y - offy)) - accum_y;   //calculate the GCode Y value
                            output += "G0 X" + GX.ToString() + " Y" + GY.ToString() + " F" + F_draw + "\r\n";       //write the move to the output string

                            lastx = accum_x + x2 + offx;                            //keep the last x/y so we can test it for pen up on next loop
                            lasty = (char_height - y2) + (max_y - offy);
                        }
                        accum_x += (Convert.ToDouble(thewidth) * scale) + letter_spacing;   //accumulated X value plus spacing
                    }
                    else
                    {
                        accum_x += Convert.ToDouble(thewidth) * scale + letter_spacing; //accumulated X value (space) plus spacing
                    }
                }
                accum_x = 0;    //CR                                                    //reset the accumulated X value
                accum_y += char_height + line_spacing;  //LF                            //increment the accumulated Y value plus spacing
                //end lines loop
            }
            //end of ploting moves
            output += "G0 Z" + penup.Text + " F" + F_travel + "\r\n";                   //Raise the pen

            if (!(!homex.Checked && !homey.Checked))                                    //Home the pen (if enabled in UI)
            {
                output += "G0 " + (homex.Checked ? "X0" : "") + " " + (homey.Checked ? "Y0" : "") + " F" + F_travel + "\r\n";
            }

            pb_preview.Image = preview;                                                 //write the preview image to the picturebox
            if (saving)                                                                 //if "Render GCode" was clicked, offer to save the GCode file
            {
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = last_filename!=""? last_filename : "3DWriter.gcode";
                save.Filter = "Gcode File | *.gcode";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    last_filename = save.FileName;
                    StreamWriter writer = new StreamWriter(save.OpenFile());
                    writer.WriteLine(output);
                    writer.Dispose();
                    writer.Close();
                }
            }
            toolStripStatusLabel3.Text = "";

            if (out_of_bounds) {                                                        //Complain about life
                MessageBox.Show("Warning: Pen went out of bounds !");
            }
            button1.Enabled = true;                                                     //re-enable the buttons
            button2.Enabled = true;
        }

      

        private void update_bed_size()
        {
            //update the preview picturebox and form size based on bed x/y settings
            //also sets the preview magnification level
            if (preview_mag1.Checked)
            {
                preview_mag = 1;
            }
            if (preview_mag2.Checked)
            {
                preview_mag = 2;
            }
            if (preview_mag4.Checked)
            {
                preview_mag = 4;
            }

            if (bedwidth.Text != "" && IsNumeric(bedwidth.Text) && int.Parse(bedwidth.Text) > 10)
            {
                if (int.Parse(bedwidth.Text) > Screen.PrimaryScreen.Bounds.Width ) { bedwidth.Text = (Screen.PrimaryScreen.Bounds.Width-700).ToString(); }
                bedwidth.BackColor = System.Drawing.SystemColors.Window;
                pb_preview.Width = Convert.ToInt32(bedwidth.Text) * preview_mag;        //apply the magnification factor
                this.Width = pb_preview.Width + 700;                                    //resize the form to fit new preview size
            }
            else
            {
                bedwidth.BackColor = Color.Red;                                         //incorrect value entered, make it visible
            }

            if (beddepth.Text != "" && IsNumeric(beddepth.Text) && int.Parse(beddepth.Text) > 10)
            {
                if (int.Parse(beddepth.Text) > Screen.PrimaryScreen.Bounds.Height) { beddepth.Text = (Screen.PrimaryScreen.Bounds.Height-170).ToString(); }
                beddepth.BackColor = System.Drawing.SystemColors.Window;
                pb_preview.Height = Convert.ToInt32(beddepth.Text) * preview_mag;       //apply the magnification factor
                this.Height = (pb_preview.Height + 170) < 565 ? 565 : (pb_preview.Height + 170);    //resize the form to fit new preview size
            }
            else
            {
                beddepth.BackColor = Color.Red;                                         //incorrect value entered, make it visible
            }
            
        }

        private void bedwidth_TextChanged(object sender, EventArgs e)
        {
            update_bed_size();
        }

        private void beddepth_TextChanged(object sender, EventArgs e)
        {
            update_bed_size();
        }

        private void FontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_font(FontComboBox.Text);
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/boy1dr");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripButton2.Text = (toolStripButton2.Checked ? "Simple Fonts" : "All fonts");
            LoadFonts(toolStripButton2.Checked);
        }

        private bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        private void label28_DoubleClick(object sender, EventArgs e)
        {
            fontscale_value.Text = "0.2";
            trackBar1.Value = 20;
            update_font_size();
        }

        private void label29_DoubleClick(object sender, EventArgs e)
        {
            fontscale_value.Text = "0.35";
            trackBar1.Value = 35;
            update_font_size();
        }

        private void label30_DoubleClick(object sender, EventArgs e)
        {
            fontscale_value.Text = "0.5";
            trackBar1.Value = 50;
            update_font_size();
        }

        private void resetDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //reset default - these are just safe settings i used initially
            bedwidth.Text = "200";
            beddepth.Text = "200";
            penup.Text = "50";
            pendown.Text = "45";
            tspeed.Text = "100";
            dspeed.Text = "40";
            homex.Checked = true;
            homey.Checked = true;
            homez.Checked = true;
            offsetx.Text = "45";
            offsety.Text = "45";
            fontscale_value.Text = "0.2";
            trackBar1.Value = 20;
            tb_input.Text = "Welcome to 3DWriter!";
            lspacing.Text = "0";
            letspacing.Text = "0";
            toolStripButton2.Checked = true;
            FontComboBox.Text = "cursive";
            preview_mag2.Checked = true;
            update_bed_size();
       
        }

        private void preview_mag1_Click(object sender, EventArgs e)
        {
            update_bed_size();
        }

        private void preview_mag2_Click(object sender, EventArgs e)
        {
            update_bed_size();
        }

        private void preview_mag4_Click(object sender, EventArgs e)
        {
            update_bed_size();
        }

        //Wow you made it, take a break :P
    }
}
