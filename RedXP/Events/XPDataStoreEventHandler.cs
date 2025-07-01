using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace RedXP.Events;

class XPDataStoreEventHandler : CustomEventsHandler {
  private Database database => RedXP.Instance.Database;

  // fetch player's data from the database
  // (as the user id is not available in OnInstanceCreated
  // of XPDataStore) and update display name
  public override void OnPlayerJoined(PlayerJoinedEventArgs ev) {
    XPDataStore xpStore = XPDataStore.Get(ev.Player);

    if (!ev.Player.DoNotTrack && database.Available)
      database.FetchPlayer(xpStore);

    xpStore.UpdateDisplayName();
  }

  // TODO: handle OnPlayerChangingNickname
}
