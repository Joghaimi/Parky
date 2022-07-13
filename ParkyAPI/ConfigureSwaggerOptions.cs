using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ParkyAPI
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        //readonly IApiVersionDescriptionProvider provider;
        public void Configure(SwaggerGenOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
