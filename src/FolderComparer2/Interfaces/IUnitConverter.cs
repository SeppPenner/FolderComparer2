// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitConverter.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The unit converter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.Interfaces
{
    using FolderComparer2.Models;

    /// <summary>
    /// The unit converter interface.
    /// </summary>
    public interface IUnitConverter
    {
        /// <summary>
        /// Evaluates the byte size.
        /// </summary>
        /// <param name="compare">The compare object.</param>
        void EvaluateByteSize(CompareObject compare);
    }
}