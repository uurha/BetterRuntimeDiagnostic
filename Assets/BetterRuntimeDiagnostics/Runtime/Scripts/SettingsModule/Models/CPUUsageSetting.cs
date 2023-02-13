﻿using System;
using Better.Diagnostics.Runtime.InfoDisplayer.Interfaces;
using Better.Diagnostics.Runtime.InfoDisplayer.Models;
using UnityEngine;

namespace Better.Diagnostics.Runtime.SettingsModule.Models
{
    [Serializable]
    public class CPUUsageSetting : BaseSettings
    {
        [Tooltip("In which interval should the CPU usage be updated?")] [SerializeField]
        private UpdateInterval cpuUpdateInterval = 0.5f;

        public override IDebugInfo GetInfo()
        {
            return new CPUUsage(Position, cpuUpdateInterval);
        }
    }
}