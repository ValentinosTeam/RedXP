using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using System.Collections.Generic;
using LabApi.Features.Wrappers;
using System;

namespace RedXP.Events.XPGainEvents;

public class GeneratorDectivationHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private Dictionary<Generator, DateTime> generatorCooldownTimer = new();

  public override void OnPlayerDeactivatedGenerator(PlayerDeactivatedGeneratorEventArgs ev) {
    if (!ev.Player.IsSCP) return;
    
    if (!generatorCooldownTimer.ContainsKey(ev.Generator))
      generatorCooldownTimer.Add(ev.Generator, DateTime.Now);
    
    if (generatorCooldownTimer[ev.Generator] > DateTime.Now) return;
    
    generatorCooldownTimer[ev.Generator] = DateTime.Now.AddSeconds(config.DeactivateGenerator_Cooldown);
    
    XPGainEvents.AddXPAndNotify(ev.Player, config.DeactivateGenerator_XP, translations.DeactivateGenerator_Msg);
  }
}
