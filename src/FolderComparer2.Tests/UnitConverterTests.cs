// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitConverterTests.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A test class to test the unit converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.Tests;

/// <summary>
/// A test class to test the unit converter.
/// </summary>
[TestClass]
public sealed class UnitConverterTests
{
    /// <summary>
    /// The delta used to compare the byte sizes, the same magnitude the coloring in the form uses.
    /// </summary>
    private const double Delta = 0.00001;

    /// <summary>
    /// The unit converter under test.
    /// </summary>
    private readonly IUnitConverter unitConverter = new UnitConverter();

    /// <summary>
    /// Tests that a size below one kilobyte is left in bytes.
    /// </summary>
    [TestMethod]
    public void SizeBelowOneKilobyteIsLeftInBytes()
    {
        var compare = new CompareObject(1) { Size = 1023, ByteSize = 1023 };
        this.unitConverter.EvaluateByteSize(compare);
        Assert.AreEqual(1023d, compare.Size, Delta);
        Assert.AreEqual(Unit.B, compare.Unit);
    }

    /// <summary>
    /// Tests that an empty folder is left in bytes.
    /// </summary>
    [TestMethod]
    public void SizeOfZeroIsLeftInBytes()
    {
        var compare = new CompareObject(1);
        this.unitConverter.EvaluateByteSize(compare);
        Assert.AreEqual(0d, compare.Size, Delta);
        Assert.AreEqual(Unit.B, compare.Unit);
    }

    /// <summary>
    /// Tests that an exact power of 1024 ends up as one of the matching unit.
    /// </summary>
    /// <param name="size">The size in bytes.</param>
    /// <param name="expectedUnit">The expected unit.</param>
    [TestMethod]
    [DataRow(1024d, Unit.KB)]
    [DataRow(1048576d, Unit.Mb)]
    [DataRow(1073741824d, Unit.Gb)]
    [DataRow(1099511627776d, Unit.Tb)]
    [DataRow(1125899906842624d, Unit.Pb)]
    [DataRow(1152921504606846976d, Unit.Eb)]
    public void ExactPowerOfOneKilobyteIsConvertedToTheMatchingUnit(double size, Unit expectedUnit)
    {
        var compare = new CompareObject(1) { Size = size, ByteSize = size };
        this.unitConverter.EvaluateByteSize(compare);
        Assert.AreEqual(1d, compare.Size, Delta);
        Assert.AreEqual(expectedUnit, compare.Unit);
    }

    /// <summary>
    /// Tests that the chain stops at the last unit that is still below 1024.
    /// </summary>
    [TestMethod]
    public void SizeJustBelowTheNextUnitStaysInTheCurrentUnit()
    {
        var compare = new CompareObject(1) { Size = 1048575, ByteSize = 1048575 };
        this.unitConverter.EvaluateByteSize(compare);
        Assert.AreEqual(1023.9990234375d, compare.Size, Delta);
        Assert.AreEqual(Unit.KB, compare.Unit);
    }

    /// <summary>
    /// Tests that everything above one exabyte stays in exabytes, because the chain ends there.
    /// </summary>
    [TestMethod]
    public void SizeAboveOneExabyteStaysInExabytes()
    {
        var compare = new CompareObject(1) { Size = 1180591620717411303424d, ByteSize = 1180591620717411303424d };
        this.unitConverter.EvaluateByteSize(compare);
        Assert.AreEqual(1024d, compare.Size, Delta);
        Assert.AreEqual(Unit.Eb, compare.Unit);
    }

    /// <summary>
    /// Tests that the raw byte count survives the conversion, the coloring in the form compares it.
    /// </summary>
    [TestMethod]
    public void ByteSizeIsNotTouchedByTheConversion()
    {
        var compare = new CompareObject(1) { Size = 5242880, ByteSize = 5242880 };
        this.unitConverter.EvaluateByteSize(compare);
        Assert.AreEqual(5d, compare.Size, Delta);
        Assert.AreEqual(Unit.Mb, compare.Unit);
        Assert.AreEqual(5242880d, compare.ByteSize, Delta);
    }

    /// <summary>
    /// Tests that the converter leaves the number of the compare object alone, the form switches on it.
    /// </summary>
    [TestMethod]
    public void NumberOfTheCompareObjectIsNotTouched()
    {
        var compare = new CompareObject(2) { Size = 2048, ByteSize = 2048 };
        this.unitConverter.EvaluateByteSize(compare);
        Assert.AreEqual(2, compare.Number);
    }
}
