using LabApi.Events.CustomHandlers;
using RedXP.Patched.Events.Arguments;

namespace RedXP.Patched.Events.CustomHandlers;

public abstract class PatchedEventsHandler : CustomEventsHandler {
  public virtual void OnWarheadEnabled(WarheadEnabledEventArgs ev) {}
  public virtual void OnSCP3114Disguised(SCP3114DisguisedEventArgs ev) {}
}
