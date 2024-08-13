namespace NomuBackend.Services
{
    public interface ISalaryService
    {
        Task<List<Salary>> GetSalariesAsync();
        Task<Salary> GetSalaryByIdAsync(string id);
        Task CreateSalaryAsync(Salary salary);
        Task UpdateSalaryAsync(string id, Salary salaryIn);
        Task RemoveSalaryAsync(string id);
        void CalculateProfit(Salary salary);
        void DisplayProfitDetails(Salary salary);
    }
}
