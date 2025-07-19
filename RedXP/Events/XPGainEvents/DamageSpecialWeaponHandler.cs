using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerStatsSystem;

namespace RedXP.Events.XPGainEvents;

public class DamageSpecialWeaponHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerHurt(PlayerHurtEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;
    if (ev.DamageHandler is not JailbirdDamageHandler
        && ev.DamageHandler is not DisruptorDamageHandler) return;

    // TODO: handle the microhid (handle bursts as single damage events)
    XPGainEvents.AddXPAndNotify(ev.Attacker, config.DamageSpecialWeapon_XP, translations.DamageSpecialWeapon_Msg);
  }
}
