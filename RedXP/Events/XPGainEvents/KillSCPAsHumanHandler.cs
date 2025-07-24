using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class KillSCPAsHumanHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDying(PlayerDyingEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;
    // treat being dead as being human (workaround for kills after death)
    if (!(ev.Attacker.IsHuman || !ev.Attacker.IsAlive) || !ev.Player.IsSCP) return;

    XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillScpAsHuman_XP, translations.KillScpAsHuman_Msg);
  }
}
