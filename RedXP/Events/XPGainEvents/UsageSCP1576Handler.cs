using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP1576Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    if (ev.UsableItem.Type != ItemType.SCP1576) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP1576_XP, translations.UsageSCP1576_Msg);
  }
}
