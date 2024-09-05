using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoSampleProject.Core.Services;
using UmbracoSampleProject.Core.Services.Implimentations;

namespace UmbracoSampleProject.Core.DI
{
    public class RegisterDependencies : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IBasePageService,BasePageService>();
        }
    }
}
