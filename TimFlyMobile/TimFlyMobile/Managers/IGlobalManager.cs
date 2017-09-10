using System.Threading.Tasks;

namespace TimFlyMobile.Managers
{
    public interface IGlobalManager
    {

        Task<bool> Connect(string address, int port);

        void ChangeElevation(int value);
    }
}
