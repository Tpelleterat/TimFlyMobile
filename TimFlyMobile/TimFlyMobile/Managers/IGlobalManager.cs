using System.Threading.Tasks;

namespace TimFlyMobile.Managers
{
    public interface IGlobalManager
    {

        Task<bool> Connect(string address, int port);

        void StartSendCommandsLoop();

        void StopSendCommandsLoop();

        void ChangeElevation(int value);

        void ChangeRoll(int value);

        void ChangePitch(int value);

        void SendInitialization();
    }
}
