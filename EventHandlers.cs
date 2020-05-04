using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

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
                if (Plugin.area && (Plugin.rooms.Contains(ev.Player.GetCurrentRoom().Name) | Plugin.zones.Contains(getZone(ev.Player.GetCurrentRoom().Name))))
                {
                    ev.Player.AddHealth(ev.Amount);
                }
                else if (Plugin.plrTKs[ev.Attacker.GetPlayerId()] >= Plugin.tkLimit && Plugin.limiter)
                {
                    if (ev.Attacker.GetPlayerId() != ev.Player.GetPlayerId())
                    {
                        ev.Player.AddHealth(ev.Amount);
                    }
                }
            }
        }

        internal void OnPlayerDeath(ref PlayerDeathEvent ev)
        {
            if (ev.Player.GetTeam() == ev.Killer.GetTeam())
            {
                if (ev.Killer.GetPlayerId() != ev.Player.GetPlayerId())
                {
                    Plugin.plrTKs[ev.Killer.GetPlayerId()]++;
                    if (Plugin.log == true)
                    {
                        if (Plugin.plrTKs[ev.Killer.GetPlayerId()] >= Plugin.tkLimit)
                        {
                            Log.Info($"{ev.Killer.GetNickname()} attempted to tk {ev.Player.GetNickname()} as {ev.Killer.GetRole()}");
                        }
                        else
                        {
                            Log.Info($"{ev.Killer.GetNickname()} has tked {ev.Player.GetNickname()} as {ev.Killer.GetRole()}");
                        }
                    }
                }
            }
        }

        internal void OnPlayerJoin(PlayerJoinEvent ev)
        {
            Plugin.plrTKs.Add(ev.Player.GetPlayerId(), 0);
        }

        internal void OnPlayerLeave(PlayerLeaveEvent ev)
        {
            Plugin.plrTKs.Remove(ev.Player.GetPlayerId());
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
