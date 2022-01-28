using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Net;
using System.IO;
using System;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;
namespace SuperNewRoles
{
    public static class ConfigRoles
    {
        public static ConfigEntry<bool> StreamerMode { get; set; }
        public static ConfigEntry<bool> AutoUpdate { get; set; }
        public static ConfigEntry<bool> DebugMode { get; set; }
        public static void Load()
        {
            StreamerMode = SuperNewRolesPlugin.Instance.Config.Bind("Custom", "Enable Streamer Mode", false);
            AutoUpdate = SuperNewRolesPlugin.Instance.Config.Bind("Custom", "Auto Update", true);
            DebugMode = SuperNewRolesPlugin.Instance.Config.Bind("Custom", "Debug Mode", false);
            
        }
    }
}