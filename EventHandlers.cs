using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;
using UnityEngine.Playables;

namespace TKLimit
{
    class EventHandlers
    {
        private Plugin Plugin;
        public EventHandlers(Plugin plugin) => Plugin = plugin;

        internal void OnPlayerDamage(ref PlayerHurtEvent ev)
        {
            if (ev.Player.GetTeam() == ev.Attacker.GetTeam())
            {
                bool healplr = false;
                if (!(Plugin.revenge && ev.Player.GetPlayerId() == Plugin.lastTK[ev.Attacker.GetPlayerId()]))
                {
                    healplr = true;
                }
                if (Plugin.rooms.Contains(ev.Player.GetCurrentRoom().Name) | Plugin.zones.Contains(getZone(ev.Player.GetCurrentRoom().Name)))
                {
                    healplr = true;
                    if (Plugin.room)
                    {
                        ev.Attacker.Broadcast(Plugin.roomtime, Plugin.roombc, false);
                    }
                }
                if (Plugin.plrTKs[ev.Attacker.GetPlayerId()] >= Plugin.tkLimit && Plugin.limiter)
                {
                    healplr = true;
                }
                if (ev.Attacker.GetPlayerId() != ev.Player.GetPlayerId() && healplr == true)
                {
                    ev.Player.AddHealth(ev.Amount);
                }
            }
        }

        internal void OnPlayerDeath(ref PlayerDeathEvent ev)
        {
            if (ev.Player.GetTeam() == ev.Killer.GetTeam())
            {
                if (ev.Killer.GetPlayerId() != ev.Player.GetPlayerId())
                {
                    if (!Plugin.revenge | ev.Player.GetPlayerId() != Plugin.lastTK[ev.Killer.GetPlayerId()])
                    {
                        Plugin.plrTKs[ev.Killer.GetPlayerId()]++;
                        Plugin.lastTK[ev.Player.GetPlayerId()] = ev.Killer.GetPlayerId();
                    }
                    if (Plugin.plrTKs[ev.Killer.GetPlayerId()] >= Plugin.tkLimit && Plugin.ban)
                    {
                        ev.Killer.BanPlayer(Plugin.bantime, Plugin.banreason);
                    }
                    if (Plugin.plrTKs[ev.Killer.GetPlayerId()]+1 >= Plugin.tkLimit && Plugin.warning)
                    {
                        ev.Killer.Broadcast(Plugin.warningtime, Plugin.warningbc, false);
                    }
                    if (Plugin.plrTKs[ev.Killer.GetPlayerId()] >= Plugin.tkLimit && Plugin.max)
                    {
                        ev.Killer.Broadcast(Plugin.maxtime, Plugin.maxbc, false);
                    }
                    if (Plugin.plrTKs[ev.Killer.GetPlayerId()] < Plugin.tkLimit && Plugin.log == true)
                    {
                        Log.Info($"{ev.Killer.GetNickname()} has tked {ev.Player.GetNickname()} as {ev.Killer.GetRole()} in {ev.Killer.GetCurrentRoom().Name}");
                    }
                }
            }
        }

        internal void OnPlayerJoin(PlayerJoinEvent ev)
        {
            Plugin.plrTKs.Add(ev.Player.GetPlayerId(), 0);
            Plugin.lastTK.Add(ev.Player.GetPlayerId(), 0);
        }

        internal void OnPlayerLeave(PlayerLeaveEvent ev)
        {
            Plugin.plrTKs.Remove(ev.Player.GetPlayerId());
            Plugin.lastTK.Remove(ev.Player.GetPlayerId());
        }

        string getZone(string room)
        {
            int index = room.LastIndexOf("_");
            if (index > 0)
            {
                room = room.Substring(0, index);
            }
            return room;
        }
    }
}
