using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using System.Collections.Generic;
using LabApi.Features.Enums;

namespace RedXP.Events.XPGainEvents;

public class OpenGateHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  Dictionary<DoorName, bool> gatesOpened = new() {
    { DoorName.EzGateA, false },
    { DoorName.EzGateB, false }
  };

  public override void OnPlayerInteractedDoor(PlayerInteractedDoorEventArgs ev) {
    // opening a gate for the first time
    if (!gatesOpened.ContainsKey(ev.Door.DoorName)) return;
    if (gatesOpened[ev.Door.DoorName]) return;

    gatesOpened[ev.Door.DoorName] = true;
    XPGainEvents.AddXPAndNotify(ev.Player, config.OpenGateFirstTime_XP, translations.OpenGateFirstTime_Msg);
  }
}
