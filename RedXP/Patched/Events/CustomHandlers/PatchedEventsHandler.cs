using System;
using System.Collections.Generic;
using System.Reflection;
using LabApi.Events.CustomHandlers;
using RedXP.Patched.Events.Arguments;

namespace RedXP.Patched.Events.CustomHandlers;

public abstract class PatchedEventsHandler : CustomEventsHandler {
  internal readonly Dictionary<EventInfo, Delegate> InternalEvents = new();

  public virtual void OnWarheadEnabled(WarheadEnabledEventArgs ev) {}
  public virtual void OnSCP3114Disguised(SCP3114DisguisedEventArgs ev) {}
}
