using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class PickupSCP127Handler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private bool rewardClaimed = false;

  public override void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs ev) {
    if (ev.Item.Type != ItemType.GunSCP127) return;
    if (rewardClaimed) return;

    rewardClaimed = true;

    XPGainEvents.AddXPAndNotify(ev.Player, config.PickupSCP127_XP, translations.PickupSCP127_Msg);
  }
}
