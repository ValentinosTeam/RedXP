using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace RedXP.Events.XPGainEvents;

public class UsageSCP1344Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private List<Player> rewardsClaimed = new();

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    if (ev.UsableItem.Type != ItemType.SCP1344) return;
    if (rewardsClaimed.Contains(ev.Player)) return;

    rewardsClaimed.Add(ev.Player);

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageSCP1344_XP, translations.UsageSCP1344_Msg);
  }
}
