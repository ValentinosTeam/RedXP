using MySqlConnector;
using LabApi.Features.Wrappers;
using System.Data;
using System;
using LabApi.Features.Console;

namespace RedXP;

public class Database {
  private MySqlConnection _connection;
  public MySqlConnection Connection {
    get {
      if (_connection.State != ConnectionState.Open) {
        try {
          Connect();
        } catch (Exception ex) {
          Logger.Error($"Failed to reconnect to the database: {ex}");
          return null;
        }
      }

      return _connection;
    }
  }

  public bool Available { get; private set; } = false;

  public Database(string connectionString) {
    _connection = new(connectionString);
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
    _connection.Open();
    Available = true;
    CreateTable();
  }

  public void Disconnect() {
    Available = false;
    _connection.Close();
  }

  public void SavePlayers() {
    foreach (Player player in Player.ReadyList) {
      if (!XPUserData.TryGet(player, out XPUserData xpData))
        continue;

      xpData.SaveToDB();
    }
  }
}
