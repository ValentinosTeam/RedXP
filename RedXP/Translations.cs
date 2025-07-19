namespace RedXP;

public class Translations {
  // C#'s multiline strings look even uglier
  public string XPSummaryTemplate { get; set; } = "XP summary\nUser ID: {0}\nDisplay name: {1}\nXP: {2}\nLevel: {3}\n"
                                                + "Last XP gain event: {4}\nFirst XP gained on: {5}";
  
  public string XPGainHintTemplate { get; set; } = "\n\n\n\n\n\n\n\n\n<b><size=0.8em><color=#EFBF04>{0}</color>\n<color=#AB8803>+{1} XP</color></size></b>";
  
  public string DisplayNameTemplate { get; set; } = "LVL {0} | {1}";
  
  public string LevelHidden_Msg { get; set; } = "Your level is now hidden.";
  public string LevelVisible_Msg { get; set; } = "Your level is now visible.";

  public string LeaderboardHeader { get; set; } = "\nLEADERBOARD";
  public string LeaderboardEntryTemplate { get; set; } = "LVL {0:D3} | {1}";

  public string SSSettings_DisplayLevel_Msg = "Show level alongside nickname";
  public string SSSettings_ShowXPGainHints_Msg = "Show XP gain hints";
  public string SSSettings_Enabled = "Enabled";
  public string SSSettings_Disabled = "Disabled";

  public string PermissionError_Msg { get; set; } = "No permission.";
  public string NotAPlayerError_Msg { get; set; } = "You are not a player.";
  public string PlayerNotFoundError_Msg { get; set; } = "Player not found.";
  public string InvalidUsageError_Msg { get; set; } = "Invalid usage.";
  public string ParsingError_Msg { get; set; } = "Parsing error.";
  public string SeeXPAdminUsage_Msg { get; set; } = "Usage: seexp {id}";
  public string SetXPUsage_Msg { get; set; } = "Usage: setplayerxp {id} {amount}";
  public string SetXPSuccessOnline_Msg { get; set; } = "Done online!";
  public string SetXPSuccessOffline_Msg { get; set; } = "Done offline!";
  public string SetXPFailureOffline_Msg { get; set; } = "Failed to set XP offline.";

  public string KillHumanAsHuman_Msg { get; set; } = "Killed a human as a human";
  public string KillHumanAsScp_Msg { get; set; } = "Killed a human as an SCP";
  public string KillScpAsHuman_Msg { get; set; } = "Killed an SCP as a human";
  public string KillHumanAsZombie_Msg { get; set; } = "Killed a human as a zombie";
  public string SuicideKill_Msg { get; set; } = "Killed somebody with a suicide explosion";
  public string DamageSpecialWeapon_Msg { get; set; } = "Damaged somebody with a special weapon";
  public string DamageSCP018_Msg { get; set; } = "Damaged somebody with SCP-018";
  public string Cuffed_Msg { get; set; } = "Cuffed somebody";
  public string EscapeAssist_Msg { get; set; } = "Helped somebody escape";
  public string RoundPresence_Msg { get; set; } = "Present at round start and end";
  public string SCPWin_Msg { get; set; } = "Won as the SCP team";
  public string SCP049CreateZombie_Msg { get; set; } = "Created a zombie as SCP-049";
  public string Escape_Msg { get; set; } = "Escaped the facility";
  public string SCP079LevelUp_Msg { get; set; } = "Leveled up as SCP-079";
  public string ActivateUniqueGenerator_Msg { get; set; } = "Activated a generator for the first time";
  public string DeactivateGenerator_Msg { get; set; } = "Deactivated a generator as an SCP";
  public string OpenGateFirstTime_Msg { get; set; } = "Opened a gate for the first time";
  public string WarheadStarted_Msg { get; set; } = "Started the warhead";
}
