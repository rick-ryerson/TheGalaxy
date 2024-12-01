using GalacticSenate.Library.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.Organization
{
    public interface IFamiliesService
    {
        Task<ModelResponse<Model.Family, AddFamilyRequest>> AddAsync(AddFamilyRequest request);
    }
    public class FamiliesService
    {
    }
}
