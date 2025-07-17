using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles;
using PlayerStatsSystem;

namespace RedXP.Events.XPGainEvents;

public class KillEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDeath(PlayerDeathEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;

    // kill human as a zombie
    if (ev.Attacker.Role == RoleTypeId.Scp0492 && ev.Player.IsHuman)
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillHumanAsZombie_XP, translations.KillHumanAsZombie_Msg);
    // kill human as human
    else if (ev.Attacker.IsHuman && ev.Player.IsHuman)
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillHumanAsHuman_XP, translations.KillHumanAsHuman_Msg);
    // kill human as SCP
    else if (ev.Attacker.IsSCP && ev.Player.IsHuman)
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillHumanAsScp_XP, translations.KillHumanAsScp_Msg);
    // kill SCP as human
    else if (ev.Attacker.IsHuman && ev.Player.IsSCP)
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.KillScpAsHuman_XP, translations.KillScpAsHuman_Msg);

    // suicide explosion kill
    if (ev.DamageHandler is ExplosionDamageHandler explosionDamageHandler
        && (explosionDamageHandler.ExplosionType == ExplosionType.PinkCandy
        || explosionDamageHandler.ExplosionType == ExplosionType.Cola
        || explosionDamageHandler.ExplosionType == ExplosionType.Jailbird)) {
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.SuicideKill_XP, translations.SuicideKill_Msg);
    }
  }
}
