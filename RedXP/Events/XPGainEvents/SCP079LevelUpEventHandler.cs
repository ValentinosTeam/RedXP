using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.Scp079Events;

namespace RedXP.Events.XPGainEvents;

public class SCP079LevelUpEventHandler : CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnScp079LeveledUp(Scp079LeveledUpEventArgs ev) {
    XPGainEvents.AddXPAndNotify(ev.Player, config.SCP079LevelUp_XP, translations.SCP079LevelUp_Msg);
  }
}
