using System;
using LabApi.Features.Stores;
using LabApi.Features.Wrappers;

namespace RedXP;

public class XPDataStore : CustomDataStore<XPDataStore> {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;
  private static Database database => RedXP.Instance.Database;

  public XPUserData XPData { get; set; } = null;

  public XPDataStore(Player owner) : base(owner) {}

  public void UpdateDisplayName() {
    if (Owner.Nickname == null) return;

    string displayLevel;
    bool levelPublic = ServerSpecificSettings.IsLevelPublic(Owner);

    if (!levelPublic || Owner.DoNotTrack)
      displayLevel = "?";
    else
      displayLevel = XPData.Level.ToString();

    Owner.DisplayName = String.Format(
        translations.DisplayNameTemplate,
        displayLevel, Owner.Nickname);
  }

  protected override void OnInstanceDestroyed() {
    if (Owner.DoNotTrack || !database.Available) return;

    XPData.SaveToDB();
  }

  public void AddXP(int amount) {
    if (Owner.DoNotTrack) return;

    if (XPData.FirstXPDate == null)
      XPData.FirstXPDate = DateTime.Now;

    XPData.XP += amount;

    UpdateDisplayName();
  }
}
