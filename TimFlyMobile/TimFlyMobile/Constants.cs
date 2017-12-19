using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimFlyMobile
{
    public static class Constants
    {
        //Connection page default values
        public const string DEFAULT_ADDRESS = "172.18.2.74";
        public const string DEFAULT_PORT = "1337";

        //Sockets commands
        public const string COMMAND_SEPARATOR = "|";
        public const string ASKSTATUS_COMMAND = "ASKSTATUS";
        public const string STATUS_COMMAND = "STATUS";
        public const string ELEVATION_COMMAND = "ELEVATION";
        public const int ELEVATION_COMMAND_MAX_VALUE = 200;
        public const string PITCH_COMMAND = "PITCH";
        public const string ROLL_COMMAND = "ROLL";
        public const string INITIALIZATION_COMMAND = "INITIALIZATION";

        //Miliseconds
        public const int FREQUENCE_SEND_COMMANDS = 50;
        public const int FREQUENCE_UPDATE_ELEVATION = 100;
    }
}
