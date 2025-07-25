using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace RedXP.Patched.Events.Arguments;

public class SCP3114DisguisedEventArgs : EventArgs, IPlayerEvent {
  public Player Player { get; }

  public SCP3114DisguisedEventArgs(ReferenceHub player) {
    Player = Player.Get(player);
  }
}
