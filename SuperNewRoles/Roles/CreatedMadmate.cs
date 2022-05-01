using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SuperNewRoles.CustomObject;
using SuperNewRoles.Patches;
using SuperNewRoles.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Hazel;
using SuperNewRoles.Patches;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using SuperNewRoles;
using SuperNewRoles.CustomOption;
using SuperNewRoles.Helpers;

namespace SuperNewRoles.Roles
{
    [HarmonyPatch]
    public class CreatedMadmate : ModifierBase<CreatedMadmate>
    {
        public static Color color = Palette.ImpostorRed;

        public enum CreatedMadmateType
        {
            Simple = 0,
            WithRole = 1,
            Random = 2,
        }

        public enum CreatedMadmateAbility
        {
            None = 0,
            Fanatic = 1,
        }

        public static bool canEnterVents { get { return CustomOptions.createdMadmateCanEnterVents.getBool(); } }
        public static bool hasImpostorVision { get { return CustomOptions.createdMadmateHasImpostorVision.getBool(); } }
        public static bool canSabotage { get { return CustomOptions.createdMadmateCanSabotage.getBool(); } }
        public static bool canFixComm { get { return CustomOptions.createdMadmateCanFixComm.getBool(); } }

        public static CreatedMadmateType madmateType { get { return CreatedMadmateType.Simple; } }
        public static CreatedMadmateAbility madmateAbility { get { return (CreatedMadmateAbility)CustomOptions.createdMadmateAbility.getSelection(); } }

        public static int numTasks { get { return (int)CustomOptions.createdMadmateNumTasks.getFloat(); } }

        public static bool hasTasks { get { return madmateAbility == CreatedMadmateAbility.Fanatic; } }
        public static bool exileCrewmate { get {return CustomOptions.createdMadmateExileCrewmate.getBool(); } }

        public static string prefix
        {
            get
            {
                return ModTranslation.getString("madmatePrefix");
            }
        }

        public static string fullName
        {
            get
            {
                return ModTranslation.getString("madmate");
            }
        }

        public CreatedMadmate()
        {
            ModType = modId = ModifierType.CreatedMadmate;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }

        public override void OnDeath(PlayerControl killer = null)
        {
            
        }

        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public void assignTasks()
        {
            player.generateAndAssignTasks(0, numTasks, 0);
        }
        public static bool knowsImpostors(PlayerControl player)
        {
            return hasTasks && hasModifier(player) && tasksComplete(player);
        }

        public static bool tasksComplete(PlayerControl player)
        {
            if (!hasTasks) return false;

            int counter = 0;
            int totalTasks = numTasks;
            if (totalTasks == 0) return true;
            foreach (var task in player.Data.Tasks)
            {
                if (task.Complete)
                {
                    counter++;
                }
            }
            return counter >= totalTasks;
        }

        public static void Clear()
        {
            players = new List<CreatedMadmate>();
        }
    }
}