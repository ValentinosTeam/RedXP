using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Features.Stores;
using LabApi.Events.CustomHandlers;
using LabApi.Loader;
using LabApi.Features.Console;
using RedXP.Events;
using RedXP.Events.XPGainEvents;
using MySqlConnector;
using HarmonyLib;

namespace RedXP;

internal class RedXP : Plugin<Config> {
  public override string Name { get; }                = "RedXP";
  public override string Description { get; }         = "XP/Level plugin for the Valentinos server.";
  public override string Author { get; }              = "Team Valentinos";
  public override Version Version { get; }            = new Version(1, 0, 0);
  public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

  public static RedXP Instance { get; private set; }
  public new Config Config { get; private set; }
  public Translations Translations { get; private set; }
  public Database Database { get; private set; }
  private XPGainEvents xpGainEvents = new();
  private XPDataStoreEventHandler xpStoreEventHandler = new();
  private Harmony Harmony = new("gg.valentinos.redxp");

  public override void Enable() {
    Instance = this;

    Harmony.PatchAll();

    Database = new(Config.MySql_ConnectionString);

    CustomDataStoreManager.RegisterStore<XPDataStore>();
    xpGainEvents.Register();
    CustomHandlersManager.RegisterEventsHandler(xpStoreEventHandler);
    ServerSpecificSettings.Register();
    
    try {
      Database.Connect();
    } catch (MySqlException ex) {
      Logger.Error($"Database connection error: {ex.Message}\nPersistence features will be unavailable.");
    }
  }

  public override void Disable() {
    CustomHandlersManager.UnregisterEventsHandler(xpStoreEventHandler);
    xpGainEvents.Unregister();
    CustomDataStoreManager.UnregisterStore<XPDataStore>();
    
    Database.Disconnect();
  }

  public override void LoadConfigs() {
    if (!this.TryLoadConfig("config.yml", out Config config)) {
      Logger.Error("Failed to load config. Using default.");
      config = new();
    }
    if (!this.TryLoadConfig("translations.yml", out Translations translations)) {
      Logger.Error("Failed to load translations. Using default.");
      translations = new();
    }

    Config = config;
    Translations = translations;
  }
}
