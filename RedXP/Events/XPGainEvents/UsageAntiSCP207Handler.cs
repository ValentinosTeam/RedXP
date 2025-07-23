using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class UsageAntiSCP207Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev) {
    if (ev.UsableItem.Type != ItemType.AntiSCP207) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.UsageAntiSCP207_XP, translations.UsageAntiSCP207_Msg);
  }
}
