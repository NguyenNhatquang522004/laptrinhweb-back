using backapi.Helpers;
using backapi.Model;
using static backapi.Enums.enums;

namespace backapi.Repository
{
    public interface IGrammarRuleRepository
    {
        public Task<globalResponds> GetAllAsync();
        public Task<globalResponds> GetByIdAsync(Guid id);
        public Task<globalResponds> CreateAsync(GrammarRule grammarRule, Category category);
        public Task<globalResponds> UpdateAsync(Guid id, GrammarRule grammarRule);
        public Task<globalResponds> DeleteAsync(Guid id);
        public Task<globalResponds> GetByCategoryAsync(Guid categoryId);
        public Task<globalResponds> GetByLevelAsync(CefrLevel level);

        public Task<globalResponds> CreateGrammaRuleAndCategory(Category category, GrammarRule grammarRule);

        public Task<globalResponds> DeleteGrammaRuleAndCategory(Category category, GrammarRule grammarRule);

    }
}
