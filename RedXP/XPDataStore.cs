using System;
using LabApi.Features.Stores;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace RedXP;

public class XPDataStore : CustomDataStore<XPDataStore> {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;
  private static Database database => RedXP.Instance.Database;

  public int XP { get; set; } = 0;
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
  public DateTime? FirstXPDate { get; set; } = null;
  public string LastXPGainEvent { get; set; } = "";

  // used to prevent saving to the database
  // if the fetch was unsuccessful
  public bool FetchSuccessful { get; set; } = false;

  public XPDataStore(Player owner) : base(owner) {}

  public void UpdateDisplayName() {
    if (Owner.Nickname == null) return;

    string displayLevel;
    bool levelPublic = ServerSpecificSettings.IsLevelPublic(Owner);

    if (!levelPublic || Owner.DoNotTrack)
      displayLevel = "?";
    else
      displayLevel = Level.ToString();

    Owner.DisplayName = String.Format(
        translations.DisplayNameTemplate,
        displayLevel, Owner.Nickname);
  }

  protected override void OnInstanceCreated() {
    // ideally we would fetch player data from the db here
    // but the user id is not available at this stage
  }

  protected override void OnInstanceDestroyed() {
    if (Owner.DoNotTrack || !database.Available) return;

    // only save to the db if the fetch was successful
    if (FetchSuccessful)
      database.StorePlayer(this);
  }

  public void AddXP(int amount) {
    if (Owner.DoNotTrack) return;

    if (FirstXPDate == null)
      FirstXPDate = DateTime.Now;

    XP += amount;

    UpdateDisplayName();
  }
}
