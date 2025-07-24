using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP500Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    if (ev.UsableItem.Type != ItemType.SCP500) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP500_XP, translations.UsageSCP500_Msg);
  }
}
