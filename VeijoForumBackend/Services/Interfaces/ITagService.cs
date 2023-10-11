using VeijoForumBackend.Models.Dto.TagDtos;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface ITagService
    {
        public Task<List<TagDto>> GetTags();

        public Task<TagDto> GetTagById(int id);

        public bool CreateTag(CreateTagDto createTagDto);
    }
}
