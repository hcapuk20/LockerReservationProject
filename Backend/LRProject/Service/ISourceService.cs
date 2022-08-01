using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;

namespace LRProject.Service
{
    public interface ISourceService
    {
        Task<List<Source>> GetAllSources();

        Task<List<Owns>> GetAllRelationships();

        Task<List<Source>> AddSource(int source_id, String type);

        Task<List<Owns>> ManualAddRelationship(int source_id, int employee_id);

        Task<List<Source>> RemoveSource(int source_id);

        Task<List<SourceGroup>> GetAllSourceGroups();

        Task<List<SourceGroup>> AddSourceGroup(int SG_id, string name, int cap);
    }
}