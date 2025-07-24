using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP330Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    if (ev.UsableItem.Type != ItemType.SCP330) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP330_XP, translations.UsageSCP330_Msg);
  }
}
