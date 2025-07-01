using MySqlConnector;
using System;

namespace RedXP;

public class Database {
  private MySqlConnection connection;

  public bool Available { get; private set; } = false;

  public Database(string connectionString) {
    connection = new(connectionString);
  }

  private void createTable() {
    using MySqlCommand cmd = connection.CreateCommand();
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
    connection.Open();
    Available = true;
    createTable();
  }

  public void Disconnect() {
    Available = false;
    connection.Close();
  }

  public void FetchPlayer(XPDataStore xpStore) {
    // assume failure until the end
    xpStore.FetchSuccessful = false;
    
    using MySqlCommand cmd = connection.CreateCommand();
    cmd.CommandText = @"SELECT xp, first_xp_date, last_xp_gain_event FROM players WHERE id = @id;";
    cmd.Parameters.AddWithValue("@id", xpStore.Owner.UserId);

    using MySqlDataReader reader = cmd.ExecuteReader();

    if (!reader.HasRows) {
      xpStore.FetchSuccessful = true;
      return;
    }

    while (reader.Read()) {
      xpStore.XP = reader.GetInt32("xp");
      if (reader.IsDBNull(reader.GetOrdinal("first_xp_date")))
        xpStore.FirstXPDate = null;
      else
        xpStore.FirstXPDate = reader.GetDateTime("first_xp_date");
      xpStore.LastXPGainEvent = reader.GetString("last_xp_gain_event");
    }

    xpStore.FetchSuccessful = true;
  }

  public void StorePlayer(XPDataStore xpStore) {
    using MySqlCommand cmd = connection.CreateCommand();
    cmd.CommandText = @"
REPLACE INTO players (id, nickname, xp, first_xp_date, last_xp_gain_event)
VALUES (@id, @nickname, @xp, @first_xp_date, @last_xp_gain_event);";
    cmd.Parameters.AddWithValue("@id", xpStore.Owner.UserId);
    cmd.Parameters.AddWithValue("@nickname", xpStore.Owner.Nickname);
    cmd.Parameters.AddWithValue("@xp", xpStore.XP);
    cmd.Parameters.AddWithValue("@first_xp_date", xpStore.FirstXPDate);
    cmd.Parameters.AddWithValue("@last_xp_gain_event", xpStore.LastXPGainEvent);

    cmd.ExecuteNonQuery();
  }
  
  // TODO: make a less jank unified interface for interacting
  // with the database directly
  public bool SetXPOffline(string userId, int xp) {
    using MySqlCommand cmd = connection.CreateCommand();
    cmd.CommandText = @"
UPDATE players SET xp = @xp
WHERE id = @id;";
    cmd.Parameters.AddWithValue("@id", userId);
    cmd.Parameters.AddWithValue("@xp", xp);

    return cmd.ExecuteNonQuery() == 1;
  }
}

