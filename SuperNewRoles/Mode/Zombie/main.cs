﻿using HarmonyLib;
using SuperNewRoles.Helpers;
using SuperNewRoles.Mode.SuperHostRoles;
using SuperNewRoles.Patch;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static SuperNewRoles.EndGame.CheckGameEndPatch;

namespace SuperNewRoles.Mode.Zombie
{
    static class main
    {
        public static Color Zombiecolor = new Color32(143, 188, 143,byte.MaxValue);
        public static Color Policecolor = Color.blue;
        public static List<int> ZombiePlayers;
        public static bool IsZombie(this PlayerControl player) {
            if (player.Data.Role.IsImpostor || ZombiePlayers.Contains(player.PlayerId)) return true;
            return false;
        }
        [HarmonyPatch(typeof(GameData), nameof(GameData.RecomputeTaskCounts))]
        private static class GameDataRecomputeTaskCountsPatch
        {
            public static void Postfix(GameData __instance)
            {
                if (!Mode.ModeHandler.isMode(Mode.ModeId.Zombie) || !AmongUsClient.Instance.AmHost) return;
                __instance.TotalTasks = 0;
                __instance.CompletedTasks = 0;
                for (int i = 0; i < __instance.AllPlayers.Count; i++)
                {
                    GameData.PlayerInfo playerInfo = __instance.AllPlayers[i];
                    if (!playerInfo.Object.IsZombie())
                    {
                        var (playerCompleted, playerTotal) = TaskCount.TaskDate(playerInfo);
                        __instance.TotalTasks += playerTotal;
                        __instance.CompletedTasks += playerCompleted;
                    }
                }
            }
        }
        public static void SetZombie(this PlayerControl player)
        {
            /*
            player.RpcSetHat("");
            player.RpcSetSkin("");
            */
            player.RpcSetColor(2);
            /*
            player.UncheckSetVisor("visor_pk01_DumStickerVisor");
            */
            
            foreach (PlayerTask task in player.myTasks)
            {
                task.Complete();
            }
            /*
            var Data = PlayerControl.GameOptions;
            Data.CrewLightMod = ZombieOptions.ZombieLight.getFloat();
            RPCHelper.RPCGameOptionsPrivate(Data,player);
            */
            if (!ZombiePlayers.Contains(player.PlayerId)) ZombiePlayers.Add(player.PlayerId);
        }
        public static void SetNotZombie(this PlayerControl player)
        {
            if (ZombiePlayers.Contains(player.PlayerId)) ZombiePlayers.Remove(player.PlayerId);
        }
        public static bool EndGameCheck(ShipStatus __instance, PlayerStatistics statistics)
        {
            bool IsZombieWin = true;
            foreach(PlayerControl p in PlayerControl.AllPlayerControls)
            {
                if (!ZombiePlayers.Contains(p.PlayerId) && p.isAlive())
                {
                    IsZombieWin = false;
                }
            }
            if (IsZombieWin)
            {
                __instance.enabled = false;
                ShipStatus.RpcEndGame(GameOverReason.ImpostorByKill, false);
                return true;
            }
            if (GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks)
            {
                __instance.enabled = false;
                ShipStatus.RpcEndGame(GameOverReason.HumansByVote, false);
                return true;
            }
            return false;
        }
        public static void ClearAndReload()
        {
            /*
            PlayerControl.GameOptions.ImpostorLightMod = ZombieOptions.ZombieLight.getFloat();
            PlayerControl.LocalPlayer.RpcSyncSettings(PlayerControl.GameOptions);
            */
            ZombiePlayers = new List<int>();
            if (AmongUsClient.Instance.AmHost) { 
                FixedUpdate.IsStart = false;
                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    p.getDefaultName();
                    /*
                    p.UncheckSetVisor("visor_EmptyVisor");
                    */
                    /*
                    p.RpcSetHat("");
                    p.RpcSetSkin("");
                    */
                }
            }
        }
        public static void SetTimer()
        {
            FixedUpdate.IsStart = true;
            foreach (PlayerControl p in PlayerControl.AllPlayerControls)
            {
                foreach (PlayerControl p2 in PlayerControl.AllPlayerControls)
                {
                    if (p2.PlayerId != p.PlayerId)
                    {
                        p2.RpcSetNamePrivate("Playing on SuperNewRoles!", p);
                    }
                }
            }
            FixedUpdate.NameChangeTimer = 5f;
        }
    }
}
