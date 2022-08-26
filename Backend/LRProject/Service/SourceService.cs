using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LRProject.Data;
using LRProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LRProject.Service
{
    public class SourceService : ISourceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SourceService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<ReturnEmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _context.Employees.Where(e => e.Status == 1).ToListAsync();

            List<ReturnEmployeeDTO> returnEmployees = _mapper.Map<List<ReturnEmployeeDTO>>(employees);
            foreach (var employee in returnEmployees)
            {
                var sources = await _context.EmployeeSources.Where(es => es.EmployeeId == employee.Id && es.Status == 1).Select(es => es.Source).ToListAsync();
                List<ReturnSourceDTO> sourceDTOs = _mapper.Map<List<ReturnSourceDTO>>(sources);
                employee.Sources = sourceDTOs;
                var sgs = await _context.EmployeeSourcesGroups.Where(es => es.EmployeeId == employee.Id && es.Status == 1).Select(es => es.SourceGroup).ToListAsync();
                var sgDTOs = _mapper.Map<List<ReturnSourceGroupDTO>>(sgs);
                foreach (var sgDTO in sgDTOs)
                {
                    var sgSources = await _context.Sources.Where(s => s.SourceGroupId == sgDTO.Id && s.Status == 1).ToListAsync();
                    var mappedSources = _mapper.Map<List<ReturnSourceDTO>>(sgSources);
                    sgDTO.Sources = mappedSources;
                }
                employee.SourceGroups = sgDTOs;
            }
            var response = new Response<List<ReturnEmployeeDTO>>()
            {
                Data = returnEmployees,
                StatusCode = 200,
                Message = "Success, returned all employees."
            };
            return response;

        }

        public async Task<Response<ReturnEmployeeDTO>> GetEmployeeById(int employee_id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employee_id && e.Status == 1);
            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            var sources = await _context.EmployeeSources.Where(es => es.EmployeeId == employee.Id && es.Status == 1).Select(e => e.Source).ToListAsync();
            var sgs = await _context.EmployeeSourcesGroups.Where(es => es.EmployeeId == employee.Id && es.Status == 1).Select(e => e.SourceGroup).ToListAsync();
            var newEmpDTO = _mapper.Map<ReturnEmployeeDTO>(employee);
            List<ReturnSourceDTO> sourceDTOs = _mapper.Map<List<ReturnSourceDTO>>(sources);
            List<ReturnSourceGroupDTO> sgDTOs = _mapper.Map<List<ReturnSourceGroupDTO>>(sgs);
            foreach (var sgDTO in sgDTOs)
            {
                var sgSources = await _context.Sources.Where(s => s.SourceGroupId == sgDTO.Id && s.Status == 1).ToListAsync();
                var mappedSources = _mapper.Map<List<ReturnSourceDTO>>(sgSources);
                sgDTO.Sources = mappedSources;
            }
            newEmpDTO.Sources = sourceDTOs;
            newEmpDTO.SourceGroups = sgDTOs;
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = newEmpDTO,
                StatusCode = 200,
                Message = "Success, employee returned."
            };
            return response;
        }
        public async Task<Response<ReturnEmployeeDTO>> AddEmployee(int employee_id, string name, string password, string role, int sg_id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee_id);
            Employee newEmployee = new Employee() { Id = employee_id, Name = name, Password = password, Role = role, Status = 1 };
            if (employee == null)
            {
                _context.Add(newEmployee);
            }
            else if (employee.Status == 0)
            {
                employee.Name = name;
                employee.Password = password;
                employee.Status = 1;
            }
            else if (employee.Status == 1)
            {

                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 200,
                    Message = "An employee with the given Id already exists."
                };
            }
            await _context.SaveChangesAsync();
            var sourceAdded = await AddAutoRelationship(employee_id, sg_id);
            var req = await GetEmployeeById(employee_id);
            if (sourceAdded.StatusCode == 404)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = req.Data,
                    StatusCode = 201,
                    Message = "New employee added, but no available source found in the given group."
                };
            }
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = req.Data,
                StatusCode = 201,
                Message = "Success, new Employee added and relationship made."
            };
            return response;
        }
        public async Task<Response<List<ReturnEmployeeDTO>>> RemoveEmployee(int employee_id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(s => s.Id == employee_id && s.Status == 1);
            if (employee == null)
            {
                return new Response<List<ReturnEmployeeDTO>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            var sources = await _context.EmployeeSources.Where(s => s.EmployeeId == employee.Id && s.Status == 1).Select(s => s.Source).ToListAsync();
            foreach (var source in sources)
            {
                source.Space++;
            }
            var sourceRelations = await _context.EmployeeSources.Where(r => r.EmployeeId == employee_id && r.Status == 1).ToListAsync();
            foreach (var relation in sourceRelations)
            {
                relation.Status = 0;
            }
            var sgRelations = await _context.EmployeeSourcesGroups.Where(r => r.EmployeeId == employee_id && r.Status == 1).ToListAsync();
            foreach (var relation in sgRelations)
            {
                relation.Status = 0;
            }
            employee.Status = 0;

            // _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            var req = await GetAllEmployees();
            var response = new Response<List<ReturnEmployeeDTO>>()
            {
                Data = req.Data,
                StatusCode = 200,
                Message = "Success, Employee removed."
            };
            return response;
        }
        public async Task<Response<List<ReturnSourceDTO>>> GetAllSources()
        {
            var sources = await _context.Sources.Where(s => s.Status == 1).ToListAsync();
            List<ReturnSourceDTO> sourceDTOs = _mapper.Map<List<ReturnSourceDTO>>(sources);
            var response = new Response<List<ReturnSourceDTO>>()
            {
                Data = sourceDTOs,
                StatusCode = 200,
                Message = "Success, all sources returned."
            };
            return response;
        }
        public async Task<Response<List<GetSourcesByGroupDTO>>> GetSourcesByGroup(int sg_id)
        {
            var sources = await _context.SourceGroups.Where(x => x.Id == sg_id && x.Status == 1).SelectMany(e => e.Sources).Include(s => s.EmployeeSources).ToListAsync();
            List<GetSourcesByGroupDTO> sourcesDTO = _mapper.Map<List<GetSourcesByGroupDTO>>(sources);
            foreach (var source in sourcesDTO)
            {
                var employees = await _context.EmployeeSources.Where(es => es.SourceId == source.Id && es.Status == 1).Select(e => e.Employee).ToListAsync();
                source.Employees = _mapper.Map<List<EmpDTOIdName>>(employees);
            }

            var response = new Response<List<GetSourcesByGroupDTO>>()
            {
                Data = sourcesDTO,
                StatusCode = 200,
                Message = "Success, all sources of the group returned."
            };
            return response;
        }
        public async Task<Response<List<ReturnSourceDTO>>> AddSource(int source_id, int source_group_id)
        {
            var sg = _context.SourceGroups.FirstOrDefault(s => s.Id == source_group_id);
            if (sg == null)
            {
                return new Response<List<ReturnSourceDTO>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source group could not be found."
                };
            }
            var source = await _context.Sources.FirstOrDefaultAsync(s => s.Id == source_id);
            var newSource = new Source() { Id = source_id, SourceGroupId = source_group_id, Status = 1 };
            newSource.Space = sg.Capacity;
            if (source == null)
            {
                _context.Add(newSource);
            }
            else if (source.Status == 0)
            {
                source.Status = 1;
                source.SourceGroupId = source_group_id;
                source.Space = sg.Capacity;
            }
            else if (source.Status == 1)
            {

                return new Response<List<ReturnSourceDTO>>()
                {
                    Data = null,
                    StatusCode = 200,
                    Message = "A source with the given Id and SourceGroup Id already exists."
                };
            }


            await _context.SaveChangesAsync();
            var req = await GetAllSources();
            var response = new Response<List<ReturnSourceDTO>>()
            {
                Data = req.Data,
                StatusCode = 201,
                Message = "Success, Source created."
            };
            return response;
        }
        public async Task<Response<List<ReturnSourceDTO>>> RemoveSource(int source_id)
        {
            var source = await _context.Sources.FirstOrDefaultAsync(s => s.Id == source_id && s.Status == 1);
            if (source == null)
            {
                return new Response<List<ReturnSourceDTO>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };
            }
            source.Status = 0;
            var relations = await _context.EmployeeSources.Where(r => r.SourceId == source_id).ToListAsync();
            foreach (var relation in relations)
            {
                relation.Status = 0;
            }


            //_context.Remove(source);
            await _context.SaveChangesAsync();
            var req = await GetAllSources();
            var response = new Response<List<ReturnSourceDTO>>()
            {
                Data = req.Data,
                StatusCode = 200,
                Message = "Success, source removed"
            };
            return response;
        }

        public async Task<Response<List<ReturnSourceGroupDTO>>> GetAllSourceGroups()
        {
            var sgs = await _context.SourceGroups.Where(s => s.Status == 1).Include(s => s.Sources).ToListAsync();
            var sgDTOs = _mapper.Map<List<ReturnSourceGroupDTO>>(sgs);
            foreach (var sgDTO in sgDTOs)
            {
                var sources = await _context.Sources.Where(s => s.SourceGroupId == sgDTO.Id && s.Status == 1).ToListAsync();
                sgDTO.Sources = _mapper.Map<List<ReturnSourceDTO>>(sources);
            }
            var response = new Response<List<ReturnSourceGroupDTO>>()
            {
                Data = sgDTOs,
                StatusCode = 200,
                Message = "Success, all source groups returned."
            };
            return response;
        }
        public async Task<Response<List<ReturnSourceGroupDTO>>> AddSourceGroup(int SG_id, string name, int cap)
        {
            SourceGroup newSG = new SourceGroup() { Id = SG_id, Name = name, Capacity = cap, Status = 1 };
            var sg = await _context.SourceGroups.FirstOrDefaultAsync(sg => sg.Id == SG_id);
            if (sg == null)
            {
                _context.Add(newSG);
            }
            else if (sg.Status == 0)
            {
                sg.Status = 1;
                sg.Name = name;
                sg.Capacity = cap;
            }
            else if (sg.Status == 1)
            {

                return new Response<List<ReturnSourceGroupDTO>>()
                {
                    Data = null,
                    StatusCode = 200,
                    Message = "A source group with the given Id already exists."
                };
            }

            await _context.SaveChangesAsync();
            var req = await GetAllSourceGroups();
            var response = new Response<List<ReturnSourceGroupDTO>>()
            {
                Data = req.Data,
                StatusCode = 201,
                Message = "Success, new Source Group created."
            };
            return response;
        }
        public async Task<Response<List<ReturnSourceGroupDTO>>> RemoveSourceGroup(int sg_id)
        {
            var sg = _context.SourceGroups.FirstOrDefault(s => s.Id == sg_id && s.Status == 1);
            if (sg == null)
            {
                return new Response<List<ReturnSourceGroupDTO>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source Group could not be found."
                };
            }
            sg.Status = 0;
            sg.DateDeleted = DateTimeOffset.UtcNow;
            var relations = await _context.EmployeeSourcesGroups.Where(s => s.SourceGroupId == sg.Id && s.Status == 1).ToListAsync();
            var sources = await _context.Sources.Where(s => s.SourceGroupId == sg.Id && s.Status == 1).ToListAsync();
            foreach (var relation in relations)
            {
                relation.Status = 0;
            }
            foreach (var source in sources)
            {
                source.Status = 0;
                var owns = await _context.EmployeeSources.Where(es => es.SourceId == source.Id && es.Status == 1).ToListAsync();
                foreach (var own in owns)
                {
                    own.Status = 1;
                }
            }
            await _context.SaveChangesAsync();
            var req = await GetAllSourceGroups();
            var response = new Response<List<ReturnSourceGroupDTO>>()
            {
                Data = req.Data,
                StatusCode = 200,
                Message = "Success, Source Group removed."
            };
            return response;
        }


        public async Task<Response<ReturnEmployeeDTO>> AddRelationship(int employee_id, int source_id)
        {
            //.Include(e => e.Sources).Include(e => e.SourceGroups).
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee_id && e.Status == 1);
            var source = await _context.Sources.FirstOrDefaultAsync(s => s.Id == source_id && s.Status == 1);
            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            else if (source == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };
            }
            if (source.Space == 0)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "This source is not available."
                };
            }
            source.Space--;
            var employeeSource = new EmployeeSource()
            {
                Employee = employee,
                Source = source,
                Status = 1,
            };
            var relation = await _context.EmployeeSources.FirstOrDefaultAsync(r => r.EmployeeId == employee_id && r.SourceId == source_id);
            if (relation == null)
            {
                _context.Add(employeeSource);
            }
            else if (relation.Status == 0)
            {
                relation.Status = 1;
            }
            else if (relation.Status == 1)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 200,
                    Message = "Relationship already exists."
                };
            }


            await _context.SaveChangesAsync();
            var sources = await _context.EmployeeSources.Where(es => es.EmployeeId == employee.Id && es.Status == 1).Select(e => e.Source).ToListAsync();
            var empDTO = _mapper.Map<ReturnEmployeeDTO>(employee);
            empDTO.Sources = _mapper.Map<List<ReturnSourceDTO>>(sources);
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = empDTO,
                StatusCode = 201,
                Message = "Success, Added new relationship."
            };
            return response;
        }

        public async Task<Response<List<ReturnSourceDTO>>> GetSourcesOfEmployee(int employee_id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == employee_id && x.Status == 1);
            if (employee == null)
            {
                return new Response<List<ReturnSourceDTO>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee not found"
                };
            }
            var sources = await _context.EmployeeSources.Where(es => es.EmployeeId == employee_id && es.Status == 1).Select(s => s.Source).ToListAsync();
            List<ReturnSourceDTO> sourceDTOs = _mapper.Map<List<ReturnSourceDTO>>(sources);
            var response = new Response<List<ReturnSourceDTO>>()
            {
                Data = sourceDTOs,
                StatusCode = 200,
                Message = "Success, resources of the employee returned."
            };
            return response;
        }

        public async Task<Response<List<EmpDTOIdName>>> GetOwnersOfSource(int source_id)
        {
            var source = _context.Sources.FirstOrDefault(x => x.Id == source_id && x.Status == 1);
            if (source == null)
            {
                return new Response<List<EmpDTOIdName>>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };

            }
            var employees = await _context.EmployeeSources.Where(s => s.SourceId == source_id && s.Status == 1).Select(x => x.Employee).ToListAsync();
            List<EmpDTOIdName> returnDTOS = _mapper.Map<List<EmpDTOIdName>>(employees);
            var response = new Response<List<EmpDTOIdName>>()
            {
                Data = returnDTOS,
                StatusCode = 200,
                Message = "Success, owners of the source returned"
            };
            return response;
        }

        public async Task<Response<ReturnEmployeeDTO>> RemoveRelationship(int employee_id, int source_id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee_id && e.Status == 1);
            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            var relation = await _context.EmployeeSources.FirstOrDefaultAsync(es => es.EmployeeId == employee_id && es.SourceId == source_id && es.Status == 1);
            if (relation == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Relation could not be found."
                };
            }
            var foundSources = await _context.Sources.Where(s => s.Id == relation.SourceId && s.Status == 1).ToListAsync();
            foreach (var foundSource in foundSources)
            {
                foundSource.Space++;
            }
            relation.Status = 0;

            await _context.SaveChangesAsync();
            var req = await GetEmployeeById(employee.Id);
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = req.Data,
                StatusCode = 200,
                Message = "Success, Relationship removed."
            };
            return response;
        }

        public async Task<Response<ReturnEmployeeDTO>> AddAdministration(int employee_id, int sg_id)
        {

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee_id && e.Status == 1);
            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            var sg = await _context.SourceGroups.FirstOrDefaultAsync(sg => sg.Id == sg_id && sg.Status == 1);
            if (sg == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source group could not be found."
                };
            }
            var employeeSourceGroup = new EmployeeSourceGroup()
            {
                Employee = employee,
                SourceGroup = sg,
                Status = 1
            };
            var relation = await _context.EmployeeSourcesGroups.FirstOrDefaultAsync(esg => esg.EmployeeId == employee_id && esg.SourceGroupId == sg_id);
            if (relation == null)
            {
                _context.Add(employeeSourceGroup);
            }
            else if (relation.Status == 0)
            {
                relation.Status = 0;
            }
            else if (relation.Status == 1)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 200,
                    Message = "Administration already exists"
                };
            }


            await _context.SaveChangesAsync();
            var empDTO = _mapper.Map<ReturnEmployeeDTO>(employee);
            var sources = await _context.EmployeeSources.Where(es => es.EmployeeId == empDTO.Id && es.Status == 1).Select(es => es.Source).ToListAsync();
            var sgs = await _context.EmployeeSourcesGroups.Where(es => es.EmployeeId == empDTO.Id && es.Status == 1).Select(esg => esg.SourceGroup).ToListAsync();
            empDTO.Sources = _mapper.Map<List<ReturnSourceDTO>>(sources);
            empDTO.SourceGroups = _mapper.Map<List<ReturnSourceGroupDTO>>(sgs);
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = empDTO,
                StatusCode = 200,
                Message = "Success, administration added."
            };
            return response;
        }

        public async Task<Response<ReturnEmployeeDTO>> UpdateEmployee(UpdateEmployeeDTO request)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id && e.Status == 1);
            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            employee.Name = request.Name;
            employee.Role = request.Role;
            await _context.SaveChangesAsync();
            var empDTO = _mapper.Map<ReturnEmployeeDTO>(employee);
            var sources = await _context.EmployeeSources.Where(es => es.EmployeeId == empDTO.Id && es.Status == 1).ToListAsync();
            var sgs = await _context.EmployeeSourcesGroups.Where(es => es.EmployeeId == empDTO.Id && es.Status == 1).ToListAsync();
            empDTO.Sources = _mapper.Map<List<ReturnSourceDTO>>(sources);
            empDTO.SourceGroups = _mapper.Map<List<ReturnSourceGroupDTO>>(sgs);
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = empDTO,
                StatusCode = 200,
                Message = "Success, employee updated."
            };
            return response;
        }

        public async Task<Response<ReturnSourceGroupDTO>> UpdateSourceGroup(int sg_id, int former_emp_id, int employee_id)
        {
            var sg = await _context.SourceGroups.FirstOrDefaultAsync(s => s.Id == sg_id && s.Status == 1);
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee_id && e.Status == 1);
            var former_emp = await _context.Employees.FirstOrDefaultAsync(e => e.Id == former_emp_id && e.Status == 1);
            if (employee == null || former_emp == null)
            {
                return new Response<ReturnSourceGroupDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            else if (sg == null)
            {
                return new Response<ReturnSourceGroupDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source Group could not be found."
                };
            }
            var relation = await _context.EmployeeSourcesGroups.FirstOrDefaultAsync(esg => esg.EmployeeId == former_emp_id && esg.SourceGroupId == sg_id && esg.Status == 1);
            if (relation == null)
            {
                return new Response<ReturnSourceGroupDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Relation could not be found."
                };
            }


            var newRelation = new EmployeeSourceGroup()
            {
                Employee = employee,
                SourceGroup = sg,
                Status = 1
            };
            var existingRelation = await _context.EmployeeSourcesGroups.FirstOrDefaultAsync(esg => esg.EmployeeId == employee.Id && esg.SourceGroupId == sg_id);

            if (existingRelation == null)
            {
                relation.Status = 0;
                _context.Add(newRelation);
            }
            else if (existingRelation.Status == 0)
            {
                existingRelation.Status = 1;
                relation.Status = 0;
            }
            await _context.SaveChangesAsync();
            var sgDTO = _mapper.Map<ReturnSourceGroupDTO>(sg);
            var sources = await _context.Sources.Where(s => s.SourceGroupId == sgDTO.Id).ToListAsync();
            var mappedSources = _mapper.Map<List<ReturnSourceDTO>>(sources);
            sgDTO.Sources = mappedSources;
            var response = new Response<ReturnSourceGroupDTO>()
            {
                Data = sgDTO,
                StatusCode = 200,
                Message = "Success, Source Group updated."
            };
            return response;
        }

        public async Task<Response<ReturnEmployeeDTO>> UpdateRelationship(int employee_id, int source_id, int new_source_id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee_id && e.Status == 1);
            var source = await _context.Sources.FirstOrDefaultAsync(s => s.Id == source_id && s.Status == 1);
            var newSource = await _context.Sources.FirstOrDefaultAsync(s => s.Id == new_source_id && s.Status == 1);

            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            else if (source == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Source could not be found."
                };
            }
            else if (newSource == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "new Source could not be found."
                };
            }
            var relation = await _context.EmployeeSources.FirstOrDefaultAsync(es => es.EmployeeId == employee_id && es.SourceId == source_id && es.Status == 1);
            if (relation == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Relation could not be found."
                };
            }
            else if (newSource.Space == 0)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Target source is not available."
                };
            }
            await RemoveRelationship(employee.Id, source.Id);
            await AddRelationship(employee.Id, newSource.Id);
            var req = await GetEmployeeById(employee.Id);
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = req.Data,
                StatusCode = 200,
                Message = "Success, relationship updated."
            };
            await _context.SaveChangesAsync();
            return response;
        }



        public async Task<Response<ReturnEmployeeDTO>> AddAutoRelationship(int employee_id, int sg_id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee_id && e.Status == 1);
            if (employee == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "Employee could not be found."
                };
            }
            var source = await _context.Sources.FirstOrDefaultAsync(s => s.SourceGroupId == sg_id && s.Space > 0 && s.Status == 1);
            if (source == null)
            {
                return new Response<ReturnEmployeeDTO>()
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No available source found."
                };
            }
            // if an available source is found in that said source group, make the relationship.
            await AddRelationship(employee_id, source.Id);
            var req = await GetEmployeeById(employee_id);
            var response = new Response<ReturnEmployeeDTO>()
            {
                Data = req.Data,
                StatusCode = 201,
                Message = "Success, Added new relationship."
            };
            return response;
        }

    }
}