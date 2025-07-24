namespace RedXP;

public class Config {
  public float XPGainHintDuration { get; set; } = 1.5f;
  public string LevelFormula { get; set; } = "1 + 0.45 * sqrt(x)";

  public int Leaderboard_N { get; set; } = 10;

  public string MySql_ConnectionString { get; set; } = "server=127.0.0.1;port=3306;uid=root;pwd=testpw;database=redxp";

  public int ActivateGenerator_Cooldown { get; set; } = 90;
  public int ActivateGenerator_MaxTimes { get; set; } = 3;
  public int DeactivateGenerator_Cooldown { get; set; } = 90;

  public int KillHumanAsHuman_XP { get; set; } = 5;
  public int KillHumanAsScp_XP { get; set; } = 3;
  public int KillScpAsHuman_XP { get; set; } = 15;
  public int KillHumanAsZombie_XP { get; set; } = 5;
  public int SuicideKill_XP { get; set; } = 7;
  public int UsageSCP1853_XP { get; set; } = 5;
  public int UsageSCP207_XP { get; set; } = 5;
  public int UsageAntiSCP207_XP { get; set; } = 7;
  public int UsageSCP018_XP { get; set; } = 5;
  public int UsageSCP2176_XP { get; set; } = 5;
  public int UsageSCP500_XP { get; set; } = 5;
  public int UsageSCP268_XP { get; set; } = 3;
  public int UsageSCP1576_XP { get; set; } = 3;
  public int UsageSCP244_XP { get; set; } = 5;
  public int UsageSCP330_XP { get; set; } = 1;
  public int PickupSpecialWeapon_XP { get; set; } = 7;
  public int Cuffed_XP { get; set; } = 7;
  public int EscapeAssist_XP { get; set; } = 3;
  public int RoundPresence_XP { get; set; } = 5;
  public int SCPWin_XP { get; set; } = 10;
  public int SCPWinZombies_XP { get; set; } = 5;
  public int SCP049CreateZombie_XP { get; set; } = 2;
  public int Escape_XP { get; set; } = 7;
  public int SCP079LevelUp_XP { get; set; } = 3;
  public int ActivateGenerator_XP { get; set; } = 3;
  public int DeactivateGenerator_XP { get; set; } = 3;
  public int OpenGateFirstTime_XP { get; set; } = 5;
  public int WarheadStarted_XP { get; set; } = 7;

  public int SSSettings_ShowXPGainHints_ID { get; set; } = 51413;
  public int SSSettings_DisplayLevel_ID { get; set; } = 81712;
}
