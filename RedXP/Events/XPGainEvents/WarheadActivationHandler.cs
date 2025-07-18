using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.WarheadEvents;

namespace RedXP.Events.XPGainEvents;

public class WarheadActivationHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnWarheadStarted(WarheadStartedEventArgs ev) {
    if (ev.Player == null) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.WarheadStarted_XP, translations.WarheadStarted_Msg);
  }
}
