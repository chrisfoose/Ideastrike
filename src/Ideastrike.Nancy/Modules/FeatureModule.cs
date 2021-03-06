﻿using System;
using System.Linq;
using Ideastrike.Nancy.Models;
using Nancy;
using Nancy.Security;

namespace Ideastrike.Nancy.Modules
{
    public class FeatureModule : NancyModule
    {
        private readonly IFeatureRepository _features;

        public FeatureModule(IFeatureRepository features, IUserRepository users)
            : base("/idea")
        {
            _features = features;

            this.RequiresAuthentication();

            Post["/{idea}/feature"] = _ =>
            {
                int id = _.Idea;
                var feature = new Feature
                                {
                                    Time = DateTime.UtcNow,
                                    Text = Request.Form.feature,
                                    User = Context.GetCurrentUser(users)
                                };
                _features.Add(id, feature);

                return Response.AsRedirect(string.Format("/idea/{0}#{1}", id, feature.Id));
            };
        }
    }
}