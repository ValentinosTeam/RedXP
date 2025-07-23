using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using System.Collections.Generic;
using System;

namespace RedXP.Events.XPGainEvents;

public class GeneratorActivationHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private Dictionary<Generator, int> generatorActivationCounter = new();
  private Dictionary<Generator, DateTime> generatorCooldownTimer = new();

  public override void OnPlayerActivatedGenerator(PlayerActivatedGeneratorEventArgs ev) {
    if (!generatorActivationCounter.ContainsKey(ev.Generator))
      generatorActivationCounter.Add(ev.Generator, 0);
    if (!generatorCooldownTimer.ContainsKey(ev.Generator))
      generatorCooldownTimer.Add(ev.Generator, DateTime.Now);

    if (generatorActivationCounter[ev.Generator] >= config.ActivateGenerator_MaxTimes) return;
    if (generatorCooldownTimer[ev.Generator] > DateTime.Now) return;

    generatorActivationCounter[ev.Generator]++;
    generatorCooldownTimer[ev.Generator] = DateTime.Now.AddSeconds(config.ActivateGenerator_Cooldown);

    XPGainEvents.AddXPAndNotify(ev.Player, config.ActivateGenerator_XP, translations.ActivateGenerator_Msg);
  }
}
