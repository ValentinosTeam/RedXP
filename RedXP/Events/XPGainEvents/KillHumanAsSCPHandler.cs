using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class KillHumanAsSCPHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDeath(PlayerDeathEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;

    if (ev.Attacker.IsSCP && ev.Player.IsHuman)
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillHumanAsScp_XP, translations.KillHumanAsScp_Msg);
  }
}
