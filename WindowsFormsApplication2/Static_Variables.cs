﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication2
{
    static class Static_Variables
    {


        public static int button1_threshold = 15;
        public static int button2_threshold = 15;
        public static int button3_threshold = 15;

        // three rectangle will store each of the virtual button
        public static Rectangle[] rects;

        //detect motion or not
        public static bool IsDetecting = false;

        // should image show virtual buttons
        public static bool ShowButtons = true;

        //motion percentile
        public static int[] percentile_motion = new int[3];

        //motion percentile diffrence
        public static int[] percentile_motion_diffrence = new int[3];

        //make rectangles
        public static void Make_rectangles(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;

            // get rectangles for each virtual button
            Static_Variables.rects = new Rectangle[3];

            int rect_width = Math.Max(width / 5, height / 5);
            Size size = new Size(rect_width, rect_width);

            /* first virtual button at top and other two at bottom corners
             * you can further add more buttons here */
            int x = (width/2)-(rect_width/2);
            int y = 0;
            Point p = new Point(x, y);
            Static_Variables.rects[0] = new Rectangle(p, size);

            /*second virtual button*/
            x = 0;
            y = (height - 1) - rect_width;
            p = new Point(x, y);
            Static_Variables.rects[1] = new Rectangle(p, size);

            /*third virtual button*/
            x = (width-1)-rect_width;
            y = (height - 1) - rect_width;
            p = new Point(x, y);
            Static_Variables.rects[2] = new Rectangle(p, size);
        }
    }
}
