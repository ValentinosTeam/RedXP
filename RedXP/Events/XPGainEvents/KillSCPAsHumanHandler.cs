using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class KillSCPAsHuman : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDeath(PlayerDeathEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;

    if (ev.Attacker.IsHuman && ev.Player.IsSCP)
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillScpAsHuman_XP, translations.KillScpAsHuman_Msg);
  }
}
