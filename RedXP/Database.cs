using MySqlConnector;
using LabApi.Features.Wrappers;

namespace RedXP;

public class Database {
  public MySqlConnection Connection { get; private set; }

  public bool Available { get; private set; } = false;

  public Database(string connectionString) {
    Connection = new(connectionString);
  }

  public void CreateTable() {
    using MySqlCommand cmd = Connection.CreateCommand();
    cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS players (
id VARCHAR(255) PRIMARY KEY,
nickname VARCHAR(32),
xp INT NOT NULL,
first_xp_date DATETIME,
last_xp_gain_event VARCHAR(255) NOT NULL
);";

    cmd.ExecuteNonQuery();
  }

  public void Connect() {
    Connection.Open();
    Available = true;
    CreateTable();
  }

  public void Disconnect() {
    Available = false;
    Connection.Close();
  }

  public void SavePlayers() {
    foreach (Player player in Player.ReadyList) {
      if (!XPUserData.TryGet(player, out XPUserData xpData))
        continue;

      xpData.SaveToDB();
    }
  }
}
