using CommandSystem;
using MySqlConnector;
using System;

namespace RedXP.Commands;

[CommandHandler(typeof(GameConsoleCommandHandler))]
public class WipeXPDatabaseCommand : ICommand {
  public string Command       => "wipexpdatabase";
  public string[] Aliases     => [];
  public string Description   => "Wipes the XP database.";

  private Config config => RedXP.Instance.Config;
  private Translations translations => RedXP.Instance.Translations;
  private Database database => RedXP.Instance.Database;

  public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response) {
    if (!database.Available) {
      response = translations.DatabaseNotAvailableError_Msg;
      return false;
    }

    using MySqlCommand cmd = database.Connection.CreateCommand();
    cmd.CommandText = "DROP TABLE players;";
    cmd.ExecuteNonQuery();
    cmd.Dispose();

    database.CreateTable();

    response = translations.Success_Msg;
    return true;
  }
}
