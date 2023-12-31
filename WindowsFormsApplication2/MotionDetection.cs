﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication2
{
    class MotionDetection
    {
        //bool
        bool first_motion_data = false;

        //public delegates
        public delegate void MotionEventHandler(object MotionDetection, EventArgs eventArgs);

        //public event for the class
        public event MotionEventHandler MotionEvent;

        //event firing method
        public void OnMotionEvent(object MotionDetection, EventArgs eventArgs)
        {
            // Check if there are any Subscribers
            if (MotionEvent != null)
            {
                // Call the Event
                MotionEvent(MotionDetection, eventArgs);
            }
        }
        //threshold for rate of change of frame
        public int threshold = 15;

        //check if rate of change has normalized before triggring event
        bool normalized = false;

        //loop
        int loop = 0;

        //max bound
        int max_bound = max_bound_s;

        //max boud
        static public int max_bound_s = 30;

        //average motion percentage over 100 pixels
        public int average_motion_percentage = 0;

        //total moion
        int total = 0;

        //percentile motion
        int motion_percentage = 0;


        //refrence bitmap data
        byte[,] ref_data = null;

        //threshold sensity for change in value of each pixel
        public static byte each_pixel_threshold = 40;

        //process an image
        public int Process(Bitmap image)
        {

            if (!first_motion_data)
            {
                ImageMatrix temp = new ImageMatrix(image);
                ref_data = temp.function;
                first_motion_data = true;
                return 0;
            }
            int width = image.Width;
            int height = image.Height;
            int value = each_pixel_threshold;
            int total_pixels = width * height;
            int pixel_motion = 0;

            byte[,] current_data = new ImageMatrix(image).function;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (Math.Abs(current_data[i, j] - ref_data[i, j]) > value)
                    {
                        pixel_motion++;
                    }
                }
            }
            Array.Copy(current_data, ref_data, current_data.Length);
            motion_percentage = (pixel_motion * 100) / total_pixels;
            motionevent();
            if (motion_percentage < 9)
            {
                normalized = false;
            }
            return motion_percentage;
        }

        public void motionevent()
        {
            if (loop < max_bound)
            {
                total += motion_percentage;
                loop++;
            }
            else
            {
                loop = 0;
                average_motion_percentage = total / max_bound;
                max_bound = max_bound_s;
                total = 0;
                if (!normalized)
                {
                    if (average_motion_percentage > threshold)
                    {
                        OnMotionEvent(this, new EventArgs());
                    }
                    normalized = true;
                }
            }
        }
    }
}
