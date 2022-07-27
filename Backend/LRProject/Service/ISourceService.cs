using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;

namespace LRProject.Service
{
    public interface ISourceService
    {
        public List<Source> GetAllSources();

    }
}