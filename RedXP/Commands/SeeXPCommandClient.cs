using CommandSystem;
using LabApi.Features.Wrappers;
using System;

namespace RedXP.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class SeeXPCommandClient : ICommand {
  public string Command       => "seexp";
  public string[] Aliases     => [ "seelvl" ];
  public string Description   => "Shows your XP statistics";

  private Translations translations => RedXP.Instance.Translations;

  public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response) {
    if (!Player.TryGet(sender, out Player player)) {
      response = translations.NotAPlayerError_Msg;
      return false;
    }

    XPDataStore xpStore = XPDataStore.Get(player);
    
    if (!xpStore.DataAvailable) {
      response = translations.DataNotAvailableError_Msg;
      return false;
    }

    response = CommandUtils.GenerateSummary(XPUserData.Get(player));

    return true;
  }
}
