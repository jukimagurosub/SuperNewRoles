﻿using HarmonyLib;
using static SuperNewRoles.Roles.EvilGambler;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperNewRoles.Buttons
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnDisconnected))]
    public class OnGameDisconnect
    {

        public static void Prefix(AmongUsClient __instance)
        {
            Roles.SpeedBooster.ResetSpeed();
            Roles.EvilSpeedBooster.ResetSpeed();
            Roles.Clergyman.LightOutEnd();
            Roles.Lighter.LightOutEnd();
            PlayerControl.GameOptions.KillCooldown = EvilGamblerMurder.temp;
        }
    }
   [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    public class OnGameTimeEnd
    {

        public static void Prefix(AmongUsClient __instance, [HarmonyArgument(0)] ref EndGameResult endGameResult)
        {
            Roles.SpeedBooster.ResetSpeed();
            Roles.EvilSpeedBooster.ResetSpeed();
            Roles.Clergyman.LightOutEnd();
            Roles.Lighter.LightOutEnd();
            PlayerControl.GameOptions.KillCooldown = EvilGamblerMurder.temp;
        }
    }
}
