using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchroApp.Classes;
using System.Text;

[TestClass]
public class SynchroFileTests
{
    #region Public Methods

    [TestMethod]
    public void Equals_ReturnsTrue_ForSamePathAndHash()
    {
        byte[] hash = Encoding.UTF8.GetBytes("test");
        SynchroFile file1 = new SynchroFile("file.txt", hash);
        SynchroFile file2 = new SynchroFile("file.txt", hash);

        Assert.IsTrue(file1.Equals(file2));
    }

    [TestMethod]
    public void Equals_ReturnsFalse_ForDifferentPath()
    {
        byte[] hash = Encoding.UTF8.GetBytes("test");
        SynchroFile file1 = new SynchroFile("file1.txt", hash);
        SynchroFile file2 = new SynchroFile("file2.txt", hash);

        Assert.IsFalse(file1.Equals(file2));
    }

    [TestMethod]
    public void ToString_ReturnsFormattedOutput()
    {
        byte[] hash = Encoding.UTF8.GetBytes("test");
        SynchroFile file = new SynchroFile("path.txt", hash);

        StringAssert.Contains(file.ToString(), "path.txt");
        StringAssert.Contains(file.ToString(), "Hash:");
    }

    #endregion Public Methods
}