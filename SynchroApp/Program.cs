using SynchroApp.Classes;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Timers;

namespace SynchroApp
{
    public class Program
    {
        #region Private Fields

        private static TimeSpan synchroPeriod = TimeSpan.Zero;

        private static System.Timers.Timer timer;

        private static FilesData sourceData;

        private static FilesData replicaData;

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            sourceData = new FilesData(args[0]);
            replicaData = new FilesData(args[1]);
            Logger.SetLogFile(args[2]);

            if (!TimeSpan.TryParse(args[3], out synchroPeriod))
            {
                Logger.LogMessage($"[ERROR] Synchronization time span is in wrong format: {args[3]} \n" +
                    $"[INFO] Please input time span in proper format: dd:hh:mm:ss");
                return;
            }

            Logger.LogUserData(sourceData, replicaData, synchroPeriod);

            SetTimer(synchroPeriod);

            Console.ReadLine();
        }

        private static void SetTimer(TimeSpan synchroPeriod)
        {
            timer = new System.Timers.Timer(synchroPeriod.TotalSeconds * 1000);
            timer.Elapsed += PerformSynchronization;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void PerformSynchronization(object? sender, ElapsedEventArgs e)
        {
            Logger.LogMessage($"Synchronization started at: {e.SignalTime}");

            DataLoader sourceDataLoader = new(sourceData);
            DataLoader replicaDataLoader = new(replicaData);

            if (!sourceDataLoader.LoadData() || !replicaDataLoader.LoadData())
            {
                sourceData.ClearData();
                replicaData.ClearData();
                return;
            }

            DataComparator dc = new(sourceData, replicaData);

            DataSynchronizer ds = new DataSynchronizer(sourceData, replicaData);
            ds.SynchronizeData(dc);

            sourceData.ClearData();
            replicaData.ClearData();

            Logger.LogMessage($"Synchronization ended at: {DateTime.Now}");
        }

        #endregion Private Methods
    }
}