using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.Scp049Events;

namespace RedXP.Events.XPGainEvents;

public class CreateZombieHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnScp049ResurrectedBody(Scp049ResurrectedBodyEventArgs ev) {
    XPGainEvents.AddXPAndNotify(ev.Player, config.SCP049CreateZombie_XP, translations.SCP049CreateZombie_Msg);
  }
}
