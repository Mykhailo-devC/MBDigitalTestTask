using AutoMapper;

namespace MBDigitalTestTask.Services.DataManager
{
    public abstract class DataManagerBase
    {
        protected readonly IMapper _mapper;
        protected DataManagerBase(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
