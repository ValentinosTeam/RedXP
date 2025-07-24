using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP268Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    if (ev.UsableItem.Type != ItemType.SCP268) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP268_XP, translations.UsageSCP268_Msg);
  }
}
