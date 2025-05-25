using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchroApp.Classes;
using System.IO;

[TestClass]
public class DataSynchronizerTests
{
    #region Private Fields

    private string sourceDir;

    private string replicaDir;

    #endregion Private Fields

    #region Public Methods

    [TestInitialize]
    public void Setup()
    {
        this.sourceDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        this.replicaDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        Directory.CreateDirectory(this.sourceDir);
        Directory.CreateDirectory(this.replicaDir);

        File.WriteAllText(Path.Combine(this.sourceDir, "file.txt"), "sync me");
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (Directory.Exists(this.sourceDir)) Directory.Delete(this.sourceDir, true);
        if (Directory.Exists(this.replicaDir)) Directory.Delete(this.replicaDir, true);
    }

    [TestMethod]
    public void SynchronizeData_CopiesNewFile()
    {
        FilesData sourceData = new FilesData(this.sourceDir);
        FilesData replicaData = new FilesData(this.replicaDir);

        DataLoader loader1 = new DataLoader(sourceData);
        DataLoader loader2 = new DataLoader(replicaData);

        loader1.LoadData();
        loader2.LoadData();

        DataComparator dc = new DataComparator(sourceData, replicaData);
        DataSynchronizer ds = new DataSynchronizer(sourceData, replicaData);
        ds.SynchronizeData(dc);

        Assert.IsTrue(File.Exists(Path.Combine(this.replicaDir, "file.txt")));
    }

    #endregion Public Methods
}