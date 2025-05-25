using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchroApp.Classes;
using System.IO;

[TestClass]
public class LoggerTests
{
    #region Private Fields

    private string logFile;

    #endregion Private Fields

    #region Public Methods

    [TestInitialize]
    public void Setup()
    {
        this.logFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".txt");
        Logger.SetLogFile(this.logFile);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(this.logFile))
        {
            File.Delete(this.logFile);
        }
    }

    [TestMethod]
    public void LogMessage_WritesToFile()
    {
        Logger.LogMessage("Test log message");

        string content = File.ReadAllText(this.logFile);

        StringAssert.Contains(content, "Test log message");
    }

    #endregion Public Methods
}