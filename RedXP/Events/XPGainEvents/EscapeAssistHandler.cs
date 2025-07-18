using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class EscapeAssistHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerEscaping(PlayerEscapingEventArgs ev) {
    if (ev.Player.IsDisarmed && ev.Player.DisarmedBy != null) {
      XPGainEvents.AddXPAndNotify(ev.Player.DisarmedBy, config.EscapeAssist_XP, translations.EscapeAssist_Msg);
    }
  }
}
