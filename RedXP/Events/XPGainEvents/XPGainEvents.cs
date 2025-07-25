using LabApi.Features.Wrappers;
using LabApi.Events.CustomHandlers;
using System;
using LabApi.Features.Console;
using RedXP.Patched.Events.Handlers;

namespace RedXP.Events.XPGainEvents;

public class XPGainEvents {
  private static Config config => RedXP.Instance.Config;
  private static Translations translations => RedXP.Instance.Translations;

  private KillHumanAsHumanHandler killHHHandler = new();
  private KillHumanAsSCPHandler killHSHandler = new();
  private KillSCPAsHumanHandler killSHHandler = new();
  private KillHumanAsZombieHandler killHZHandler = new();
  private KillSuicideExplosionHandler killSEHandler = new();
  private KillZombieAsHumanHandler killZHHandler = new();

  private UsageSCP1853Handler usageSCP1853Handler = new();
  private UsageSCP207Handler usageSCP207Handler = new();
  private UsageAntiSCP207Handler usageAntiSCP207Handler = new();
  private UsageSCP018Handler usageSCP018Handler = new();
  private UsageSCP2176Handler usageSCP2176Handler = new();
  private UsageSCP500Handler usageSCP500Handler = new();
  private UsageSCP268Handler usageSCP268Handler = new();
  private UsageSCP1576Handler usageSCP1576Handler = new();
  private UsageSCP244Handler usageSCP244Handler = new();
  private UsageSCP330Handler usageSCP330Handler = new();
  private UsageSCP1344Handler usageSCP1344Handler = new();

  private PickupSpecialWeaponHandler pickupSpecialWeaponHandler = new();
  private PickupSCP127Handler pickupSCP127Handler = new();

  private EscapeHandler escapeHandler = new();
  private EscapeAssistHandler escapeAssistHandler = new();

  private RoundPresenceHandler roundPresenceHandler = new();
  private SCPWinHandler SCPWinHandler = new();
  
  private GeneratorActivationHandler generatorActivationHandler = new();
  private GeneratorDectivationHandler generatorDeactivationHandler = new();
  
  private WarheadActivationHandler warheadActivationHandler = new();
  private WarheadEnabledHandler warheadEnabledHandler = new();

  private SCP079LevelUpHandler scp079LevelUpHandler = new();
  private CreateZombieHandler createZombieHandler = new();
  private OpenGateHandler openGateHandler = new();
  private CuffHandler cuffHandler = new();

  public void Register() {
    CustomHandlersManager.RegisterEventsHandler(killHHHandler);
    CustomHandlersManager.RegisterEventsHandler(killHSHandler);
    CustomHandlersManager.RegisterEventsHandler(killSHHandler);
    CustomHandlersManager.RegisterEventsHandler(killHZHandler);
    CustomHandlersManager.RegisterEventsHandler(killSEHandler);
    CustomHandlersManager.RegisterEventsHandler(killZHHandler);
    
    CustomHandlersManager.RegisterEventsHandler(usageSCP1853Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP207Handler);
    CustomHandlersManager.RegisterEventsHandler(usageAntiSCP207Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP018Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP2176Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP500Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP268Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP1576Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP244Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP330Handler);
    CustomHandlersManager.RegisterEventsHandler(usageSCP1344Handler);
    
    CustomHandlersManager.RegisterEventsHandler(pickupSpecialWeaponHandler);
    CustomHandlersManager.RegisterEventsHandler(pickupSCP127Handler);

    CustomHandlersManager.RegisterEventsHandler(escapeHandler);
    CustomHandlersManager.RegisterEventsHandler(escapeAssistHandler);

    CustomHandlersManager.RegisterEventsHandler(roundPresenceHandler);
    CustomHandlersManager.RegisterEventsHandler(SCPWinHandler);

    CustomHandlersManager.RegisterEventsHandler(warheadActivationHandler);
    WarheadEvents.Enabled += warheadEnabledHandler.OnWarheadEnabled;

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
    CustomHandlersManager.UnregisterEventsHandler(killZHHandler);
    
    CustomHandlersManager.UnregisterEventsHandler(usageSCP1853Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP207Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageAntiSCP207Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP018Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP2176Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP500Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP268Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP1576Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP244Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP330Handler);
    CustomHandlersManager.UnregisterEventsHandler(usageSCP1344Handler);
   
    CustomHandlersManager.UnregisterEventsHandler(pickupSpecialWeaponHandler);
    CustomHandlersManager.UnregisterEventsHandler(pickupSCP127Handler);

    CustomHandlersManager.UnregisterEventsHandler(escapeHandler);
    CustomHandlersManager.UnregisterEventsHandler(escapeAssistHandler);

    CustomHandlersManager.UnregisterEventsHandler(roundPresenceHandler);
    CustomHandlersManager.UnregisterEventsHandler(SCPWinHandler);

    CustomHandlersManager.UnregisterEventsHandler(warheadActivationHandler);
    WarheadEvents.Enabled -= warheadEnabledHandler.OnWarheadEnabled;
    
    CustomHandlersManager.UnregisterEventsHandler(generatorActivationHandler);
    CustomHandlersManager.UnregisterEventsHandler(generatorDeactivationHandler);
    
    CustomHandlersManager.UnregisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.UnregisterEventsHandler(createZombieHandler);
    CustomHandlersManager.UnregisterEventsHandler(openGateHandler);
    CustomHandlersManager.UnregisterEventsHandler(cuffHandler);
  }
  
  public static void AddXPAndNotify(Player player, int amount, string eventMessage) {
    XPDataStore xpStore = XPDataStore.Get(player);
    
    if (!xpStore.DataAvailable) return;

    if (player.IsDummy) {
      Logger.Info(
          String.Format("Skipping addition of {0} XP for dummy {1} ({2})",
            amount, player.Nickname, eventMessage)
      );
      return;
    }
    
    xpStore.AddXP(amount);
    xpStore.XPData.LastXPGainEvent = eventMessage;

    if (!ServerSpecificSettings.IsXPGainHintsDisplayEnabled(player)) return;

    player.SendHint(
        String.Format(translations.XPGainHintTemplate, eventMessage, amount),
        config.XPGainHintDuration);
  }
}
