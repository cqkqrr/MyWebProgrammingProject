namespace MyWebProgrammingProject.Services
{
    public interface IAiRecommendationService
    {
        Task<string> GetWorkoutAndDietAsync(
            int heightCm,
            int weightKg,
            string bodyType,
            string goal,
            CancellationToken cancellationToken = default);
    }
}
