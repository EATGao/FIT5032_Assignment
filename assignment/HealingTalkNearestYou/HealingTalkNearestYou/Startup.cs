using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealingTalkNearestYou.Startup))]
namespace HealingTalkNearestYou
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
