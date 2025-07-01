using LabApi.Features.Wrappers;
using System;

namespace RedXP.Commands;

public class CommandUtils {
  private static Translations translations => RedXP.Instance.Translations;

  public static string GenerateSummaryOnline(Player player) {
    XPDataStore xpStore = XPDataStore.Get(player);

    return String.Format(translations.XPSummaryTemplate,
        player.UserId, player.Nickname, xpStore.XP, xpStore.Level,
        xpStore.LastXPGainEvent, xpStore.FirstXPDate);
  }
}
