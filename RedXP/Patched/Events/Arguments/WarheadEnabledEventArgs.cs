using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace RedXP.Patched.Events.Arguments;

public class WarheadEnabledEventArgs : EventArgs, IPlayerEvent {
  public Player Player { get; }
  public AlphaWarheadSyncInfo WarheadState { get; }

  public WarheadEnabledEventArgs(ReferenceHub player, AlphaWarheadSyncInfo warheadState) {
    Player = Player.Get(player);
    WarheadState = warheadState;
  }
}
