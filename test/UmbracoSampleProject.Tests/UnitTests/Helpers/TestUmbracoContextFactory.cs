﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Tests.Common;
using Umbraco.Cms.Web.Common.AspNetCore;
using Umbraco.Cms.Web.Common.UmbracoContext;


namespace UmbracoSampleProject.Tests.UnitTests.Helpers
{
    public class TestUmbracoContextFactory
    {
        public static IUmbracoContextFactory Create(
            GlobalSettings globalSettings = null,
            IUmbracoContextAccessor umbracoContextAccessor = null,
            IHttpContextAccessor httpContextAccessor = null,
            IPublishedUrlProvider publishedUrlProvider = null,
            UmbracoRequestPathsOptions umbracoRequestPathsOptions = null)
        {
            if (globalSettings == null)
            {
                globalSettings = new GlobalSettings();
            }

            if (umbracoContextAccessor == null)
            {
                umbracoContextAccessor = new TestUmbracoContextAccessor();
            }

            if (httpContextAccessor == null)
            {
                httpContextAccessor = Mock.Of<IHttpContextAccessor>();
            }

            if (publishedUrlProvider == null)
            {
                publishedUrlProvider = Mock.Of<IPublishedUrlProvider>();
            }

            if (umbracoRequestPathsOptions == null)
            {
                umbracoRequestPathsOptions = new UmbracoRequestPathsOptions();
            }

            var contentCache = new Mock<IPublishedContentCache>();
            var mediaCache = new Mock<IPublishedMediaCache>();
            var snapshot = new Mock<IPublishedSnapshot>();
            snapshot.Setup(x => x.Content).Returns(contentCache.Object);
            snapshot.Setup(x => x.Media).Returns(mediaCache.Object);
            var snapshotService = new Mock<IPublishedSnapshotService>();
            snapshotService.Setup(x => x.CreatePublishedSnapshot(It.IsAny<string>())).Returns(snapshot.Object);

            var hostingEnvironment = TestHelper.GetHostingEnvironment();

            var umbracoContextFactory = new UmbracoContextFactory(
            umbracoContextAccessor,
            snapshotService.Object,
            new UmbracoRequestPaths(Options.Create(globalSettings), hostingEnvironment, Options.Create(umbracoRequestPathsOptions)),
            hostingEnvironment,
            new UriUtility(hostingEnvironment),
            new AspNetCoreCookieManager(httpContextAccessor),
            httpContextAccessor);

            return umbracoContextFactory;
        }
    }
}
