namespace Smakoowa_Api.Services
{
    public class ControllerStatisticsService : IControllerStatisticsService
    {
        private readonly IRequestCountRepository _requestCountRepository;
        private readonly IRequestCountMapperService _requestCountMapperService;
        private readonly IHelperService<ControllerStatisticsService> _helperService;

        public ControllerStatisticsService(IRequestCountRepository requestCountRepository, IHelperService<ControllerStatisticsService> helperService, 
            IRequestCountMapperService requestCountMapperService)
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
    }
}
