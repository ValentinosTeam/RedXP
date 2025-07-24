using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp106Events;

namespace RedXP.Events.XPGainEvents;

public class KillHumanAsSCPHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDying(PlayerDyingEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;
    if (!ev.Attacker.IsSCP || !ev.Player.IsHuman) return;

    XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillHumanAsScp_XP, translations.KillHumanAsScp_Msg);
  }

  // count teleportation by 106 as a kill
  public override void OnScp106TeleportedPlayer(Scp106TeleportedPlayerEvent ev) {
    if (!ev.Target.IsHuman) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.KillHumanAsScp_XP, translations.KillHumanAsScp_Msg);
  }
}
