using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class KillHumanAsHumanHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDying(PlayerDyingEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;

    if (ev.Attacker.IsHuman && ev.Player.IsHuman)
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillHumanAsHuman_XP, translations.KillHumanAsHuman_Msg);
  }
}
