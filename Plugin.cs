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
        private static RoleType[] NTFArray = {RoleType.Scientist, RoleType.FacilityGuard, RoleType.NtfCadet, RoleType.NtfScientist, RoleType.NtfLieutenant, RoleType.NtfCommander};
        private static RoleType[] CHAOSArray = {RoleType.ClassD, RoleType.ChaosInsurgency};
        private static RoleType[] SCPArray = {RoleType.Scp049, RoleType.Scp0492, RoleType.Scp079, RoleType.Scp096, RoleType.Scp106, RoleType.Scp173, RoleType.Scp93953, RoleType.Scp93989};
        public List<RoleType> NTF = new List<RoleType>(NTFArray);
        public List<RoleType> CHAOS = new List<RoleType>(CHAOSArray);
        public List<RoleType> SCP = new List<RoleType>(SCPArray);
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
