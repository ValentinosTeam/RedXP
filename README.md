# RedXP
A LabAPI plugin for SCP: Secret Laboratory that implements an XP/level system.

# Usage
## Dependencies and prerequisites
### Prerequisites
- a running MySQL-compatible database (MariaDB, Postgres, etc.)
- an SCP: Secret Laboratory server with LabAPI installed
### Dependencies
- [`Lib.Harmony`](https://www.nuget.org/packages/Lib.Harmony/2.3.6)
- [`System.Diagnostics.DiagnosticSource`](https://www.nuget.org/packages/System.Diagnostics.DiagnosticSource/)
- [`Microsoft.Extensions.Logging.Abstractions.dll`](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/)
- [`MySqlConnector`](https://www.nuget.org/packages/MySqlConnector/2.4.0)

These are included in the GitHub releases. You don't have to download them manually.
## Installation
### GitHub releases
Download the latest GitHub release and unpack it into the LabAPI directory.
### Manual compilation & installation
1. Copy/symlink the required game assemblies to `deps/`. Namely:
- `CommandSystem.Core.dll`
- `LabApi.dll`
- `UnityEngine.CoreModule.dll`
- `Mirror.dll`
- `Assembly-CSharp.dll`

2. Compile the project with your IDE of choice or with the .NET CLI.
3. Copy `RedXP.dll` to `<config directory>/LabAPI/plugins`.
4. Copy the dependencies to `<config directory>/LabAPI/dependencies`. Usually you can find them in the same directory as the compilation output.
## Configuration
The plugin configuration is split into `config.yml` and `translations.yml`. <br>
`config.yml` is for most configuration options, including XP rewards. <br>
`translations.yml` defines all the messages shown to the user. <br>
*Only the basic configuration settings are documented.*
### Database connection (required)
Change `my_sql_connection_string` according to the template: `server=<database address>;port=<database port>;uid=<database user>;pwd=<user password>;database=<database name>`
### Level formula
Defines the XP->level conversion. `x` in the formula represents the amount of XP. <br>
Uses Unity's [`ExpressionEvaluator.Evaluate`](https://docs.unity3d.com/ScriptReference/ExpressionEvaluator.Evaluate.html) under the hood.
### XP rewards
Defined in `config.yml`, XP rewards are suffixed with `_x_p`. They specify the amount of XP given for performing certain actions (further referred to as XP gain events).
### XP gain hints
Defined in `translations.yml`, XP gain hints are suffixed with `_msg`. These are the messages players receive when executing XP gain events. <br>
`x_p_gain_hint_template` allows you to apply formatting to these hints. `{0}` is replaced with the hint text, `{1}` is replaced with the amount of XP gained.
## Commands
| **Command**                   | **Description**                                                               | **Type**       |
|-------------------------------|-------------------------------------------------------------------------------|----------------|
| setplayerxp {id} {amount}     | Sets a player's XP to the given amount.                                       | Admin          |
| seelvl {id} <br> seexp {id}   | Shows a player's XP and level.                                                | Admin          |
| leaderboard <br> .leaderboard | Displays the top N players and their levels.                                  | Admin/Client   |
| .seexp <br> .seelvl           | Shows the current player's XP and level.                                      | Client         |
| wipexpdatabase                | Wipes the XP database. Does not delete the data of players currently in-game. | Server console |