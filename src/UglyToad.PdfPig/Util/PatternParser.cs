﻿namespace UglyToad.PdfPig.Util
{
    using System;
    using System.Linq;
    using UglyToad.PdfPig.Content;
    using UglyToad.PdfPig.Core;
    using UglyToad.PdfPig.Filters;
    using UglyToad.PdfPig.Graphics.Colors;
    using UglyToad.PdfPig.Parser.Parts;
    using UglyToad.PdfPig.Tokenization.Scanner;
    using UglyToad.PdfPig.Tokens;

    internal static class PatternParser
    {
        /// <summary>
        /// TODO
        /// </summary>
        public static PatternColor Create(IToken pattern, IPdfTokenScanner scanner, IResourceStore resourceStore, ILookupFilterProvider filterProvider)
        {
            if (DirectObjectFinder.TryGet(pattern, scanner, out DictionaryToken patternArray))
            {
                int patternType = (patternArray.Data[NameToken.PatternType] as NumericToken).Int;

                if (!(patternArray.Data.ContainsKey(NameToken.Matrix) &&
                    DirectObjectFinder.TryGet(patternArray.Data[NameToken.Matrix], scanner, out ArrayToken patternMatrix)))
                {
                    // optional - Default value: the identity matrix [1 0 0 1 0 0]
                    patternMatrix = new ArrayToken(new decimal[] { 1, 0, 0, 1, 0, 0 }.Select(v => new NumericToken(v)).ToArray());
                }

                DictionaryToken patternExtGState = null;
                if (!(patternArray.Data.ContainsKey(NameToken.ExtGState) &&
                    DirectObjectFinder.TryGet(patternArray.Data[NameToken.ExtGState], scanner, out patternExtGState)))
                {
                    // optional
                }

                switch (patternType)
                {
                    case 1: // Tiling
                        throw new NotImplementedException("Tiling style pattern");

                    case 2: // Shading
                        Shading patternShading = null;
                        if (DirectObjectFinder.TryGet(patternArray.Data[NameToken.Shading], scanner, out DictionaryToken patternShadingDic))
                        {
                            patternShading = ShadingParser.Create(patternShadingDic, scanner, resourceStore, filterProvider);
                        }
                        else if (DirectObjectFinder.TryGet(patternArray.Data[NameToken.Shading], scanner, out StreamToken patternShadingStr))
                        {
                            patternShading = ShadingParser.Create(patternShadingStr, scanner, resourceStore, filterProvider);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                        return new PatternColor(patternType, patternMatrix, patternShading, patternExtGState);

                    default:
                        throw new PdfDocumentFormatException($"Invalid Pattern type encountered in page resource dictionary: {patternType}.");
                }
            }
            else
            {
                throw new PdfDocumentFormatException($"Invalid Pattern token encountered in page resource dictionary: {pattern}.");
            }
        }
    }
}
