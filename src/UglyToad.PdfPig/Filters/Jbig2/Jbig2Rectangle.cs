﻿namespace UglyToad.PdfPig.Filters.Jbig2
{
    internal struct Jbig2Rectangle
    {
        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Create a new <see cref="Jbig2Rectangle"/>.
        /// </summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public Jbig2Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
