using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.TagDtos;
using VeijoForumBackend.Repositories.Interfaces;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public bool CreateTag(CreateTagDto createTagDto)
        {
            if (createTagDto == null)
            {
                return false;
            }

            var tag = _mapper.Map<Tag>(createTagDto);

            try
            {
                _tagRepository.CreateTag(tag);
                _tagRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TagDto> GetTagById(int id)
        {
            var tag = await _tagRepository.GetTagById(id);

            var mappedTag = _mapper.Map<TagDto>(tag);

            return mappedTag;
        }

        public async Task<List<TagDto>> GetTags()
        {
            var tags = await _tagRepository.GetTags();

            var mappedTags = _mapper.Map<List<TagDto>>(tags);

            return mappedTags;
        }
    }
}
