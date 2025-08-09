using HarmonyLib;
using RedXP.Patched.Events.Arguments;
using RedXP.Patched.Events.Handlers;
using PlayerRoles.PlayableScps.Scp3114;

namespace RedXP.Patched.Patches;

// as said in SCP3114Events.cs
// this patched event only serves as an example
[HarmonyPatch(typeof(Scp3114Disguise), "ServerComplete")]
class SCP3114DisguiseEventPatch {
  static void Postfix(Scp3114Disguise __instance) {
    SCP3114DisguisedEventArgs ev = new(__instance.Owner);
    SCP3114Events.OnDisguised(ev);
  }
}
