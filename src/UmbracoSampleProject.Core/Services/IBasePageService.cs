using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoSampleProject.Models.Models.ViewModels;

namespace UmbracoSampleProject.Core.Services
{
    public interface IBasePageService
    {
        BasePageViewModel GetModel(IPublishedContent content);
    }
}
