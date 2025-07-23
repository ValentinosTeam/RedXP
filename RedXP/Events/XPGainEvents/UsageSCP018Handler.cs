using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP018Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerThrewProjectile(PlayerThrewProjectileEventArgs ev) {
    if (ev.ThrowableItem.Type != ItemType.SCP018) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP018_XP, translations.UsageSCP018_Msg);
  }
}
