using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public ReportingStructure GetReportingStructure(string id)
        {
            
            Employee employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return null;
            }

            ReportingStructure reportingStructure = new ReportingStructure();
            reportingStructure.EmployeeId = id;
            reportingStructure.numberOfReports = CalculateNumberOfDirectReports(employee);
            return reportingStructure;
        }

        // Method calculates the number of direct reports and their immediate direct reports for a given employee.
        // Note: This method only includes the direct reports up to the second level, not all levels to the bottom of the org, I was not sure based off the wording of the ReadMe whether or not to stop after the second level or to keep going to the bottom of the organization.
        // Returns: Int describing the number of direct reports
        private int CalculateNumberOfDirectReports(Employee employee)
        {
            if (employee.DirectReports != null) {
                int reportCount = employee.DirectReports.Count;
                foreach (var reportId in employee.DirectReports)
                {
                    var directReport = _employeeRepository.GetById(reportId.EmployeeId);
                    if (directReport.DirectReports != null)
                    {
                        reportCount += directReport.DirectReports.Count;
                    }
                }
                return reportCount;
            }
            else
            {
                return 0;
            }
            
        }
    }

    
}
