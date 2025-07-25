using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.Scp106Events;

namespace RedXP.Events.XPGainEvents;

public class SCP106TeleportPDHandler: CustomEventsHandler {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  public override void OnScp106TeleportedPlayer(Scp106TeleportedPlayerEvent ev) {
    if (!ev.Target.IsHuman) return;

    XPGainEvents.AddXPAndNotify(ev.Player, config.SCP106TeleportPD_XP, translations.SCP106TeleportPD_Msg);
  }
}
