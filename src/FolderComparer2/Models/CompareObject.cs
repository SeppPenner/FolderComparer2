// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareObject.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The compare object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.Models;

/// <summary>
/// The compare object.
/// </summary>
public class CompareObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompareObject"/> class.
    /// </summary>
    /// <param name="number">The number.</param>
    public CompareObject(int number)
    {
        this.Number = number;
    }

    /// <summary>
    /// Gets or sets the sub folder count.
    /// </summary>
    public double SubFolderCount { get; set; }

    /// <summary>
    /// Gets or sets the file count.
    /// </summary>
    public double FileCount { get; set; }

    /// <summary>
    /// Gets or sets the byte size.
    /// </summary>
    public double ByteSize { get; set; }

    /// <summary>
    /// Gets or sets the size.
    /// </summary>
    public double Size { get; set; }

    /// <summary>
    /// Gets or sets the unit.
    /// </summary>
    public Unit Unit { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    public int Number { get; }
}
