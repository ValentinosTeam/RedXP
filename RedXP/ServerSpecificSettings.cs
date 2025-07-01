using UserSettings.ServerSpecific;
using LabApi.Features.Wrappers;

namespace RedXP;

public class ServerSpecificSettings {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public static void Register() {
    SSGroupHeader header = new("RedXP");
    // TODO: update display name when this is toggled
    SSTwoButtonsSetting displayLevelToggle = new(config.SSSettings_DisplayLevel_ID,
        translations.SSSettings_DisplayLevel_Msg,
        translations.SSSettings_Enabled, translations.SSSettings_Disabled,
        true);
    SSTwoButtonsSetting xpGainHintsDisplayToggle = new(config.SSSettings_ShowXPGainHints_ID,
        translations.SSSettings_ShowXPGainHints_Msg,
        translations.SSSettings_Enabled, translations.SSSettings_Disabled,
        true);

    ServerSpecificSettingsSync.DefinedSettings = [..ServerSpecificSettingsSync.DefinedSettings ?? [], header, displayLevelToggle, xpGainHintsDisplayToggle];
    ServerSpecificSettingsSync.SendToAll();
  }

  public static bool IsLevelPublic(Player player) {
    SSTwoButtonsSetting setting = ServerSpecificSettingsSync
      .GetSettingOfUser<SSTwoButtonsSetting>(
          player.ReferenceHub,
          config.SSSettings_DisplayLevel_ID);

    return setting.SyncIsA;
  }

  public static bool IsXPGainHintsDisplayEnabled(Player player) {
    SSTwoButtonsSetting setting = ServerSpecificSettingsSync
      .GetSettingOfUser<SSTwoButtonsSetting>(
          player.ReferenceHub,
          config.SSSettings_ShowXPGainHints_ID);

    return setting.SyncIsA;
  }
}
