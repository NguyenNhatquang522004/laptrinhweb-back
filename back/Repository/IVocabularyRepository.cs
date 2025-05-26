using backapi.Helpers;
using backapi.Model;
using static backapi.Enums.enums;

namespace backapi.Repository
{
    public interface IVocabularyRepository
    {
        public Task<globalResponds> GetAllAsync();
        public Task<globalResponds> GetByIdAsync(Guid id);
        public Task<globalResponds> CreateAsync(Vocabulary vocabulary);
        public Task<globalResponds> UpdateAsync(Guid id, Vocabulary vocabulary);

        public Task<globalResponds> DeleteAsync(Guid id);
        public Task<globalResponds> GetByCategoryAsync(Guid categoryId);
        public Task<globalResponds> GetByLevelAsync(CefrLevel level);


        public Task<globalResponds> GetByPartOfSpeechAsync(PartOfSpeech partOfSpeech);

        public Task<globalResponds> CreateVocabularyAndCategory(Category category, Vocabulary vocabulary);

        public Task<globalResponds> DeleteVocabularyAndCategory(Category category, Vocabulary vocabulary);


    }
}
