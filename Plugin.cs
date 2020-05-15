using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;

namespace TKLimit
{
    public class Plugin : EXILED.Plugin
    {
        private EventHandlers EventHandlers;

        public static bool enabled;
        public static int tkLimit;
        public static bool log;
        public static bool limiter;
        public static bool revenge;
        public static bool ban;
        public static int bantime;
        public static string banreason;
        public static bool warning;
        public static string warningbc;
        public static uint warningtime;
        public static bool room;
        public static string roombc;
        public static uint roomtime;
        public static bool max;
        public static string maxbc;
        public static uint maxtime;
        public static List<string> rooms;
        public static List<string> zones;
        public Dictionary<int, int> plrTKs = new Dictionary<int, int>();
        public Dictionary<int, int> lastTK = new Dictionary<int, int>();

        public override string getName { get; } = "TKLimit";

        public override void OnDisable()
        {
            Events.PlayerJoinEvent -= EventHandlers.OnPlayerJoin;
            Events.PlayerLeaveEvent -= EventHandlers.OnPlayerLeave;
            Events.PlayerHurtEvent -= EventHandlers.OnPlayerDamage;
            Events.PlayerDeathEvent -= EventHandlers.OnPlayerDeath;

            EventHandlers = null;
        }

        public void loadConfig()
        {
            enabled = Config.GetBool("tkl_enable", true);
            tkLimit = Config.GetInt("tkl_limit", 4);
            log = Config.GetBool("tkl_log", true);
            limiter = Config.GetBool("tkl_limiter", true);
            revenge = Config.GetBool("tkl_revenge", false);
            ban = Config.GetBool("tkl_ban", false);
            bantime = Config.GetInt("tkl_bantime", 0);
            banreason = Config.GetString("tkl_banreason", "TK");
            warning = Config.GetBool("tkl_warning", false);
            warningbc = Config.GetString("tkl_warningbc", "You have one tk left");
            warningtime = Config.GetUInt("tkl_warningtime", 5);
            room = Config.GetBool("tkl_room", false);
            roombc = Config.GetString("tkl_roombc", "You can't tk in this room");
            roomtime = Config.GetUInt("tkl_roomtime", 5);
            max = Config.GetBool("tkl_max", false);
            maxbc = Config.GetString("tkl_maxbc", "You can't tk anymore");
            maxtime = Config.GetUInt("tkl_maxtime", 5);
            rooms = Config.GetStringList("tkl_rooms");
            zones = Config.GetStringList("tkl_zones");
        }

        public override void OnEnable()
        {
            loadConfig();
            if (!enabled) { return; }

            Log.Info("Starting TKLimiter...");
            EventHandlers = new EventHandlers(this);

            Events.PlayerJoinEvent += EventHandlers.OnPlayerJoin;
            Events.PlayerLeaveEvent += EventHandlers.OnPlayerLeave;
            Events.PlayerHurtEvent += EventHandlers.OnPlayerDamage;
            Events.PlayerDeathEvent += EventHandlers.OnPlayerDeath;
        }

        public override void OnReload()
        {
            loadConfig();
            if (!enabled) { return; }
        }
    }
}
