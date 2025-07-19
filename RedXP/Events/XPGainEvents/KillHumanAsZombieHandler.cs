using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles;

namespace RedXP.Events.XPGainEvents;

public class KillHumanAsZombieHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDying(PlayerDyingEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;
    if (ev.Attacker.Role != RoleTypeId.Scp0492 || !ev.Player.IsHuman) return;

    XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillHumanAsZombie_XP, translations.KillHumanAsZombie_Msg);
  }
}
