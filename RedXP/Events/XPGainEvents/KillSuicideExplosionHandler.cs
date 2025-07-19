using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerStatsSystem;

namespace RedXP.Events.XPGainEvents;

public class KillSuicideExplosionHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerDeath(PlayerDeathEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;
    if (ev.DamageHandler is not ExplosionDamageHandler explosionDamageHandler) return;
    if (explosionDamageHandler.ExplosionType != ExplosionType.PinkCandy
        && explosionDamageHandler.ExplosionType != ExplosionType.Cola
        && explosionDamageHandler.ExplosionType != ExplosionType.Jailbird) return;

    XPGainEvents.AddXPAndNotify(ev.Attacker, config.SuicideKill_XP, translations.SuicideKill_Msg);
  }
}
