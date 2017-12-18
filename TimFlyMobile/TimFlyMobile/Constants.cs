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
        public static string DEFAULT_ADDRESS = "172.18.2.74";
        public static string DEFAULT_PORT = "1337";

        //Sockets commands
        public static string COMMAND_SEPARATOR = "|";
        public static string ASKSTATUS_COMMAND = "ASKSTATUS";
        public static string STATUS_COMMAND = "STATUS";
        public static string ELEVATION_COMMAND = "ELEVATION";
        public static string PITCH_COMMAND = "PITCH";
        public static string ROLL_COMMAND = "ROLL";
        public static string INITIALIZATION_COMMAND = "INITIALIZATION";

    }
}
