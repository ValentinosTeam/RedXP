using LabApi.Events.CustomHandlers;
using RedXP.Patched.Events.Arguments;

namespace RedXP.Patched.Events.CustomHandlers;

public abstract class PatchedEventsHandler : CustomEventsHandler {
  public virtual void OnWarheadEnabled(WarheadEnabledEventArgs ev) {}
}
