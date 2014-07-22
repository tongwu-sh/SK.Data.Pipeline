﻿using SK.Data.Pipeline.Core.Common;
using SK.RetryLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SK.Data.Pipeline.Core
{
    public class WebSourceNode : SourceNode
    {
        public string Url { set; get; }
        public ICredentials Credentials { get; set; }
        public CookieContainer CookieContainer { get; set; }

        public WebSourceNode(string url, CookieContainer cookieContainer = null, ICredentials credentials = null)
            : base()
        {
            Url = url;
            CookieContainer = cookieContainer;
        }

        protected override IEnumerable<Entity> GetEntities()
        {
            string content = HttpRequestHelper.GetContentFromHttpUrl(Url, CookieContainer, Credentials);

            var entity = new Entity();
            entity.SetValue(Entity.DefaultColumn, content);

            yield return entity;
        }
    }
}
