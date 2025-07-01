namespace RedXP;

public class Config {
  public float XPGainHintDuration { get; set; } = 1.5f;
  public string LevelFormula { get; set; } = "1 + 0.1*x";

  public string MySql_ConnectionString { get; set; } = "server=127.0.0.1;port=3306;uid=root;pwd=testpw;database=redxp";

  public int KillHumanAsHuman_XP { get; set; } = 5;
  public int KillHumanAsScp_XP { get; set; } = 3;
  public int KillScpAsHuman_XP { get; set; } = 15;
  public int Escape_XP { get; set; } = 10;
  public int SCP079LevelUp_XP { get; set; } = 7;
  public int ActivateUniqueGenerator_XP { get; set; } = 3;
  public int WarheadStarted_XP { get; set; } = 10;

  public int SSSettings_ShowXPGainHints_ID { get; set; } = 51413;
  public int SSSettings_DisplayLevel_ID { get; set; } = 81712;
}
