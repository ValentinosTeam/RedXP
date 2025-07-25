using HarmonyLib;
using RedXP.Patched.Events.Arguments;
using RedXP.Patched.Events.Handlers;
using PlayerRoles.PlayableScps.Scp3114;

namespace RedXP.Patched.Patches;

[HarmonyPatch(typeof(Scp3114Disguise), "ServerComplete")]
class SCP3114DisguiseEventPatch {
  static void Postfix(Scp3114Disguise __instance) {
    SCP3114DisguisedEventArgs ev = new(__instance.Owner);
    SCP3114Events.OnDisguised(ev);
  }
}
