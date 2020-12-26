// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Unit.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The unit enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.Enumerations
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The unit enumeration.
    /// </summary>
    public enum Unit
    {
        /// <summary>
        /// The byte unit.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        B,

        /// <summary>
        /// The kilobyte unit.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        KB,

        /// <summary>
        /// The megabyte unit.
        /// </summary>
        Mb,

        /// <summary>
        /// The gigabyte unit.
        /// </summary>
        Gb,

        /// <summary>
        /// The terrabyte unit.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        Tb,

        /// <summary>
        /// The petabyte unit.
        /// </summary>
        Pb,

        /// <summary>
        /// The exabyte unit.
        /// </summary>
        Eb
    }
}