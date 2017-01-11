# 3DWriter
Use your 3D Printer with a pen to write letters, birthday cards etc  

I couldn't find any small programs that i could use to write letters using my 3D Printer as a plotter so i wrote one.
After mucking about with different fonts i found a fontset called the Hershey fonts. These are the only fonts used in this application since they are primarily stroke based fonts rather than outline fonts that every other program i tried had.

Video demo over on youtube [https://youtu.be/yK_YGwMRR40](https://youtu.be/yK_YGwMRR40)

## How to use it
There are 3 main columns. 
- Text entry
- Preview
- GCode settings

Once you have selected the font from the toolstrip at the top, you can click right next to it on the "Preview" button to see a rendered character set of the font. There is a check button beside that "Simple fonts" which will alternate between the full set of fonts and a simple list.
Fonts in other languages will most likely not map to the english character set. Appologies, i only know english.  

Type some text in to the Text input. Choose your scale (0.2 is pretty close to handwriting) then click the Preview button to see your text rendered in the preview window.  
There are options for scaling up the preview render to make it easier to see what it might look like.  

If your GCode settings are complete, just click "Generate GCode" and save the file for your printer.

![alt text](https://github.com/boy1dr/3DWriter/blob/master/interface.PNG "Interface")

## GCode settings
*Pay close attention here*

Bed X/Bed Y - Set the size of your printers bed here.  
Offset X/Offset Y - My pen is strapped to the side of my extruder so there is an offset.  
Pen up/Pen down - This is the height of the extruder/pen head from your build plate. Test this manually on your printer to get these.  
Travel / Draw speed - The speed at which the printer will move at. Slower is better.  

Line Spacing - The gap between the lines measured in 'units'.  
Letter Spacing - The gap between the letters measured in 'units'. Letters already have individual spacing but sometimes more is required.  

Home X/Y/Z - Before and after the printer does the writing you probably want to home all your axis. Untick those you don't.  
Dry run - Doesn't put the pen down, ever. This is so you can put your paper/card in place and see if it will go where you want it.  

Preview magnification makes no difference to the GCode.  
The GCode output is higher resolution than the preview window.  

## What is a 'unit' ?
The fonts are described in multiple x/y points as integers. Each font has a height around 37.
This isn't mm or pixels, it's just what was used to describe the strokes so i call them units.
This height is multiplied by the scale to render the fonts. Line and letter spacing is also multiplied by scale so the end result is proportional.

## Setting up your printer as a plotter
At first i just used a rubber band to hold the pen on but it wiggled around too much so i printed a custom holder that attached firmly to my extruder. Then i printed mating piece and glued it to my pen (4 colour Bic pen).  
I made it so when the pen tip is retracted it is higher than my nozzle so i can keep the pen there all the time.  
Using a program like Pronterface you can manually move your extruder around to find the correct height for pen up & down, also the X and Y offset so you don't write off the edge of the page.


# USE AT YOUR OWN RISK
The only 3D Printer i own is a RepRap i3 clone and i can confirm it has been tested and works well on that style of printer.
I know from other software made for the ultimaker that sometimes the Z move is the wrong direction, i would advise that you simulate the gcode files prior to printing just to be sure you know what is going to happen. 
And as always when trying new software with your 3D Printer, keep one hand on the off switch :)

## Final notes
I don't claim to be the worlds best programmer, nor am i a self professed 3D Printer expert but i have many years of experience with both and i'm pretty confident that this software works.

I have included the C# 2015 project that you can compile yourself along with a binary build for MS Windows.

I would love to hear your feedback :)
