using LabApi.Features.Wrappers;
using LabApi.Events.CustomHandlers;
using System;
using LabApi.Features.Console;

namespace RedXP.Events.XPGainEvents;

public class XPGainEvents {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private KillEventHandler killHandler = new();
  private EscapeEventHandler escapeHandler = new();
  private SCP079LevelUpEventHandler scp079LevelUpHandler = new();
  private GeneratorEventHandler generatorHandler = new();
  private WarheadEventHandler warheadHandler = new();
  private DamageEventHandler damageHandler = new();
  private RoundEndEventHandler roundEndHandler = new();
  private SCP049EventHandler scp049Handler = new();

  public void Register() {
    CustomHandlersManager.RegisterEventsHandler(killHandler);
    CustomHandlersManager.RegisterEventsHandler(escapeHandler);
    CustomHandlersManager.RegisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.RegisterEventsHandler(generatorHandler);
    CustomHandlersManager.RegisterEventsHandler(warheadHandler);
    CustomHandlersManager.RegisterEventsHandler(damageHandler);
    CustomHandlersManager.RegisterEventsHandler(roundEndHandler);
    CustomHandlersManager.RegisterEventsHandler(scp049Handler);
  }

  public void Unregister() {
    CustomHandlersManager.UnregisterEventsHandler(killHandler);
    CustomHandlersManager.UnregisterEventsHandler(escapeHandler);
    CustomHandlersManager.UnregisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.UnregisterEventsHandler(generatorHandler);
    CustomHandlersManager.UnregisterEventsHandler(warheadHandler);
    CustomHandlersManager.UnregisterEventsHandler(damageHandler);
    CustomHandlersManager.UnregisterEventsHandler(roundEndHandler);
    CustomHandlersManager.UnregisterEventsHandler(scp049Handler);
  }
  
  public static void AddXPAndNotify(Player player, int amount, string eventMessage) {
    if (player.IsDummy) {
      Logger.Info(
          String.Format("Skipping addition of {0} XP for dummy {1} ({2})",
            amount, player.Nickname, eventMessage)
      );
      return;
    }
    
    XPDataStore xpStore = XPDataStore.Get(player);
    xpStore.AddXP(amount);
    xpStore.XPData.LastXPGainEvent = eventMessage;

    if (!ServerSpecificSettings.IsXPGainHintsDisplayEnabled(player)) return;

    player.SendHint(
        String.Format(translations.XPGainHintTemplate, eventMessage, amount),
        config.XPGainHintDuration);
  }
}
