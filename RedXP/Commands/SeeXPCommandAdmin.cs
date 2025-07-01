using CommandSystem;
using LabApi.Features.Wrappers;
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

    if (Player.TryGet(args.At(0), out Player player)) {
      response = CommandUtils.GenerateSummaryOnline(player);
    } else {
      // TODO: fetch from DB here
      response = "WIP";
    }
    
    return true;
  }
}
