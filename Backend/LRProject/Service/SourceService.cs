using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;

namespace LRProject.Service
{
    public class SourceService : ISourceService
    {
        private List<Source> _sourceList = new List<Source>{
            new Source(){Source_Id = 1, Type = "Locker"},
            new Source(){Source_Id = 2, Type="Locker"},
            new Source(){Source_Id = 3, Type="Bus"},
            new Source(){Source_Id = 4, Type="Bus"},
        };
        public List<Source> GetAllSources()
        {
            return _sourceList;
        }
    }
}