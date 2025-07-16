using System;
using UnityEngine;
using LabApi.Features.Wrappers;
using MySqlConnector;
using System.Collections.Generic;

namespace RedXP;

public class XPUserData {
  private static Config config => RedXP.Instance.Config;
  private static Database database => RedXP.Instance.Database;

  public string UserID { get; set; }
  public string Nickname { get; set; }
  public int XP { get; set; }
  public int Level {
    get {
      // somewhat of a hack, but let's blame unity's
      // crappy evaluator for not providing variable support
      // TODO: improve variable substitution
      string substitutedFormula = config.LevelFormula.Replace("x", XP.ToString());
      ExpressionEvaluator.Evaluate(substitutedFormula, out double result);
      return (int) Math.Floor(result);
    }
  }
  public DateTime? FirstXPDate { get; set; }
  public string LastXPGainEvent { get; set; }

  public bool Online { get; set; }

  private XPUserData(string userId, string nickname, int xp, DateTime? firstXPDate, string lastXPGainEvent, bool online) {
    this.UserID = userId;
    this.Nickname = nickname;
    this.XP = xp;
    this.FirstXPDate = firstXPDate;
    this.LastXPGainEvent = lastXPGainEvent;
    this.Online = online;
  }

  public static XPUserData New(Player player) {
    return new XPUserData(player.UserId, player.Nickname, 0, null, "", true);
  }

  public static XPUserData GetOnline(Player player) {
    XPDataStore xpStore = XPDataStore.Get(player);
    return xpStore.XPData;
  }

  public static XPUserData GetOffline(string userId) {
    if (!database.Available) return null;

    using MySqlCommand cmd = database.Connection.CreateCommand();
    cmd.CommandText = @"SELECT nickname, xp, first_xp_date, last_xp_gain_event FROM players WHERE id = @id;";
    cmd.Parameters.AddWithValue("@id", userId);

    using MySqlDataReader reader = cmd.ExecuteReader();

    if (!reader.HasRows)
      return null;

    string nickname = null;
    int xp = 0;
    DateTime? firstXPDate = null;
    string lastXPGainEvent = null;

    while (reader.Read()) {
      nickname = reader.GetString("nickname");
      xp = reader.GetInt32("xp");
      if (!reader.IsDBNull(reader.GetOrdinal("first_xp_date"))) {
         firstXPDate = reader.GetDateTime("first_xp_date");
      }
      lastXPGainEvent = reader.GetString("last_xp_gain_event");
    }
    
    return new XPUserData(userId, nickname, xp, firstXPDate, lastXPGainEvent, false);
  }

  public static XPUserData Get(string userId) {
    if (Player.TryGet(userId, out Player player) && XPDataStore.Get(player).XPData != null)
      return GetOnline(player);
    else
      return GetOffline(userId);
  }
  
  public static XPUserData Get(Player player) {
    return XPDataStore.Get(player).XPData;
  }

  public static bool TryGet(string userId, out XPUserData xpData) {
    xpData = XPUserData.Get(userId);
    return xpData != null;
  }

  public static bool TryGet(Player player, out XPUserData xpData) {
    xpData = XPDataStore.Get(player).XPData;
    return xpData != null;
  }

  public void SaveToDB() {
    using MySqlCommand cmd = database.Connection.CreateCommand();
    cmd.CommandText = @"
REPLACE INTO players (id, nickname, xp, first_xp_date, last_xp_gain_event)
VALUES (@id, @nickname, @xp, @first_xp_date, @last_xp_gain_event);";
    cmd.Parameters.AddWithValue("@id", UserID);
    cmd.Parameters.AddWithValue("@nickname", Nickname);
    cmd.Parameters.AddWithValue("@xp", XP);
    cmd.Parameters.AddWithValue("@first_xp_date", FirstXPDate);
    cmd.Parameters.AddWithValue("@last_xp_gain_event", LastXPGainEvent);

    cmd.ExecuteNonQuery();
  }

  public static List<XPUserData> GetTopN(int n) {
    List<string> ids = new();
    List<XPUserData> topList = new();

    database.SavePlayers();

    using MySqlCommand cmd = database.Connection.CreateCommand();
    cmd.CommandText = @"SELECT id FROM players ORDER BY xp DESC LIMIT @n";
    cmd.Parameters.AddWithValue("@n", n);

    using MySqlDataReader reader = cmd.ExecuteReader();

    while (reader.Read()) {
      string userId = reader.GetString("id");
      ids.Add(userId);
    }

    reader.Close();

    foreach (string userId in ids) {
      if (!XPUserData.TryGet(userId, out XPUserData xpData)) continue;
      topList.Add(xpData);
    }

    return topList;
  }
}
