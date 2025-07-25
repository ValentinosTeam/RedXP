using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles;

namespace RedXP.Events.XPGainEvents;

public class KillZombieAsHumanHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDying(PlayerDyingEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;
    // treat being dead as being human (workaround for kills after death)
    if (!(ev.Attacker.IsHuman || !ev.Attacker.IsAlive) || ev.Player.Role != RoleTypeId.Scp0492) return;

    XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillZombieAsHuman_XP, translations.KillZombieAsHuman_Msg);
  }
}
