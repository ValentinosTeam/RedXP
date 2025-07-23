using LabApi.Features.Wrappers;
using LabApi.Events.CustomHandlers;
using System;
using LabApi.Features.Console;

namespace RedXP.Events.XPGainEvents;

public class XPGainEvents {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private KillHumanAsHumanHandler killHHHandler = new();
  private KillHumanAsSCPHandler killHSHandler = new();
  private KillSCPAsHumanHandler killSHHandler = new();
  private KillHumanAsZombieHandler killHZHandler = new();
  private KillSuicideExplosionHandler killSEHandler = new();

  private EscapeHandler escapeHandler = new();
  private EscapeAssistHandler escapeAssistHandler = new();

  private RoundPresenceHandler roundPresenceHandler = new();
  private SCPWinHandler SCPWinHandler = new();
  
  private GeneratorActivationHandler generatorActivationHandler = new();
  private GeneratorDectivationHandler generatorDeactivationHandler = new();

  private SCP079LevelUpHandler scp079LevelUpHandler = new();
  private WarheadActivationHandler warheadActivationHandler = new();
  private CreateZombieHandler createZombieHandler = new();
  private OpenGateHandler openGateHandler = new();
  private CuffHandler cuffHandler = new();

  public void Register() {
    CustomHandlersManager.RegisterEventsHandler(killHHHandler);
    CustomHandlersManager.RegisterEventsHandler(killHSHandler);
    CustomHandlersManager.RegisterEventsHandler(killSHHandler);
    CustomHandlersManager.RegisterEventsHandler(killHZHandler);
    CustomHandlersManager.RegisterEventsHandler(killSEHandler);

    CustomHandlersManager.RegisterEventsHandler(escapeHandler);
    CustomHandlersManager.RegisterEventsHandler(escapeAssistHandler);

    CustomHandlersManager.RegisterEventsHandler(roundPresenceHandler);
    CustomHandlersManager.RegisterEventsHandler(SCPWinHandler);

    CustomHandlersManager.RegisterEventsHandler(warheadActivationHandler);
    
    CustomHandlersManager.RegisterEventsHandler(generatorActivationHandler);
    CustomHandlersManager.RegisterEventsHandler(generatorDeactivationHandler);

    CustomHandlersManager.RegisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.RegisterEventsHandler(createZombieHandler);
    CustomHandlersManager.RegisterEventsHandler(openGateHandler);
    CustomHandlersManager.RegisterEventsHandler(cuffHandler);
  }

  public void Unregister() {
    CustomHandlersManager.UnregisterEventsHandler(killHHHandler);
    CustomHandlersManager.UnregisterEventsHandler(killHSHandler);
    CustomHandlersManager.UnregisterEventsHandler(killSHHandler);
    CustomHandlersManager.UnregisterEventsHandler(killHZHandler);
    CustomHandlersManager.UnregisterEventsHandler(killSEHandler);
    
    CustomHandlersManager.UnregisterEventsHandler(escapeHandler);
    CustomHandlersManager.UnregisterEventsHandler(escapeAssistHandler);

    CustomHandlersManager.UnregisterEventsHandler(roundPresenceHandler);
    CustomHandlersManager.UnregisterEventsHandler(SCPWinHandler);

    CustomHandlersManager.UnregisterEventsHandler(warheadActivationHandler);
    
    CustomHandlersManager.UnregisterEventsHandler(generatorActivationHandler);
    CustomHandlersManager.UnregisterEventsHandler(generatorDeactivationHandler);
    
    CustomHandlersManager.UnregisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.UnregisterEventsHandler(createZombieHandler);
    CustomHandlersManager.UnregisterEventsHandler(openGateHandler);
    CustomHandlersManager.UnregisterEventsHandler(cuffHandler);
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
