using RedXP.Patched.Events.Arguments;
using RedXP.Patched.Events.CustomHandlers;

namespace RedXP.Events.XPGainEvents;

public class SCP3114DisguiseHandler : PatchedEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnSCP3114Disguised(SCP3114DisguisedEventArgs ev) {
    XPGainEvents.AddXPAndNotify(ev.Player, config.SCP3114Disguise_XP, translations.SCP3114Disguise_Msg);
  }
}
