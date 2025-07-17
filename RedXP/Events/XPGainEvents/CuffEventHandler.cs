using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace RedXP.Events.XPGainEvents;

public class CuffEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  List<Player> beenCuffed = new();

  public override void OnPlayerCuffed(PlayerCuffedEventArgs ev) {
    // cuffing a player for the first time
    if (ev.Player == null) return;
    if (beenCuffed.Contains(ev.Target)) return;

    beenCuffed.Add(ev.Target);

    XPGainEvents.AddXPAndNotify(ev.Player, config.Cuffed_XP, translations.Cuffed_Msg);
  }
}
