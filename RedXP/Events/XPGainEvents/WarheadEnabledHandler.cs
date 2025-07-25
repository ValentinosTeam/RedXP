using System;
using System.Collections.Generic;
using LabApi.Features.Wrappers;
using RedXP.Patched.Events.Arguments;
using RedXP.Patched.Events.CustomHandlers;

namespace RedXP.Events.XPGainEvents;

public class WarheadEnabledHandler : PatchedEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private List<Player> rewardsClaimed = new();
  private DateTime cooldownTimer = new();
  
  public override void OnWarheadEnabled(WarheadEnabledEventArgs ev) {
    if (ev.Player == null) return;
    if (rewardsClaimed.Contains(ev.Player)) return;
    if (cooldownTimer > DateTime.Now) return;

    rewardsClaimed.Add(ev.Player);
    cooldownTimer = DateTime.Now.AddSeconds(config.WarheadEnabled_Cooldown);

    XPGainEvents.AddXPAndNotify(ev.Player, config.WarheadEnabled_XP, translations.WarheadEnabled_Msg);
  }
}
