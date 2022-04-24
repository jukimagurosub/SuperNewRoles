using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Hazel;
using SuperNewRoles.Helpers;
using SuperNewRoles.Patch;
using UnityEngine;
using SuperNewRoles.CustomRPC;
using HarmonyLib;
using SuperNewRoles.Helpers;
using SuperNewRoles.Mode;
using SuperNewRoles.Mode.SuperHostRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperNewRoles.Roles
{
    class BlueTooth
    {
        public static void BlueToothSound(GameData.PlayerInfo __instance)
        {
            if (__instance == null) return;
            {
                if (AmongUsClient.Instance.AmHost)
                {
                    if (__instance != null && RoleClass.BlueTooth.BlueToothPlayer.IsCheckListPlayerControl(__instance.Object))
                    {

                        CustomRPC.PlaySoundRPC(,__instance(この__instanceはPlayerControl ,__instance).PlayerId, Sound.KillSound);
                    }
                }
            }
        }
    }
}


