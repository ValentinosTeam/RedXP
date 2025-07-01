using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using System.Collections.Generic;

namespace RedXP.Events.XPGainEvents;

public class GeneratorEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private List<Generator> generatorsClaimed = new();

  public override void OnPlayerActivatedGenerator(PlayerActivatedGeneratorEventArgs ev) {
    if (generatorsClaimed.Contains(ev.Generator)) return;

    generatorsClaimed.Add(ev.Generator);

    XPGainEvents.AddXPAndNotify(ev.Player, config.ActivateUniqueGenerator_XP, translations.ActivateUniqueGenerator_Msg);
  }
}
