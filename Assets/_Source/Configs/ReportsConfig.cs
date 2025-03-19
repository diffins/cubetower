using System;
using System.Collections.Generic;
using System.Linq;
using _Source.Enums;
using UnityEngine;
using UnityEngine.Localization;

namespace _Source.Configs
{
    [CreateAssetMenu(fileName = "ReportsConfig", menuName = "Configs/ReportsConfig")]
    public class ReportsConfig : ScriptableObject
    {
        public List<ReportConfig> Reports = new List<ReportConfig>();

        public LocalizedString GetMessage(ReportType type)
        {
            return Reports.FirstOrDefault(x => x.Type == type)!.Message;
        }
        
        [Serializable]
        public struct ReportConfig
        {
            public ReportType Type;
            public LocalizedString Message;
        }
    }
}