using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class GeneratorDectivationHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDeactivatedGenerator(PlayerDeactivatedGeneratorEventArgs ev) {
    if (!ev.Player.IsSCP) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.DeactivateGenerator_XP, translations.DeactivateGenerator_Msg);
  }
}
