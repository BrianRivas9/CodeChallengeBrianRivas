using CodeChallenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{
    public class CompensationDataSeeder
    {
        private CompensationContext _compensationContext;

        public CompensationDataSeeder(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public async Task Seed()
        {
            if (!_compensationContext.Compensations.Any())
            {
                List<Compensation> compensations = new List<Compensation>();
                Compensation compensation = new Compensation();
                compensation.EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f";
                compensation.Salary = 110000;
                compensation.EffectiveDate = new DateTime(2024, 4, 23);
                compensations.Add(compensation);
                _compensationContext.Compensations.AddRange(compensations);

                await _compensationContext.SaveChangesAsync();
            }
        }
    }
}
