#if CrazyGamesPlatform_yg
using CrazyGames;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
#if Authorization_yg
        public PortalUser User;
#endif
        public void InitAwake()
        {
            if (CrazySDK.IsAvailable)
            {
                CrazySDK.Init(() =>
                {
#if Authorization_yg
                    if (CrazySDK.User.IsUserAccountAvailable)
                    {
                        CrazySDK.User.GetUser(user =>
                        {
                            User = user;
                            YG2.SyncInitialization();
                        });
                    }
                    else
                    {
                        YG2.SyncInitialization();
                    }
#else
                    YG2.SyncInitialization();
#endif
                });
            }
        }
    }
}
#endif