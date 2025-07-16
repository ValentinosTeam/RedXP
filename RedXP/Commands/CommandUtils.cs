using System;

namespace RedXP.Commands;

public class CommandUtils {
  private static Translations translations => RedXP.Instance.Translations;

  public static string GenerateSummary(XPUserData xpData) {
    return String.Format(translations.XPSummaryTemplate,
        xpData.UserID, xpData.Nickname, xpData.XP, xpData.Level,
        xpData.LastXPGainEvent, xpData.FirstXPDate);
  }
}
