﻿namespace Smakoowa_Api.Services
{
    public class ControllerStatisticsService : IControllerStatisticsService
    {
        private readonly IRequestCountRepository _requestCountRepository;
        private readonly IRequestCountMapperService _requestCountMapperService;
        private readonly IHelperService<ControllerStatisticsService> _helperService;

        public ControllerStatisticsService(IRequestCountRepository requestCountRepository, 
            IHelperService<ControllerStatisticsService> helperService, IRequestCountMapperService requestCountMapperService)
        {
            _requestCountRepository = requestCountRepository;
            _helperService = helperService;
            _requestCountMapperService = requestCountMapperService;
        }

        public async Task<ServiceResponse> GetAll()
        {
            try
            {
                var reqeustCounts = await _requestCountRepository.FindAll();
                var getCategoryStatisticsResponseDto = _requestCountMapperService.MapGetControllerStatisticsResponseDto(reqeustCounts);
                return ServiceResponse<List<ControllerStatisticsResponseDto>>.Success(getCategoryStatisticsResponseDto, "Statistics retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the statistics.");
            }
        }

        public async Task<int> GetRecipeViewCount(int recipeId)
        {
            var recipeViewCount = await _requestCountRepository.FindByConditionsFirstOrDefault(rc =>
            rc.ControllerName == "Recipes"
            && rc.ActionName == "GetByIdDetailed"
            && rc.RemainingPath == recipeId.ToString());

            if (recipeViewCount == null) return 0;
            else return recipeViewCount.Count;
        }
    }
}
