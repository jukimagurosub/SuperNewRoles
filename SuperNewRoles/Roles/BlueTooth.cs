using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Hazel;
using SuperNewRoles.Helpers;
using SuperNewRoles.Patch;
using UnityEngine;

namespace SuperNewRoles.Roles
{
    class BlueTooth
    {
        CustomRPC.PlaySoundRPC(__instance(この__instanceはPlayerControl __instance).PlayerId,Sounds.KillSound);
    }  
} 
