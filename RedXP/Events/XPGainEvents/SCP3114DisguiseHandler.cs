using LabApi.Events.Arguments.Scp3114Events;
using LabApi.Events.CustomHandlers;

namespace RedXP.Events.XPGainEvents;

public class SCP3114DisguiseHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnScp3114Disguised(Scp3114DisguisedEventArgs ev) {
    XPGainEvents.AddXPAndNotify(ev.Player, config.SCP3114Disguise_XP, translations.SCP3114Disguise_Msg);
  }
}
