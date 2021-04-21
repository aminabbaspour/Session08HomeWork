﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NationalCodeConstraint.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalCodeConstraint.Constraint
{
    public class NationalCodeRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Validations.IsValidIranianNationalCode(values[routeKey].ToString());
        }

        
    }
}
