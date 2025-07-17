using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.ServerEvents;
using PlayerRoles;
using LabApi.Features.Wrappers;
using System.Linq;
using System.Collections.Generic;

namespace RedXP.Events.XPGainEvents;

public class RoundEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private List<Player> roundStartPlayerList;

  public override void OnServerRoundStarted() {
    handleRoundPresenceStart();
  }

  public override void OnServerRoundEnded(RoundEndedEventArgs ev) {
    handleSCPWin(ev);
    handleRoundPresenceEnd(ev);
  }

  private void handleSCPWin(RoundEndedEventArgs ev) { 
    if (ev.LeadingTeam != RoundSummary.LeadingTeam.Anomalies) return;

    foreach (Player player in Player.List.Where(p => p.IsSCP)) {
      int xp = config.SCPWin_XP;
      if (player.Role == RoleTypeId.Scp0492) xp = config.SCPWinZombies_XP;

      XPGainEvents.AddXPAndNotify(player, xp, translations.SCPWin_Msg);
    }
  }
  
  private void handleRoundPresenceStart() {
    roundStartPlayerList = Player.List.ToList();
  }

  private void handleRoundPresenceEnd(RoundEndedEventArgs ev) {
    List<Player> presentStartAndEnd =
      Player.List.ToList().Intersect(roundStartPlayerList).ToList();

    foreach (Player player in presentStartAndEnd) {
      XPGainEvents.AddXPAndNotify(player, config.RoundPresence_XP, translations.RoundPresence_Msg);
    }
  }
}
