using LabApi.Features.Wrappers;
using LabApi.Events.CustomHandlers;
using System;

namespace RedXP.Events.XPGainEvents;

public class XPGainEvents {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private KillEventHandler killHandler = new();
  private EscapeEventHandler escapeHandler = new();
  private SCP079LevelUpEventHandler scp079LevelUpHandler = new();
  private GeneratorEventHandler generatorEventHandler = new();
  private WarheadEventHandler warheadEventHandler = new();

  public void Register() {
    CustomHandlersManager.RegisterEventsHandler(killHandler);
    CustomHandlersManager.RegisterEventsHandler(escapeHandler);
    CustomHandlersManager.RegisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.RegisterEventsHandler(generatorEventHandler);
    CustomHandlersManager.RegisterEventsHandler(warheadEventHandler);
  }

  public void Unregister() {
    CustomHandlersManager.UnregisterEventsHandler(killHandler);
    CustomHandlersManager.UnregisterEventsHandler(escapeHandler);
    CustomHandlersManager.UnregisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.UnregisterEventsHandler(generatorEventHandler);
    CustomHandlersManager.UnregisterEventsHandler(warheadEventHandler);
  }
  
  public static void AddXPAndNotify(Player player, int amount, string eventMessage) {
    XPDataStore xpStore = XPDataStore.Get(player);
    xpStore.AddXP(amount);
    xpStore.XPData.LastXPGainEvent = eventMessage;

    if (!ServerSpecificSettings.IsXPGainHintsDisplayEnabled(player)) return;

    player.SendHint(
        String.Format(translations.XPGainHintTemplate, eventMessage, amount),
        config.XPGainHintDuration);
  }
}
