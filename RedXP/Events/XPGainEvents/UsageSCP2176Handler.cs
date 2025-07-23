using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP2176Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerThrewProjectile(PlayerThrewProjectileEventArgs ev) {
    if (ev.ThrowableItem.Type != ItemType.SCP2176) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP018_XP, translations.UsageSCP018_Msg);
  }
}
