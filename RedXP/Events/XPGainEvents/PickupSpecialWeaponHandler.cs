using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace RedXP.Events.XPGainEvents;

public class PickupSpecialWeaponHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private Dictionary<ItemType, List<Player>> rewardsClaimed = new() {
    { ItemType.MicroHID, new() },
    { ItemType.ParticleDisruptor, new() },
    { ItemType.Jailbird, new() },
    { ItemType.GunCom45, new() },
    { ItemType.GunA7, new() }
  };

  public override void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs ev) {
    if (!rewardsClaimed.ContainsKey(ev.Item.Type)) return;
    if (rewardsClaimed[ev.Item.Type].Contains(ev.Player)) return;

    rewardsClaimed[ev.Item.Type].Add(ev.Player);

    XPGainEvents.AddXPAndNotify(ev.Player, config.PickupSpecialWeapon_XP, translations.PickupSpecialWeapon_Msg);
  }
}
