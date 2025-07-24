using HarmonyLib;
using RedXP.Patched.Events.Arguments;
using RedXP.Patched.Events.Handlers;

namespace RedXP.Patched.Patches;

[HarmonyPatch(typeof(AlphaWarheadNukesitePanel), nameof(AlphaWarheadNukesitePanel.ServerInteract))]
class WarheadEnabledEventPatch {
  static void Postfix(ReferenceHub ply, byte colliderId) {
    AlphaWarheadSyncInfo info = AlphaWarheadController.Singleton.Info;

    if (colliderId != 2) return; // 2 is the lever
    if (AlphaWarheadNukesitePanel.Singleton.enabled) {
      WarheadEnabledEventArgs ev = new(ply, info);
      WarheadEvents.OnEnabled(ev);
    } else {
      // invoke the Disabled event here when needed
    }
  }
}
