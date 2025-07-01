using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using UserSettings.ServerSpecific;

namespace RedXP.Events;

public class SSSetingsEventHandler : CustomEventsHandler {
  private Config config;

  public SSSetingsEventHandler(Config config) {
    this.config = config;
  }

  public override void OnPlayerJoined(PlayerJoinedEventArgs ev) {
    ServerSpecificSettingsSync.SendToPlayer(ev.Player.ReferenceHub);
  }
}
