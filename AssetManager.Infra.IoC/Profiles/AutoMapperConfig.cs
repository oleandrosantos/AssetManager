using AssetManager.Application.Profiles;
using AutoMapper;

namespace AssetManager.Application.Profiles
{
    public static class AutoMapperConfig
    {

        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssetProfile());
                cfg.AddProfile(new AssetEventsProfile());
                cfg.AddProfile(new CompanyProfile());
                cfg.AddProfile(new UserProfile());
            });

            return config;
        }
    }
}