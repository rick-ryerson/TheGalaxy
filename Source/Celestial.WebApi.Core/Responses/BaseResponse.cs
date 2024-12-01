using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.WebApi.Core.Responses {
    public class BaseResponse {
        public TimeSpan Execution { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
