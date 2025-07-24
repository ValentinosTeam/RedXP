using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using System.Collections.Generic;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP244Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private Dictionary<ItemType, List<Player>> rewardsClaimed = new() {
    { ItemType.SCP244a, new() },
    { ItemType.SCP244b, new() }
  };

  private static bool isVase(ItemType itemType) {
    return itemType == ItemType.SCP244a || itemType == ItemType.SCP244b;
  }

  private void handle(Player player, ItemType itemType) {
    if (!isVase(itemType)) return;
    if (rewardsClaimed[itemType].Contains(player)) return;

    rewardsClaimed[itemType].Add(player);
    
    XPGainEvents.AddXPAndNotify(player, config.UsageSCP244_XP, translations.UsageSCP244_Msg);
  }

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    handle(ev.Player, ev.UsableItem.Type);
  }

  public override void OnPlayerThrewItem(PlayerThrewItemEventArgs ev) {
    handle(ev.Player, ev.Pickup.Type);
  }
}
