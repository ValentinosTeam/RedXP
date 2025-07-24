using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP244Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private static bool isVase(ItemType itemType) {
    return itemType == ItemType.SCP244a || itemType == ItemType.SCP244b;
  }

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    if (!isVase(ev.UsableItem.Type)) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP244_XP, translations.UsageSCP244_Msg);
  }

  public override void OnPlayerThrewItem(PlayerThrewItemEventArgs ev) {
    if (!isVase(ev.Pickup.Type)) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP244_XP, translations.UsageSCP244_Msg);
  }
}
