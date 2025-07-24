using LabApi.Events;
using RedXP.Patched.Events.Arguments;

namespace RedXP.Patched.Events.Handlers;

public static class WarheadEvents {
  public static event LabEventHandler<WarheadEnabledEventArgs> Enabled;

  public static void OnEnabled(WarheadEnabledEventArgs ev) {
    WarheadEvents.Enabled.InvokeEvent(ev);
  }
}
