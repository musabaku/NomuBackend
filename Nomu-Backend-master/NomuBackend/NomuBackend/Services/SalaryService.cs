using MongoDB.Driver;
using NomuBackend.Settings;
using NomuBackend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace NomuBackend.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IMongoCollection<Salary> _salaries;

        public SalaryService(IMongoClient client, IMongoDbSettings settings)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            var database = client.GetDatabase(settings.DatabaseName);
            _salaries = database.GetCollection<Salary>("Salaries");
        }

        public async Task<List<Salary>> GetSalariesAsync() =>
            await _salaries.Find(salary => true).ToListAsync();

        public async Task<Salary> GetSalaryByIdAsync(string id) =>
            await _salaries.Find<Salary>(salary => salary.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

        public async Task CreateSalaryAsync(Salary salary) =>
            await _salaries.InsertOneAsync(salary);

        public async Task UpdateSalaryAsync(string id, Salary salaryIn) =>
            await _salaries.ReplaceOneAsync(salary => salary.Id == ObjectId.Parse(id), salaryIn);

        public async Task RemoveSalaryAsync(string id) =>
            await _salaries.DeleteOneAsync(salary => salary.Id == ObjectId.Parse(id));

        public void CalculateProfit(Salary salary)
        {
            // Implementing simple logic for profit calculation
            const double dailyGrowthRate = 1.02; // 2% daily growth
            const double weeklyGrowthRate = 1.05; // 5% weekly growth
            const double monthlyGrowthRate = 1.10; // 10% monthly growth
            const double yearlyGrowthRate = 1.20; // 20% yearly growth

            salary.DailyProfit *= dailyGrowthRate;
            salary.WeeklyProfit *= weeklyGrowthRate;
            salary.MonthlyProfit *= monthlyGrowthRate;
            salary.YearlyProfit *= yearlyGrowthRate;

            // Pending profit is calculated as 10% of monthly profit
            salary.PendingProfit = salary.MonthlyProfit * 0.10;
        }

        public void DisplayProfitDetails(Salary salary)
        {
            // Console output to display profit details
            Console.WriteLine($"Salary ID: {salary.Id}");
            Console.WriteLine($"Daily Profit: {salary.DailyProfit:C}");
            Console.WriteLine($"Weekly Profit: {salary.WeeklyProfit:C}");
            Console.WriteLine($"Monthly Profit: {salary.MonthlyProfit:C}");
            Console.WriteLine($"Yearly Profit: {salary.YearlyProfit:C}");
            Console.WriteLine($"Pending Profit: {salary.PendingProfit:C}");
        }
    }

   
}
