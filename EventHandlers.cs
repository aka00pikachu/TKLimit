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
            string plrClass = GetPlrClass(ev.Player.GetRole());
            string atkClass = GetPlrClass(ev.Attacker.GetRole());
            if (plrClass == atkClass)
            {
                if (Plugin.plrTKs[ev.Attacker.GetPlayerId()] >= Plugin.tkLimit)
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
            string plrClass = GetPlrClass(ev.Player.GetRole());
            string kilClass = GetPlrClass(ev.Killer.GetRole());
            if (plrClass == kilClass)
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

        public string GetPlrClass(RoleType plrRole)
        {
            string plrClass = null;
            if (Plugin.NTF.Contains(plrRole))
            {
                plrClass = "NTF";
            }
            if (Plugin.CHAOS.Contains(plrRole))
            {
                plrClass = "CHAOS";
            }
            if (Plugin.SCP.Contains(plrRole))
            {
                plrClass = "SCP";
            }
            return plrClass;
        }
    }
}
