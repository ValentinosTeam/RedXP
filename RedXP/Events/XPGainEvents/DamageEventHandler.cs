using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerStatsSystem;

namespace RedXP.Events.XPGainEvents;

public class DamageEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerHurt(PlayerHurtEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;

    // special weapon damage
    // TODO: handle the microhid (handle bursts as single damage events)
    if (ev.DamageHandler is JailbirdDamageHandler
        || ev.DamageHandler is DisruptorDamageHandler) {
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.DamageSpecialWeapon_XP, translations.DamageSpecialWeapon_Msg);
    }
    // SCP-018 damage
    else if (ev.DamageHandler is Scp018DamageHandler) {
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.DamageSCP018_XP, translations.DamageSCP018_Msg);
    }
  }
}
