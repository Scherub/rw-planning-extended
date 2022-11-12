using System;
using System.IO;
using UnityEngine;

namespace PlanningExtended.Plans.Persistence
{
    public class PlanFileInfo
    {
        public static readonly Color UnimportantTextColor = new(1f, 1f, 1f, 0.5f);

        public FileInfo FileInfo { get; }

        public string FileName { get; }

        public DateTime LastWriteTime { get; }

        public PlanFileInfo(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            FileName = fileInfo.Name;
            LastWriteTime = fileInfo.LastWriteTime;
        }

        public void LoadData()
        {

        }
    }
}
