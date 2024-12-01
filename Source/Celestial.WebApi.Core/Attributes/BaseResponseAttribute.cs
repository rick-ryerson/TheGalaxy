using Celestial.WebApi.Core.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.WebApi.Core.Attributes {
    public class BaseResponseControllerAttribute : TypeFilterAttribute {
        public BaseResponseControllerAttribute() : base(typeof(BaseResponseFilter)) { }
    }
}
