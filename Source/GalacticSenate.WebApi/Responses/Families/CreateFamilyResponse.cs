using Celestial.WebApi.Core.Responses;
using GalacticSenate.WebApi.Requests.Families;
using System;
using System.Collections.Generic;

namespace GalacticSenate.WebApi.Responses.Families {
    public class CreateFamilyResponse : BaseResponse {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CreateFamilyRequest CreateFamilyRequest { get; set; }
    }
}
