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

  private DamageSpecialWeaponHandler damageSpecialWeaponHandler = new();
  private DamageSCP018Handler damageSCP018Handler = new();

  private EscapeHandler escapeHandler = new();
  private EscapeAssistHandler escapeAssistHandler = new();

  private SCP079LevelUpEventHandler scp079LevelUpHandler = new();
  private GeneratorActivationHandler generatorActivationHandler = new();
  private WarheadEventHandler warheadHandler = new();
  private RoundEventHandler roundHandler = new();
  private SCP049EventHandler scp049Handler = new();
  private DoorEventHandler doorHandler = new();
  private CuffEventHandler cuffHandler = new();

  public void Register() {
    CustomHandlersManager.RegisterEventsHandler(killHHHandler);
    CustomHandlersManager.RegisterEventsHandler(killHSHandler);
    CustomHandlersManager.RegisterEventsHandler(killSHHandler);
    CustomHandlersManager.RegisterEventsHandler(killHZHandler);
    CustomHandlersManager.RegisterEventsHandler(killSEHandler);

    CustomHandlersManager.RegisterEventsHandler(damageSpecialWeaponHandler);
    CustomHandlersManager.RegisterEventsHandler(damageSCP018Handler);

    CustomHandlersManager.RegisterEventsHandler(escapeHandler);
    CustomHandlersManager.RegisterEventsHandler(escapeAssistHandler);

    CustomHandlersManager.RegisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.RegisterEventsHandler(generatorActivationHandler);
    CustomHandlersManager.RegisterEventsHandler(warheadHandler);
    CustomHandlersManager.RegisterEventsHandler(roundHandler);
    CustomHandlersManager.RegisterEventsHandler(scp049Handler);
    CustomHandlersManager.RegisterEventsHandler(doorHandler);
    CustomHandlersManager.RegisterEventsHandler(cuffHandler);
  }

  public void Unregister() {
    CustomHandlersManager.UnregisterEventsHandler(killHHHandler);
    CustomHandlersManager.UnregisterEventsHandler(killHSHandler);
    CustomHandlersManager.UnregisterEventsHandler(killSHHandler);
    CustomHandlersManager.UnregisterEventsHandler(killHZHandler);
    CustomHandlersManager.UnregisterEventsHandler(killSEHandler);
    
    CustomHandlersManager.UnregisterEventsHandler(damageSpecialWeaponHandler);
    CustomHandlersManager.UnregisterEventsHandler(damageSCP018Handler);

    CustomHandlersManager.UnregisterEventsHandler(escapeHandler);
    CustomHandlersManager.UnregisterEventsHandler(escapeAssistHandler);
    
    CustomHandlersManager.UnregisterEventsHandler(scp079LevelUpHandler);
    CustomHandlersManager.UnregisterEventsHandler(generatorActivationHandler);
    CustomHandlersManager.UnregisterEventsHandler(warheadHandler);
    CustomHandlersManager.UnregisterEventsHandler(roundHandler);
    CustomHandlersManager.UnregisterEventsHandler(scp049Handler);
    CustomHandlersManager.UnregisterEventsHandler(doorHandler);
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
