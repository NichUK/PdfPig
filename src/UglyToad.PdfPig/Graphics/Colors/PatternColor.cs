﻿namespace UglyToad.PdfPig.Graphics.Colors
{
    using System;
    using System.Collections.Generic;
    using UglyToad.PdfPig.Tokens;

    /// <summary>
    /// TODO
    /// </summary>
    public class PatternColor : IColor, IEquatable<PatternColor>
    {
        /// <summary>
        /// 1 for tilling, 2 for shading.
        /// </summary>
        public int PatternType { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public ArrayToken Matrix { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public Shading Shading { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public DictionaryToken ExtGState { get; }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="patternType"></param>
        /// <param name="matrix"></param>
        /// <param name="shading"></param>
        /// <param name="extGState"></param>
        public PatternColor(int patternType, ArrayToken matrix, Shading shading, DictionaryToken extGState)
        {
            PatternType = patternType;
            Matrix = matrix;
            Shading = shading;
            ExtGState = extGState;
        }

        #region IColor
        /// <inheritdoc/>
        public ColorSpace ColorSpace { get; } = ColorSpace.Pattern;

        /// <inheritdoc/>
        public (decimal r, decimal g, decimal b) ToRGBValues()
        {
            throw new InvalidOperationException("Cannot call ToRGBValues in a Pattern color.");
        }
        #endregion

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is RGBColor color)
            {
                return Equals(color);
            }

            return false;
        }

        /// <inheritdoc/>
        public bool Equals(PatternColor other)
        {
            return PatternType == other.PatternType &&
                   Matrix == other.Matrix &&
                   Shading == other.Shading &&
                   ExtGState == other.ExtGState;
        }

        /// <inheritdoc />
        public override int GetHashCode() => (PatternType, Matrix, Shading, ExtGState).GetHashCode();

        /// <summary>
        /// Equals.
        /// </summary>
        public static bool operator ==(PatternColor color1, PatternColor color2) =>
            EqualityComparer<PatternColor>.Default.Equals(color1, color2);

        /// <summary>
        /// Not Equals.
        /// </summary>
        public static bool operator !=(PatternColor color1, PatternColor color2) => !(color1 == color2);

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Pattern: ({PatternType})"; // TODO
        }
    }
}
