using System;
using System.Collections.Generic;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;

namespace RedXP.Events.XPGainEvents;

public class WarheadEnabledHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private List<Player> rewardsClaimed = new();
  private DateTime cooldownTimer = new();
  
  public override void OnPlayerInteractedWarheadLever(PlayerInteractedWarheadLeverEventArgs ev) {
    if (ev.Player == null) return;
    if (ev.Enabled != true) return;
    if (rewardsClaimed.Contains(ev.Player)) return;
    if (cooldownTimer > DateTime.Now) return;

    rewardsClaimed.Add(ev.Player);
    cooldownTimer = DateTime.Now.AddSeconds(config.WarheadEnabled_Cooldown);

    XPGainEvents.AddXPAndNotify(ev.Player, config.WarheadEnabled_XP, translations.WarheadEnabled_Msg);
  }
}
