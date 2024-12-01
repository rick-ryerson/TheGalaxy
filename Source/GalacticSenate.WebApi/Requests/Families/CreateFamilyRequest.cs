using System;
using System.Collections.Generic;

namespace GalacticSenate.WebApi.Requests.Families {
    public class CreateFamilyRequest {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; }
    }
}