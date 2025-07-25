using System;
using System.Collections.Generic;
using System.Reflection;
using RedXP.Patched.Events.Handlers;

namespace RedXP.Patched.Events.CustomHandlers;

// copypasted from LabAPI
public static class PatchedHandlersManager {
  public static void RegisterEventsHandler<T>(T handler) where T: PatchedEventsHandler {
    Type type = handler.GetType();
    RegisterEvents(handler, type);
  }

  public static void UnregisterEventsHandler<T>(T handler) where T: PatchedEventsHandler {
    foreach(KeyValuePair<EventInfo, Delegate> internalEvent in handler.InternalEvents) {
      EventInfo key = internalEvent.Key;
      Delegate value = internalEvent.Value;

      key.RemoveEventHandler(null, value);
    }
    
    handler.InternalEvents.Clear();
  }

  private static void CheckEvent<T>(T handler, Type handlerType, string methodDelegate,
      Type eventType, string eventName) where T: PatchedEventsHandler {
    MethodInfo method = handlerType.GetMethod(methodDelegate, BindingFlags.Instance | BindingFlags.Public);
    if (!(method == null) && IsOverride(method)) {
      EventInfo @event = eventType.GetEvent(eventName);
      Delegate @delegate = Delegate.CreateDelegate(@event.EventHandlerType, handler, method);
      
      @event.AddEventHandler(null, @delegate);
      handler.InternalEvents.Add(@event, @delegate);
    }
  }

  private static bool IsOverride(MethodInfo method) {
    return method.GetBaseDefinition().DeclaringType != method.DeclaringType; 
  }

  private static void RegisterEvents<T>(T handler, Type handlerType) where T: PatchedEventsHandler {
    CheckEvent(handler, handlerType, nameof(PatchedEventsHandler.OnWarheadEnabled), typeof(WarheadEvents), nameof(WarheadEvents.Enabled));
    CheckEvent(handler, handlerType, nameof(PatchedEventsHandler.OnSCP3114Disguised), typeof(SCP3114Events), nameof(SCP3114Events.Disguised));
  }
}
