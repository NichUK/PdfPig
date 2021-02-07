﻿using System;
using System.Collections.Generic;
using UglyToad.PdfPig.Tokens;

namespace UglyToad.PdfPig.Content
{
    /// <summary>
    /// An optional content group is a dictionary representing a collection of graphics
    /// that can be made visible or invisible dynamically by users of viewers applications.
    /// </summary>
    public class OptionalContentGroupElement
    {
        /// <summary>
        /// The type of PDF object that this dictionary describes.
        /// <para>Must be OCG for an optional content group dictionary.</para>
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// The name of the optional content group, suitable for presentation in a viewer application's user interface.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A single name or an array containing any combination of names.
        /// <para>Default value is 'View'.</para>
        /// </summary>
        public IReadOnlyList<string> Intent { get; }

        /// <summary>
        /// A usage dictionary describing the nature of the content controlled by the group.
        /// </summary>
        public IDictionary<string, object> Usage { get; }

        /// <summary>
        /// Underlying <see cref="MarkedContentElement"/>.
        /// </summary>
        public MarkedContentElement MarkedContent { get; }

        internal OptionalContentGroupElement(MarkedContentElement markedContentElement)
        {
            MarkedContent = markedContentElement;

            // Type - Required
            if (markedContentElement.Properties.TryGet(NameToken.Type, out NameToken type))
            {
                Type = type.Data;
            }
            else if (markedContentElement.Properties.TryGet(NameToken.Type, out StringToken typeStr))
            {
                Type = typeStr.Data;
            }
            else
            {
                throw new ArgumentException($"Cannot parse optional content's {nameof(Type)} from {nameof(markedContentElement.Properties)}. This is a required field.", nameof(markedContentElement.Properties));
            }

            switch (Type)
            {
                case "OCG": // Optional content group dictionary
                    // Name - Required
                    if (markedContentElement.Properties.TryGet(NameToken.Name, out NameToken name))
                    {
                        Name = name.Data;
                    }
                    else if (markedContentElement.Properties.TryGet(NameToken.Name, out StringToken nameStr))
                    {
                        Name = nameStr.Data;
                    }
                    else
                    {
                        throw new ArgumentException($"Cannot parse optional content's {nameof(Name)} from {nameof(markedContentElement.Properties)}. This is a required field.", nameof(markedContentElement.Properties));
                    }

                    // Intent - Optional
                    if (markedContentElement.Properties.TryGet(NameToken.Intent, out NameToken intentName))
                    {
                        Intent = new string[] { intentName.Data };
                    }
                    else if (markedContentElement.Properties.TryGet(NameToken.Intent, out StringToken intentStr))
                    {
                        Intent = new string[] { intentStr.Data };
                    }
                    else if (markedContentElement.Properties.TryGet(NameToken.Intent, out ArrayToken intentArray))
                    {
                        List<string> intentList = new List<string>();
                        foreach (var token in intentArray.Data)
                        {
                            if (token is NameToken nameA)
                            {
                                intentList.Add(nameA.Data);
                            }
                            else if (token is StringToken strA)
                            {
                                intentList.Add(strA.Data);
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                        Intent = intentList;
                    }
                    else
                    {
                        // Default value is 'View'.
                        Intent = new string[] { "View" };
                    }

                    // Usage - Optional
                    if (markedContentElement.Properties.TryGet(NameToken.Usage, out DictionaryToken usage))
                    {
                        throw new NotImplementedException();
                    }
                    break;

                case "OCMD":
                    // OCGs - Optional
                    if (markedContentElement.Properties.TryGet(NameToken.Ocgs, out DictionaryToken ocgsD))
                    {
                        // dictionary or array
                        throw new NotImplementedException();
                    }
                    else if (markedContentElement.Properties.TryGet(NameToken.Ocgs, out ArrayToken ocgsA))
                    {
                        // dictionary or array
                        throw new NotImplementedException();
                    }

                    // P - Optional
                    if (markedContentElement.Properties.TryGet(NameToken.P, out NameToken p))
                    {
                        throw new NotImplementedException();
                    }

                    // VE - Optional
                    if (markedContentElement.Properties.TryGet(NameToken.VE, out ArrayToken ve))
                    {
                        throw new NotImplementedException();
                    }
                    break;

                default:
                    throw new ArgumentException($"Unknown Optional Content of type '{Type}'.", nameof(Type));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return $"{Type} - {Name} [{string.Join(",", Intent)}]: {MarkedContent?.ToString()}";
        }
    }
}
