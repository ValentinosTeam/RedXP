using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace RedXP.Events.XPGainEvents;

public class EscapeEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnPlayerEscaped(PlayerEscapedEventArgs ev) {
    XPGainEvents.AddXPAndNotify(ev.Player, config.Escape_XP, translations.Escape_Msg);
  }
}
