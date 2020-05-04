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
        public static bool area;
        public static List<string> rooms;
        public static List<string> zones;
        public Dictionary<int, int> plrTKs = new Dictionary<int, int>();

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
            area = Config.GetBool("tkl_area", false);
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
            
        }
    }
}
