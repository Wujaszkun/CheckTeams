using System;
using System.Linq;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("CheckTeams", "Wujaszkun", "1.10.06")]
    [Description("Returns team information")]
    internal class CheckTeams : RustPlugin
    {
        [ChatCommand("checkall")]
        private void CheckAllCommand(BasePlayer player, string command, string[] args)
        {
            Puts("TEAMS");
            CheckTeamsCommand(player, command, args);
            Puts("");
            Puts("Lock");
            CheckLockCommand(player, command, args);
            Puts("");
            Puts("Szafa");
            CheckSzafaCommand(player, command, args);
            Puts("");
        }

        [ChatCommand("checkteams")]
        private void CheckTeamsCommand(BasePlayer player, string command, string[] args)
        {
            if (player.IsAdmin)
            {
                foreach (var playerTeam in BasePlayer.allPlayerList.GroupBy(x=>x.Team).ToList())
                {
                    try
                    {
                        if (playerTeam?.Key.members.Count > 3)
                        {
                            Puts($"====={playerTeam?.Key.members.Count}====={playerTeam?.Key.teamLeader}==");
                            Puts(String.Join(" ", playerTeam?.Key.members.ToArray()));
                            Puts("");
                        }
                    }
                    catch { }
                }
            }
        }

        [ChatCommand("checklock")]
        private void CheckLockCommand(BasePlayer player, string command, string[] args)
        {
            if (player.IsAdmin)
            {
                foreach (var codeLock in GameObject.FindObjectsOfType<CodeLock>())
                {
                    if (codeLock.whitelistPlayers.Count > 3)
                    {
                        Puts($"======CODELOCK==={codeLock.whitelistPlayers.Count.ToString()}===");
                        Puts($"CodeLock owner: {covalence.Players.FindPlayerById(codeLock.OwnerID.ToString())}");

                        Puts(String.Join(" ", codeLock.whitelistPlayers.ToArray()));
                    }
                }
            }
        }

        [ChatCommand("checkszafa")]
        private void CheckSzafaCommand(BasePlayer player, string command, string[] args)
        {
            if (player.IsAdmin)
            {
                foreach (var cupBoard in GameObject.FindObjectsOfType<BuildingPrivlidge>())
                {
                    if (cupBoard.authorizedPlayers.Count > 3)
                    {
                        Puts("======SZAFA======");
                        Puts($"SZAFA owner: {covalence.Players.FindPlayerById(cupBoard.OwnerID.ToString())}");
                        foreach (var play in cupBoard.authorizedPlayers)
                        {
                            Puts(play.username);
                        }
                    }
                }
            }
        }
    }
}