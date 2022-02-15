// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitConverter.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The unit converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.Implementation;

/// <inheritdoc cref="IUnitConverter"/>
/// <summary>
/// The unit converter.
/// </summary>
/// <seealso cref="IUnitConverter"/>
public class UnitConverter : IUnitConverter
{
    /// <inheritdoc cref="IUnitConverter"/>
    /// <summary>
    /// Evaluates the byte size.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    /// <seealso cref="IUnitConverter"/>
    public void EvaluateByteSize(CompareObject compare)
    {
        if (!EvaluateKb(compare))
        {
            return;
        }

        if (!EvaluateMb(compare))
        {
            return;
        }

        if (!EvaluateGb(compare))
        {
            return;
        }

        if (!EvaluateTb(compare))
        {
            return;
        }

        if (!EvaluatePb(compare))
        {
            return;
        }

        EvaluateEb(compare);
    }

    /// <summary>
    /// Evaluates the byte size in kilobytes.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    /// <returns>A value indicating whether the byte size could be evaluated to a higher data unit or not.</returns>
    private static bool EvaluateKb(CompareObject compare)
    {
        if (compare.Size < 1024)
        {
            return false;
        }

        compare.Size /= 1024;
        compare.Unit = Unit.KB;
        return true;
    }

    /// <summary>
    /// Evaluates the byte size in megabytes.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    /// <returns>A value indicating whether the byte size could be evaluated to a higher data unit or not.</returns>
    private static bool EvaluateMb(CompareObject compare)
    {
        if (compare.Size < 1024)
        {
            return false;
        }

        compare.Size /= 1024;
        compare.Unit = Unit.Mb;
        return true;
    }

    /// <summary>
    /// Evaluates the byte size in gigabytes.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    /// <returns>A value indicating whether the byte size could be evaluated to a higher data unit or not.</returns>
    private static bool EvaluateGb(CompareObject compare)
    {
        if (compare.Size < 1024)
        {
            return false;
        }

        compare.Size /= 1024;
        compare.Unit = Unit.Gb;
        return true;
    }

    /// <summary>
    /// Evaluates the byte size in terrabytes.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    /// <returns>A value indicating whether the byte size could be evaluated to a higher data unit or not.</returns>
    private static bool EvaluateTb(CompareObject compare)
    {
        if (compare.Size < 1024)
        {
            return false;
        }

        compare.Size /= 1024;
        compare.Unit = Unit.Tb;
        return true;
    }

    /// <summary>
    /// Evaluates the byte size in petabytes.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    /// <returns>A value indicating whether the byte size could be evaluated to a higher data unit or not.</returns>
    private static bool EvaluatePb(CompareObject compare)
    {
        if (compare.Size < 1024)
        {
            return false;
        }

        compare.Size /= 1024;
        compare.Unit = Unit.Pb;
        return true;
    }

    /// <summary>
    /// Evaluates the byte size in exabytes.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    /// <returns>A value indicating whether the byte size could be evaluated to a higher data unit or not.</returns>
    private static bool EvaluateEb(CompareObject compare)
    {
        if (compare.Size < 1024)
        {
            return false;
        }

        compare.Size /= 1024;
        compare.Unit = Unit.Eb;
        return true;
    }
}
