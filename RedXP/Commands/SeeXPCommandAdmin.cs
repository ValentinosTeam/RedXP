using CommandSystem;
using System;

namespace RedXP.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class SeeXPCommandAdmin : ICommand {
  public string Command       => "seexp";
  public string[] Aliases     => [ "seelvl" ];
  public string Description   => "Shows a player's XP statistics";

  private Translations translations => RedXP.Instance.Translations;

  public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response) {
    if (args.Count != 1) {
      response = translations.SeeXPAdminUsage_Msg;
      return false;
    }

    if (!XPUserData.TryGet(args.At(0), out XPUserData xpData)) {
      response = "Player not found.";
      return false;
    }
    
    response = CommandUtils.GenerateSummary(xpData);
    return true;
  }
}
