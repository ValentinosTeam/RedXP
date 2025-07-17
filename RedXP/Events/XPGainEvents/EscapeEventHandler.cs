using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class EscapeEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerEscaped(PlayerEscapedEventArgs ev) {
    // escaping the facility
    XPGainEvents.AddXPAndNotify(ev.Player, config.Escape_XP, translations.Escape_Msg);
  }

  public override void OnPlayerEscaping(PlayerEscapingEventArgs ev) {
    // helping someone escape
    if (ev.Player.IsDisarmed && ev.Player.DisarmedBy != null) {
      XPGainEvents.AddXPAndNotify(ev.Player.DisarmedBy, config.EscapeAssist_XP, translations.EscapeAssist_Msg);
    }
  }
}
