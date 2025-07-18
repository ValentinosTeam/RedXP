using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Wrappers;
using System.Linq;
using System.Collections.Generic;

namespace RedXP.Events.XPGainEvents;

public class RoundPresenceHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private List<Player> roundStartPlayerList;

  public override void OnServerRoundStarted() {
    roundStartPlayerList = Player.ReadyList.ToList();
  }

  public override void OnServerRoundEnded(RoundEndedEventArgs ev) {
    List<Player> presentStartAndEnd =
      Player.ReadyList.ToList().Intersect(roundStartPlayerList).ToList();

    foreach (Player player in presentStartAndEnd) {
      XPGainEvents.AddXPAndNotify(player, config.RoundPresence_XP, translations.RoundPresence_Msg);
    }
  }
}
