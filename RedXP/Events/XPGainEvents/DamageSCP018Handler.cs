using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerStatsSystem;

namespace RedXP.Events.XPGainEvents;

public class DamageSCP018Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerHurt(PlayerHurtEventArgs ev) {
    if (ev.Attacker == null || ev.Attacker == ev.Player) return;

    // SCP-018 damage
    if (ev.DamageHandler is Scp018DamageHandler) {
      XPGainEvents.AddXPAndNotify(ev.Attacker, config.DamageSCP018_XP, translations.DamageSCP018_Msg);
    }
  }
}
