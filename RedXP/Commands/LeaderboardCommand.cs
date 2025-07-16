using CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedXP.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class LeaderboardCommand : ICommand {
  public string Command       => "leaderboard";
  public string[] Aliases     => [];
  public string Description   => "Displays the XP leaderboard.";

  private Config config => RedXP.Instance.Config;
  private Translations translations => RedXP.Instance.Translations;

  public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response) {
    List<XPUserData> topList = XPUserData.GetTopN(config.Leaderboard_N);

    StringBuilder responseBuilder = new();
    responseBuilder.AppendLine(translations.LeaderboardHeader);

    foreach (XPUserData xpData in topList) {
      string entry = String.Format(translations.LeaderboardEntryTemplate,
          xpData.Level, xpData.Nickname);
      responseBuilder.AppendLine(entry);
    }

    response = responseBuilder.ToString();
    return true;
  }
}
