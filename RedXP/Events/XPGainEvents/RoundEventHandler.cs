using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.ServerEvents;
using PlayerRoles;
using LabApi.Features.Wrappers;
using System.Linq;

namespace RedXP.Events.XPGainEvents;

public class RoundEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnServerRoundEnded(RoundEndedEventArgs ev) {
    if (ev.LeadingTeam != RoundSummary.LeadingTeam.Anomalies) return;

    foreach (Player player in Player.List.Where(p => p.IsSCP)) {
      int xp = config.SCPWin_XP;
      if (player.Role == RoleTypeId.Scp0492) xp = config.SCPWinZombies_XP;

      XPGainEvents.AddXPAndNotify(player, xp, translations.SCPWin_Msg);
    }
  }
}
