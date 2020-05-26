using EXILED;
using EXILED.Extensions;
using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKLimit
{
    class RACommandHandler
    {
        public Plugin plugin;

        public RACommandHandler(Plugin plugin) => this.plugin = plugin;

        internal void OnRACommand(ref RACommandEvent ev)
        {
            if (string.IsNullOrEmpty(ev.Command) | !ev.Command.StartsWith("tkl")) return;

            List<string> args = ev.Command.Split(' ').ToList();
            if (args.Count <= 1) return;
            ev.Allow = false;

            string cmd = args[1];

            ReferenceHub sender = Player.GetPlayer(ev.Sender.SenderId);
            if (sender == null)
            {
                sender = PlayerManager.localPlayer.GetComponent<ReferenceHub>();
            }

            switch (cmd)
            {
                case "getroom":
                    if (!sender.CheckPermission("tkl.getroom"))
                    {
                        ev.Sender.RAMessage("Invalid permissions");
                    }
                    ev.Sender.RAMessage($"{sender.GetCurrentRoom().Name}");
                    break;
                case "addroom":
                    if (!sender.CheckPermission("tkl.addroom"))
                    {
                        ev.Sender.RAMessage("Invalid permissions");
                    }
                    //Plugin.Config.SetStringListItem("tkl_rooms", $"{Plugin.rooms.Count}", $"{sender.GetCurrentRoom().Name}");
                    //plugin.loadConfig();
                    ev.Sender.RAMessage("This command is still being worked on");
                    break;
                default:
                    ev.Sender.RAMessage("This command does not currently exist");
                    break;
            }
        }
    }
}
