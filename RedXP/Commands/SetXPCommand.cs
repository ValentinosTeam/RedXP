using CommandSystem;
using LabApi.Features.Wrappers;
using System;

namespace RedXP.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class SetXPCommand : ICommand {
  public string Command       => "setplayerxp";
  public string[] Aliases     => [];
  public string Description   => "Sets a player's XP value";

  private Translations translations => RedXP.Instance.Translations;
  private Database database => RedXP.Instance.Database;

  public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response) {
    if (args.Count != 2) {
      response = translations.SetXPUsage_Msg;
      return false;
    }

    if (!sender.CheckPermission(PlayerPermissions.PlayersManagement)) {
      response = translations.PermissionError_Msg;
      return false;
    }

    if (!int.TryParse(args.At(1), out int amount)) {
      response = translations.ParsingError_Msg;
      return false;
    }

    string userId = args.At(0);

    if (Player.TryGet(userId, out Player target) && target != null)
      return setOnline(target, amount, out response);
    else {
      return setOffline(userId, amount, out response);
    }
  }

  private bool setOnline(Player player, int amount, out string response) {
    XPDataStore xpStore = XPDataStore.Get(player);
    XPUserData xpData = XPUserData.Get(player);
    xpData.XP = amount;
    xpStore.UpdateDisplayName();

    response = translations.SetXPSuccessOnline_Msg;
    return true;
  }

  private bool setOffline(string userId, int amount, out string response) {
    if (!XPUserData.TryGet(userId, out XPUserData xpData)) {
      response = translations.PlayerNotFoundError_Msg;
      return false;
    }

    xpData.XP = amount;
    xpData.SaveToDB();

    response = translations.SetXPSuccessOffline_Msg;
    return true;
  }
}
