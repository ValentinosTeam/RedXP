using LabApi.Events;
using RedXP.Patched.Events.Arguments;

namespace RedXP.Patched.Events.Handlers;

public static class SCP3114Events {
  // LabAPI 1.1.1 eliminated the need for this patched event
  // it was kept to serve as an example
  public static event LabEventHandler<SCP3114DisguisedEventArgs> Disguised;

  public static void OnDisguised(SCP3114DisguisedEventArgs ev) {
    SCP3114Events.Disguised.InvokeEvent(ev);
  }
}
