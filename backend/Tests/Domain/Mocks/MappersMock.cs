using AutoMapper;
using Domain.MapperProfiles;

namespace Tests.Domain.Mocks
{
    public static class MappersMock
    {
        public static IMapper GetMock()
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();

            mapperConfigurationExpression.AddProfile<UserProfile>();

            var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);
            mapperConfiguration.AssertConfigurationIsValid();

            return new Mapper(mapperConfiguration);
        }
    }
}
