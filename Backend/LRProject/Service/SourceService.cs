using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Data;
using LRProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LRProject.Service
{
    public class SourceService : ISourceService
    {
        private readonly DataContext _context;
        public SourceService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Source>>> AddSource(int source_id, int source_group_id)
        {
            var sg = _context.SourceGroups.FirstOrDefault(s => s.Id == source_group_id);
            if (sg == null)
            {
                return new Response<List<Source>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source group could not be found."
                };
            }
            var newSource = new Source() { Id = source_id, SourceGroupId = source_group_id };
            int capacity = _context.SourceGroups.FirstOrDefault(s => s.Id == source_group_id).Capacity;
            newSource.Space = capacity;
            //var SG = await _context.SourceGroups.FindAsync(source_group_id);
            _context.Sources.Add(newSource);
            //SG.Sources.Add(newSource);
            await _context.SaveChangesAsync();
            var response = new Response<List<Source>>()
            {
                Data = await _context.Sources.ToListAsync(),
                StatusCode = 201,
                Message = "Success, Source created."
            };
            return response;
        }

        public async Task<Response<List<SourceGroup>>> GetAllSourceGroups()
        {
            var response = new Response<List<SourceGroup>>()
            {
                Data = await _context.SourceGroups.Include(s => s.Sources).ToListAsync(),
                StatusCode = 200,
                Message = "Success, all source groups returned."
            };
            return response;
        }

        public async Task<Response<List<Source>>> GetAllSources()
        {
            var response = new Response<List<Source>>()
            {
                Data = await _context.Sources.ToListAsync(),
                StatusCode = 200,
                Message = "Success, all sources returned."
            };
            return response;
        }

        public async Task<Response<List<Source>>> RemoveSource(int source_id)
        {
            var source = _context.Sources.First(s => s.Id == source_id);
            if (source == null)
            {
                return new Response<List<Source>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };
            }
            _context.Sources.Remove(source);
            await _context.SaveChangesAsync();
            var response = new Response<List<Source>>()
            {
                Data = await _context.Sources.ToListAsync(),
                StatusCode = 200,
                Message = "Success, source removed"
            };
            return response;
        }

        public async Task<Response<List<SourceGroup>>> AddSourceGroup(int SG_id, string name, int cap)
        {
            SourceGroup newSG = new SourceGroup() { Id = SG_id, Name = name, Capacity = cap };
            _context.SourceGroups.Add(newSG);
            await _context.SaveChangesAsync();
            var response = new Response<List<SourceGroup>>()
            {
                Data = await _context.SourceGroups.ToListAsync(),
                StatusCode = 201,
                Message = "Success, new Source Group created."
            };
            return response;
        }

        public async Task<Response<List<Employee>>> AddEmployee(int employee_id, string password, string name)
        {
            Employee newEmployee = new Employee() { Id = employee_id, Password = password, Name = name };
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            var response = new Response<List<Employee>>()
            {
                Data = await _context.Employees.ToListAsync(),
                StatusCode = 201,
                Message = "Success, new Employee added"
            };
            return response;
        }

        public async Task<Response<List<ReturnEmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _context.Employees.Include(e => e.Sources).Include(e => e.SourceGroups).ToListAsync();
            List<ReturnEmployeeDTO> returnEmployees = new List<ReturnEmployeeDTO>();
            foreach (var employee in employees)
            {
                var newEmpDTO = new ReturnEmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Sources = employee.Sources,
                    SourceGroups = employee.SourceGroups
                };
                returnEmployees.Add(newEmpDTO);
            }
            var response = new Response<List<ReturnEmployeeDTO>>()
            {
                Data = returnEmployees,
                StatusCode = 200,
                Message = "Success, returned all employees."
            };
            return response;

        }

        public async Task<Response<Employee>> AddRelationship(int employee_id, int source_id)
        {
            var employee = await _context.Employees.FindAsync(employee_id);
            var source = await _context.Sources.FindAsync(source_id);
            if (employee == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            else if (source == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };
            }
            if (source.Space == 0)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "This source is not available."
                };
            }
            source.Space--;
            employee.Sources.Add(source);
            source.Employees.Add(employee);

            await _context.SaveChangesAsync();
            var response = new Response<Employee>()
            {
                Data = employee,
                StatusCode = 201,
                Message = "Success, Added new relationship."
            };
            return response;
        }

        public async Task<Response<List<Source>>> GetSourcesOfEmployee(int employee_id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == employee_id);
            if (employee == null)
            {
                return new Response<List<Source>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee not found"
                };
            }
            var sources = _context.Employees.Where(x => x.Id == employee_id).SelectMany(e => e.Sources);
            var response = new Response<List<Source>>()
            {
                Data = sources.ToList(),
                StatusCode = 200,
                Message = "Success, resources of the employee returned."
            };
            return response;
        }

        public async Task<Response<List<EmpDTOIdName>>> GetOwnersOfSource(int source_id)
        {
            var source = _context.Sources.FirstOrDefault(x => x.Id == source_id);
            if (source == null)
            {
                return new Response<List<EmpDTOIdName>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };

            }
            var employees = _context.Sources.Where(s => s.Id == source_id).SelectMany(x => x.Employees);
            var returnDTOS = new List<EmpDTOIdName>();
            foreach (var employee in employees)
            {
                var dto = new EmpDTOIdName()
                {
                    Id = employee.Id,
                    Name = employee.Name
                };
                returnDTOS.Add(dto);
            }
            var response = new Response<List<EmpDTOIdName>>()
            {
                Data = returnDTOS,
                StatusCode = 200,
                Message = "Success, owners of the source returned"
            };
            return response;
        }

        public async Task<Response<List<Employee>>> RemoveEmployee(int employee_id)
        {
            var employee = _context.Employees.FirstOrDefault(s => s.Id == employee_id);
            if (employee == null)
            {
                return new Response<List<Employee>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            var response = new Response<List<Employee>>()
            {
                Data = await _context.Employees.ToListAsync(),
                StatusCode = 200,
                Message = "Success, Employee removed."
            };
            return response;
        }

        public async Task<Response<List<SourceGroup>>> RemoveSourceGroup(int sg_id)
        {
            var sg = _context.SourceGroups.FirstOrDefault(s => s.Id == sg_id);
            if (sg == null)
            {
                return new Response<List<SourceGroup>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source Group could not be found."
                };
            }
            _context.SourceGroups.Remove(sg);
            await _context.SaveChangesAsync();
            var response = new Response<List<SourceGroup>>()
            {
                Data = await _context.SourceGroups.ToListAsync(),
                StatusCode = 200,
                Message = "Success, Source Group removed."
            };
            return response;
        }

        public async Task<Response<ReturnEmployeeDTO>> GetEmployeeById(int employee_id)
        {
            var employee = await _context.Employees.FindAsync(employee_id);
            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            var newEmpDTO = new ReturnEmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Sources = employee.Sources,
                SourceGroups = employee.SourceGroups
            };
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = newEmpDTO,
                StatusCode = 200,
                Message = "Success, employee returned."
            };
            return response;
        }

        public async Task<Response<Employee>> RemoveRelationship(int employee_id, int source_id)
        {
            var employee = _context.Employees.Include(e => e.Sources).SingleOrDefault(e => e.Id == employee_id);
            if (employee == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }

            if (employee != null)
            {
                foreach (var source in employee.Sources.Where(s => s.Id == source_id).ToList())
                {
                    source.Space++;
                    employee.Sources.Remove(source);
                }
            }
            await _context.SaveChangesAsync();
            var response = new Response<Employee>()
            {
                Data = employee,
                StatusCode = 200,
                Message = "Success, Relationship removed."
            };
            return response;
        }

        public async Task<Response<Employee>> AddAdministration(int employee_id, int sg_id)
        {

            var employee = await _context.Employees.FindAsync(employee_id);
            if (employee == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            var sg = await _context.SourceGroups.FindAsync(sg_id);
            if (sg == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source group could not be found."
                };
            }
            employee.SourceGroups.Add(sg);

            await _context.SaveChangesAsync();
            var response = new Response<Employee>()
            {
                Data = employee,
                StatusCode = 200,
                Message = "Success, administration added."
            };
            return response;
        }

        public async Task<Response<List<GetSourcesByGroupDTO>>> GetSourcesByGroup(int sg_id)
        {
            var sources = _context.SourceGroups.Where(x => x.Id == sg_id).SelectMany(e => e.Sources).Include(s => s.Employees);
            var sourcesDTO = new List<GetSourcesByGroupDTO>();
            foreach (var source in sources)
            {
                var newSourceDTO = new GetSourcesByGroupDTO()
                {
                    Id = source.Id,
                    SourceGroupId = source.SourceGroupId,
                    Space = source.Space,
                };
                if (source.Employees.Count > 0)
                {
                    foreach (var employee in source.Employees)
                    {
                        var newEmpDTO = new EmpDTOIdName()
                        {
                            Id = employee.Id,
                            Name = employee.Name,
                        };
                        newSourceDTO.Employees.Add(newEmpDTO);
                    }
                }
                sourcesDTO.Add(newSourceDTO);
            }
            var response = new Response<List<GetSourcesByGroupDTO>>()
            {
                Data = sourcesDTO,
                StatusCode = 200,
                Message = "Success, all sources of the group returned."
            };
            return response;
        }

        public async Task<Response<List<Employee>>> UpdateEmployee(UpdateEmployeeDTO request)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == request.Id);
            if (employee == null)
            {
                return new Response<List<Employee>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            employee.Name = request.Name;
            await _context.SaveChangesAsync();
            var response = new Response<List<Employee>>()
            {
                Data = await _context.Employees.ToListAsync(),
                StatusCode = 200,
                Message = "Success, employee updated."
            };
            return response;
        }

        public async Task<Response<List<SourceGroup>>> UpdateSourceGroup(int sg_id, int former_emp_id, int employee_id)
        {

            var sg = _context.SourceGroups.FirstOrDefault(s => s.Id == sg_id);
            var employee = _context.Employees.Include(e => e.SourceGroups).SingleOrDefault(e => e.Id == employee_id);
            var former_emp = _context.Employees.Include(e => e.SourceGroups).SingleOrDefault(e => e.Id == former_emp_id);
            if (employee == null || former_emp == null)
            {
                return new Response<List<SourceGroup>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            else if (sg == null)
            {
                return new Response<List<SourceGroup>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source Group could not be found."
                };
            }
            former_emp.SourceGroups.Remove(sg);
            sg.Employees.Remove(former_emp);
            employee.SourceGroups.Add(sg);
            sg.Employees.Add(employee);

            await _context.SaveChangesAsync();
            var response = new Response<List<SourceGroup>>()
            {
                Data = await _context.SourceGroups.ToListAsync(),
                StatusCode = 200,
                Message = "Success, Source Group updated."
            };
            return response;
        }

        public async Task<Response<Employee>> UpdateRelationship(int employee_id, int source_id, int new_source_id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employee_id);
            var source = _context.Sources.FirstOrDefault(s => s.Id == source_id);
            var newSource = _context.Sources.FirstOrDefault(s => s.Id == new_source_id);

            if (employee == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            else if (source == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };
            }
            else if (newSource == null)
            {
                return new Response<Employee>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };
            }
            RemoveRelationship(employee.Id, source.Id);
            AddRelationship(employee.Id, newSource.Id);
            var response = new Response<Employee>()
            {
                Data = employee,
                StatusCode = 200,
                Message = "Success, relationship updated."
            };
            await _context.SaveChangesAsync();
            return response;
        }
    }
}