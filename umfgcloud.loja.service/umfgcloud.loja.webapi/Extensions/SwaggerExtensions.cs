namespace umfgcloud.loja.webapi.Extensions
{
    internal static class SwaggerExtensions
    {
        private const string C_FILENAME_XML = "umfgcloud-loja-service.xml";

        internal static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.ResolveConflictingActions(x => x.FirstOrDefault());
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, C_FILENAME_XML));
            });
        }
    }
}